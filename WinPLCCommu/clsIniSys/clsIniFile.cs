using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.Library
{
    public class clsIniFile
    {
        #region 變數
        private string strIniFilePath;
        #endregion 變數

        #region clsNativeMethods
        /// <summary>
        /// win API Class
        /// </summary>
        private class clsNativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            public static extern int WritePrivateProfileString(string Section, string Key, string Value, string FilePath);
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            public static extern int GetPrivateProfileString(string Section, string Key, string Definition, StringBuilder stringBuilder, int Size, string FilePath);
        }
        #endregion clsNativeMethods

        #region 建構函式
        /// <summary>
        /// 初始化 Mirle.Library.clsIniFile 類別的新執行個體
        /// </summary>
        public clsIniFile()
        {
        }

        /// <summary>
        /// 初始化 Mirle.Library.clsIniFile 類別的新執行個體
        /// </summary>
        /// <param name="IniFilePath">Ini檔名與目錄</param>
        public clsIniFile(string IniFilePath)
        {
            strIniFilePath = IniFilePath;
        }
        #endregion 建構函式

        #region 屬性
        /// <summary>
        /// 取得或設定Ini檔名與目錄
        /// </summary>
        public string gstrIniFilePath
        {
            get
            {
                return strIniFilePath;
            }
            set
            {
                strIniFilePath = value;
            }
        }
        #endregion 屬性

        #region 公用方法
        /// <summary>
        /// 寫入Ini中特定位置
        /// </summary>
        /// <param name="Section">Section位置</param>
        /// <param name="Key">参數位置</param>
        /// <param name="Value">参數</param>
        public void funWriteValue(string Section, string Key, string Value)
        {
            clsNativeMethods.WritePrivateProfileString(Section, Key, Value, this.strIniFilePath);
        }

        public string funReadValue(string Section, string Key, string Default)
        {
            StringBuilder buffer = new StringBuilder(255);
            clsNativeMethods.GetPrivateProfileString(Section, Key, Default, buffer, 255, this.strIniFilePath);

            return buffer.ToString();
        }

        /// <summary>
        /// 讀取Ini中特定位置
        /// </summary>
        /// <param name="Section">Section位置</param>
        /// <param name="Key">参數位置</param>
        /// <param name="Default">默認参數</param>
        /// <returns>成功時傳回参數，否則傳回默認参數</returns>
        public int funReadValue(string Section, string Key, int Default)
        {
            StringBuilder buffer = new StringBuilder(255);
            clsNativeMethods.GetPrivateProfileString(Section, Key, Default.ToString(), buffer, 255, this.strIniFilePath);

            return int.Parse(buffer.ToString());
        }
        #endregion 公用方法
    }
}
