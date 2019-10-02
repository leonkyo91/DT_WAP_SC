using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT_WAP_SC
{
    /// <summary>
    /// Trace Log 類型列舉
    /// </summary>
    public enum enuTraceLog
    {
        None = 0,
        System = 1,
        MPLC = 2,
        Alarm = 3,
        SQL,
    }

    public class clsTraceLogEventArgs : EventArgs
    {
        public enuTraceLog objTraceLog;
        public string LogMessage;
        public DateTime LogDateTime;
        //大立光----
        public string CmdSno;
        public string Mode;
        public string IniNotice;
        public string FunNotice_1;
        public string FunNotice_2;
        public string FunNotice_3;
        //-----------
        public string LeftCmdSno;
        public string RightCmdSno;
        public string CmdSts;
        public string CmdMode;
        public string CmdCraneNo;
        public string Trace;
        public string StnNo;
        public string LocID;
        public string NewLocID;
        public string LocSts;
        public string OldLocSts;
        public string SNO;

        public string BCRNo;
        public string BCRSts;
        public string BCRID;

        public string CraneNo;
        public string CraneModeLast;
        public string CraneMode;
        public string CraneStsLast;
        public string CraneSts;

        public string BufferName;
        public string AddressSection;
        public string[] PLCValues;

        public bool AlarmClear;

        public string WTNo;
        public string WTSts;
        public string WTData;

        public string Weight;
        /// <summary>
        /// 初始化 Mirle.WinPLCCommu.clsTraceLog 類別的新執行個體
        /// </summary>
        /// <param name="TraceLog"></param>
        public clsTraceLogEventArgs(enuTraceLog TraceLog)
        {
            objTraceLog = TraceLog;
            LogMessage = string.Empty;
            LogDateTime = DateTime.Now;

            //大立光
            CmdSno = string.Empty;
            Mode = string.Empty;
            IniNotice = string.Empty;
            FunNotice_1 = string.Empty;
            FunNotice_2 = string.Empty;
            FunNotice_3 = string.Empty;

            LeftCmdSno = string.Empty;
            RightCmdSno = string.Empty;
            CmdMode = string.Empty;
            CmdCraneNo = string.Empty;
            CmdSts = string.Empty;
            Trace = string.Empty;
            StnNo = string.Empty;
            LocID = string.Empty;
            NewLocID = string.Empty;
            LocSts = string.Empty;
            OldLocSts = string.Empty;
            BCRNo = string.Empty;
            BCRSts = string.Empty;
            BCRID = string.Empty;
            SNO = string.Empty;

            CraneNo = string.Empty;
            CraneModeLast = string.Empty;
            CraneMode = string.Empty;
            CraneStsLast = string.Empty;
            CraneSts = string.Empty;

            BufferName = string.Empty;
            AddressSection = string.Empty;
            PLCValues = new string[0];

            AlarmClear = false;
            Weight = string.Empty;
        }
    }
}
