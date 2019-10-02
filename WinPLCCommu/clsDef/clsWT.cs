using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WinPLCCommu
{
    public class clsWT
    {
        #region 列舉
        /// <summary>
        /// WT位置
        /// </summary>
        public enum enuWTLoc
        {
            None = 0,
            Once = 1
        }
        /// <summary>
        /// WT讀取狀態
        /// </summary>
        public enum enuWTSts
        {
            None = 0,
            Reading = 1,
            ReadFinish = 2,
        }
        #endregion
        public int StnIndex
        {
            get;
            set;
        }
        public string StnNo
        {
            get;
            set;
        }
        public string WeightNo
        {
            get;
            set;
        }
        public int WeightIndex
        {
            get;
            set;
        }
        public string Buffer
        {
            get;
            set;
        }

        /// <summary>
        /// 當前Weight狀態
        /// </summary>
        public enuWTSts WTSts
        {
            get;
            set;
        }
        /// <summary>
        /// 當前秤重值
        /// </summary>
        public string WT_Data
        {
            get;
            set;
        }
        /// <summary>
        /// WT位置
        /// </summary>
        public enuWTLoc WTLoc
        {
            get;
            set;
        }
        public bool bolWTSts
        {
            get;
            set;
        }
        #region 建構函式
        /// <summary>
        /// 初始化 Mirle.WinPLCCommu.clsBCR 類別的新執行個體
        /// </summary>
        public clsWT()
        {
            StnNo = string.Empty;
            WeightNo = string.Empty;
            WT_Data = string.Empty;
            Buffer = string.Empty;
            //BufferName = string.Empty;
            //PLC2PCBufferIndex = 0;
            StnIndex = 0;
            WeightIndex = 0;
            WTSts = enuWTSts.None;
            WTLoc = enuWTLoc.None;

        }
        #endregion 建構函式
    }
}
