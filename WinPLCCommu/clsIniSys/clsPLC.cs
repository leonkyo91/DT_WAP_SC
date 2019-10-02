using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ACTMULTILib;

namespace Mirle.Library
{
    public class clsPLC
    {
        #region 變數
        private int intStationNumber = 1;
        private bool bolConnectionFlag;
        private ActEasyIF gobjPLC;
        #endregion 變數

        /// <summary>
        /// PLC Address 特定位置列舉
        /// </summary>
        public enum enuAddressSection
        {
            LeftCmdSno = 0,
            RightCmdSno = 1,
            CmdMode = 2,
            StnMode = 3,
            //InitialNotice = 4,
            
            //PathNotice = 6,
            //大立光PC->PLC列舉------------------
            Cmdsno = 0,//序號
            Mode = 1,//模式
            StnModeNo = 2,//站口模式碼
            Ready = 3,
            ReadNotice = 4,
            //FunNotice = 5,
            PathNotice = 5,
            InitialNotice = 6,
            Error = 8,
            StnModeChange = 7,
            //Avail = 19,
            //------------------------
            IniNotice = 2,//初始通知
            FunNotice_1 = 4,//功能通知(1);2通知無誤放行，3:NG退出站口
            //FunNotice_2 = 4,//功能通知(2);0:站口無模式，1:站口入庫模式，2:站口出庫模式
            //FunNotice_3 = 5,//功能通知(3);0:無，1:無命令，2:條碼讀不到，3:超重，4:儲位不符
            //大立光PC->PLC 列舉------
            
            //------------------------
            HandShake = 0,
            DataTime = 1,
        }

        #region 屬性
        /// <summary>
        /// 取得或設定Mx Logical Station Number
        /// </summary>
        public int StationNumber
        {
            get
            {
                return intStationNumber;
            }
            set
            {
                if(!bolConnectionFlag)
                    intStationNumber = value;
            }
        }

        /// <summary>
        /// 取得PLC連線狀態
        /// </summary>
        public bool ConnectionFlag
        {
            get
            {
                return bolConnectionFlag;
            }
        }
        #endregion 屬性

        #region 建構函式
        /// <summary>
        /// 初始化 Mirle.WinPLCCommu.clsPLC 類別的新執行個體
        /// </summary>
        public clsPLC()
        {
            bolConnectionFlag = false;
            gobjPLC = new ActEasyIF();
        }

        /// <summary>
        /// 初始化 Mirle.WinPLCCommu.clsPLC 類別的新執行個體
        /// </summary>
        /// <param name="StationNumber">Mx Logical Station Number</param>
        public clsPLC(int StationNumber)
            : this()
        {
            intStationNumber = StationNumber;
        }
        #endregion 建構函式

        #region 公用方法
        /// <summary>
        /// 與PLC連線
        /// </summary>
        /// <returns>若PLC連線成功傳回True，否則傳回false</returns>
        public bool funOpen()
        {
            gobjPLC.ActLogicalStationNumber = intStationNumber;
            if(gobjPLC.Open() == 0)
                return bolConnectionFlag = true;
            else
                return bolConnectionFlag = false;
        }

        public void funClose()
        {
            gobjPLC.Close();
            bolConnectionFlag = false;
        }

        /// <summary>
        /// 取得PLC Data
        /// </summary>
        /// <param name="Address">PLC位置</param>
        /// <param name="Length">讀取長度</param>
        /// <param name="RetData">欲接收資料之目的地</param>
        /// <returns>若讀取成功傳回True，否則傳回false</returns>
        public bool funReadPLC(string Address, int Length, ref int[] RetData,string strPLCNo)
        {
            string strLog = string.Empty;
            try
            {
                
                if(gobjPLC.ReadDeviceBlock(Address, Length, out RetData[0]) == 0)
                {
                    for(int intLength = 0; intLength < RetData.Length; intLength++)
                    {
                        if(string.IsNullOrWhiteSpace(strLog))
                            strLog = RetData[intLength].ToString();
                        else
                            strLog += "," + RetData[intLength].ToString();
                    }
                    //將Log存成File
                    clsSystem.funWritePLCRLog("ReadDeviceBlock(" + Address + ") Success! ->" + strLog, strPLCNo);

                    //存成即時(DB)
                    clsSystem.funWritePLCRLog_DB(strLog, strPLCNo, Address);
                    return bolConnectionFlag = true;
                }
                else
                {
                    clsSystem.funWritePLCRLog("ReadDeviceBlock(" + Address + ") Fail!", strPLCNo);
                    return bolConnectionFlag = false;
                }
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
                return bolConnectionFlag = false;
            }
        }

        /// <summary>
        /// 寫入PLC資料
        /// </summary>
        /// <param name="Address">PLC位置</param>
        /// <param name="SetData">欲寫入之資料</param>
        /// <returns></returns>
        public bool funWritePLC(string Address, params string[] SetData)
        {
            //string strLog = string.Empty;
            System.Text.StringBuilder sb = new StringBuilder();
            try
            {
                int[] intData = new int[SetData.Length];
                for (int intLength = 0; intLength < SetData.Length; intLength++)
                {
                    if (string.IsNullOrWhiteSpace(SetData[intLength]))
                        SetData[intLength] = "0";

                    intData[intLength] = int.Parse(SetData[intLength]);

                    //if(string.IsNullOrWhiteSpace(strLog))
                    //    strLog = SetData[intLength];
                    //else
                    //    strLog += "," + SetData[intLength];
                    sb.Append(SetData[intLength]);
                }

                if (gobjPLC.WriteDeviceBlock(Address, SetData.Length, ref intData[0]) == ErrDef.ProcSuccess)
                {
                    //clsSystem.funWritePLCWLog("WriteDeviceBlock(" + Address + ") Success! ->" + strLog);
                    clsSystem.funWritePLCWLog("WriteDeviceBlock(" + Address + ") Success! ->" + sb);
                    return bolConnectionFlag = true;
                }
                else
                {
                    //clsSystem.funWritePLCWLog("WriteDeviceBlock(" + Address + ") Fail! ->" + strLog);
                    clsSystem.funWritePLCWLog("WriteDeviceBlock(" + Address + ") Fail! ->" + sb);
                    return bolConnectionFlag = false;
                }
            }
            catch (Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
                return bolConnectionFlag = false;
            }
            finally
            {
                if (sb != null)
                {
                    sb = null;
                }
            }
        }

        /// <summary>
        /// 寫入PLC資料
        /// </summary>
        /// <param name="StnNo">PLC位置</param>
        /// <param name="SetData">欲寫入之資料</param>
        /// <returns></returns>
        public bool funWritePLCBit(string StnNo, bool SetBit)
        {
            try
            {
                string strAdrress = string.Empty;
                switch(StnNo)
                {
                    case "A01":
                        strAdrress = "D3000.0";
                        break;
                    case "A02":
                        strAdrress = "D3000.1";
                        break;
                    case "A03":
                        strAdrress = "D3000.2";
                        break;
                    case "D04":
                        strAdrress = "D3000.3";
                        break;
                }

                if(gobjPLC.SetDevice(strAdrress, SetBit ? 1 : 0) == ErrDef.ProcSuccess)
                {
                    clsSystem.funWritePLCWLog("SetDevice(" + strAdrress + ") Success! ->" + (SetBit ? "1" : "0"));
                    return bolConnectionFlag = true;
                }
                else
                {
                    clsSystem.funWritePLCWLog("SetDevice(" + strAdrress + ") Fail! ->" + (SetBit ? "1" : "0"));
                    return bolConnectionFlag = false;
                }
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
                return bolConnectionFlag = false;
            }
        }
        #endregion 公用方法
    }
}
