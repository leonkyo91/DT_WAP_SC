using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WinPLCCommu
{
    public class clsBCR
    {
        #region 列舉
        /// <summary>
        /// BCR讀取狀態
        /// </summary>
        public enum enuBCRSts
        {
            None = 0,
            Reading = 1,
            ReadFinish = 2,
        }

        /// <summary>
        /// BCR位置
        /// </summary>
        public enum enuBCRLoc
        {
            None = 0,
            Left = 1,
            Right = 2,
            Once =3,
        }
        #endregion 列舉

        #region 屬性
        /// <summary>
        /// BCR編號
        /// </summary>
        public string BCRNo
        {
            get;
            set;
        }

        /// <summary>
        /// BCR位置
        /// </summary>
        public enuBCRLoc BCRLoc
        {
            get;
            set;
        }

        /// <summary>
        /// PLC Buffer Name
        /// </summary>
        public string BufferName
        {
            get;
            set;
        }

        /// <summary>
        /// Stn No
        /// </summary>
        public string StnNo
        {
            get;
            set;
        }

        /// <summary>
        /// Stn Index
        /// </summary>
        public int StnIndex
        {
            get;
            set;
        }
        /// <summary>
        /// PLCIndex
        /// </summary>
        public string PLCIndex
        {
            get;
            set;
        }

        /// <summary>
        /// PLC->PC Buffer Name
        /// </summary>
        public int PLC2PCBufferIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 當前BCR狀態
        /// </summary>
        public enuBCRSts BCRSts
        {
            get;
            set;
        }

        /// <summary>
        /// 當前BCR ID
        /// </summary>
        public string BCRID
        {
            get;
            set;
        }/// <summary>
        /// 
        /// </summary>
        public string CmdSno
        {
            get;
            set;
        }
        public string CmdMode
        {
            get;
            set;
        }
        public string Trace
        {
            get;
            set;
        }
        public string IOType
        {
            get;
            set;
        }


        #endregion 屬性

        #region 建構函式
        /// <summary>
        /// 初始化 Mirle.WinPLCCommu.clsBCR 類別的新執行個體
        /// </summary>
        public clsBCR()
        {
            StnNo = string.Empty;
            BCRNo = string.Empty;
            BCRID = string.Empty;
            BufferName = string.Empty;
            PLC2PCBufferIndex = 0;
            StnIndex = 0;

            BCRLoc = enuBCRLoc.None;
            BCRSts = enuBCRSts.None;
        }
        #endregion 建構函式
    }
}
