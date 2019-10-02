using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mirle.Library;
using System.Reflection;
using System.Data;
using Mirle;

namespace DT_WAP_SC
{
    public partial class FormNewLook : Form
    {
        /// <summary>
        /// 更新 CraneMode
        /// </summary>
        /// <param name="CraneMode"></param>
        /// <param name="lblCraneMode"></param>
        private void funShowCraneMode(string CraneMode, ref Label lblCraneMode)
        {
            switch(CraneMode)
            {
                case "C":
                    lblCraneMode.Text = "C:電腦模式";
                    lblCraneMode.BackColor = Color.Lime;
                    break;
                case "R":
                    lblCraneMode.Text = "R:地上盤模式";
                    lblCraneMode.BackColor = Color.Red;
                    break;
                case "M":
                    lblCraneMode.Text = "M:維護模式";
                    lblCraneMode.BackColor = Color.Red;
                    break;
                case "I":
                    lblCraneMode.Text = "I:維護模式";
                    lblCraneMode.BackColor = Color.Red;
                    break;
                case "N":
                    lblCraneMode.Text = "N:未開啟";
                    lblCraneMode.BackColor = Color.Red;
                    break;
                case "X":
                default:
                    lblCraneMode.Text = "X:未連線";
                    lblCraneMode.BackColor = Color.Red;
                    break;
            }
        }

        /// <summary>
        /// 更新 CraneSts
        /// </summary>
        /// <param name="CraneSts"></param>
        /// <param name="lblCraneSts"></param>
        private void funShowCraneSts(string CraneSts, ref Label lblCraneSts)
        {
            switch(CraneSts)
            {
                case "W":
                    lblCraneSts.Text = "W:待機中";
                    lblCraneSts.BackColor = Color.Lime;
                    break;
                case "A":
                    lblCraneSts.Text = "A:動作中";
                    lblCraneSts.BackColor = Color.Lime;
                    break;
                case "E":
                    lblCraneSts.Text = "E:異常中";
                    lblCraneSts.BackColor = Color.Red;
                    break;
                case "I":
                    lblCraneSts.Text = "I:檢查中";
                    lblCraneSts.BackColor = Color.Lime;
                    break;
                case "N":
                    lblCraneSts.Text = "N:未開啟";
                    lblCraneSts.BackColor = Color.Red;
                    break;
                case "X":
                default:
                    lblCraneSts.Text = "X:未連線";
                    lblCraneSts.BackColor = Color.Red;
                    break;
            }
        }

        /// <summary>
        /// 更新 PLC 或 DB 連線狀態
        /// </summary>
        /// <param name="Connect"></param>
        /// <param name="lblConnect"></param>
        private void funShowConnect(bool Connect, ref Label lblConnect)
        {
            bool bolLastConnect = lblConnect.Text == "已連線";
            bool bolFlag = lblConnect.Text == "None";

            clsTraceLogEventArgs SystemTraceLog = new clsTraceLogEventArgs(enuTraceLog.System);
            switch(Connect)
            {
                case true:
                    lblConnect.Text = "已連線";
                    lblConnect.BackColor = Color.Lime;

                    //if(!bolLastConnect || bolFlag)
                    //{
                    //    SystemTraceLog.LogMessage =
                    //        lblConnect.Name.Contains("DB") ? "Connection DB Success!" : "Connection " + lblConnect.Name.Substring(3, 5) + " Success!";
                    //    funShowSystemTrace(lsbSystemTrace, SystemTraceLog, true);
                    //}
                    break;
                case false:
                default:
                    lblConnect.Text = "未連線";
                    lblConnect.BackColor = Color.Red;

                    //if(bolLastConnect || bolFlag)
                    //{
                    //    SystemTraceLog.LogMessage =
                    //        lblConnect.Name.Contains("DB") ? "Connection DB Fail!" : "Connection " + lblConnect.Name.Substring(3, 5) + " Fail!";
                    //    funShowSystemTrace(lsbSystemTrace, SystemTraceLog, true);
                    //}
                    break;
            }
        }

        /// <summary>
        /// 取得 Crane Mode 並更新
        /// </summary>
        private void funReadCraneMode()
        {
            string strSQL = "SELECT * FROM EQUMODELOG WHERE ENDDT IN ('',' ')";
            string strEM = string.Empty;
            DataTable objDataTable = new DataTable();

            try
            {
                if(clsSystem.gobjDB.funGetDT(strSQL, ref objDataTable, ref strEM) == ErrDef.ProcSuccess)
                {
                    for(int intCount = 0; intCount < objDataTable.Rows.Count; intCount++)
                    {
                        if(tlpCraneSts.Controls.ContainsKey("lblCrane" + objDataTable.Rows[intCount]["EQUNO"].ToString() + "Mode"))
                        {
                            Label objLabel = (Label)tlpCraneSts.Controls["lblCrane" + objDataTable.Rows[intCount]["EQUNO"].ToString() + "Mode"];
                            string strCurrentCraneMode = objDataTable.Rows[intCount]["EQUMODE"].ToString();
                            string strLastCraneMode = objLabel.Text.Substring(0, 1);
                            string strCraneNo = objDataTable.Rows[intCount]["EQUNO"].ToString();

                            if(strLastCraneMode != strCurrentCraneMode)
                            {
                                funShowCraneMode(strCurrentCraneMode, ref objLabel);
                                clsTraceLogEventArgs SystemTraceLog = new clsTraceLogEventArgs(enuTraceLog.System);
                                SystemTraceLog.LogMessage = "Crane " + strCraneNo + " Mode Change";
                                SystemTraceLog.CraneNo = strCraneNo;
                                SystemTraceLog.CraneMode = strCurrentCraneMode;
                                SystemTraceLog.CraneModeLast = strLastCraneMode;
                                funShowSystemTrace(lsbSystemTrace, SystemTraceLog, true);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
            }
            finally
            {
                if(objDataTable != null)
                {
                    objDataTable.Clear();
                    objDataTable.Dispose();
                    objDataTable = null;
                }
            }
        }

        /// <summary>
        /// 取得 Crane Sts 並更新
        /// </summary>
        private void funReadCraneSts()
        {
            string strSQL = "SELECT * FROM EQUSTSLOG WHERE ENDDT = ' '";
            string strEM = string.Empty;
            DataTable objDataTable = new DataTable();

            try
            {
                if(clsSystem.gobjDB.funGetDT(strSQL, ref objDataTable, ref strEM) == ErrDef.ProcSuccess)
                {
                    for(int intCount = 0; intCount < objDataTable.Rows.Count; intCount++)
                    {
                        if(tlpCraneSts.Controls.ContainsKey("lblCrane" + objDataTable.Rows[intCount]["EQUNO"].ToString() + "Sts"))
                        {
                            clsTraceLogEventArgs SystemTraceLog = new clsTraceLogEventArgs(enuTraceLog.System);
                            Label objLabel = (Label)tlpCraneSts.Controls["lblCrane" + objDataTable.Rows[intCount]["EQUNO"].ToString() + "Sts"];
                            string strCurrentCraneSts = objDataTable.Rows[intCount]["EQUSTS"].ToString();
                            string strLastCraneSts = objLabel.Text.Substring(0, 1);
                            string strCraneNo = objDataTable.Rows[intCount]["EQUNO"].ToString();


                            SystemTraceLog.LogMessage = "intCount " + intCount;
                            //clsSystem.funWriteExceptionLog("Crane_Mode_sts:  ", "funReadCraneSts      ", SystemTraceLog.LogMessage);

                            SystemTraceLog.LogMessage = "strCurrentCraneSts " + objDataTable.Rows[intCount]["EQUSTS"].ToString();
                            //clsSystem.funWriteExceptionLog("Crane_Mode_sts:  ", "funReadCraneSts      ", SystemTraceLog.LogMessage);

                            SystemTraceLog.LogMessage = "objLabel " + objDataTable.Rows[intCount]["EQUNO"].ToString();
                            //clsSystem.funWriteExceptionLog("Crane_Mode_sts:  ", "funReadCraneSts      ", SystemTraceLog.LogMessage);

                            SystemTraceLog.LogMessage = "strCurrentCraneSts " + objDataTable.Rows[intCount]["EQUSTS"].ToString();
                            //clsSystem.funWriteExceptionLog("Crane_Mode_sts:  ", "funReadCraneSts      ", SystemTraceLog.LogMessage);

                            SystemTraceLog.LogMessage = "strLastCraneSts " + objLabel.Text.Substring(0, 1);
                            //clsSystem.funWriteExceptionLog("Crane_Mode_sts:  ", "funReadCraneSts      ", SystemTraceLog.LogMessage);

                            SystemTraceLog.LogMessage = "strCraneNo " + objDataTable.Rows[intCount]["EQUNO"].ToString();
                            //clsSystem.funWriteExceptionLog("Crane_Mode_sts:  ", "funReadCraneSts      ", SystemTraceLog.LogMessage);


                            if (strLastCraneSts != strCurrentCraneSts)
                            {
                                funShowCraneSts(strCurrentCraneSts, ref objLabel);
                                SystemTraceLog.LogMessage = "Crane " + strCraneNo + " Sts Change";
                                SystemTraceLog.CraneNo = strCraneNo;
                                SystemTraceLog.CraneSts = strCurrentCraneSts;
                                SystemTraceLog.CraneStsLast = strLastCraneSts;
                                funShowSystemTrace(lsbSystemTrace, SystemTraceLog, true);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
            }
            finally
            {
                if(objDataTable != null)
                {
                    objDataTable.Clear();
                    objDataTable.Dispose();
                    objDataTable = null;
                }
            }
        }
    }
}
