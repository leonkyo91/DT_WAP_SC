using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WinPLCCommu
{
    public class clsWTData
    {
        private clsWT[] objarWT = new clsWT[0];
        private int intWTCount = 0;
        private int intStnCount = 0;

        #region 屬性
        public clsWT[] this[int StnIndex]
        {
            get
            {
                List<clsWT> LstTmp = new List<clsWT>();
                foreach (clsWT WT in objarWT)
                {
                    if (WT.StnIndex == StnIndex)
                    {
                        LstTmp.Add(WT);
                    }
                }
                return LstTmp.ToArray();
            }
        }

        /// <summary>
        /// 取得WTData
        /// </summary>
        /// <param name="StnIndex"></param>
        /// <returns></returns>
        public clsWT this[int StnIndex, clsWT.enuWTLoc WTLoc]
        {
            get
            {
                foreach (clsWT WT in objarWT)
                {
                    if (WT.StnIndex == StnIndex)
                        return WT;
                }
                return null;
            }
        }

        #endregion 屬性

         #region 建構函式
        /// <summary>
        /// 初始化 Mirle.WinPLCCommu.clsWTData 類別的新執行個體
        /// </summary>
        /// <param name="BCR"></param>
        public clsWTData()
        {
            objarWT = new clsWT[0];
            intWTCount = 0;
            intStnCount = 0;
        }
        #endregion 建構函式

        /// <summary>
        /// 設定WT
        /// </summary>
        /// <param name="BCR"></param>
        public void funAddWT(clsWT[] WT)
        {
            objarWT = WT;
            intWTCount = WT.Length;

            string strStnNo = string.Empty;
            foreach (clsWT objTmp in objarWT)
            {
                if (objTmp.StnNo != strStnNo)
                {
                    intStnCount++;
                    strStnNo = objTmp.StnNo;
                }
            }
        }

    }
}
