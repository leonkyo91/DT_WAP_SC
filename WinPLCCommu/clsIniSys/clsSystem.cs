using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.Library
{
    public class clsSystem
    {
        private clsSystem()
        {
        }

        #region 常數
        /// <summary>
        /// Ini檔名稱
        /// </summary>
        public const string cstrIniFileName = "WinPLCCommu.ini";
        /// <summary>
        /// System 用資料庫
        /// </summary>
        public const string cstrSystemDataBase = "WinPLCCommu.DB";
        #endregion 常數

        #region 全域變數
        /// <summary>
        /// IniFile物件
        /// </summary>
        public static clsIniFile gobjIniFile = new clsIniFile();
        /// <summary>
        /// DB連線物件
        /// </summary>
        public static DB gobjDB = new DB(true);
        /// <summary>
        /// 系統用DB連線物件
        /// </summary>
        public static DB gobjSystemDB = new DB();
        /// <summary>
        /// PLC1連線物件
        /// </summary>
        public static clsPLC gobjPLC = new clsPLC();
        /// <summary>
        /// PLC2連線物件
        /// </summary>
        public static clsPLC gobjPLC2 = new clsPLC();
        /// <summary>
        /// PLC2連線物件
        /// </summary>
        public static clsPLC gobjPLC3 = new clsPLC();

        /// <summary>
        /// Log物件
        /// </summary>
        public static Log gobjLog = new Log(Application.StartupPath + @"\Log\WinPLCCommu\");
        /// <summary>
        /// PLC Log物件
        /// </summary>
        public static Log gobjPLCLog = new Log(Application.StartupPath + @"\Log\MPLC\");
        /// <summary>
        /// 文字檔Log壓縮時間
        /// </summary>
        public static int gintTxtLogCompressDay = 30;
        /// <summary>
        /// 文字檔Log刪除時間
        /// </summary>
        public static int gintTxtLogDeleteDay = 180;
        /// <summary>
        /// MX Logical Station Number_1
        /// </summary>
        public static int gintActLogicalStationNumber_1 = 1;
        /// <summary>
        /// MX Logical Station Number_2
        /// </summary>
        public static int gintActLogicalStationNumber_2 = 2;
        /// <summary>
        /// MX Logical Station Number_3
        /// </summary>
        public static int gintActLogicalStationNumber_3 = 3;
        /// <summary>
        /// Crane 數量
        /// </summary>
        public static int gintRMQty = 11;// for Far Eastern
        /// <summary>
        /// 站口數量
        /// </summary>
        public static int gintStnQty = 22;// for Far Eastern
        /// <summary>
        /// Buffer數量(不含站口)
        /// </summary>
        public static int gintBufferQty_1 = 22;// for Far Eastern
        /// <summary>
        /// PLC->PC Buffer Word Total 長度
        /// </summary>
        public static int gintPLC2PCBufferTotalWord_1 = 10;// for Far Eastern
        /// <summary>
        /// PLC->PC PLC1 Word Total 長度
        /// </summary>
        //public static int gintPLC2PCTotalWord_1 = 483;//for Largan
        public static int gintPLC2PCTotalWord_1 = 450;//for Far Eastern
        /// <summary>
        /// PLC->PC PLC2 Word Total 長度
        /// </summary>
        //public static int gintPLC2PCTotalWord_2 = 723;//for Largan
        public static int gintPLC2PCTotalWord_2 = 740;//for Largan
        /// <summary>
        /// PLC->PC PLC3 Word Total 長度
        /// </summary>
        //public static int gintPLC2PCTotalWord_3 = 483;//for Largan
        public static int gintPLC2PCTotalWord_3 = 500;//for Largan
        /// <summary>
        /// PLC->PC PLC Word 開始位置
        /// </summary>
        public static string gstrPLC2PCWordAddressStart_1 = "D1000";// for Far Eastern
        /// <summary>
        /// PC->PLC Buffer Word Total 長度
        /// </summary>
        public static int gintPC2PLCBufferTotalWord_1 = 10;
        /// <summary>
        /// PC->PLC PLC1 Word Total 長度
        /// </summary>
        //public static int gintPC2PLCTotalWord_1 = 243;//for Largan
        public static int gintPC2PLCTotalWord_1 = 450;// for Far Eastern
        /// <summary>
        /// PC->PLC PLC2 Word Total 長度
        /// </summary>
        //public static int gintPC2PLCTotalWord_2 = 363;//for Largan
        public static int gintPC2PLCTotalWord_2 = 370;//for Largan
        /// <summary>
        /// PC->PLC PLC3 Word Total 長度
        /// </summary>
        //public static int gintPC2PLCTotalWord_3 = 243;//for Largan
        public static int gintPC2PLCTotalWord_3 = 250;//for Largan
        /// <summary>
        /// PC->PLC PLC Word 開始位置
        /// </summary>
        public static string gstrPC2PLCWordAddressStart_1 = "D3000";
        /// <summary>
        /// 校正儲位數量
        /// </summary>
        public static int gintRepairLocCount = 6;
        /// <summary>
        /// 校正儲位1
        /// </summary>
        public static string gstrRepairLoc1 = "010203";
        /// <summary>
        /// 校正儲位2
        /// </summary>
        public static string gstrRepairLoc2 = "020203";
        /// <summary>
        /// 校正儲位3
        /// </summary>
        public static string gstrRepairLoc3 = "050203";
        /// <summary>
        /// 校正儲位4
        /// </summary>
        public static string gstrRepairLoc4 = "060203";
        /// <summary>
        /// 校正儲位5
        /// </summary>
        public static string gstrRepairLoc5 = "090203";
        /// <summary>
        /// 校正儲位6
        /// </summary>
        public static string gstrRepairLoc6 = "100203";
        /// <summary>
        /// 秤重機MAX荷重(KG)
        /// </summary>
        public static double gstrWeightOver = 1000;

        public static Dictionary<string, int> gdicPC2PLCMap = new Dictionary<string, int>();
        public static Dictionary<string, int> gdicBufferIndex = new Dictionary<string, int>();
        public static Dictionary<string, int> gdicPLCIndex = new Dictionary<string, int>();

        public static int intBegin = 0;
        #endregion 全域變數

        #region 靜態方法
        /// <summary>
        /// 寫入Exception Log
        /// </summary>
        /// <param name="FunName">組件名稱</param>
        /// <param name="FunctionName">方法名稱</param>
        /// <param name="Message">訊息</param>
        public static void funWriteExceptionLog(string FunName, string FunctionName, string Message)
        {
            try
            {
                gobjLog.funWriteLogFile(
                       DateTime.Now.ToString("yyyyMMdd") + "_Exception.log",
                       FunName.Trim() + "." + FunctionName.Trim() + "|" + Message,
                       Mirle.Log.enuTitleType.TimeMS);
            }
            catch(FieldAccessException exFile)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + exFile.Message);
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + ex.Message);
            }
        }

        /// <summary>
        /// 寫入SystemTrace Log
        /// </summary>
        /// <param name="MPLCTrace">System Trace Log</param>
        public static void funWriteSystemTraceLog(string SystemTrace)
        {
            try
            {
                gobjLog.funWriteLogFile(DateTime.Now.ToString("yyyyMMdd") + "_SystemTrace.log", SystemTrace, Mirle.Log.enuTitleType.TimeMS);
            }
            catch(FieldAccessException exFile)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + exFile.Message);
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + ex.Message);
            }
        }
        /// <summary>
        /// 寫入MPLC Trace Log
        /// </summary>
        /// <param name="MPLCTrace">System Trace Log</param>
        public static void funWriteMPLCTraceLog(string MPLCTrace)
        {
            try
            {
                gobjLog.funWriteLogFile(DateTime.Now.ToString("yyyyMMdd") + "_MPLCTrace.log", MPLCTrace, Mirle.Log.enuTitleType.TimeMS);
            }
            catch(FieldAccessException exFile)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + exFile.Message);
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + ex.Message);
            }
        }

        /// <summary>
        /// 寫入PLCW Log
        /// </summary>
        /// <param name="PLCWLog">PLCW Log</param>
        public static void funWritePLCWLog(string PLCWLog)
        {
            try
            {
                gobjPLCLog.funWriteLogFile(DateTime.Now.ToString("yyyyMMdd") + "_PLCW.log", PLCWLog, Mirle.Log.enuTitleType.TimeMS);
            }
            catch(FieldAccessException exFile)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + exFile.Message);
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + ex.Message);
            }
        }

        /// <summary>
        /// 寫入PLCData Log
        /// </summary>
        /// <param name="PLCDataLog">PLC Data Log</param>
        public static void funWritePLCRLog(string PLCDataLog,string strPLCNo)
        {
            try
            {
                gobjPLCLog.funWriteLogFile(DateTime.Now.ToString("yyyyMMddHH") + "_PLC" + strPLCNo + "Data.log", PLCDataLog, Mirle.Log.enuTitleType.TimeMS);
            }
            catch(FieldAccessException exFile)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + exFile.Message);
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + ex.Message);
            }
        }

        public static void funWriteAlarmLog(string AlarmData)
        {
            try
            {
                gobjLog.funWriteLogFile(DateTime.Now.ToString("yyyyMMdd") + "_AlarmData.log", AlarmData, Mirle.Log.enuTitleType.TimeMS);
            }
            catch(FieldAccessException exFile)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + exFile.Message);
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                MessageBox.Show(varObject.DeclaringType.FullName + "." + varObject.Name + " Exception:" + ex.Message);
            }
        }

        /// <summary>
        /// 寫入PLCData Log
        /// </summary>
        /// <param name="PLCDataLog">PLC Data Log</param>
        public static void funWritePLCRLog_DB(string PLCDataLog,string strPLCNo,string strAddr)
        {
            string strSql = string.Empty;
            string strEM = string.Empty;
            try
            {
                strSql = "Update PLC_Log Set PLC_Data='" + PLCDataLog + "' Where PLC_No ='" + strPLCNo + "' AND STR_ADDR ='" + strAddr +"'";
                if (clsSystem.gobjDB.funExecSql(strSql, ref strEM) != 0)
                {
                    //無資料的話 Insert
                    strSql = "Insert Into PLC_Log(PLC_No,STR_ADDR,PLC_Data)Values('" + strPLCNo + "','" + strAddr + "','" + PLCDataLog + "')";
                    clsSystem.gobjDB.funExecSql(strSql, ref strEM);
                }

            }
            catch (Exception ex)
            {
            }
        }

        #endregion 靜態方法
    }
}
