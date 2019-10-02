using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mirle.Library;


namespace Mirle.WinPLCCommu
{
    public class clsBufferData
    {
        public class clsAlarmEventArgs : EventArgs
        {
            public string AlarmDesc;
            public bool AlarmClear;
            public int BuferIndex;
            public int AlarmCode;
            public clsAlarmEventArgs(string alarmDesc, bool alarmClear, int buferIndex, int alarmCode)
            {
                AlarmDesc = alarmDesc;
                AlarmClear = alarmClear;
                BuferIndex = buferIndex;
                AlarmCode = alarmCode;
            }
        }

        #region 變數
        private clsAlarmData[] objarSystemErrorCode = new clsAlarmData[32];
        private clsPLC2PCBuffer[] objarPLC2PCBuffer;
        private clsPC2PLCBuffer[] objarPC2PLCBuffer;
        private int[] intarPLC2PCResultData_PLC1 = new int[0];
        private int[] intarPC2PLCResultData_PLC1 = new int[0];
        private int[] intarPLC2PCResultData_PLC2 = new int[0];
        private int[] intarPC2PLCResultData_PLC2 = new int[0];
        private int[] intarPLC2PCResultData_PLC3 = new int[0];
        private int[] intarPC2PLCResultData_PLC3 = new int[0];
        private bool bolHandShaking = false;

        public delegate void delAlarmEventHandler(object sender, clsAlarmEventArgs e);
        public event delAlarmEventHandler AlarmData;

        public delegate void wcsAlarmEventHandler(object sender, clsAlarmEventArgs e);
        public event wcsAlarmEventHandler wcsAlarmData;
        #endregion 變數

        #region 屬性
        /// <summary>
        /// 系統錯誤碼
        /// </summary>
        public clsAlarmData[] SystemErrorCode
        {
            get
            {
                return objarSystemErrorCode;
            }
        }

        /// <summary>
        /// PLC->PC Buffer 暫存緩衝區
        /// </summary>
        public clsPLC2PCBuffer[] PLC2PCBuffer
        {
            get
            {
                return objarPLC2PCBuffer;
            }
        }

        /// <summary>
        /// PC->PLC Buffer 暫存緩衝區
        /// </summary>
        public clsPC2PLCBuffer[] PC2PLCBuffer
        {
            get
            {
                return objarPC2PLCBuffer;
            }
        }

        /// <summary>
        /// Hand Shak Bit
        /// </summary>
        public bool HandShaking
        {
            get
            {
                return bolHandShaking;
            }
        }
        #endregion 屬性

        #region 列舉
        /// <summary>
        /// PC->PLC Buffer Address
        /// </summary>
        public enum enuPC2PLCBufferAddress
        {
            #region PC->PLC Buffer Address
            System = 2991,
            Stn_B01_1_1 = 3001,
            B01_1_2 = 3011,
            B01_2 = 3021,
            B01_3 = 3031,
            B01_4_1 = 3041,
            Stn_B01_4_2 = 3051,
            B01_4_3 = 3061,
            B01_5 = 3071,
            B01_6 = 3081,
            B01_7_1 = 3091,
            Stn_B01_7_2 = 3101,
            Stn_A01_1_1 = 3111,
            A01_1_2 = 3121,
            A01_2 = 3131,
            A19_1 = 3141,
            A19_2_1 = 3151,
            Stn_A19_2_2 = 3161,
            Stn_B02_1_1 = 3171,
            B02_1_2 = 3181,
            B02_2 = 3191,
            B02_3 = 3201,
            B02_4_1 = 3211,
            Stn_B02_4_2 = 3221,
            B02_4_3 = 3231,
            B02_5 = 3241,
            B02_6 = 3251,
            B02_7_1 = 3261,
            Stn_B02_7_2 = 3271,
            A02_1_1 = 3281,
            Stn_A02_1_2 = 3291,
            A02_2 = 3301,
            A20_1 = 3311,
            A20_2_1 = 3321,
            Stn_A20_2_2 = 3331,
            Stn_B03_1_1 = 3341,
            B03_1_2 = 3351,
            B03_2 = 3361,
            B03_3 = 3371,
            B03_4_1 = 3381,
            Stn_B03_4_2 = 3391,
            B03_4_3 = 3401,
            B03_5 = 3411,
            B03_6 = 3421,
            B03_7_1 = 3431,
            Stn_B03_7_2 = 3441,
            Stn_A03_1_1 = 3451,
            A03_1_2 = 3461,
            A03_2 = 3471,
            A21_1 = 3481,
            A21_2_1 = 3491,
            Stn_A21_2_2 = 3501,
            A04_1 = 3511,
            A04_2 = 3521,
            A04_3 = 3531,
            A05 = 3541,
            A06_1 = 3551,
            A06_2 = 3561,
            A06_3 = 3571,
            A06_4 = 3581,
            A07_1 = 3591,
            A07_2 = 3601,
            A07_3 = 3611,
            A08_1 = 3621,
            A09_1 = 3631,
            Stn_A10_1 = 3641,
            A11 = 3651,
            A10_2 = 3661,
            A09_2 = 3671,
            A08_2 = 3681,
            A12 = 3691,
            A13 = 3701,
            A14 = 3711,
            A15_0_1 = 3721,
            A15_0_2 = 3731,
            A16_1 = 3741,
            A16_2 = 3751,
            A16_3 = 3761,
            A17_0_1 = 3771,
            A17_0_2 = 3781,
            A18_1 = 3791,
            A18_2 = 3801,
            A18_3 = 3811,
            #endregion PC->PLC Buffer Address
        }

        /// <summary>
        /// PLC->PC Buffer Address
        /// </summary>
        public enum enuPLC2PCBufferAddress
        {
            #region PLC->PC Buffer Address
            System = 1000,
            Stn_B01_1_1 = 101,
            B01_1_2 = 121,
            B01_2 = 141,
            B01_3 = 161,
            B01_4_1 = 181,
            Stn_B01_4_2 = 201,
            B01_4_3 = 221,
            B01_5 = 241,
            B01_6 = 261,
            B01_7_1 = 281,
            Stn_B01_7_2 = 301,
            Stn_A01_1_1 = 321,
            A01_1_2 = 341,
            A01_2 = 361,
            A19_1 = 381,
            A19_2_1 = 401,
            Stn_A19_2_2 = 421,
            Stn_B02_1_1 = 441,
            B02_1_2 = 461,
            B02_2 = 481,
            B02_3 = 501,
            B02_4_1 = 521,
            Stn_B02_4_2 = 541,
            B02_4_3 = 561,
            B02_5 = 581,
            B02_6 = 601,
            B02_7_1 = 621,
            Stn_B02_7_2 = 641,
            Stn_A02_1_1 = 661,
            A02_1_2 = 681,
            A02_2 = 701,
            A20_1 = 721,
            A20_2_1 = 741,
            Stn_A20_2_2 = 761,
            Stn_B03_1_1 = 781,
            B03_1_2 = 801,
            B03_2 = 821,
            B03_3 = 841,
            B03_4_1 = 861,
            Stn_B03_4_2 = 881,
            B03_4_3 = 901,
            B03_5 = 921,
            B03_6 = 941,
            B03_7_1 = 961,
            Stn_B03_7_2 = 981,
            Stn_A03_1_1 = 1001,
            A03_1_2 = 1021,
            A03_2 = 1041,
            A21_1 = 1061,
            A21_2_1 = 1081,
            Stn_A21_2_2 = 1101,
            A04_1 = 1121,
            A04_2 = 1141,
            A04_3 = 1161,
            A05 = 1181,
            A06_1 = 1201,
            A06_2 = 1221,
            A06_3 = 1241,
            A06_4 = 1261,
            A07_1 = 1281,
            A07_2 = 1301,
            A07_3 = 1321,
            A08_1 = 1341,
            A09_1 = 1361,
            Stn_A10_1 = 1381,
            A11 = 1401,
            A10_2 = 1421,
            A09_2 = 1441,
            A08_2 = 1461,
            A12 = 1481,
            A13 = 1501,
            A14 = 1521,
            A15_0_1 = 1541,
            A15_0_2 = 1561,
            A16_1 = 1581,
            A16_2 = 1601,
            A16_3 = 1621,
            A17_0_1 = 1641,
            A17_0_2 = 1661,
            A18_1 = 1681,
            A18_2 = 1701,
            A18_3 = 1721,
            #endregion PLC->PC Buffer Address
        }
        #endregion 列舉

        #region 建構函式
        /// <summary>
        /// 初始化 Mirle.WinPLCCommu.clsBufferData 類別的新執行個體
        /// </summary>
        public clsBufferData()
        {
            for (int intLength = 0; intLength < 32; intLength++)
                objarSystemErrorCode[intLength] = new clsAlarmData();

            objarPC2PLCBuffer = new clsPC2PLCBuffer[clsSystem.gintBufferQty_1 + clsSystem.gintStnQty];
            for (int intLength = 0; intLength < objarPC2PLCBuffer.Length; intLength++)
                objarPC2PLCBuffer[intLength] = new clsPC2PLCBuffer();

            objarPLC2PCBuffer = new clsPLC2PCBuffer[clsSystem.gintBufferQty_1 + clsSystem.gintStnQty];
            for (int intLength = 0; intLength < objarPLC2PCBuffer.Length; intLength++)
                objarPLC2PCBuffer[intLength] = new clsPLC2PCBuffer();


            #region PLC1
            intarPC2PLCResultData_PLC1 = new int[clsSystem.gintPC2PLCTotalWord_1];
            for (int intLength = 0; intLength < intarPC2PLCResultData_PLC1.Length; intLength++)
                intarPC2PLCResultData_PLC1[intLength] = 0;

            intarPLC2PCResultData_PLC1 = new int[clsSystem.gintPLC2PCTotalWord_1];
            for (int intLength = 0; intLength < intarPLC2PCResultData_PLC1.Length; intLength++)
                intarPLC2PCResultData_PLC1[intLength] = 0;
            #endregion

            #region PLC2
            intarPC2PLCResultData_PLC2 = new int[clsSystem.gintPC2PLCTotalWord_2];
            for (int intLength = 0; intLength < intarPC2PLCResultData_PLC2.Length; intLength++)
                intarPC2PLCResultData_PLC2[intLength] = 0;

            intarPLC2PCResultData_PLC2 = new int[clsSystem.gintPLC2PCTotalWord_2];
            for (int intLength = 0; intLength < intarPLC2PCResultData_PLC2.Length; intLength++)
                intarPLC2PCResultData_PLC2[intLength] = 0;
            #endregion

            #region PLC3
            intarPC2PLCResultData_PLC3 = new int[clsSystem.gintPC2PLCTotalWord_3];
            for (int intLength = 0; intLength < intarPC2PLCResultData_PLC3.Length; intLength++)
                intarPC2PLCResultData_PLC3[intLength] = 0;

            intarPLC2PCResultData_PLC3 = new int[clsSystem.gintPLC2PCTotalWord_3];
            for (int intLength = 0; intLength < intarPLC2PCResultData_PLC3.Length; intLength++)
                intarPLC2PCResultData_PLC3[intLength] = 0;
            #endregion

        }
        #endregion 建構函式

        #region 公用方法
        public void funSetAlarmCode(DataTable AlarmCode)
        {
            Parallel.For(0, AlarmCode.Rows.Count, intRow =>
            {
                try
                {
                    int intBufferIndex = int.Parse(AlarmCode.Rows[intRow]["BufferIndex"].ToString());
                    int intAlarmIndex = int.Parse(AlarmCode.Rows[intRow]["AlarmIndex"].ToString());

                    if (AlarmCode.Rows[intRow]["BufferName"].ToString() != "System")
                    {
                        objarPLC2PCBuffer[intBufferIndex].AlarmSignal[intAlarmIndex].BufferName = AlarmCode.Rows[intRow]["BufferName"].ToString();
                        objarPLC2PCBuffer[intBufferIndex].AlarmSignal[intAlarmIndex].AlarmDesc = AlarmCode.Rows[intRow]["AlarmDesc"].ToString();
                        objarPLC2PCBuffer[intBufferIndex].AlarmSignal[intAlarmIndex].AlarmIndex = intAlarmIndex;
                        objarPLC2PCBuffer[intBufferIndex].AlarmSignal[intAlarmIndex].BufferIndex = intBufferIndex;
                        objarPLC2PCBuffer[intBufferIndex].AlarmSignal[intAlarmIndex].Signal = false;
                    }
                    else
                    {
                        objarSystemErrorCode[intAlarmIndex].BufferName = AlarmCode.Rows[intRow]["BufferName"].ToString();
                        objarSystemErrorCode[intAlarmIndex].AlarmDesc = AlarmCode.Rows[intRow]["AlarmDesc"].ToString();
                        objarSystemErrorCode[intAlarmIndex].AlarmIndex = intAlarmIndex;
                        objarSystemErrorCode[intAlarmIndex].BufferIndex = intBufferIndex;
                        objarSystemErrorCode[intAlarmIndex].Signal = false;
                    }
                }
                catch (Exception ex)
                {
                    var varObject = MethodBase.GetCurrentMethod();
                    clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
                }
            });
        }

        /// <summary>
        /// 取得PLC->PC資料並暫存於Buffer
        /// </summary>
        /// <returns>若讀取成功傳回True，否則傳回false</returns>
        public bool funReadPLC2PCDataBuffer(int intPLC)
        {
            bool bolPLC = false;
            int intBuffer_PLC = 0;
            int intBufferTotal = clsSystem.gintBufferQty_1 + clsSystem.gintStnQty;
            int[] intarPLC2PCResultData = new int[0];
            switch (intPLC)
            {
                case 1:
                    bolPLC = clsSystem.gobjPLC.funReadPLC(clsSystem.gstrPLC2PCWordAddressStart_1, clsSystem.gintPLC2PCTotalWord_1, ref intarPLC2PCResultData_PLC1, "1");
                    intarPLC2PCResultData = intarPLC2PCResultData_PLC1;
                    intBuffer_PLC = 0;//PLC Buffer 起始點
                    //intBufferTotal = 24;//PLC Buffer 結束點
                    intBufferTotal = 44;//PLC Buffer 結束點 By Leon
                    break;
                case 2:
                    bolPLC = clsSystem.gobjPLC2.funReadPLC(clsSystem.gstrPLC2PCWordAddressStart_1, clsSystem.gintPLC2PCTotalWord_2, ref intarPLC2PCResultData_PLC2, "2");
                    intarPLC2PCResultData = intarPLC2PCResultData_PLC2;
                    intBufferTotal = 60;
                    intBuffer_PLC = 24;
                    break;
                case 3:
                    bolPLC = clsSystem.gobjPLC3.funReadPLC(clsSystem.gstrPLC2PCWordAddressStart_1, clsSystem.gintPLC2PCTotalWord_3, ref intarPLC2PCResultData_PLC3, "3");
                    intarPLC2PCResultData = intarPLC2PCResultData_PLC3;
                    intBufferTotal = 84;
                    intBuffer_PLC = 60;
                    break;
            }
            if (bolPLC)
            {
                int intResultData = 0;

                try
                {
                    bolHandShaking = intarPLC2PCResultData[0] == 0 ? false : true;
                    string strErrorCode = Convert.ToString(intarPLC2PCResultData[9], 2).PadLeft(16, '0');
                    strErrorCode += Convert.ToString(intarPLC2PCResultData[8], 2).PadLeft(16, '0');
                    for (int intIndex = 0; intIndex < strErrorCode.Length; intIndex++)
                    {
                        int intLength = strErrorCode.Length - intIndex - 1;
                        if (intIndex >= 0)
                        {
                            objarSystemErrorCode[intIndex].LastSignal = objarSystemErrorCode[intIndex].Signal;
                            objarSystemErrorCode[intIndex].Signal = strErrorCode[intLength] == '1' ? true : false;
                            if (objarSystemErrorCode[intIndex].LastSignal != objarSystemErrorCode[intIndex].Signal && AlarmData != null)
                            {
                                AlarmData(this, new clsAlarmEventArgs(objarSystemErrorCode[intIndex].AlarmDesc, !objarSystemErrorCode[intIndex].Signal, 0, 0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var varObject = MethodBase.GetCurrentMethod();
                    clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, "Buffer(System) =>" + ex.Message);
                }

                intResultData = clsSystem.gintPLC2PCBufferTotalWord_1;


                for (int intBuffer = intBuffer_PLC; intBuffer < intBufferTotal; intBuffer++)
                {
                    try
                    {
                        int intBufferLoc = 0;
                        if (intPLC != 1)
                            intBufferLoc = intBuffer - intBuffer_PLC;
                        else
                            intBufferLoc = intBuffer;

                        //越南遠紡--------------------------------------------------------------------------------------------------------------
                        //
                        objarPLC2PCBuffer[intBuffer].LeftCmdSno =
                            intarPLC2PCResultData[intResultData * (intBufferLoc + 1)] == 0 ?
                            string.Empty : intarPLC2PCResultData[intResultData * (intBufferLoc + 1)].ToString().PadLeft(5, '0');//序號
                        objarPLC2PCBuffer[intBuffer].StnMode = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 1];//模式
                        string strTmp = Convert.ToString(intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 2], 2).PadLeft(16, '0');

                        objarPLC2PCBuffer[intBuffer].StnModeCode_None = strTmp.Substring(15, 1) == "1" ? true : false;
                        objarPLC2PCBuffer[intBuffer].StnModeCode_In = strTmp.Substring(14, 1) == "1" ? true : false;
                        objarPLC2PCBuffer[intBuffer].StnModeCode_Out = strTmp.Substring(13, 1) == "1" ? true : false;
                        objarPLC2PCBuffer[intBuffer].StnModeCode_Error = strTmp.Substring(11, 1) == "1" ? true : false;
                        objarPLC2PCBuffer[intBuffer].StnModeCode_Auto = strTmp.Substring(10, 1) == "1" ? true : false;
                        objarPLC2PCBuffer[intBuffer].StnModeCode_Manual = strTmp.Substring(9, 1) == "1" ? true : false;
                        objarPLC2PCBuffer[intBuffer].StnModeCode_PalletLoad = strTmp.Substring(8, 1) == "1" ? true : false;
                        objarPLC2PCBuffer[intBuffer].StnModeCode_CargoLoad = strTmp.Substring(7, 1) == "1" ? true : false;
                        objarPLC2PCBuffer[intBuffer].StnModeCode_Position = strTmp.Substring(6, 1) == "1" ? true : false;
                        objarPLC2PCBuffer[intBuffer].StnModeCode_Finish = strTmp.Substring(5, 1) == "1" ? true : false;
                        objarPLC2PCBuffer[intBuffer].StnModeCode_Stop = strTmp.Substring(4, 1) == "1" ? true : false;

                        objarPLC2PCBuffer[intBuffer].Ready = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 3];
                        objarPLC2PCBuffer[intBuffer].ReadNotice = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 4];
                        //objarPLC2PCBuffer[intBuffer].FunNotice = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 5];
                        objarPLC2PCBuffer[intBuffer].PathNotice = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 5];
                        objarPLC2PCBuffer[intBuffer].FunNotice = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 6];

                        objarPLC2PCBuffer[intBuffer].OverSize = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 7];

                        objarPLC2PCBuffer[intBuffer].Error = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 8];//異常碼 1
                        if (objarPLC2PCBuffer[intBuffer].Error > 0)
                        {
                            //代表有異常 紀錄Log並寫在Trace
                            //funAlarmLog(intBuffer, objarPLC2PCBuffer[intBuffer].Error);

                            //funAlarmLog(intBuffer, objarPLC2PCBuffer[intBuffer].Error);

                            // Error 1 寫入 Alarm Log
                            wcsAlarmData(this, new clsAlarmEventArgs(funAlarmLog(intBuffer, objarPLC2PCBuffer[intBuffer].Error,1), false, intBuffer, objarPLC2PCBuffer[intBuffer].Error));
                            
                            //Update MVS_MST
                            
                        }
                        objarPLC2PCBuffer[intBuffer].Error_2 = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 9];//異常碼 2
                        if (objarPLC2PCBuffer[intBuffer].Error_2 > 0)
                        {
                            //代表有異常 紀錄Log並寫在Trace
                            //funAlarmLog(intBuffer, objarPLC2PCBuffer[intBuffer].Error);

                            //funAlarmLog(intBuffer, objarPLC2PCBuffer[intBuffer].Error);
                            

                            // Error 2 寫入 Alarm Log
                            wcsAlarmData(this, new clsAlarmEventArgs(funAlarmLog(intBuffer, objarPLC2PCBuffer[intBuffer].Error_2,2), false, intBuffer, objarPLC2PCBuffer[intBuffer].Error_2));
                            
                            
                            //Update MVS_MST

                        }
                     

                        //objarPLC2PCBuffer[intBuffer].StnChange = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 7];
                        //objarPLC2PCBuffer[intBuffer].Avail = intarPLC2PCResultData[intResultData * (intBufferLoc + 1) + 19];

                        //--------------------------------------------------------------------------------------------------------------------
                        #region 矽品---已註解
                        //objarPLC2PCBuffer[intBuffer].LeftCmdSno =
                        //                    intarPLC2PCResultData[intResultData * (intBuffer + 1)] == 0 ?
                        //                    string.Empty : intarPLC2PCResultData[intResultData * (intBuffer + 1)].ToString().PadLeft(5, '0');

                        //objarPLC2PCBuffer[intBuffer].RightCmdSno =
                        //    intarPLC2PCResultData[intResultData * (intBuffer + 1) + 1] == 0 ?
                        //    string.Empty : intarPLC2PCResultData[intResultData * (intBuffer + 1) + 1].ToString().PadLeft(5, '0');
                        //objarPLC2PCBuffer[intBuffer].CmdMode = intarPLC2PCResultData[intResultData * (intBuffer + 1) + 2];
                        //objarPLC2PCBuffer[intBuffer].StnMode = intarPLC2PCResultData[intResultData * (intBuffer + 1) + 3];

                        ////string strTmp = Convert.ToString(intarPLC2PCResultData[intResultData * (intBuffer + 1) + 4], 2).PadLeft(16, '0');
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_None = strTmp.Substring(15, 1) == "1" ? true : false;
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_In = strTmp.Substring(14, 1) == "1" ? true : false;
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_Out = strTmp.Substring(13, 1) == "1" ? true : false;
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_Auto = strTmp.Substring(10, 1) == "1" ? true : false;
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_Manual = strTmp.Substring(9, 1) == "1" ? true : false;
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_Load = strTmp.Substring(8, 1) == "1" ? true : false;
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_Position = strTmp.Substring(7, 1) == "1" ? true : false;
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_Finish = strTmp.Substring(6, 1) == "1" ? true : false;
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_Stop = strTmp.Substring(5, 1) == "1" ? true : false;
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_LeftLoad = strTmp.Substring(3, 1) == "1" ? true : false;
                        //objarPLC2PCBuffer[intBuffer].StnModeCode_RightLoad = strTmp.Substring(2, 1) == "1" ? true : false;

                        //if (strTmp.Substring(1, 1) == "1")
                        //    objarPLC2PCBuffer[intBuffer].StnModeCode_RightLoad = true;

                        //objarPLC2PCBuffer[intBuffer].Ready = intarPLC2PCResultData[intResultData * (intBuffer + 1) + 5];
                        //objarPLC2PCBuffer[intBuffer].ReadNotice = intarPLC2PCResultData[intResultData * (intBuffer + 1) + 6];
                        //objarPLC2PCBuffer[intBuffer].PathNotice = intarPLC2PCResultData[intResultData * (intBuffer + 1) + 7];

                        //string strErrorCode = Convert.ToString(intarPLC2PCResultData[intResultData * (intBuffer + 1) + 9], 2).PadLeft(16, '0');
                        //strErrorCode += Convert.ToString(intarPLC2PCResultData[intResultData * (intBuffer + 1) + 8], 2).PadLeft(16, '0');

                        //bool bolFalg = false;
                        //for (int intIndex = 0; intIndex < strErrorCode.Length; intIndex++)
                        //{
                        //    int intLength = strErrorCode.Length - intIndex - 1;
                        //    objarPLC2PCBuffer[intBuffer].AlarmSignal[intIndex].LastSignal = objarPLC2PCBuffer[intBuffer].AlarmSignal[intIndex].Signal;
                        //    objarPLC2PCBuffer[intBuffer].AlarmSignal[intIndex].Signal = strErrorCode[intLength] == '1' ? true : false;
                        //    if (objarPLC2PCBuffer[intBuffer].AlarmSignal[intIndex].Signal)
                        //        bolFalg = true;
                        //    if (objarPLC2PCBuffer[intBuffer].AlarmSignal[intIndex].LastSignal != objarPLC2PCBuffer[intBuffer].AlarmSignal[intIndex].Signal &&
                        //        AlarmData != null)
                        //    {
                        //        AlarmData(this, new clsAlarmEventArgs(objarPLC2PCBuffer[intBuffer].AlarmSignal[intIndex].AlarmDesc,
                        //                                              !objarPLC2PCBuffer[intBuffer].AlarmSignal[intIndex].Signal));
                        //    }
                        //}
                        //objarPLC2PCBuffer[intBuffer].Error = bolFalg;
                        //objarPLC2PCBuffer[intBuffer].FunNotice = intarPLC2PCResultData[intResultData * (intBuffer + 1) + 10];
                        #endregion





                    }
                    catch (Exception ex)
                    {
                        var varObject = MethodBase.GetCurrentMethod();
                        clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, "Buffer(" + intBuffer + ") =>" + ex.Message);
                    }
                }
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 取得PC->PLC資料並暫存於Buffer
        /// </summary>
        /// <returns>若讀取成功傳回True，否則傳回false</returns>
        public bool funReadPC2PLCData2Buffer(int intPLC)
        {
            bool bolre = false;
            bool bolReadPLC = false;
            //switch (intPLC)
            //{
            //    case 1:
            //        bolReadPLC = clsSystem.gobjPLC.funReadPLC(clsSystem.gstrPC2PLCWordAddressStart_1, clsSystem.gintPC2PLCTotalWord_1, ref intarPC2PLCResultData_PLC1);
            //        break;
            //    case 2:
            //        break;
            //    case 3:
            //        break;
            //}

            switch (intPLC)
            {
                case 1:
                    if (clsSystem.gobjPLC.funReadPLC(clsSystem.gstrPC2PLCWordAddressStart_1, clsSystem.gintPC2PLCTotalWord_1, ref intarPC2PLCResultData_PLC1, "1"))
                    {
                        int intResultData = 0;
                        intResultData = clsSystem.gintPC2PLCBufferTotalWord_1;

                        for (int intBuffer = 0; intBuffer < 44; intBuffer++)
                        {
                            try
                            {
                                objarPC2PLCBuffer[intBuffer].CmdSno =
                                    intarPC2PLCResultData_PLC1[intResultData * (intBuffer + 1)] == 0 ?
                                    string.Empty : intarPC2PLCResultData_PLC1[intResultData * (intBuffer + 1)].ToString().PadLeft(5, '0');
                                //越南遠紡--------------------------------------------------------------------------------------------------
                                //--------------------------------------------------------------------------------------------------------
                                objarPC2PLCBuffer[intBuffer].StnMode = intarPC2PLCResultData_PLC1[intResultData * (intBuffer + 1) + 1];
                                //intarPC2PLCResultData[intResultData * (intBuffer + 1) + 1] == 0 ?
                                //string.Empty : intarPC2PLCResultData[intResultData * (intBuffer + 1) + 1].ToString().PadLeft(5, '0');     
                                objarPC2PLCBuffer[intBuffer].FunNotice_1 = intarPC2PLCResultData_PLC1[intResultData * (intBuffer + 1) + 4].ToString();
                                objarPC2PLCBuffer[intBuffer].PathNotice = intarPC2PLCResultData_PLC1[intResultData * (intBuffer + 1) + 5];
                                objarPC2PLCBuffer[intBuffer].IniNotice = intarPC2PLCResultData_PLC1[intResultData * (intBuffer + 1) + 6];
                                //objarPC2PLCBuffer[intBuffer].FunNotice_2 = intarPC2PLCResultData_PLC1[intResultData * (intBuffer + 1) + 4].ToString();
                                //objarPC2PLCBuffer[intBuffer].FunNotice_3 = intarPC2PLCResultData_PLC1[intResultData * (intBuffer + 1) + 5].ToString();
                                //objarPC2PLCBuffer[intBuffer].PathNotice = intarPC2PLCResultData[intResultData * (intBuffer + 1) + 6];
                            }
                            catch (Exception ex)
                            {
                                var varObject = MethodBase.GetCurrentMethod();
                                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, "Buffer(" + intBuffer + ") =>" + ex.Message);
                            }
                        }
                        bolre = true;
                    }
                    else
                        bolre = false;
                    break;
                case 2:
                    if (clsSystem.gobjPLC2.funReadPLC(clsSystem.gstrPC2PLCWordAddressStart_1, clsSystem.gintPC2PLCTotalWord_2, ref intarPC2PLCResultData_PLC2, "2"))
                    {
                        int intResultData = 0;
                        intResultData = clsSystem.gintPC2PLCBufferTotalWord_1;
                        int iPLC2 = 0;
                        for (int intBuffer = 24; intBuffer < 60; intBuffer++)
                        {

                            try
                            {
                                objarPC2PLCBuffer[intBuffer].CmdSno =
                                    intarPC2PLCResultData_PLC2[intResultData * (iPLC2 + 1)] == 0 ?
                                    string.Empty : intarPC2PLCResultData_PLC2[intResultData * (iPLC2 + 1)].ToString().PadLeft(5, '0');
                                //大立光--------------------------------------------------------------------------------------------------
                                //--------------------------------------------------------------------------------------------------------
                                objarPC2PLCBuffer[intBuffer].StnMode = intarPC2PLCResultData_PLC2[intResultData * (iPLC2 + 1) + 1];
                                //intarPC2PLCResultData[intResultData * (intBuffer + 1) + 1] == 0 ?
                                //string.Empty : intarPC2PLCResultData[intResultData * (intBuffer + 1) + 1].ToString().PadLeft(5, '0');
                                objarPC2PLCBuffer[intBuffer].IniNotice = intarPC2PLCResultData_PLC2[intResultData * (iPLC2 + 1) + 2];
                                objarPC2PLCBuffer[intBuffer].FunNotice_1 = intarPC2PLCResultData_PLC2[intResultData * (iPLC2 + 1) + 3].ToString();
                                objarPC2PLCBuffer[intBuffer].FunNotice_2 = intarPC2PLCResultData_PLC2[intResultData * (iPLC2 + 1) + 4].ToString();
                                objarPC2PLCBuffer[intBuffer].FunNotice_3 = intarPC2PLCResultData_PLC2[intResultData * (iPLC2 + 1) + 5].ToString();
                                //objarPC2PLCBuffer[intBuffer].PathNotice = intarPC2PLCResultData[intResultData * (intBuffer + 1) + 6];
                            }
                            catch (Exception ex)
                            {
                                var varObject = MethodBase.GetCurrentMethod();
                                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, "Buffer(" + intBuffer + ") =>" + ex.Message);
                            }
                            iPLC2++;
                        }
                        bolre = true;
                    }
                    else
                        bolre = false;
                    break;

                case 3:
                    if (clsSystem.gobjPLC3.funReadPLC(clsSystem.gstrPC2PLCWordAddressStart_1, clsSystem.gintPC2PLCTotalWord_3, ref intarPC2PLCResultData_PLC3, "3"))
                    {
                        int intResultData = 0;
                        int iPLC3 = 0;
                        intResultData = clsSystem.gintPC2PLCBufferTotalWord_1;

                        for (int intBuffer = 60; intBuffer < 84; intBuffer++)
                        {
                            try
                            {
                                objarPC2PLCBuffer[intBuffer].CmdSno =
                                    intarPC2PLCResultData_PLC3[intResultData * (iPLC3 + 1)] == 0 ?
                                    string.Empty : intarPC2PLCResultData_PLC3[intResultData * (iPLC3 + 1)].ToString().PadLeft(5, '0');
                                //大立光--------------------------------------------------------------------------------------------------
                                //--------------------------------------------------------------------------------------------------------
                                objarPC2PLCBuffer[intBuffer].StnMode = intarPC2PLCResultData_PLC3[intResultData * (iPLC3 + 1) + 1];
                                //intarPC2PLCResultData[intResultData * (intBuffer + 1) + 1] == 0 ?
                                //string.Empty : intarPC2PLCResultData[intResultData * (intBuffer + 1) + 1].ToString().PadLeft(5, '0');
                                objarPC2PLCBuffer[intBuffer].IniNotice = intarPC2PLCResultData_PLC3[intResultData * (iPLC3 + 1) + 2];
                                objarPC2PLCBuffer[intBuffer].FunNotice_1 = intarPC2PLCResultData_PLC3[intResultData * (iPLC3 + 1) + 3].ToString();
                                objarPC2PLCBuffer[intBuffer].FunNotice_2 = intarPC2PLCResultData_PLC3[intResultData * (iPLC3 + 1) + 4].ToString();
                                objarPC2PLCBuffer[intBuffer].FunNotice_3 = intarPC2PLCResultData_PLC3[intResultData * (iPLC3 + 1) + 5].ToString();
                                //objarPC2PLCBuffer[intBuffer].PathNotice = intarPC2PLCResultData[intResultData * (intBuffer + 1) + 6];
                            }
                            catch (Exception ex)
                            {
                                var varObject = MethodBase.GetCurrentMethod();
                                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, "Buffer(" + intBuffer + ") =>" + ex.Message);
                            }
                            iPLC3++;
                        }
                        bolre = true;
                    }
                    else
                        bolre = false;
                    break;

            }
            return bolre;
        }

        //iErrorFlag 用來區分異常碼1 OR 2
        public string funAlarmLog(int iBufferIndex, int iAlarmCode, int iErrorFlag)
        {
            string strAlarmMsg = string.Empty;
            string strSQL = string.Empty;
            string strEM = string.Empty;

            try
            {
                string strAlarmCode = Convert.ToString(iAlarmCode, 2);
                char[] charAlarm = strAlarmCode.ToCharArray();
                int iAlarmIndex = charAlarm.Length;
                for (int i = 0; i < charAlarm.Length; i++)
                {

                    if (charAlarm[i].ToString() == "1")
                    {
                        if (iErrorFlag == 2)
                            iAlarmIndex += 16;
                        strAlarmMsg += objarPLC2PCBuffer[iBufferIndex].AlarmSignal[iAlarmIndex].AlarmDesc;

                    }
                    if (iErrorFlag == 2)
                        iAlarmIndex -= 16;
                    iAlarmIndex--;
                }
                
                return strAlarmMsg;
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
            }
        }
        #endregion 公用方法

    }
}
