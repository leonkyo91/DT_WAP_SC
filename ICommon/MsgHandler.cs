using CmdCode;
using mirle.stone.utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICommon
{
    public class MsgHandler : MsgModel//, IProtocol
    {
        public event EventHandler<MsgEventArg> DoWork;
        public event EventHandler<MsgEventArg> MsgErrorReport;
        public event EventHandler<MsgEventArg> CmdsAck;
        private bool _isSent = false;
        private int _seqno ;
        private Equipment Eqp;
        public int seqno { get { return _seqno; } set { _seqno = value; } }
        protected virtual void Work(MsgEventArg e)
        {
            
            if (DoWork != null)
            {
                DoWork(this, e);
            }
        }
        protected virtual void MsgErrReport(MsgEventArg e)
        {
            if (MsgErrorReport != null)
            {
                MsgErrorReport(this, e);
            }
        }
        protected virtual void CmdAck(MsgEventArg e)
        {

            if (CmdsAck != null)
            {
                CmdsAck(this, e);
            }
        }
        public bool Handle(Equipment eqp, string[] bt,bool isSent,byte[] byt)
        {
            try
            {
                bool stx = false;
                _isSent = isSent;
                string[] btpool = new string[bt.Count()];
                byte[] anbt=new byte[byt.Count()];
                Dictionary<string, byte[]> msglist = new Dictionary<string, byte[]>();
                int msgcounts = 0;
                int s = 0;
                int waitingbyte = 0;
                if (!isSent)
                {
                    Eqp = eqp;
                    bt.CopyTo(btpool, 0);
                    
                    //for (int i = 0; i < btpool.Count(); i++)
                    //{
                    //    //byte tt = Convert.ToByte(_startPoint, 16);
                    //    if (btpool[i].Replace("\0","").Equals("ETX:"+_endPoint) && stx)//(btpool[i].Equals(Convert.ToByte(_endPoint, 16)) && stx)//Msg Start
                    //    {
                    //        string spl = btpool[i].Split(':')[1].Replace("\0", "");
                    //        int st = Convert.ToInt32(spl, 16);
                    //        string sx = Convert.ToString(st);
                    //        stx = false;
                    //        anbt[s] = Convert.ToByte(sx);
                    //        msgcounts += 1;

                    //        s = 0;
                    //        anbt = new byte[btpool.Count()];
                    //    }
                    //    else if (btpool[i].Equals("STX:" + _startPoint) && !stx)
                    //    {
                    //        string spl = btpool[i].Split(':')[1].Replace("\0", "");
                    //        int st = Convert.ToInt32(spl, 16);
                    //        string sx = Convert.ToString(st);
                    //        stx = true;
                    //        anbt[s] = Convert.ToByte(sx);
                    //        s++;
                    //        if (!msglist.Keys.Contains(msgcounts.ToString()))
                    //        {
                    //            msglist.Add(msgcounts.ToString(), anbt);
                    //        }
                    //        else
                    //        {
                    //            msglist[msgcounts.ToString()] = anbt;
                    //        }
                    //    }
                    //    else if (btpool[i].Equals("STX:" + _startPoint) || stx)//(btpool[i].Equals(Convert.ToByte(_startPoint, 16)) || stx) //Msg End
                    //    {
                    //        string spl = btpool[i].Split(':')[1].Replace("\0", "");
                    //        int st = Convert.ToInt32(spl, 10);
                    //        string sx = Convert.ToString(st);
                    //        stx = true;
                    //        anbt[s] = Convert.ToByte(sx);
                    //        s++;
                    //        if (!msglist.Keys.Contains(msgcounts.ToString()))
                    //        {
                    //            msglist.Add(msgcounts.ToString(), anbt);
                    //        }
                    //        else
                    //        {
                    //            msglist[msgcounts.ToString()] = anbt;
                    //        }
                    //    }
                    //}
                    string[] strlib = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
                    for (int i = 0; i < byt.Count(); i++)
                    {
                        string spl = byt[i].ToString();
                        int st = Convert.ToInt32(spl, 10);
                        string sx = Convert.ToString((char)st);
                        stx = true;
                        if (strlib.Contains(spl)||st>64)
                        {
                            anbt[s] = Convert.ToByte(spl);
                        }
                        else
                        {
                            anbt[s] = Convert.ToByte(sx);
                        }
                        s++;
                        if (!msglist.Keys.Contains(msgcounts.ToString()))
                        {
                            msglist.Add(msgcounts.ToString(), anbt);
                        }
                        else
                        {
                            msglist[msgcounts.ToString()] = anbt;
                        }
                    }
                    foreach (string str in msglist.Keys)
                    {
                        Thread t1 = new Thread(Analysis);
                        t1.Start(msglist[str]);
                    }
                }
                else
                {
                    
                }
                
            }
            catch(Exception ex)
            {
                MsgEventArg _msg = new MsgEventArg();
                _msg._message = ex.Message + "-" + ex.StackTrace;
                MsgErrReport(_msg);
            }
            return false;
        }

        public void Analysis(object obj)
        {
            byte[] cmdtp = new byte[2];
            Dictionary<string, int> iIdx = new Dictionary<string, int>();
            int _commandcounts = 0;
            byte[] bt = obj as byte[];
            try
            {   //
                int _commandidx = 0;
                foreach (string s in _defaultDataItem.Keys)
                {
                    iIdx.Add(s, _commandidx);
                    _commandidx += _defaultDataItem[s];
                    if (s.Equals("Command"))
                    {
                        _commandcounts = _defaultDataItem[s];
                    }
                }
                int c = 0;
                for (int i = iIdx["Command"]; i < bt.Count() && _commandcounts > 0; i++)
                {
                    cmdtp[c]=bt[i];
                    c++;
                    _commandcounts --;
                }
                CollectData(cmdtp, bt);

            }
            catch (Exception ex)
            {
                MsgEventArg _msg = new MsgEventArg();
                _msg._message = ex.Message + "-" + ex.StackTrace;
                MsgErrReport(_msg);
            }
        }
        private void CollectData(byte[]code,byte[] bt)
        {
            string cmdcode = code[0].ToString() + code[1].ToString();
            Dictionary<string, string> ivalues = new Dictionary<string, string>();
            try
            {   //Get DataItem's Values 
                if (_commandType.Keys.Contains(cmdcode))
                {
                    int _commandidx = 0;
                    Dictionary<string, int> iIdx = new Dictionary<string, int>();
                    foreach (string s in _commandType[cmdcode].Keys)
                    {
                        iIdx.Add(s, _commandidx);
                        _commandidx += _commandType[cmdcode][s];
                    }
                    _itemIdx = new Dictionary<string, int>(iIdx);
                    foreach (string item in _commandType[cmdcode].Keys)
                    {
                        string value = string.Empty;
                        if (iIdx.Keys.Contains(item))
                        {
                            int count = _commandType[cmdcode][item];
                            for(int i = iIdx[item]; i < bt.Count()&& count>0; i++)
                            {
                                if (bt[i] >= 10)
                                    value += Convert.ToString(bt[i],16).ToUpper();
                                else
                                    value += bt[i].ToString();
                                count --;
                            }
                        }
                        ivalues.Add(item,value);
                    }
                    byte[]verify= BCC(ivalues);
                    string bcccode = string.Empty;
                    if (verify[1] >= 10)
                    {
                        bcccode = verify[0].ToString() + Convert.ToString(verify[1], 16).ToUpper();
                    }
                    else
                    {
                        bcccode = verify[0].ToString() + verify[1].ToString();
                    }
                    if (ivalues["BCC"] != bcccode)
                    {
                        MsgEventArg _msg = new MsgEventArg();
                        _msg._btdata = bt;
                        _msg._message = "BCC Error ! please check.\n[" + cmdcode + "->" + ivalues["BCC"] + "-" + verify[0].ToString() + verify[1].ToString() + "]";
                        MsgErrReport(_msg);
                        return;
                    }

                    _itemValue = new Dictionary<string, string>(ivalues);
                    MsgEventArg msgE = new MsgEventArg();
                    msgE._value = ivalues;
                    msgE._cmdCode = cmdcode;
                    Dispatch(msgE);
                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
                MsgEventArg _msg = new MsgEventArg();
                _msg._message = ex.Message + "-" + ex.StackTrace;
                MsgErrReport(_msg);
            }
        }
        private void Dispatch(MsgEventArg msg)
        {
            try
            {
                var sum = 0;
                byte[] reply ;
                string[] data;
                switch (int.Parse(msg._cmdCode))
                {
                    case (int)Enmu.CmdCode.EQP_START://01 WCS->Console
                        break;
                    case (int)Enmu.CmdCode.EQP_STOP://02 WCS->Console
                        break;
                    case (int)Enmu.CmdCode.CYCLE_CMD://03 WCS->Console,reply 23->MCS
                        break;
                    case (int)Enmu.CmdCode.DATA_DELETE_CMD://04 WCS->Console,replt 24->MCS
                        break;
                    case (int)Enmu.CmdCode.CYCLE_COMPLETE_REPLY://05 WCS->Console,reply 35->MCS
                        break;
                    case (int)Enmu.CmdCode.EQP_STATUS_CMD://06 WCS->Console,reply 26->MCS
                        break;
                    case (int)Enmu.CmdCode.HEART_BEAT://10 WCS->Console,reply 30->MCS
                        break;
                    case (int)Enmu.CmdCode.PLATFORM_MODE_CHANGE://40 WCS->Console,reply 42->MCS
                        break;
                    case (int)Enmu.CmdCode.CYCLE_REPLY://23 Console->WCS
                        
                        Eqp.UpdateInfo(msg._value);
                        break;
                    case (int)Enmu.CmdCode.DATA_DELETE_REPLY://24 Console->WCS
                        
                        break;
                    case (int)Enmu.CmdCode.EQP_STATUS://26 Console->WCS
                        Eqp.UpdateInfo(msg._value);
                        msg._cmdAck = true;
                        CmdAck(msg);
                        break;
                    case (int)Enmu.CmdCode.REPLY_HEART_BEAT://30 Console->WCS
                        Eqp.UpdateInfo(msg._value);
                        break;
                    case (int)Enmu.CmdCode.CYCLE_COMPLETE://35 Console->WCS
                        sum = _commandType[((int)Enmu.CmdCode.CYCLE_COMPLETE_REPLY).ToString().PadLeft(2,'0')].Select(o => o.Value).Sum();
                        reply = new byte[sum];
                        Cmd05 cmd05 = new Cmd05();
                        msg._cmdCode = "05";
                        if (msg._value.Keys.Contains("Reply"))
                        {
                            msg._value["Reply"] = "0";
                        }
                        else
                        {
                            msg._value.Add("Reply", "0");
                        }
                        data = cmd05.GetData(msg, ref reply);
                        Reply(Enmu.CmdCode.CYCLE_COMPLETE_REPLY, data, reply);
                        msg._cmdAck = true;
                        CmdAck(msg);
                        break;
                    case (int)Enmu.CmdCode.PLATFORM_MODE_CHANGE_REPLY://42 Console->WCS
                        
                        break;
                    case (int)Enmu.CmdCode.DATA_REPLY://50 Console->WCS
                        
                        break;
                    default:
                        
                        break;

                }
                
            }
            catch (Exception ex)
            {
                MsgEventArg _msg = new MsgEventArg();
                _msg._message = ex.Message + "-" + ex.StackTrace;
                MsgErrReport(_msg);
            }
        }
        private void Reply(Enmu.CmdCode cmdCode, string[]data, byte[] rep)
        {
            try
            {
                MsgEventArg _msg = new MsgEventArg();
                _msg._reply = data;
                _msg._cmdCode = ((int)cmdCode).ToString();
                _msg._btdata = rep;
                Work(_msg);
            }
            catch (Exception ex)
            {
                MsgEventArg _msg = new MsgEventArg();
                _msg._message = ex.Message + "-" + ex.StackTrace;
                MsgErrReport(_msg);
            }
        }

    }

    
}
