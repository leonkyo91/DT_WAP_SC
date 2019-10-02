using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.Library
{
    public class clsInitSys
    {
        private clsInitSys()
        {
        }

        #region 靜態方法
        /// <summary>
        /// 檢查是否已重復開啟程式，若有重復開啟會將前一個程式關閉
        /// </summary>
        public static void funCheckProcess()
        {
            Process[] objarProcess = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if(objarProcess.Length > 1)
            {
                foreach(Process objProcess in objarProcess)
                {
                    try
                    {
                        if(objProcess.Id != Process.GetCurrentProcess().Id)
                            objProcess.Kill();
                    }
                    catch(Exception ex)
                    {
                        var varObject = MethodBase.GetCurrentMethod();
                        clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 初始化系統
        /// </summary>
        public static void funInitSystem()
        {
            if(!funCheckIniFile())
                Environment.Exit(0);

            #region 讀取Ini設定
            funSetSystemConfig(clsSystem.gobjIniFile);
            funSetPLC(clsSystem.gobjIniFile);
            funSetDB(clsSystem.gobjIniFile);
            funSetRepairLoc(clsSystem.gobjIniFile);
            funSetComPort(clsSystem.gobjIniFile);
            #endregion 讀取Ini設定

            funOpendDB();
            funOpendPLC();

            funSetLog();
        }
        #endregion 靜態方法

        #region 私用靜態方法

        #region 讀取ini相關
        /// <summary>
        /// 檢查是否存在Ini檔
        /// </summary>
        /// <param name="IniFile">IniFile物件</param>
        /// <returns>若存在時傳回true，否則回false</returns>
        private static bool funCheckIniFile()
        {
            try
            {
                if (!File.Exists(Application.StartupPath + "\\" + clsSystem.cstrIniFileName))
                {
                    MessageBox.Show(
                        "找不到" + clsSystem.cstrIniFileName + "，請洽系統管理人員 !!",
                        "WinPLCCommu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return false;
                }
                else if (!File.Exists(Application.StartupPath + "\\" + clsSystem.cstrSystemDataBase))
                {
                    MessageBox.Show(
                        "找不到" + clsSystem.cstrSystemDataBase + "，請洽系統管理人員 !!",
                        "WinPLCCommu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return false;
                }
                else
                {

                    clsSystem.gobjSystemDB.DBType = DB.enuDatabaseType.SQLite;
                    clsSystem.gobjSystemDB.DBName = Application.StartupPath + "\\" + clsSystem.cstrSystemDataBase;

                    clsSystem.gobjIniFile.gstrIniFilePath = Application.StartupPath + "\\" + clsSystem.cstrIniFileName;
                    return true;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                //subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 讀取Ini中SystemConfig區塊相關設定
        /// </summary>
        /// <param name="IniFile">IniFile物件</param>
        private static void funSetSystemConfig(clsIniFile IniFile)
        {
            string strSection = "System Config";
            string strKey = string.Empty;
            string strValues = string.Empty;

            strKey = "CompressDay";
            clsSystem.gintTxtLogCompressDay = IniFile.funReadValue(strSection, strKey, 30);
            strKey = "DeleteDay";
            clsSystem.gintTxtLogDeleteDay = IniFile.funReadValue(strSection, strKey, 180);
            strKey = "RMQty";
            clsSystem.gintRMQty = IniFile.funReadValue(strSection, strKey, 3);
        }

        /// <summary>
        /// 讀取Ini中PLC區塊相關設定
        /// </summary>
        /// <param name="IniFile">IniFile物件</param>
        private static void funSetPLC(clsIniFile IniFile)
        {
            string strSection = string.Empty;
            string strKey = string.Empty;

            strSection = "PLC Info";
            #region PLC1---For 遠紡
            strKey = "ActLogicalStationNo_1";
            clsSystem.gintActLogicalStationNumber_1 = IniFile.funReadValue(strSection, strKey, 1);


            strKey = "PLC2PCTotalWord_1";
            clsSystem.gintPLC2PCTotalWord_1 = IniFile.funReadValue(strSection, strKey, 450);
            strKey = "PLC2PCBufferTotalWord";
            clsSystem.gintPLC2PCBufferTotalWord_1 = IniFile.funReadValue(strSection, strKey, 10);
            strKey = "PLC2PCWordAddressStart";
            clsSystem.gstrPLC2PCWordAddressStart_1 = IniFile.funReadValue(strSection, strKey, "D1000");

            strKey = "PC2PLCTotalWord_1";
            clsSystem.gintPC2PLCTotalWord_1 = IniFile.funReadValue(strSection, strKey, 450);
            strKey = "PC2PLCBufferTotalWord";
            clsSystem.gintPC2PLCBufferTotalWord_1 = IniFile.funReadValue(strSection, strKey, 10);
            strKey = "PC2PLCWordAddressStart";
            clsSystem.gstrPC2PLCWordAddressStart_1 = IniFile.funReadValue(strSection, strKey, "D3000"); 
            #endregion

            #region PLC2---For 大立光
            strKey = "ActLogicalStationNo_2";
            clsSystem.gintActLogicalStationNumber_2 = IniFile.funReadValue(strSection, strKey, 2);            
            strKey = "PLC2PCTotalWord_2";
            clsSystem.gintPLC2PCTotalWord_2 = IniFile.funReadValue(strSection, strKey, 723);            
            strKey = "PC2PLCTotalWord_2";
            clsSystem.gintPC2PLCTotalWord_2 = IniFile.funReadValue(strSection, strKey, 363);
            #endregion

            #region PLC3---For大立光
            strKey = "ActLogicalStationNo_3";
            clsSystem.gintActLogicalStationNumber_3 = IniFile.funReadValue(strSection, strKey, 3);
            strKey = "PLC2PCTotalWord_3";
            clsSystem.gintPLC2PCTotalWord_3 = IniFile.funReadValue(strSection, strKey, 723);
            strKey = "PC2PLCTotalWord_3";
            clsSystem.gintPC2PLCTotalWord_3 = IniFile.funReadValue(strSection, strKey, 363);
            #endregion


            strKey = "BufferQty";
            clsSystem.gintBufferQty_1 = IniFile.funReadValue(strSection, strKey, 72);
            strKey = "StnQty";
            clsSystem.gintStnQty = IniFile.funReadValue(strSection, strKey, 10);
        }

        /// <summary>
        /// 讀取Ini中Data Base區塊相關設定
        /// </summary>
        /// <param name="IniFile">IniFile物件</param>
        private static void funSetDB(clsIniFile IniFile)
        {
            string strSection = string.Empty;
            string strKey = string.Empty;
            string strValues = string.Empty;

            strSection = "Data Base";
            strKey = "DBMS";
            strValues = IniFile.funReadValue(strSection, strKey, "MSSQL");
            clsSystem.gobjDB.DBType = clsTool.funGetEnumValue<DB.enuDatabaseType>(strValues);
            strKey = "DbServer";
            clsSystem.gobjDB.DBServer = IniFile.funReadValue(strSection, strKey, ".");
            strKey = "FODbServer";
            clsSystem.gobjDB.FODBServer = IniFile.funReadValue(strSection, strKey, ".");
            strKey = "DataBase";
            clsSystem.gobjDB.DBName = IniFile.funReadValue(strSection, strKey, "");
            strKey = "DbUser";
            clsSystem.gobjDB.DBUser = IniFile.funReadValue(strSection, strKey, "");
            strKey = "DbPswd";
            clsSystem.gobjDB.DBPassword = IniFile.funReadValue(strSection, strKey, "");
           

        }

        private static void funSetRepairLoc(clsIniFile IniFile)
        {
            string strSection = string.Empty;
            string strKey = string.Empty;
            string strValues = string.Empty;

            strSection = "RepairLoc";
            strKey = "COUNT";
            clsSystem.gintRepairLocCount = IniFile.funReadValue(strSection, strKey, 6);
            strKey = "RepairLoc1";
            clsSystem.gstrRepairLoc1 = IniFile.funReadValue(strSection, strKey, "010203");
            strKey = "RepairLoc2";
            clsSystem.gstrRepairLoc2 = IniFile.funReadValue(strSection, strKey, "020203");
            strKey = "RepairLoc3";
            clsSystem.gstrRepairLoc3 = IniFile.funReadValue(strSection, strKey, "050203");
            strKey = "RepairLoc4";
            clsSystem.gstrRepairLoc4 = IniFile.funReadValue(strSection, strKey, "060203");
            strKey = "RepairLoc5";
            clsSystem.gstrRepairLoc5 = IniFile.funReadValue(strSection, strKey, "090203");
            strKey = "RepairLoc6";
            clsSystem.gstrRepairLoc6 = IniFile.funReadValue(strSection, strKey, "100203");
        }
        private static void funSetComPort(clsIniFile IniFile)
        {
            //string strSection = string.Empty;
            //string strKey = string.Empty;
            //string strValues = string.Empty;
            //strSection = "Weight Config";
            //strKey = "WT_A_2L";
            //clsSystem.gstrWT_A_2L = IniFile.funReadValue(strSection, strKey, "COM4");
            //strKey = "WT_A_4L";
            //clsSystem.gstrWT_A_4L = IniFile.funReadValue(strSection, strKey, "COM5");
        }
        #endregion 讀取ini相關

        /// <summary>
        /// Log壓縮與刪除設定
        /// </summary>
        private static void funSetLog()
        {
            #region 系統相關
            if(clsSystem.gintTxtLogCompressDay > 0 || clsSystem.gintTxtLogDeleteDay > 0)
            {
                clsSystem.gobjLog.SetTimerEnable = false;
                clsSystem.gobjLog.SetAutoCompressReserveDay = clsSystem.gintTxtLogCompressDay;
                clsSystem.gobjLog.SetAutoCompressEnable = clsSystem.gintTxtLogCompressDay > 0 ? true : false;
                clsSystem.gobjLog.SetAutoDeleteReserveDay = clsSystem.gintTxtLogDeleteDay;
                clsSystem.gobjLog.SetAutoDeleteEnable = clsSystem.gintTxtLogDeleteDay > 0 ? true : false;
                clsSystem.gobjLog.SetTimerInterval = 1000;
                clsSystem.gobjLog.SetTimerEnable = true;
            }
            else
                clsSystem.gobjLog.SetTimerEnable = false;
            #endregion 系統相關

            #region MPLC相關
            clsSystem.gobjPLCLog.SetTimerEnable = false;
            clsSystem.gobjPLCLog.SetAutoCompressReserveDay = 7;
            clsSystem.gobjPLCLog.SetAutoCompressEnable = true;
            clsSystem.gobjPLCLog.SetAutoDeleteReserveDay = 30;
            clsSystem.gobjPLCLog.SetAutoDeleteEnable = true;
            clsSystem.gobjPLCLog.SetTimerInterval = 1000;
            clsSystem.gobjPLCLog.SetTimerEnable = true;
            #endregion MPLC相關
        }

        /// <summary>
        /// 進行資料庫連線
        /// </summary>
        private static void funOpendDB()
        {
            string strEM = string.Empty;
            clsSystem.gobjDB.funOpenDB(ref strEM);
            clsSystem.gobjSystemDB.funOpenDB(ref strEM);
        }

        /// <summary>
        /// 進行PLC連線
        /// </summary>
        private static void funOpendPLC()
        {
            string strEM = string.Empty;
            clsSystem.gobjPLC.StationNumber = clsSystem.gintActLogicalStationNumber_1;
            clsSystem.gobjPLC.funOpen();
            clsSystem.gobjPLC2.StationNumber = clsSystem.gintActLogicalStationNumber_2;
            clsSystem.gobjPLC2.funOpen();
            clsSystem.gobjPLC3.StationNumber = clsSystem.gintActLogicalStationNumber_3;
            clsSystem.gobjPLC3.funOpen();
        }

        /// <summary>
        /// Reconnection PLC
        /// </summary>
        /// <returns>若PLC連線成功傳回True，否則傳回false</returns>
        public static Task<bool> funReconnectionPLC(int intPLC)
        {
            switch (intPLC)
            {
                case 1:
                     clsSystem.gobjPLC.funClose();
                     return Task<bool>.FromResult(clsSystem.gobjPLC.funOpen());
                case 2:
                     clsSystem.gobjPLC2.funClose();
                     return Task<bool>.FromResult(clsSystem.gobjPLC2.funOpen());
                case 3:
                     clsSystem.gobjPLC3.funClose();
                     return Task<bool>.FromResult(clsSystem.gobjPLC.funOpen());
                default:
                     clsSystem.gobjPLC.funClose();
                     clsSystem.gobjPLC2.funClose();
                     clsSystem.gobjPLC3.funClose();

                     //clsSystem.gobjPLC.funOpen();
                     clsSystem.gobjPLC2.funOpen();
                     clsSystem.gobjPLC3.funOpen();
                     return Task<bool>.FromResult(clsSystem.gobjPLC.funOpen());
            }
            //clsSystem.gobjPLC.funClose();
            //return Task<bool>.FromResult(clsSystem.gobjPLC.funOpen());
        }

        /// <summary>
        /// Reconnection DB
        /// </summary>
        /// <returns>若資料庫皆連線成功傳回True，否則傳回false</returns>
        public static Task<bool> funReconnectionDB()
        {
            clsSystem.gobjDB.funCloseDB();
            clsSystem.gobjSystemDB.funCloseDB();

            string strEM = string.Empty;
            if(clsSystem.gobjDB.funOpenDB(ref strEM) != ErrDef.ProcSuccess)
                return Task<bool>.FromResult(false);
            else if(clsSystem.gobjSystemDB.funOpenDB(ref strEM) != ErrDef.ProcSuccess)
                return Task<bool>.FromResult(false);
            else
                return Task<bool>.FromResult(true);
        }
        #endregion 私用靜態方法
    }
}
