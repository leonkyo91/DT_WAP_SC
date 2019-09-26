using ICommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdCode
{
    public class Cmd10
    {
        private string _sTX = string.Empty;
        private string _seqNo = string.Empty;
        private string _command = string.Empty;
        private string _reSent = string.Empty;
        private string _triggerTime = string.Empty;
        private string _wcsNo = string.Empty;
        private string _consoleNo = string.Empty;
        private string _heartbeat = string.Empty;
        private string _bBC = string.Empty;
        private string _eTX = string.Empty;
        public string sTX { get { return _sTX; } set { _sTX = value; } }
        public string seqNo { get { return _seqNo; } set { _seqNo = value; } }
        public string command { get { return _command; } set { _command = value; } }
        public string reSent { get { return _reSent; } set { _reSent = value; } }
        public string triggerTime { get { return _triggerTime; } set { _triggerTime = value; } }
        public string consloeNo { get { return _consoleNo; } set { _consoleNo = value; } }
        public string wcsNo { get { return _wcsNo; } set { _wcsNo = value; } }
        public string heartbeat { get { return _heartbeat; } set { _heartbeat = value; } }
        public string bBC { get { return _bBC; } set { _bBC = value; } }
        public string eTX { get { return _eTX; } set { _eTX = value; } }
        public string[] GetData(MsgEventArg msg, ref byte[] rep)
        {
            string dtt = DateTime.Now.ToString("HHmmss");
            Dictionary<string, string> time = new Dictionary<string, string>() { { "TriggerTime", dtt } };
            sTX = MsgModel._startPoint;
            seqNo = msg._value["SeqNo"];
            command = msg._cmdCode;
            reSent = msg._value["ReSent"];
            triggerTime = dtt;
            consloeNo = Equipment.eqpID;
            heartbeat = msg._value["Status"];
            eTX = MsgModel._endPoint;

            StringBuilder strb = new StringBuilder();
            byte[] chkbbc = new byte[2];
            Dictionary<string, string> dt = new Dictionary<string, string>() { { "TriggerTime", triggerTime } };
            int reidx = 0;
            foreach (string key in MsgModel._commandType[msg._cmdCode].Keys)
            {
                for (int i = 0; i < MsgModel._commandType[msg._cmdCode][key]; i++)
                {
                    switch (key)
                    {
                        case "STX":
                            strb.Append(key + ":" + sTX + "\n");
                            rep[reidx] = Convert.ToByte(Convert.ToString(Convert.ToInt32(sTX, 16)));
                            break;
                        case "SeqNo":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(seqNo)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(seqNo)[i];
                            break;
                        case "Command":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(command)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(command)[i];
                            break;
                        case "ReSent":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(reSent)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(reSent)[i];
                            break;
                        case "TriggerTime":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(triggerTime)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(triggerTime)[i];
                            break;
                        case "WCSNo":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(wcsNo)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(wcsNo)[i];
                            break;
                        case "ConsoleNo":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(consloeNo)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(consloeNo)[i];
                            break;
                        case "HeartBeat":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(heartbeat)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(heartbeat)[i];
                            break;
                        case "BCC":
                            chkbbc = MsgModel.BCC(dt);
                            strb.Append(key + ":" + Convert.ToString(int.Parse(chkbbc[i].ToString()), 16).ToUpper() + "\n");//Convert.ToString(chkbbc[i]).PadLeft(2, '0')
                            if (chkbbc[i] >= 10)
                                rep[reidx] = Encoding.ASCII.GetBytes(Convert.ToString(chkbbc[i], 16).ToUpper())[0];
                            else
                                rep[reidx] = chkbbc[i];
                            break;
                        case "ETX":
                            strb.Append(key + ":" + eTX);
                            rep[reidx] = Convert.ToByte(Convert.ToString(Convert.ToInt32(eTX, 16)));
                            break;
                    }
                    reidx++;
                }
            }
            string[] sp = strb.ToString().Split('\n');
            return sp;
        }
    }
}
