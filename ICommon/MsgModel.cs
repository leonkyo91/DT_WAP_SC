using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICommon
{
    public class MsgModel 
    {
        public static Dictionary<string, int> _defaultDataItem { get; set; }
        public static Dictionary<string, Dictionary<string, int>> _commandType { get; set; }
        public static Dictionary<string, string> _itemValue { get; set; }
        public static Dictionary<string, int> _itemIdx { get; set; }
        public static string _startPoint { get; set; }
        public static string _endPoint { get; set; }
        public static string _seqNo { get; set; }
        public static string _commandCode { get; set; }
        public static string _reSent { get; set; }
        public static string _triggerTime { get; set; }
        public static Dictionary<string, string> _errorCodeList { get; set; }
        public static List<string> _waitConfirm { get; set; }
        public static int _shelfR { get; set; }
        public static int _shelfL { get; set; }
        public static int _crane { get; set; }
        public event EventHandler<MsgEventArg> ErrorReport;
        //private MsgEventArg _msg = new MsgEventArg();
        protected virtual void ErrReport(MsgEventArg e)
        {
            if (ErrorReport != null)
            {
                ErrorReport(this, e);
            }
        }
        public static byte[] BCC(Dictionary<string, string> dic)//Msg check
        {
            try
            {
                byte[] bt1 = new byte[8];
                byte[] bt2 = new byte[8];
                byte[] bt3 = new byte[8];
                byte[] bt4 = new byte[8];
                byte[] bt5 = new byte[8];
                byte[] bt6 = new byte[8];
                //if (!dic.Keys.Contains("TriggerTime"))
                //{
                //    MsgEventArg _msg = new MsgEventArg();
                //    _msg._message = "Msg without TriggerTime.";
                //    ErrReport(_msg);
                //}
                for(int b = 0; b < dic["TriggerTime"].Length; b++)
                {
                    string sf = dic["TriggerTime"];
                    int a = Convert.ToInt32(char.Parse(dic["TriggerTime"].Substring(b,1)));
                    sf = Convert.ToString(Convert.ToInt16(a), 2);
                    sf = sf.PadLeft(8, '0');
                    for (int c = 0; c < sf.Length; c++)
                    {
                        string cx = sf.Substring(c, 1);
                        switch (b)
                        {
                            case 0:
                                bt1[c] = byte.Parse(cx.ToString());
                                break;
                            case 1:
                                bt2[c] = byte.Parse(cx.ToString());
                                break;
                            case 2:
                                bt3[c] = byte.Parse(cx.ToString());
                                break;
                            case 3:
                                bt4[c] = byte.Parse(cx.ToString());
                                break;
                            case 4:
                                bt5[c] = byte.Parse(cx.ToString());
                                break;
                            case 5:
                                bt6[c] = byte.Parse(cx.ToString());
                                break;
                        }
                    }
                }
                byte[] xor = XOR(bt1, bt2, bt3, bt4, bt5, bt6);

                return xor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static byte[] XOR(byte[] hex1, byte[] hex2, byte[] hex3, byte[] hex4, byte[] hex5, byte[] hex6)
        {
            byte[] bHEX_OUT = new byte[hex1.Length];
            for (int i = 0; i < hex1.Length; i++)
            {
                bHEX_OUT[i] = (byte)(hex1[i] ^ hex2[i] ^ hex3[i] ^ hex4[i] ^ hex5[i] ^ hex6[i]);
            }
            string re1 = string.Empty, re2 = string.Empty;
            for (int i = 0; i < bHEX_OUT.Count() / 2; i++)
            {
                re1 += bHEX_OUT[i].ToString();
                re2 += bHEX_OUT[i + 4].ToString();
            }
            byte[] rebt = new byte[2];
            rebt[0] = Convert.ToByte(Convert.ToInt16(re1, 2));
            rebt[1] = Convert.ToByte(Convert.ToInt16(re2, 2));
            return rebt;
            //return Convert.ToInt16(re1, 2).ToString() + Convert.ToInt16(re2, 2).ToString();
            
        }
        public void SetData()//Default data
        {
            _crane = 0;
            _shelfR = 5;
            _shelfL = 4;
            _startPoint = "02";
            _endPoint = "03";
            _defaultDataItem = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "Data",512},
                { "BCC",2},
                { "ETX",1}
            };
            _errorCodeList = new Dictionary<string, string>()
            {
                { "01","電量低" },
                { "02","上升逾時" },
                { "03","下降逾時" },
                { "05","入RGV逾時" },
                { "06","出RGV逾時" },
                { "07","入庫放板逾時" },
                { "08","出庫放板逾時" },
                { "09","定位異常" },
                { "10","防掉落異常" },
                { "11","出庫無物異常" },
                { "12","入庫無物異常" },
                { "13","不再原點異常" },
                { "14","手動異常" },
                { "15","行走馬達異常" },
                { "16","升降馬達異常" },
                { "17","出庫RGV有物" },
                { "18","急停異常" },
                { "19","編碼器異常" },
                { "20","A面檢測RGV異常" },
                { "21","B面檢測RGV異常" },
                { "22","小車位置異常" },
                { "23","條碼異常" },
                { "24","正反轉異常" },
                { "25","條碼讀取超時" },
                { "26","阻擋讀取超時" },
                { "27","防撞異常" },
                { "28","電池通訊異常" },
                { "29","行走馬達電流異常" },
                { "30","低速運轉逾時" }
            };
            #region Default Data 01~50
            Dictionary<string, int> DefaultDataItem01 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "Counts",3},
                { "EqpNo",4},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem02 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "Counts",3},
                { "EqpNo",4},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem03 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "MCkey",4},
                { "EqpNo",4},
                { "CycleCmd",2},
                { "JobType",2},
                { "ShelfHeight",1},
                { "ShelfWidth",1},
                { "Row",2},
                { "Base",2},
                { "Layer",2},
                { "Platform",4},
                { "Pier",4},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem04 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "MCkey",4},
                { "EqpNo",4},
                { "Type",1},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem05 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "MCkey",4},
                { "EqpNo",4},
                { "Reply",1},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem06 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "EqpNo",4},
                { "Status",1},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem10 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "WCSNo",1},
                { "ConsoleNo",4},
                { "HeartBeat",1},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem23 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "MCkey",4},
                { "EqpNo",4},
                { "2CycleCmd",2},
                { "JobType",2},
                { "ShelfHeight",1},
                { "ShelfWidth",1},
                { "Row",2},
                { "Base",2},
                { "Layer",2},
                { "Platform",4},
                { "Pier",4},
                { "LoadStatus",1},
                { "Reply",1},
                { "Error",1},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem24 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "MCkey",4},
                { "EqpNo",4},
                { "Type",1},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem26 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "Counts",1},
                { "EqpNo",4},
                { "ParentEqpNo",4},
                { "Row",2},
                { "Base",2},
                { "Layer",2},
                { "EqpStatus",7},
                { "Battery",3},
                { "LoadStatus",1},
                { "ErrorCode",2},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem30 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "WCSNo",1},
                { "ConsoleNo",4},
                { "HeartBeat",1},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem35 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "MCkey",4},
                { "EqpNo",4},
                { "CycleCmd",2},
                { "JobType",2},
                { "ShelfHeight",1},
                { "ShelfWidth",1},
                { "Row",2},
                { "Base",2},
                { "Layer",2},
                { "Platform",4},
                { "Pier",4},
                { "LoadStatus",1},
                { "CompleteType",1},
                { "CompleteCode",1},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem40 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "PlatformNo",4},
                { "Mode",2},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem42 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "PlatformNo",4},
                { "Mode",2},
                { "BCC",2},
                { "ETX",1}
            };
            Dictionary<string, int> DefaultDataItem50 = new Dictionary<string, int>()
            {
                { "STX",1},
                { "SeqNo",4},
                { "Command",2},
                { "ReSent",1},
                { "TriggerTime",6},
                { "Counts",1},
                { "BlockNo",4},
                { "Storages",1},
                { "MCkey",4},
                { "Barcode",15},
                { "LoadStatus",1},
                { "ShelfHeight",1},
                { "ShelfWidth",1},
                { "Weight",6},
                { "BCC",2},
                { "ETX",1}
            };
            #endregion
            _commandType = new Dictionary<string, Dictionary<string, int>>()
            {
                { "01",DefaultDataItem01 },
                { "02",DefaultDataItem02 },
                { "03",DefaultDataItem03 },
                { "04",DefaultDataItem04 },
                { "05",DefaultDataItem05 },
                { "06",DefaultDataItem06 },
                { "10",DefaultDataItem10 },
                { "23",DefaultDataItem23 },
                { "24",DefaultDataItem24 },
                { "26",DefaultDataItem26 },
                { "30",DefaultDataItem30 },
                { "35",DefaultDataItem35 },
                { "40",DefaultDataItem40 },
                { "42",DefaultDataItem42 },
                { "50",DefaultDataItem50 }

            };
            


        }
    }
}
