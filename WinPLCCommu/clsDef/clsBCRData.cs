using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WinPLCCommu
{
    public class clsBCRData
    {
        private clsBCR[] objarBCR = new clsBCR[0];
        private int intBCRCount = 0;
        private int intStnCount = 0;

        #region 屬性

        public clsBCR[] this[int StnIndex]
        {
            get
            {
                List<clsBCR> LstTmp = new List<clsBCR>();
                foreach(clsBCR BCR in objarBCR)
                {
                    if(BCR.StnIndex == StnIndex)
                    {
                        if(BCR.BCRLoc == clsBCR.enuBCRLoc.Left)
                            LstTmp.Insert(0, BCR);
                        else
                            LstTmp.Add(BCR);
                    }
                }
                return LstTmp.ToArray();
            }
        }

        /// <summary>
        /// 取得BCRData
        /// </summary>
        /// <param name="StnIndex"></param>
        /// <returns></returns>
        public clsBCR this[int StnIndex, clsBCR.enuBCRLoc BCRLoc]
        {
            get
            {
                foreach(clsBCR BCR in objarBCR)
                {
                    if(BCR.StnIndex == StnIndex && BCR.BCRLoc == BCRLoc)
                        return BCR;
                }
                return null;
            }
        }

        /// <summary>
        /// 傳回BCR數量
        /// </summary>
        public int BCRCount
        {
            get
            {
                return intBCRCount;
            }
        }

        /// <summary>
        /// 傳回站口數量
        /// </summary>
        public int StnCount
        {
            get
            {
                return intStnCount;
            }
        }
        #endregion 屬性

        #region 建構函式
        /// <summary>
        /// 初始化 Mirle.WinPLCCommu.clsBCRData 類別的新執行個體
        /// </summary>
        /// <param name="BCR"></param>
        public clsBCRData()
        {
            objarBCR = new clsBCR[0];
            intBCRCount = 0;
            intStnCount = 0;
        }
        #endregion 建構函式

        #region 公用方法
        /// <summary>
        /// 設定BSR
        /// </summary>
        /// <param name="BCR"></param>
        public void funAddBCR(clsBCR[] BCR)
        {
            objarBCR = BCR;
            intBCRCount = BCR.Length;

            string strStnNo = string.Empty;
            foreach(clsBCR objTmp in objarBCR)
            {
                if(objTmp.StnNo != strStnNo)
                {
                    intStnCount++;
                    strStnNo = objTmp.StnNo;
                }
            }
        }

        /// <summary>
        /// 判斷 Mirle.WinPLCCommu.clsBCRData 是否包含指定的索引鍵
        /// </summary>
        /// <param name="StnNo"></param>
        /// <returns></returns>
        public bool funContainsKey(string StnNo)
        {
            foreach(clsBCR BCR in objarBCR)
            {
                if(BCR.StnNo == StnNo)
                    return true;
            }
            return false;
        }
        #endregion 公用方法
    }
}
