using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICommon
{
    public class Equipment
    {
        private static string _barCode = string.Empty;
        private static string _eqpID = string.Empty;
        private static string _eqpstatus = string.Empty;
        private static string _loadStatus = string.Empty;
        private static string _battery;
        private static string _layer;
        private static string _row;
        private static string _bas;
        private static string _shelfHeight;
        private static string _shelfWidth;
        private static string _platform;
        private static string _pier;
        private static string _parentEqpNo;
        private static int _seqno;
        private static string _eqpErrorCode;
        public static string barCode { get { return _barCode; } set { _barCode = value; } }
        public static string eqpID { get { return _eqpID; } set { _eqpID = value; } }
        public static string eqpstatus { get { return _eqpstatus; } set { _eqpstatus = value; } }
        public static string loadStatus { get { return _loadStatus; } set { _loadStatus = value; } }
        public static string battery { get { return _battery; } set { _battery = value; } }
        public static string layer { get { return _layer; } set { _layer = value; } }
        public static string row { get { return _row; } set { _row = value; } }
        public static string bas { get { return _bas; } set { _bas = value; } }
        public static string shelfHeight { get { return _shelfHeight; } set { _shelfHeight = value; } }
        public static string shelfWidth { get { return _shelfWidth; } set { _shelfWidth = value; } }
        public static string platform { get { return _platform; } set { _platform = value; } }
        public static string pier { get { return _pier; } set {  _pier = value; } }
        public static string parentEqpNo { get { return _parentEqpNo; } set { _parentEqpNo = value; } }
        public static string eqpErrorCode { get { return _eqpErrorCode; } set { _eqpErrorCode = value; } }
        public static int seqno { get { return _seqno; } set { _seqno = value; } }
        
        public event EventHandler<MsgEventArg> UpdateEQP;
        private MsgEventArg me = new MsgEventArg();
        protected virtual void UpdateAndRefresh(MsgEventArg e)
        {
            if (UpdateEQP != null)
            {
                UpdateEQP(this, e);
            }
        }
        public void UpdateInfo(Dictionary<string,string>Dic)
        {
            try
            {
                foreach (string s in Dic.Keys)
                {
                    switch (s)
                    {
                        case "EqpNo":
                            eqpID = Dic[s];
                            break;
                        case "SeqNo":
                            seqno= int.Parse(Dic[s]);
                            break;
                        case "ParentEqpNo":
                            parentEqpNo = Dic[s];
                            break;
                        case "EqpStatus":
                            eqpstatus = Dic[s];
                            break;
                        case "LoadStatus":
                            loadStatus = Dic[s];
                            break;
                        case "Battery":
                            battery = Dic[s];
                            break;
                        case "Layer":
                            layer = Dic[s];
                            break;
                        case "Row":
                            row = Dic[s];
                            break;
                        case "Base":
                            bas = Dic[s];
                            break;
                        case "ShelfHeight":
                            shelfHeight = Dic[s];
                            break;
                        case "ShelfWidth":
                            shelfWidth = Dic[s];
                            break;
                        case "Platform":
                            platform = Dic[s];
                            break;
                        case "Pier":
                            pier = Dic[s];
                            break;
                        case "ErrorCode":
                            eqpErrorCode = Dic[s];
                            break;
                    }
                }
                UpdateAndRefresh(me);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



    }
}
