using ICommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdCode
{
    public class Cmd35
    {
        private string _sTX = string.Empty;
        private string _seqNo = string.Empty;
        private string _command = string.Empty;
        private string _reSent = string.Empty;
        private string _triggerTime = string.Empty;
        private string _mCKey = string.Empty;
        private string _eqpNo = string.Empty;
        private string _cycleCmd = string.Empty;
        private string _jobType = string.Empty;
        private string _shelfHeight = string.Empty;
        private string _shelfWidth = string.Empty;
        private string _row = string.Empty;
        private string _bas = string.Empty;
        private string _layer = string.Empty;
        private string _platform = string.Empty;
        private string _pier = string.Empty;
        private string _loadStatus = string.Empty;
        private string _completeType = string.Empty;
        private string _completeCode = string.Empty;
        private string _bBC = string.Empty;
        private string _eTX = string.Empty;
        //MsgModel md = new MsgModel();
        
        public string sTX { get { return _sTX; } set { _sTX = value; } }
        public string seqNo { get { return _seqNo; } set { _seqNo = value; } }
        public string command { get { return _command; } set { _command = value; } }
        public string reSent { get { return _reSent; } set { _reSent = value; } }
        public string triggerTime { get { return _triggerTime; } set { _triggerTime = value; } }
        public string mCKey { get { return _mCKey; } set { _mCKey = value; } }
        public string eqpNo { get { return _eqpNo; } set { _eqpNo = value; } }
        public string cycleCmd { get { return _cycleCmd; } set { _cycleCmd = value; } }
        public string jobType { get { return _jobType; } set { _jobType = value; } }
        public string shelfHeight { get { return _shelfHeight; } set { _shelfHeight = value; } }
        public string shelfWidth { get { return _shelfWidth; } set { _shelfWidth = value; } }
        public string row { get { return _row; } set { _row = value; } }
        public string bas { get { return _bas; } set { _bas = value; } }
        public string layer { get { return _layer; } set { _layer = value; } }
        public string platform { get { return _platform; } set { _platform = value; } }
        public string pier { get { return _pier; } set { _pier = value; } }
        public string loadStatus { get { return _loadStatus; } set { _loadStatus = value; } }
        public string completeType { get { return _completeType; } set { _completeType = value; } }
        public string completeCode { get { return _completeCode; } set { _completeCode = value; } }
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
            mCKey = msg._value["MCkey"];
            eqpNo = Equipment.eqpID;
            cycleCmd = msg._value["CycleCmd"];
            jobType = msg._value["JobType"];
            shelfHeight = msg._value["ShelfHeight"];
            shelfWidth = msg._value["ShelfWidth"];
            row = msg._value["Row"];
            bas = msg._value["Base"];
            layer = msg._value["Layer"];
            platform = msg._value["Platform"];
            pier = msg._value["Pier"];
            loadStatus = msg._value["LoadStatus"];
            completeType = msg._value["CompleteType"];
            completeCode = msg._value["CompleteCode"];
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
                        case "MCkey":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(mCKey)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(mCKey)[i];
                            break;
                        case "EqpNo":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(eqpNo)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(eqpNo)[i];
                            break;
                        case "CycleCmd":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(cycleCmd)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(cycleCmd)[i];
                            break;
                        case "JobType":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(jobType)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(jobType)[i];
                            break;
                        case "ShelfHeight":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(shelfHeight)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(shelfHeight)[i];
                            break;
                        case "ShelfWidth":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(shelfWidth)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(shelfWidth)[i];
                            break;
                        case "Row":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(row)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(row)[i];
                            break;
                        case "Base":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(bas)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(bas)[i];
                            break;
                        case "Layer":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(layer)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(layer)[i];
                            break;
                        case "Platform":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(platform)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(platform)[i];
                            break;
                        case "Pier":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(pier)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(pier)[i];
                            break;
                        case "LoadStatus":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(loadStatus)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(loadStatus)[i];
                            break;
                        case "CompleteType":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(completeType)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(completeType)[i];
                            break;
                        case "CompleteCode":
                            strb.Append(key + ":" + Convert.ToString((char)Encoding.ASCII.GetBytes(completeCode)[i]).PadLeft(2, '0') + "\n");
                            rep[reidx] = Encoding.ASCII.GetBytes(completeCode)[i];
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
                            strb.Append(key + ":" + eTX );
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
