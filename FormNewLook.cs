using CmdCode;
using HslCommunication.Controls;
using ICommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using mirle.stone.utility;
using System.IO;
using Vcs;
using System.Runtime.InteropServices;
using Mirle.Library;
using System.Reflection;


namespace DT_WAP_SC
{
    public partial class FormNewLook : Form
    {
        #region Leon Add
        private delegate void delShowSystemTrace(ListBox TraceListBox, clsTraceLogEventArgs TraceLog, bool WriteLog);
        private clsTraceLogEventArgs strLastSystemTraceLog = new clsTraceLogEventArgs(enuTraceLog.System);
        private clsTraceLogEventArgs strLastMPLCTraceLog = new clsTraceLogEventArgs(enuTraceLog.MPLC);
        #endregion Leon Add

        private string _sc_ip = string.Empty;
        private string _sc_port = string.Empty;
        private string _bytesize = string.Empty;
        private string _heartbeat = string.Empty;
        private string _eqpid = string.Empty;
        private int _batteryOK = 0;
        private int _batteryLow = 0;
        private bool connect = false;
        private int _terminaldisplaytime = 0;
        private bool msghandel = false;
        private bool cmdcomplete = true;
        private readonly object obj = new object();
        private readonly object eqobj = new object();
        private readonly TcpConnect tcp = new TcpConnect();
        private TcpConnect VcsTcp = new TcpConnect();
        private MsgHandler MsgH = new MsgHandler();
        private Equipment EQP = new Equipment();
        private VcsHandler vcs = new VcsHandler();
        public delegate void RefreshInfo(ListBox lb, string str);
        public delegate void UpdateStatus(int i);
        public delegate void UpdateVcsStatus(int i);
        public delegate void UpdateEQP(Equipment eq);
        public delegate void StartTimer();
        public delegate void TerminalDisplay(string message);
        private Dictionary<string, string> cmd35ack = null;
        private Dictionary<string, string> cmd26ack = null;
        private bool charging = false;
        private int time_terminaldisplay = 0;
        Thread get;
        Thread Ta;
        Thread TaM;
        Thread bankIn;
        Thread bankOut;
        Thread charge;
        Thread work;
        private enum E { waiting = 1, connected = 2, display = 3, disconnected = 4 };
        #region Leon Add
        private System.Windows.Forms.Timer timRefresh = new System.Windows.Forms.Timer();
        #endregion Leon Add
        public FormNewLook()
        {
            InitializeComponent();
        }

        private void FormNewLook_Load(object sender, EventArgs e)
        {
            try
            {
                #region Leon Add
                this.Text = "Mirle AS/RS System Communication" + " (V." + Application.ProductVersion + ")";
                //btnAutoPause.Text = "Pause";
                //bolAutoPaseFlag = true;
                funInitTimer();
                #endregion Leon Add

                LoadConfig();
                tcp.RefreshMessageInfo += new EventHandler<MsgEventArg>(RefreshMsgInfo);
                tcp.ReceiveMsg += new EventHandler<MsgEventArg>(RecevieMsg);
                tcp.UpdateConnect += new EventHandler<MsgEventArg>(Updateconnect);
                //VcsTcp.RefreshMessageInfo += new EventHandler<MsgEventArg>(RefreshMsgInfo);
                //VcsTcp.ReceiveMsg += new EventHandler<MsgEventArg>(RecevieVcsMsg);
                //VcsTcp.UpdateConnect += new EventHandler<MsgEventArg>(UpdateVcsconnect);
                MsgH.DoWork += new EventHandler<MsgEventArg>(Dowork);
                MsgH.MsgErrorReport += new EventHandler<MsgEventArg>(RefreshMsgInfo);
                MsgH.ErrorReport += new EventHandler<MsgEventArg>(RefreshMsgInfo);
                MsgH.CmdsAck += new EventHandler<MsgEventArg>(CmdAck);
                MsgH.SetData();
                EQP.UpdateEQP += new EventHandler<MsgEventArg>(RefreshEQPInfo);

                vcs.Run += new EventHandler<MsgEventArg>(Dowork);
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }

        private void LoadConfig()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                ConfigurationSectionGroup csg = config.GetSectionGroup("applicationSettings");
                ClientSettingsSection css = (ClientSettingsSection)csg.Sections["common.Settings"];
                foreach (SettingElement setting in (ConfigurationElementCollection)css.Settings)
                {
                    switch (setting.Name)
                    {
                        case "SC_ip":
                            _sc_ip = setting.Value.ValueXml.InnerText;
                            break;
                        case "SC_port":
                            _sc_port = setting.Value.ValueXml.InnerText;
                            break;
                        case "ReceiveBufferSize":
                            _bytesize = setting.Value.ValueXml.InnerText;
                            break;
                        case "HeartBeat":
                            _heartbeat = setting.Value.ValueXml.InnerText;
                            timer1.Interval = int.Parse(_heartbeat);
                            break;
                        case "EQPid":
                            _eqpid = setting.Value.ValueXml.InnerText;
                            Equipment.eqpID = _eqpid;
                            break;
                        case "BatteryOK":
                            _batteryOK = int.Parse(setting.Value.ValueXml.InnerText);
                            break;
                        case "BatteryLow":
                            _batteryLow = int.Parse(setting.Value.ValueXml.InnerText);
                            break;
                        case "TerminalDisplayTime":
                            _terminaldisplaytime = int.Parse(setting.Value.ValueXml.InnerText);
                            break;

                    }
                }
                lblScIP.Text = "IP : " + _sc_ip;
                lblSCport.Text = "Port : " + _sc_port;

            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }

        private void BtnSCStart_Click(object sender, EventArgs e)
        {
            try
            {
                tcp.IPaddress = IPAddress.Parse(_sc_ip);
                tcp.Port = int.Parse(_sc_port);
                tcp.Maxbytes = int.Parse(_bytesize);
                tcp.OpenServer(false);
                btnSCStart.Enabled = false;
                btnSCEnd.Enabled = true;
                connect = true;
                timer1.Enabled = true;
                EQPstart();

            }
            catch (Exception ex)
            {
                UpdateConnectS(Convert.ToInt32(E.disconnected));
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }

        private void BtnSCEnd_Click(object sender, EventArgs e)
        {
            try
            {
                tcp.CloseServer(false);
                timer1.Enabled = false;
                btnSCStart.Enabled = true;
                btnSCEnd.Enabled = false;
                cmdcomplete = true;
                connect = false;
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void EQPstart()
        {
            try
            {
                Dictionary<string, string> time = new Dictionary<string, string>();
                time.Add("SeqNo", Equipment.seqno.ToString().PadLeft(4, '0'));
                time.Add("ReSent", "0");
                time.Add("Counts", "001");
                MsgEventArg msg = new MsgEventArg();
                msg._value = time;
                msg._cmdCode = "01";
                int sum = MsgModel._commandType[((int)Enmu.CmdCode.EQP_START).ToString().PadLeft(2, '0')].Select(o => o.Value).Sum();
                byte[] reply = new byte[sum];
                Cmd01 cmd01 = new Cmd01();
                string[] sp = cmd01.GetData(msg, ref reply);
                SentData(sp, reply);
            }
            catch (Exception ex)
            {
                BtnSCEnd_Click(null, null);
                MsgEventArg ms = new MsgEventArg();
                ms._conn = Convert.ToInt32(E.disconnected);
                UpdateConnectS(ms._conn);
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void RecevieMsg(object sender, MsgEventArg e)
        {
            try
            {
                msghandel = true;
                StringBuilder recMsg = new StringBuilder();
                string asc = Encoding.ASCII.GetString(e._btdata);
                string[] sp = asc.Replace("\0", "").Split('\n');
                MsgH.Handle(EQP, sp, false, e._btdata);
                byte[] anbt = new byte[e._btdata.Count()];
                string[] strlib = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
                int s = 0;
                for (int i = 0; i < e._btdata.Count(); i++)
                {
                    string spl = e._btdata[i].ToString();
                    int st = Convert.ToInt32(spl, 10);
                    string sx = Convert.ToString((char)st);
                    if (strlib.Contains(spl) || st > 64)
                    {
                        anbt[s] = Convert.ToByte(spl);
                    }
                    else
                    {
                        anbt[s] = Convert.ToByte(sx);
                    }
                    s++;
                }
                Dictionary<string, int> iIdx = new Dictionary<string, int>();
                int _commandidx = 0;
                string cmdcode = anbt[5].ToString() + anbt[6].ToString();
                foreach (string ss in MsgModel._commandType[cmdcode].Keys)
                {
                    iIdx.Add(ss, _commandidx);
                    _commandidx += MsgModel._commandType[cmdcode][ss];
                }
                foreach (string item in MsgModel._commandType[cmdcode].Keys)
                {
                    string value = string.Empty;
                    if (iIdx.Keys.Contains(item))
                    {
                        int count = MsgModel._commandType[cmdcode][item];
                        for (int i = iIdx[item]; i < anbt.Count() && count > 0; i++)
                        {
                            if (anbt[i] > 64)
                                value += Convert.ToString((char)anbt[i]);
                            else
                                value += anbt[i].ToString();
                            count--;
                        }
                    }
                    //ivalues.Add(item, value);
                    recMsg.Append(item + ":" + value + "\n");
                }
                if ((e._btdata[5].ToString() + e._btdata[6].ToString()).Equals("5054") || (e._btdata[5].ToString() + e._btdata[6].ToString()).Equals("5148"))
                {
                    RefreshMessageInfo(lbxHb, "Receive[SC]-[" + cmdcode + "]\n" + recMsg.ToString());
                    Log.Debug("Receive[SC]-[" + cmdcode + "]\n" + recMsg.ToString());
                }
                else
                {
                    RefreshMessageInfo(lbxCmd, "Receive[SC]-[" + cmdcode + "]\n" + recMsg.ToString());
                    Log.Info("Receive[SC]-[" + cmdcode + "]\n" + recMsg.ToString());
                }

            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
            msghandel = false;
            Equipment.seqno++;
        }
        //private void RecevieVcsMsg(object sender, MsgEventArg e)
        //{
        //    try
        //    {
        //        StringBuilder recMsg = new StringBuilder();
        //        string asc = Encoding.ASCII.GetString(e._btdata);

        //        recMsg.Append(asc);
        //        string[] sp = asc.Replace("\0", "").Split('\n');

        //        vcs.HandelVcsMsg(e._btdata);

        //        if ((e._btdata[5].ToString() + e._btdata[6].ToString()).Equals("5054") || (e._btdata[5].ToString() + e._btdata[6].ToString()).Equals("5148"))
        //        {
        //            RefreshMessageInfo(lbxVcsHB, "Receive[SC]-" + recMsg.ToString());
        //            Log.Info("Receive[SC]-" + recMsg.ToString());
        //        }
        //        else
        //        {
        //            RefreshMessageInfo(lbxVcsMsg, "Receive[SC]-" + recMsg.ToString());
        //            Log.Debug("Receive[SC]-" + recMsg.ToString());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        RefreshMessageInfo(lbxVcsMsg, ex.Message + "-" + ex.StackTrace);
        //        Log.Error(null, ex.Message + "-" + ex.StackTrace);
        //    }
        //    msghandel = false;
        //    Equipment.seqno++;
        //}
        private void CmdAck(object sender, MsgEventArg e)
        {
            try
            {
                if (e._cmdCode.Equals("05"))
                {
                    cmd35ack = e._value;
                }
                else if(e._cmdCode.Equals("26"))
                {
                    cmd26ack = e._value;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }
        [DllImport("Kernel32.dll")]
        public static extern bool SetLocalTime(ref SystemTime sysTime);
        private void Dowork(object sender, MsgEventArg ms)
        {
            try
            {
                
                switch (ms._cmdCode)
                {
                    case "HC01"://Date Time Sync
                        DateTime dtSysDT = new DateTime();
                        if (DateTime.TryParse(ms._value["synctime"], out dtSysDT))
                        {
                            SystemTime systemTime = new SystemTime();
                            systemTime.wYear = (ushort)dtSysDT.Year;
                            systemTime.wMonth = (ushort)dtSysDT.Month;
                            systemTime.wDay = (ushort)dtSysDT.Day;
                            systemTime.wHour = (ushort)dtSysDT.Hour;
                            systemTime.wMinute = (ushort)dtSysDT.Minute;
                            systemTime.wSecond = (ushort)dtSysDT.Second;
                            SetLocalTime(ref systemTime);
                        }
                        break;
                    case "HC02"://Move
                        ER01 er01 = new ER01();
                        er01.eqpid = Equipment.eqpID;
                        er01.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                        er01.movestate = "Initialed";
                        vcs.ReportMove(er01);
                        work = new Thread(ActionMove);
                        work.IsBackground = true;
                        work.Start(ms._value);
                        
                        break;
                    case "HC03"://Transfer
                        ER02 er02 = new ER02();
                        er02.eqpid = Equipment.eqpID;
                        er02.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                        er02.transferstate = "Initialed";
                        vcs.ReportCarrierTransferState(er02);
                        work = new Thread(ActionTransfer);
                        work.IsBackground = true;
                        work.Start(ms._value);

                        break;
                    case "HC04"://Scan
                        break;
                    case "HC05"://Position
                        break;
                    case "HC06"://Terminal Display Msg
                        RefreshTerminalDisplay(ms._message);
                        break;
                    case "HC07"://Query EQP Status
                        break;
                    case "HC08"://Query All Alarm/Warning

                        break;
                    case "HC09"://Charge
                        work = new Thread(ActionCharge);
                        work.IsBackground = true;
                        work.Start(ms._value);

                        break;
                    case " ":
                        break;
                    default:
                        SentData(ms._reply, ms._btdata);
                        break;
                }
                
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }

        }
        private void SentData(string[] data, byte[] byt)
        {
            try
            {
                msghandel = true;
                byte[] _msg = Encoding.ASCII.GetBytes(string.Join("\n", data));
                //int p = 0;
                //foreach (string s in data)
                //{
                //    if (string.IsNullOrEmpty(s.Replace("\0", ""))) continue;
                //    string spl = s.Split(':')[1].Replace("\0", "");
                //    int st = Convert.ToInt32(spl, 16);//16->10
                //    string sx = Convert.ToString(st);//10->ASCII
                //    msg[p] = Convert.ToByte(sx);//ASCII->byte  Convert.ToString(Convert.ToInt32(s, 16))
                //    p++;
                //}
                _msg = byt;
                string asc = Encoding.ASCII.GetString(_msg);
                tcp.Client.GetStream().BeginWrite(_msg, 0, _msg.Length, HandleDatagramWritten, tcp.Client);

                StringBuilder str = new StringBuilder();
                //for (int i = 0; i < data.Count(); i++)
                //{
                //    string sa = String.Format("0x{0:X2}", Convert.ToInt32(data[i].ToString(), 10));//Convert.ToString(data[i], 16);
                //    str.Append(sa+"\n");
                //}
                //str.Append(Encoding.ASCII.GetString(data));
                str.Append(string.Join("\n", data));
                if ((data[5].ToString().Split(':')[1] + data[6].ToString().Split(':')[1]).Equals("0006") || (data[5].ToString().Split(':')[1] + data[6].ToString().Split(':')[1]).Equals("0100"))
                {
                    RefreshMessageInfo(lbxHb, "Sent[SC]-[" + int.Parse(data[5].Split(':')[1]).ToString() + int.Parse(data[6].Split(':')[1]).ToString() + "]\n" + str.ToString());
                    Log.Debug("Sent[SC]-[" + int.Parse(data[5].Split(':')[1]).ToString() + int.Parse(data[6].Split(':')[1]).ToString() + "]\n" + str.ToString());
                }
                else
                {
                    RefreshMessageInfo(lbxCmd, "Sent[SC]-[" + int.Parse(data[5].Split(':')[1]).ToString() + int.Parse(data[6].Split(':')[1]).ToString() + "]\n" + str.ToString());
                    Log.Info("Sent[SC]-[" + int.Parse(data[5].Split(':')[1]).ToString() + int.Parse(data[6].Split(':')[1]).ToString() + "]\n" + str.ToString());
                }

            }
            catch (SocketException ex)
            {
                BtnSCEnd_Click(null, null);
                MsgEventArg ms = new MsgEventArg();
                ms._conn = Convert.ToInt32(E.disconnected);
                UpdateConnectS(ms._conn);
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
            msghandel = false;
            Equipment.seqno++;
        }
        //private void SentVcsData(string[] data, byte[] byt)
        //{
        //    try
        //    {
        //        msghandel = true;
        //        byte[] _msg = Encoding.ASCII.GetBytes(string.Join("\n", data));
        //        _msg = byt;
        //        string asc = Encoding.ASCII.GetString(_msg);
        //        VcsTcp.Client.GetStream().BeginWrite(_msg, 0, _msg.Length, HandleDatagramWritten, tcp.Client);

        //        StringBuilder str = new StringBuilder();
        //        //for (int i = 0; i < data.Count(); i++)
        //        //{
        //        //    string sa = String.Format("0x{0:X2}", Convert.ToInt32(data[i].ToString(), 10));//Convert.ToString(data[i], 16);
        //        //    str.Append(sa+"\n");
        //        //}
        //        //str.Append(Encoding.ASCII.GetString(data));
        //        str.Append(string.Join("\n", data));
        //        if ((data[5].ToString().Split(':')[1] + data[6].ToString().Split(':')[1]).Equals("0006") || (data[5].ToString().Split(':')[1] + data[6].ToString().Split(':')[1]).Equals("0100"))
        //        {
        //            RefreshMessageInfo(lbxHb, "Sent[SC]-[" + int.Parse(data[5].Split(':')[1]).ToString() + int.Parse(data[6].Split(':')[1]).ToString() + "]\n" + str.ToString());
        //            Log.Debug("Sent[SC]-[" + int.Parse(data[5].Split(':')[1]).ToString() + int.Parse(data[6].Split(':')[1]).ToString() + "]\n" + str.ToString());
        //        }
        //        else
        //        {
        //            RefreshMessageInfo(lbxCmd, "Sent[SC]-[" + int.Parse(data[5].Split(':')[1]).ToString() + int.Parse(data[6].Split(':')[1]).ToString() + "]\n" + str.ToString());
        //            Log.Info("Sent[SC]-[" + int.Parse(data[5].Split(':')[1]).ToString() + int.Parse(data[6].Split(':')[1]).ToString() + "]\n" + str.ToString());
        //        }
        //    }
        //    catch (SocketException ex)
        //    {
        //        MsgEventArg ms = new MsgEventArg();
        //        ms._conn = Convert.ToInt32(E.disconnected);
        //        UpdateConnectS(ms._conn);
        //        RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
        //        Log.Error(null, ex.Message + "-" + ex.StackTrace);
        //    }
        //    msghandel = false;
        //}
        private void RefreshTerminalDisplay(string ms)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new TerminalDisplay(ShowTerminalDisplay), ms);
            }
            else
            {
                ShowTerminalDisplay(ms);
            }
        }

        private void ShowTerminalDisplay(string ms)
        {
            try
            {
                lblTerminalDisplay.Text = "Terminal Display : " + ms;
            }
            catch(Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }

        private void RefreshMsgInfo(object sender, MsgEventArg e)
        {
            RefreshMessageInfo(lbxMsg, e._message);
        }
        private void Updateconnect(object sender, MsgEventArg e)
        {
            UpdateConnectS(e._conn);
        }
        public void UpdateConnectS(int Ee)
        {//update connect eqpstatus
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new UpdateStatus(UpdateConnectS), Ee);
                }
                else
                {
                    UpdateConnectStatus(Ee);
                }
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void UpdateConnectStatus(int e)
        {
            try
            {
                switch (e)
                {
                    case (int)E.waiting:
                        userLanternSC.LanternBackground = Color.Orange;
                        lblSCconnect.Text = "Waiting";
                        RefreshMessageInfo(lbxMsg, _sc_ip + "- Waiting.");
                        //userLanternSC_vcs.LanternBackground = Color.Orange;
                        //lblVCS_SCconnect.Text = "Waiting";
                        break;
                    case (int)E.connected:
                        userLanternSC.LanternBackground = Color.Lime;
                        lblSCconnect.Text = "Connect";
                        connect = true;
                        RefreshMessageInfo(lbxMsg, _sc_ip + "- Connect.");
                        //userLanternSC_vcs.LanternBackground = Color.Lime;
                        //lblVCS_SCconnect.Text = "Connect";
                        break;
                    default:
                        btnSCEnd.Enabled = false;
                        btnSCStart.Enabled = true;
                        userLanternSC.LanternBackground = Color.Red;
                        lblSCconnect.Text = "DisConnect";
                        uvpBattery.Value = 0;
                        connect = false;
                        RefreshMessageInfo(lbxMsg, _sc_ip + "- DisConnect.");
                        //userLanternSC_vcs.LanternBackground = Color.Red;
                        //lblVCS_SCconnect.Text = "DisConnect";
                        //uvpSCBattery.Value = 0;
                        if (get != null && get.IsAlive) {  get.Abort(); }//get.Join();
                        if (Ta != null && Ta.IsAlive) {  Ta.Abort(); }//Ta.Join();
                        if (TaM != null && TaM.IsAlive) {  TaM.Abort(); }//TaM.Join();
                        if (bankIn != null && bankIn.IsAlive) {  bankIn.Abort(); }//bankIn.Join();
                        if (bankOut != null && bankOut.IsAlive) {  bankOut.Abort(); }//bankOut.Join();
                        if (charge != null && charge.IsAlive) {  charge.Abort(); }//charge.Join();
                        break;
                }
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void UpdateVcsconnect(object sender, MsgEventArg e)
        {
            UpdateVcsConnectS(e._conn);
        }
        public void UpdateVcsConnectS(int Ee)
        {//update connect vcs eqpstatus
            //try
            //{
            //    if (this.InvokeRequired)
            //    {
            //        this.Invoke(new UpdateVcsStatus(UpdateVcsConnectS), Ee);
            //    }
            //    else
            //    {
            //        UpdateVcsConnectStatus(Ee);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    RefreshMessageInfo(lbxVcsMsg, ex.Message + "-" + ex.StackTrace);
            //    Log.Error(null, ex.Message + "-" + ex.StackTrace);
            //}
        }
        public void Starttimer()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new StartTimer(Starttimer));
                }
                else
                {
                    RunTimer();
                }
            }
            catch(Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void RunTimer()
        {
            try
            {
                timer1.Start();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void UpdateVcsConnectStatus(int e)
        {
            //try
            //{
            //    switch (e)
            //    {
            //        case (int)E.waiting:
            //            //lblVCS_connect.Text = "Waiting";
            //            //RefreshMessageInfo(lbxVcsMsg, _sc_ip + "- Waiting.");
            //            //userLanternVCS_vcs.LanternBackground = Color.Orange;
            //            break;
            //        case (int)E.connected:
            //            //lblVCS_connect.Text = "Connect";
            //            vcsconnect = true;
            //            //RefreshMessageInfo(lbxVcsMsg, _sc_ip + "- Connect.");
            //            //userLanternVCS_vcs.LanternBackground = Color.Lime;
            //            break;
            //        default:
            //            //btnVCSEnd.Enabled = false;
            //            //btnVCSStart.Enabled = true;
            //            //lblVCS_connect.Text = "DisConnect";
            //            uvpBattery.Value = 0;
            //            vcsconnect = false;
            //            //RefreshMessageInfo(lbxVcsMsg, _sc_ip + "- DisConnect.");
            //            //userLanternVCS_vcs.LanternBackground = Color.Red;
            //            //uvpSCBattery.Value = 0;
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    RefreshMessageInfo(lbxVcsMsg, ex.Message + "-" + ex.StackTrace);
            //    Log.Error(null, ex.Message + "-" + ex.StackTrace);
            //}
        }
        private void RefreshEQPInfo(object sender, MsgEventArg ms)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UpdateEQP(RefreshEQP), EQP);
            }
            else
            {
                RefreshEQP(EQP);
            }
        }
        private void RefreshEQP(Equipment eqp)
        {
            try
            {
                lock (eqobj)
                {
                    if (Equipment.eqpID != null)
                    {
                        txtEQPID.Text = Equipment.eqpID;
                        //txtVCSEQPID.Text = Equipment.eqpID;
                    }
                    if (Equipment.eqpstatus != null)
                    {
                        txtStatus.Text = Equipment.eqpstatus;
                        txtRun.Text = Equipment.eqpstatus.Substring(0, 1);
                        txtStop.Text = Equipment.eqpstatus.Substring(1, 1);
                        txtABN.Text = Equipment.eqpstatus.Substring(2, 1);
                        txtEmergencyStop.Text = Equipment.eqpstatus.Substring(3, 1);
                        txtOffline.Text = Equipment.eqpstatus.Substring(4, 1);
                        txtBatteryOK.Text = Equipment.eqpstatus.Substring(5, 1);
                        txtBatteryLow.Text = Equipment.eqpstatus.Substring(6, 1);
                    }
                    if (Equipment.loadStatus != null)
                    {
                        txtLoadStatus.Text = Equipment.loadStatus;
                        //txtVCSLoadStatus.Text = Equipment.loadStatus;
                    }
                    if (Equipment.battery != null)
                    {
                        int bat = int.Parse(Equipment.battery);
                        if (charging)
                        {
                            ER03 er03 = new ER03();
                            er03.eqpid = Equipment.eqpID;
                            er03.report_time= DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                            er03.progress = Equipment.battery;
                            vcs.ReportChargeProgress(er03);
                            lblBattery.Text = "Battery: Charging!";
                        }
                        else
                        {
                            lblBattery.Text = "Battery: ";
                        }
                        if (bat >= _batteryOK)
                        {
                            uvpBattery.ProgressColor = Color.Lime;
                            //uvpSCBattery.ProgressColor = Color.Lime;
                        }
                        else if (bat < _batteryOK && bat > _batteryLow)
                        {
                            uvpBattery.ProgressColor = Color.Orange;
                            //uvpSCBattery.ProgressColor = Color.Orange;
                        }
                        else
                        {
                            uvpBattery.ProgressColor = Color.Red;
                            //uvpSCBattery.ProgressColor = Color.Red;
                            if (!charging)
                            {
                                cmdcomplete = false;
                                charge = new Thread(ActionCharge);
                                charge.IsBackground = true;
                                charge.Start();
                            }
                        }
                        uvpBattery.Value = bat;
                        //uvpSCBattery.Value = bat;
                    }

                    if (Equipment.layer != null)
                        txtLayer.Text = Equipment.layer;
                    if (Equipment.row != null)
                        txtRow.Text = Equipment.row;
                    if (Equipment.bas != null)
                        txtBase.Text = Equipment.bas;
                    if (Equipment.shelfHeight != null)
                        txtShelfHeight.Text = Equipment.shelfHeight;
                    if (Equipment.shelfWidth != null)
                        txtShelfWidth.Text = Equipment.shelfWidth;
                    if (Equipment.platform != null)
                        txtPlatform.Text = Equipment.platform;
                    //if (Equipment.pier != null)
                    //    txtPier.Text = Equipment.pier;
                    if (Equipment.parentEqpNo != null)
                        txtparenteqp.Text = Equipment.parentEqpNo.ToString();
                    if (Equipment.eqpErrorCode != null)
                    {
                        txtErrorcode.Text = Equipment.eqpErrorCode;
                        ES02 es = new ES02();
                        es.errcode = Equipment.eqpErrorCode;
                        if(MsgModel._errorCodeList.Keys.Contains(Equipment.eqpErrorCode))
                            es.errdesc = MsgModel._errorCodeList[Equipment.eqpErrorCode];
                        vcs.ReportAlarm(es);
                        //txtVCS_SCerrorcode.Text = Equipment.eqpErrorCode;
                    }
                }
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void RefreshMessageInfo(ListBox lb, string str)
        {
            try
            {
                if (lb.InvokeRequired)
                {
                    lb.Invoke(new RefreshInfo(RefreshMessageInfo), lb, str);
                }
                else
                {
                    RefreshMessage(lb, str);
                }
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void RefreshMessage(ListBox lb, string Message)
        {
            try
            {
                lock (obj)
                {
                    if (lb.IsDisposed)
                    {
                        return;
                    }
                    if (lb.Items.Count > 1000)
                        lb.Items.RemoveAt(0);

                    lb.Items.Add(System.DateTime.Now + " - " + Message);
                    lb.TopIndex = lb.Items.Count - 1;
                    
                }
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }

        private void LbxMsg_DoubleClick(object sender, EventArgs e)
        {
            ShowMsg(sender);
        }
        private void ShowMsg(object sender)
        {
            try
            {
                FormTextMsg frmTxtMsg = new FormTextMsg();
                ListBox lbObj = (ListBox)sender;
                String msg;
                if (lbObj != null && lbObj.SelectedItem != null)
                {
                    msg = lbObj.SelectedItem.ToString();

                    frmTxtMsg.rtbMsg.AppendText(msg);
                    frmTxtMsg.Show();
                }
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }

        private void HandleDatagramWritten(IAsyncResult ar)
        {
            try
            {
                ((TcpClient)ar.AsyncState).GetStream().EndWrite(ar);
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }

        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            
            try
            {
                if (Equipment.seqno >= 9999)
                {
                    Equipment.seqno = 0;
                }
                if (lblTerminalDisplay.Text != "Terminal Display : ")
                {
                    if (time_terminaldisplay <= _terminaldisplaytime)
                        time_terminaldisplay++;
                    else
                    {
                        time_terminaldisplay = 0;
                        lblTerminalDisplay.Text = "Terminal Display : ";
                    }
                        
                }
                if (connect && !msghandel)
                {
                    
                    get = new Thread(GetEqpStatus);
                    get.IsBackground = true;
                    get.Start();
                    
                }
                else if (!connect)
                {
                    BtnSCStart_Click(null, null);
                }
                
            }
            catch (Exception ex)
            {
                BtnSCEnd_Click(null, null);
                MsgEventArg ms = new MsgEventArg();
                ms._conn = Convert.ToInt32(E.disconnected);
                UpdateConnectS(ms._conn);
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
            

        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                ES03 es03 = new ES03();
                es03.eqpid = Equipment.eqpID;
                es03.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                es03.heartbeat = "1";
                vcs.HeartBeat(es03);
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void GetEqpStatus(object obj)
        {
            timer1.Stop();
            try
            {
                string triggerTime = DateTime.Now.ToString("HHmmss");
                Dictionary<string, string> time = new Dictionary<string, string>() { { "TriggerTime", triggerTime } };
                time.Add("SeqNo", Equipment.seqno.ToString().PadLeft(4, '0'));
                time.Add("ReSent", "0");
                time.Add("Status", "0");
                MsgEventArg msg = new MsgEventArg();
                msg._value = time;
                msg._cmdCode = "06";
                int sum = MsgModel._commandType[((int)Enmu.CmdCode.EQP_STATUS_CMD).ToString().PadLeft(2, '0')].Select(o => o.Value).Sum();
                byte[] reply = new byte[sum];
                Cmd06 cmd06 = new Cmd06();
                string[] sp = cmd06.GetData(msg, ref reply);
                cmd26ack = null;
                SentData(sp, reply);
                waitack("06");
            }
            catch (Exception ex)
            {
                BtnSCEnd_Click(null, null);
                MsgEventArg ms = new MsgEventArg();
                ms._conn = Convert.ToInt32(E.disconnected);
                UpdateConnectS(ms._conn);
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
            //timer1.Start();
            Starttimer();
        }


        private void BtnManual_Click(object sender, EventArgs e)
        {
            try
            {
                if (!connect)
                {
                    MessageBox.Show("No Connect!");
                    return;
                }
                UserButton btn = (UserButton)sender;
                string funstr = btn.UIText.Split('-')[0];
                FormManualCmd frm = new FormManualCmd();
                frm.function = funstr;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    int sum = MsgModel._commandType[funstr].Select(o => o.Value).Sum();
                    byte[] reply = new byte[sum];
                    int cc = 0;
                    string triggerTime = frm.data["TriggerTime"];
                    byte[] chkbbc = new byte[2];
                    Dictionary<string, string> dt = new Dictionary<string, string>() { { "TriggerTime", triggerTime } };
                    foreach (string k in frm.data.Keys)
                    {
                        int count = MsgModel._commandType[funstr][k];
                        if (k.Equals("STX"))
                        {
                            sb.Append(k + ":" + MsgModel._startPoint + "\n");
                            reply[cc] = Convert.ToByte(Convert.ToInt32(MsgModel._startPoint, 16));
                            cc++;
                        }
                        else if (k.Equals("ETX"))
                        {
                            sb.Append(k + ":" + MsgModel._endPoint);
                            reply[cc] = Convert.ToByte(Convert.ToInt32(MsgModel._endPoint, 16));
                            cc++;
                        }
                        else if (k.Equals("BCC"))
                        {
                            chkbbc = MsgModel.BCC(dt);
                            for (int i = 0; i < count; i++)
                            {
                                if (frm.data[k].ToString().Length > 2)
                                {
                                    sb.Append(k + ":" + Convert.ToString(int.Parse(frm.data[k].ToString()),16).ToUpper() + "\n");
                                    reply[cc] = Encoding.ASCII.GetBytes(Convert.ToString(chkbbc[i], 16).ToUpper())[0];
                                }
                                else
                                {
                                    sb.Append(k + ":" + frm.data[k].ToString().Substring(i, 1).PadLeft(2, '0') + "\n");
                                    reply[cc] = chkbbc[i];
                                }
                                cc++;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < count; i++)
                            {
                                sb.Append(k + ":" + frm.data[k].ToString().Substring(i, 1).PadLeft(2, '0') + "\n");
                                reply[cc] = Encoding.ASCII.GetBytes(frm.data[k].ToString())[i]; //Convert.ToByte(frm.data[k].ToString().Substring(i, 1).PadLeft(2, '0'));
                                cc++;
                            }
                        }
                    }
                    string[] strp = sb.ToString().Split('\n');
                    SentData(sb.ToString().Split('\n'), reply);
                }

            }
            catch (Exception ex)
            {
                //BtnSCEnd_Click(null, null);
                //MsgEventArg ms = new MsgEventArg();
                //ms._conn = Convert.ToInt32(E.disconnected);
                //UpdateConnectS(ms._conn);
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (cmdcomplete)
                    {
                        FormCmd frm = new FormCmd();
                        frm.function = "Transfer";
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            cmdcomplete = false;
                            Ta = new Thread(ActionTransfer);
                            Ta.IsBackground = true;
                            Ta.Start(frm.data);


                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                    Log.Error(null, ex.Message + "-" + ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void FormNewLook_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (tcp.Client != null)
            {
                tcp.CloseServer(false);
            }
            //this.Dispose();
        }

        private void LbxCmd_DoubleClick(object sender, EventArgs e)
        {
            ShowMsg(sender);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            lbxCmd.Items.Clear();
        }

        private void BtnHBclear_Click(object sender, EventArgs e)
        {
            lbxHb.Items.Clear();
        }

        private void Btnmsgclear_Click(object sender, EventArgs e)
        {
            lbxMsg.Items.Clear();
        }

        private void LbxHb_DoubleClick(object sender, EventArgs e)
        {
            ShowMsg(sender);
        }

        private void BtnMove_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdcomplete)
                {
                    FormCmd frm = new FormCmd();
                    frm.function = "Move";
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        cmdcomplete = false;
                        TaM = new Thread(ActionMove);
                        TaM.IsBackground = true;
                        TaM.Start(frm.data);

                    }
                }
                
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void BtnBankIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdcomplete)
                {
                    FormBankCmd frm = new FormBankCmd();
                    frm.function = "BankIn";
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        cmdcomplete = false;
                        bankIn = new Thread(ActionBankIn);
                        bankIn.IsBackground = true;
                        bankIn.Start(frm.dc);
                    }
                }
                
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }

        private void BtnBankOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdcomplete)
                {
                    FormBankCmd frm = new FormBankCmd();
                    frm.function = "BankOut";
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        cmdcomplete = false;
                        bankOut = new Thread(ActionBankOut);
                        bankOut.IsBackground = true;
                        bankOut.Start(frm.dc);
                    }
                }
                
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }

        private void BtnCharge_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect&&cmdcomplete)
                {
                    DialogResult myResult = MessageBox.Show("Manual Charge ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (myResult.Equals(DialogResult.Yes))
                    {
                        cmdcomplete = false;
                        charge = new Thread(ActionCharge);
                        charge.IsBackground = true;
                        charge.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void ActionMove(object obj)
        {
            try
            {
                ER01 er01 = new ER01();
                er01.eqpid = Equipment.eqpID;
                er01.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                er01.movestate = "Moving";
                vcs.ReportMove(er01);
                Dictionary<string, string> data = obj as Dictionary<string, string>;
                string AB = MsgModel._crane.ToString().PadLeft(2, '0');
                string setRow = string.Empty;
                string setLayer = string.Empty;
                string setBase = string.Empty;
                setRow = data["TRow"];
                setBase = data["TBase"];
                setLayer = data["TLayer"];
                //if (!Equipment.row.Equals(MsgModel._crane.ToString().PadLeft(2, '0')) && (int.Parse(data["TRow"]) > MsgModel._crane|| int.Parse(data["TRow"]) < MsgModel._crane) && int.Parse(data["TLayer"]).Equals(int.Parse(Equipment.layer))&& int.Parse(data["TBase"]).Equals(int.Parse(Equipment.bas))&&((int.Parse(Equipment.row) >= MsgModel._shelfL && int.Parse(data["TRow"]) >= MsgModel._shelfL) || (int.Parse(Equipment.row) <= MsgModel._shelfR && int.Parse(data["TRow"]) <= MsgModel._shelfR)))
                //{
                //    //03_04移動
                //    //Moving(data);
                //}
                //else 
                if(int.Parse(data["TRow"]) == MsgModel._crane&& int.Parse(Equipment.row)!= MsgModel._crane)
                {
                    //if (int.Parse(Equipment.row)<= MsgModel._shelfR)
                    //    AB = MsgModel._shelfR.ToString().PadLeft(2, '0');
                    //else
                    //    AB = MsgModel._shelfL.ToString().PadLeft(2, '0');

                    //03_09空車上母車
                    AB = MsgModel._crane.ToString().PadLeft(2, '0');
                    data["TBase"] = Equipment.bas;
                    data["TLayer"] = Equipment.layer;
                    cmd35ack = null;
                    EnterCrane(data, AB,"L");
                    waitack("03");
                }
                else //if(!Equipment.row.Equals("00"))
                {
                    if (Equipment.row.Equals(MsgModel._crane.ToString().PadLeft(2,'0')) && (int.Parse(data["TRow"]) > MsgModel._crane || int.Parse(data["TRow"]) < MsgModel._crane))
                    {
                        if (int.Parse(data["TRow"]) >= MsgModel._shelfR)//MsgModel._shelfL
                        {
                            AB = MsgModel._shelfR.ToString().PadLeft(2, '0');
                        }
                        else if (int.Parse(data["TRow"]) <= MsgModel._shelfL)//MsgModel._shelfR
                        {
                            AB = MsgModel._shelfL.ToString().PadLeft(2, '0');
                        }
                        //03_10空車下母車
                        //AB = setRow;
                        data["TRow"] = setRow;
                        data["TBase"] = setBase;
                        data["TLayer"] = setLayer;
                        cmd35ack = null;
                        LeaveCrane(data, AB, "L");
                        waitack("03");
                    }
                    else if((int.Parse(Equipment.row) >= MsgModel._shelfL && int.Parse(data["TRow"]) <= MsgModel._shelfL) || (int.Parse(Equipment.row) <= MsgModel._shelfR && int.Parse(data["TRow"]) >= MsgModel._shelfR) || (int.Parse(Equipment.row)!= MsgModel._crane && ((!int.Parse(Equipment.layer).Equals(int.Parse(data["TLayer"]))) || (!int.Parse(Equipment.bas).Equals(int.Parse(data["TBase"]))))))//(int.Parse(Equipment.row) <= MsgModel._shelfL && int.Parse(data["TRow"]) >= MsgModel._shelfR && !Equipment.row.Equals("00"))
                    {
                        if (int.Parse(Equipment.row) <= MsgModel._shelfL)//MsgModel._shelfR
                            AB = MsgModel._shelfL.ToString().PadLeft(2, '0');
                        else
                            AB = MsgModel._shelfR.ToString().PadLeft(2, '0');

                        //03_09空車上母車
                        AB = MsgModel._crane.ToString().PadLeft(2,'0');
                        data["TBase"] = Equipment.bas;
                        data["TLayer"] = Equipment.layer;
                        cmd35ack = null;
                        EnterCrane(data, AB,"L");
                        waitack("03");
                        //03_10空車下母車
                        if (int.Parse(setRow) <= MsgModel._shelfL)//MsgModel._shelfR
                            AB = MsgModel._shelfL.ToString().PadLeft(2, '0');
                        else
                            AB = MsgModel._shelfR.ToString().PadLeft(2, '0');
                        //AB = setRow;
                        data["TRow"] = setRow;
                        data["TBase"] = setBase;
                        data["TLayer"] = setLayer;
                        cmd35ack = null;
                        LeaveCrane(data, AB, "L");
                        waitack("03");
                        
                    }
                    else
                    {
                        RefreshMessageInfo(lbxMsg, "SC can't move to Layer[" + setLayer + "],Row[" + setRow + "],Base[" + setBase + "]");
                    }
                    
                }
                cmdcomplete = true;
                
                er01.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                er01.movestate = "Finished";
                vcs.ReportMove(er01);
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void ActionTransfer(object obj)
        {
            try
            {
                ER02 er02 = new ER02();
                er02.eqpid = Equipment.eqpID;
                er02.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                er02.transferstate = "Moving";
                vcs.ReportCarrierTransferState(er02);
                Dictionary<string, string> data = obj as Dictionary<string, string>;
                string AB = MsgModel._crane.ToString().PadLeft(2, '0');
                string setFRow = data["FRow"];
                string setFLayer = data["FLayer"];
                string setFBase = data["FBase"];
                string setTRow = data["TRow"];
                string setTLayer = data["TLayer"];
                string setTBase = data["TBase"];
                if (Equipment.row != MsgModel._crane.ToString().PadLeft(2,'0'))
                {
                    //03_09空車上母車
                    AB = MsgModel._crane.ToString().PadLeft(2, '0');
                    data["TBase"] = Equipment.bas;
                    data["TLayer"] = Equipment.layer;
                    cmd35ack = null;
                    EnterCrane(data, AB, "L");
                    waitack("03");
                }
                //03_10空車下母車
                //AB = setFRow;
                if (int.Parse(setFRow) <= MsgModel._shelfR)
                    AB = MsgModel._shelfR.ToString().PadLeft(2, '0');
                else
                    AB = MsgModel._shelfL.ToString().PadLeft(2, '0');
                AB = setFRow;
                data["TRow"] = setFRow;
                data["TBase"] = setFBase;
                data["TLayer"] = setFLayer;
                cmd35ack = null;
                LeaveCrane(data, AB, "L");//, AB
                waitack("03");
                //03_02取貨
                data["TRow"] = setFRow;
                data["TBase"] = setFBase;
                data["TLayer"] = setFLayer;
                cmd35ack = null;
                Pickup(data);
                waitack("03");
                if ((setFLayer.Equals(setTLayer) && setFBase.Equals(setTBase)) && ((int.Parse(Equipment.row) >= MsgModel._shelfL && int.Parse(setTRow) >= MsgModel._shelfL) || (int.Parse(Equipment.row) <= MsgModel._shelfR && int.Parse(setTRow) <= MsgModel._shelfR)))
                {
                    //03_03卸貨
                    data["TRow"] = setTRow;
                    data["TBase"] = setTBase;
                    data["TLayer"] = setTLayer;
                    cmd35ack = null;
                    UnLoad(data);
                    waitack("03");
                }
                else
                {
                    //03_09載貨上母車
                    AB = MsgModel._crane.ToString().PadLeft(2, '0');
                    data["TRow"] = setTRow;
                    data["TBase"] = Equipment.bas;
                    data["TLayer"] = Equipment.layer ;
                    cmd35ack = null;
                    EnterCrane(data, AB, "U");
                    waitack("03");
                    //03_10載貨下母車
                    // AB = setTRow;
                    if (int.Parse(setTRow) <= MsgModel._shelfR)
                        AB = MsgModel._shelfR.ToString().PadLeft(2, '0');
                    else
                        AB = MsgModel._shelfL.ToString().PadLeft(2, '0');
                    AB = setTRow;
                    data["TRow"] = setTRow;
                    data["TBase"] = setTBase;
                    data["TLayer"] = setTLayer;
                    cmd35ack = null;
                    LeaveCrane(data, AB, "U");
                    waitack("03");
                    //03_03卸貨
                    data["TRow"] = setTRow;
                    data["TBase"] = setTBase;
                    data["TLayer"] = setTLayer;
                    cmd35ack = null;
                    UnLoad(data);
                    waitack("03");
                }
                //03_09空車上母車
                AB = MsgModel._crane.ToString().PadLeft(2, '0');
                data["TBase"] = Equipment.bas;
                data["TLayer"] = Equipment.layer;
                cmd35ack = null;
                EnterCrane(data, AB, "L");
                waitack("03");
                
                er02.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                er02.transferstate = "Finished";
                vcs.ReportCarrierTransferState(er02);
                cmdcomplete = true;
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void ActionCharge(object obj)
        {
            try
            {
                //03_14充電
                Dictionary<string, string> dataf = new Dictionary<string, string>();//obj as Dictionary<string, string>;
                dataf.Add("ShelfHeight", "0");
                dataf.Add("ShelfWidth", "0");
                dataf.Add("Row", "05");
                dataf.Add("Base", "01");
                dataf.Add("Layer", "01");
                dataf.Add("Platform", Equipment.eqpID);
                dataf.Add("Pier", "0000");
                dataf.Add("MCkey", "0000");
                cmd35ack = null;
                if (!Charge(dataf, "S"))
                {
                    RefreshMessageInfo(lbxMsg, "Charge Msg Error.");
                    return;
                }
                waitack("03");

                cmd35ack = null;
                if (!Charge(dataf, "E"))
                {
                    RefreshMessageInfo(lbxMsg, "Charge Msg Error.");
                    return;
                }
                waitack("03");
                cmdcomplete = true;
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void ActionBankIn(object obj)
        {
            try
            {
                Dictionary<string, string> data = obj as Dictionary<string, string>;
                string AB = MsgModel._crane.ToString().PadLeft(2, '0');
                string setTRow = data["TRow"];
                string setTLayer = data["TLayer"];
                string setTBase = data["TBase"];
                //03_13載貨出堆垛機
                if (int.Parse(setTRow) <= MsgModel._shelfL)//MsgModel._shelfR
                    AB = MsgModel._shelfL.ToString().PadLeft(2, '0');
                else
                    AB = MsgModel._shelfR.ToString().PadLeft(2, '0');
                
                cmd35ack = null;
                LeaveCrane(data, AB, "U");
                waitack("03");

                //03_03卸貨
                cmd35ack = null;
                UnLoad(data);
                waitack("03");

                //03_09空車上母車
                AB = MsgModel._crane.ToString().PadLeft(2, '0');
                data["TBase"] = Equipment.bas;
                data["TLayer"] = Equipment.layer;
                cmd35ack = null;
                EnterCrane(data, AB, "L");
                waitack("03");
                cmdcomplete = true;
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void ActionBankOut(object obj)
        {
            try
            {
                Dictionary<string, string> data = obj as Dictionary<string, string>;
                string AB = MsgModel._crane.ToString().PadLeft(2, '0');
                string setFRow = data["TRow"];
                string setFLayer = data["TLayer"];
                string setFBase = data["TBase"];

                //03_10空車下母車
                //AB = setFRow;
                if (int.Parse(setFRow) <= MsgModel._shelfL)//MsgModel._shelfR
                    AB = MsgModel._shelfL.ToString().PadLeft(2, '0');
                else
                    AB = MsgModel._shelfR.ToString().PadLeft(2, '0');
                
                cmd35ack = null;
                LeaveCrane(data, AB, "L");
                waitack("03");

                //03_02取貨
                cmd35ack = null;
                Pickup(data);
                waitack("03");

                //03_12載貨上母車
                AB = MsgModel._crane.ToString().PadLeft(2, '0');
                data["TBase"] = Equipment.bas;
                data["TLayer"] = Equipment.layer;
                cmd35ack = null;
                EnterCrane(data, AB, "U");
                waitack("03");

                //03_07移載卸貨
                //cmd35ack = false;
                //MoveUnLoad(data);
                //waitack("03");
                cmdcomplete = true;
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void waitack(object obj)
        {
            string f = obj as string;
            if (f.Equals("03"))
            {
                while (cmd35ack==null)
                {
                }
                //return cmd35ack;
            }
            else if(f.Equals("06"))
            {
                while (cmd26ack==null)
                {
                }
                //return cmd26ack;
            }
            else
            {
            }
            
        }

        

        //private void MovePickup(Dictionary<string, string> dataf)
        //{
        //    try
        //    {
        //        Dictionary<string, string> time = new Dictionary<string, string>();
        //        string AB = MsgModel._crane.ToString().PadLeft(2, '0');
        //        string setRow = dataf["TRow"];
        //        string setLayer = dataf["TLayer"];
        //        string setBase = dataf["TBase"];
        //        MsgEventArg msg = new MsgEventArg();
        //        Cmd03 cmd = new Cmd03();
        //        time.Add("SeqNo", Equipment.seqno.ToString().PadLeft(4, '0'));
        //        time.Add("ReSent", "0");
        //        time.Add("MCkey", "0000");
        //        time.Add("CycleCmd", "07");
        //        time.Add("JobType", "01");
        //        time.Add("ShelfHeight", dataf["ShelfHeight"]);
        //        time.Add("ShelfWidth", dataf["ShelfWidth"]);
        //        time.Add("Row", "04");//dataf["Row"]);
        //        time.Add("Base", "00");// dataf["Base"]);
        //        time.Add("Layer", "00");// dataf["Layer"]);
        //        time.Add("Platform", dataf["Platform"]);
        //        time.Add("Pier", dataf["Pier"]);
        //        msg._cmdCode = "03";
        //        msg._value = time;
        //        int sum = MsgModel._commandType[((int)Enmu.CmdCode.CYCLE_CMD).ToString().PadLeft(2, '0')].Select(o => o.Value).Sum();
        //        byte[] byt = new byte[sum];
        //        cmd = new Cmd03();
        //        string[] data = cmd.GetData(msg, ref byt);
        //        SentData(data, byt);
        //    }
        //    catch (Exception ex)
        //    {
        //        RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
        //        Log.Error(null, ex.Message + "-" + ex.StackTrace);
        //    }
        //}
        //private void MoveUnLoad(Dictionary<string, string> dataf)
        //{
        //    try
        //    {
        //        Dictionary<string, string> time = new Dictionary<string, string>();
        //        string AB = MsgModel._crane.ToString().PadLeft(2, '0');
        //        string setRow = dataf["TRow"];
        //        string setLayer = dataf["TLayer"];
        //        string setBase = dataf["TBase"];
        //        MsgEventArg msg = new MsgEventArg();
        //        Cmd03 cmd = new Cmd03();
        //        time.Add("SeqNo", Equipment.seqno.ToString().PadLeft(4, '0'));
        //        time.Add("ReSent", "0");
        //        time.Add("MCkey", "0000");
        //        time.Add("CycleCmd", "08");
        //        time.Add("JobType", "03");
        //        time.Add("ShelfHeight", dataf["ShelfHeight"]);
        //        time.Add("ShelfWidth", dataf["ShelfWidth"]);
        //        time.Add("Row", "04");//dataf["Row"]);
        //        time.Add("Base", "00");// dataf["Base"]);
        //        time.Add("Layer", "00");// dataf["Layer"]);
        //        time.Add("Platform", dataf["Platform"]);
        //        time.Add("Pier", dataf["Pier"]);
        //        msg._cmdCode = "03";
        //        msg._value = time;
        //        int sum = MsgModel._commandType[((int)Enmu.CmdCode.CYCLE_CMD).ToString().PadLeft(2, '0')].Select(o => o.Value).Sum();
        //        byte[] byt = new byte[sum];
        //        cmd = new Cmd03();
        //        string[] data = cmd.GetData(msg, ref byt);
        //        SentData(data, byt);
        //    }
        //    catch (Exception ex)
        //    {
        //        RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
        //        Log.Error(null, ex.Message + "-" + ex.StackTrace);
        //    }
        //}
        private void EnterCrane(Dictionary<string, string> dataf, string AB, string LU)
        {
            try
            {
                Dictionary<string, string> time = new Dictionary<string, string>();
                MsgEventArg msg = new MsgEventArg();
                Cmd03 cmd = new Cmd03();
                time.Add("SeqNo", Equipment.seqno.ToString().PadLeft(4, '0'));
                time.Add("ReSent", "0");
                time.Add("MCkey", dataf["MCkey"]);//"0000");
                if (LU.Equals("U"))//L空車上母車,U載貨上車
                    time.Add("CycleCmd", "12");
                else
                    time.Add("CycleCmd", "09");
                time.Add("JobType", "02");
                time.Add("ShelfHeight", dataf["ShelfHeight"]);
                time.Add("ShelfWidth", dataf["ShelfWidth"]);
                time.Add("Row", AB); //dataf["TRow"]
                time.Add("Base", dataf["TBase"]);
                time.Add("Layer", dataf["TLayer"]);
                time.Add("Platform", dataf["Platform"]);
                time.Add("Pier", dataf["Pier"]);
                msg._cmdCode = "03";
                msg._value = time;
                int sum = MsgModel._commandType[((int)Enmu.CmdCode.CYCLE_CMD).ToString().PadLeft(2, '0')].Select(o => o.Value).Sum();
                byte[] byt = new byte[sum];
                cmd = new Cmd03();
                string[] data = cmd.GetData(msg, ref byt);
                SentData(data, byt);

            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void LeaveCrane(Dictionary<string, string> dataf, string AB, string LU)
        {
            try
            {
                Dictionary<string, string> time = new Dictionary<string, string>();
                MsgEventArg msg = new MsgEventArg();
                Cmd03 cmd = new Cmd03();
                time.Add("SeqNo", Equipment.seqno.ToString().PadLeft(4, '0'));
                time.Add("ReSent", "0");
                time.Add("MCkey", dataf["MCkey"]);
                if (LU.Equals("U"))//L空車下母車,U載貨下車
                    time.Add("CycleCmd", "13");
                else
                    time.Add("CycleCmd", "10");
                time.Add("JobType", "02");
                time.Add("ShelfHeight", dataf["ShelfHeight"]);
                time.Add("ShelfWidth", dataf["ShelfWidth"]);
                time.Add("Row", AB);//dataf["TRow"]);
                time.Add("Base", dataf["TBase"]);
                time.Add("Layer", dataf["TLayer"]);
                time.Add("Platform", dataf["Platform"]);
                time.Add("Pier", dataf["Pier"]);
                msg._cmdCode = "03";
                msg._value = time;
                int sum = MsgModel._commandType[((int)Enmu.CmdCode.CYCLE_CMD).ToString().PadLeft(2, '0')].Select(o => o.Value).Sum();
                byte[] byt = new byte[sum];
                cmd = new Cmd03();
                string[] data = cmd.GetData(msg, ref byt);
                SentData(data, byt);
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void Moving(Dictionary<string, string> dataf)
        {
            try
            {
                Dictionary<string, string> time = new Dictionary<string, string>();
                MsgEventArg msg = new MsgEventArg();
                Cmd03 cmd = new Cmd03();
                time = new Dictionary<string, string>();
                time.Add("SeqNo", Equipment.seqno.ToString().PadLeft(4, '0'));
                time.Add("ReSent", "0");
                time.Add("MCkey", dataf["MCkey"]);
                time.Add("CycleCmd", "04");
                time.Add("JobType", "02");
                time.Add("ShelfHeight", dataf["ShelfHeight"]);
                time.Add("ShelfWidth", dataf["ShelfWidth"]);
                time.Add("Row", dataf["TRow"]);
                time.Add("Base", dataf["TBase"]);
                time.Add("Layer", dataf["TLayer"]);
                time.Add("Platform", dataf["Platform"]);
                time.Add("Pier", dataf["Pier"]);
                msg = new MsgEventArg();
                msg._cmdCode = "03";
                msg._value = time;
                int sum = MsgModel._commandType[((int)Enmu.CmdCode.CYCLE_CMD).ToString().PadLeft(2, '0')].Select(o => o.Value).Sum();
                byte[] byt = new byte[sum];
                cmd = new Cmd03();
                string[] data = cmd.GetData(msg, ref byt);
                SentData(data, byt);
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void Pickup(Dictionary<string, string> dataf)
        {
            try
            {
                Dictionary<string, string> time = new Dictionary<string, string>();
                MsgEventArg msg = new MsgEventArg();
                Cmd03 cmd = new Cmd03();
                time = new Dictionary<string, string>();
                time.Add("SeqNo", Equipment.seqno.ToString().PadLeft(4, '0'));
                time.Add("ReSent", "0");
                time.Add("MCkey", dataf["MCkey"]);
                time.Add("CycleCmd", "02");
                time.Add("JobType", "03");
                time.Add("ShelfHeight", dataf["ShelfHeight"]);
                time.Add("ShelfWidth", dataf["ShelfWidth"]);
                time.Add("Row", dataf["TRow"]);
                time.Add("Base", dataf["TBase"]);
                time.Add("Layer", dataf["TLayer"]);
                time.Add("Platform", dataf["Platform"]);
                time.Add("Pier", dataf["Pier"]);
                msg = new MsgEventArg();
                msg._cmdCode = "03";
                msg._value = time;
                int sum = MsgModel._commandType[((int)Enmu.CmdCode.CYCLE_CMD).ToString().PadLeft(2, '0')].Select(o => o.Value).Sum();
                byte[] byt = new byte[sum];
                cmd = new Cmd03();
                string[] data = cmd.GetData(msg, ref byt);
                SentData(data, byt);
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private void UnLoad(Dictionary<string, string> dataf)
        {
            try
            {
                Dictionary<string, string> time = new Dictionary<string, string>();
                MsgEventArg msg = new MsgEventArg();
                Cmd03 cmd = new Cmd03();
                time = new Dictionary<string, string>();
                time.Add("SeqNo", Equipment.seqno.ToString().PadLeft(4, '0'));
                time.Add("ReSent", "0");
                time.Add("MCkey", dataf["MCkey"]);
                time.Add("CycleCmd", "03");
                time.Add("JobType", "01");
                time.Add("ShelfHeight", dataf["ShelfHeight"]);
                time.Add("ShelfWidth", dataf["ShelfWidth"]);
                time.Add("Row", dataf["TRow"]);
                time.Add("Base", dataf["TBase"]);
                time.Add("Layer", dataf["TLayer"]);
                time.Add("Platform", dataf["Platform"]);
                time.Add("Pier", dataf["Pier"]);
                msg = new MsgEventArg();
                msg._cmdCode = "03";
                msg._value = time;
                int sum = MsgModel._commandType[((int)Enmu.CmdCode.CYCLE_CMD).ToString().PadLeft(2, '0')].Select(o => o.Value).Sum();
                byte[] byt = new byte[sum];
                cmd = new Cmd03();
                string[] data = cmd.GetData(msg, ref byt);
                SentData(data, byt);
            }
            catch (Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
        }
        private bool Charge(Dictionary<string, string> dataf,string StartorEnd)
        {
            try
            {
                
                Dictionary<string, string> time = new Dictionary<string, string>();
                MsgEventArg msg = new MsgEventArg();
                Cmd03 cmd = new Cmd03();
                time.Add("SeqNo", Equipment.seqno.ToString().PadLeft(4, '0'));
                time.Add("ReSent", "0");
                time.Add("MCkey", dataf["MCkey"]);
                if (StartorEnd.Equals("S"))
                {
                    ER04 er04 = new ER04();
                    er04.eqpid = Equipment.eqpID;
                    er04.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                    er04.chargestate = "Initialed";
                    vcs.ReportChargeState(er04);

                    time.Add("CycleCmd", "14");
                    time.Add("JobType", "07");
                    time.Add("Row", dataf["Row"]);

                    er04.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                    er04.chargestate = "Moving";
                    vcs.ReportChargeState(er04);
                    charging = true;
                }
                else
                {
                    charging = false;
                    time.Add("CycleCmd", "15");
                    time.Add("JobType", "08");
                    time.Add("Row", MsgModel._crane.ToString().PadLeft(2,'0'));

                    ER04 er04 = new ER04();
                    er04.eqpid = Equipment.eqpID;
                    er04.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                    er04.chargestate = "Finished";
                    vcs.ReportChargeState(er04);
                }
                time.Add("ShelfHeight", dataf["ShelfHeight"]);
                time.Add("ShelfWidth", dataf["ShelfWidth"]);
                time.Add("Base", dataf["Base"]);
                time.Add("Layer", dataf["Layer"]);
                time.Add("Platform", dataf["Platform"]);
                time.Add("Pier", dataf["Pier"]);
                msg._cmdCode = "03";
                msg._value = time;
                int sum = MsgModel._commandType[((int)Enmu.CmdCode.CYCLE_CMD).ToString().PadLeft(2, '0')].Select(o => o.Value).Sum();
                byte[] byt = new byte[sum];
                cmd = new Cmd03();
                string[] data = cmd.GetData(msg, ref byt);
                SentData(data, byt);
                return true;
            }
            catch(Exception ex)
            {
                RefreshMessageInfo(lbxMsg, ex.Message + "-" + ex.StackTrace);
                Log.Error(null, ex.Message + "-" + ex.StackTrace);
            }
            return false;
        }


        private void BtnScan_Click(object sender, EventArgs e)
        {

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 初始化RefreshTimer
        /// </summary>
        private void funInitTimer()
        {
            timRefresh.Stop();
            timRefresh.Tick += new EventHandler(timRefresh_Tick);
            timRefresh.Interval = 500;
            timRefresh.Start();

            // 暫時不用 By Leon
            //timProgram.Stop();
            //timProgram.Elapsed += new System.Timers.ElapsedEventHandler(timProgram_Elapsed);
            //timProgram.Interval = 3000;
            //timProgram.Start();

            // 暫時不用 By Leon
            //timProgram_2.Stop();
            //timProgram_2.Elapsed += new System.Timers.ElapsedEventHandler(timProgram_2_Elapsed);
            //timProgram_2.Interval = 1000;
            //timProgram_2.Start();

            // 暫時不用 By Leon
            //timProgram_3.Stop();
            //timProgram_3.Elapsed += new System.Timers.ElapsedEventHandler(timProgram_3_Elapsed);
            //timProgram_3.Interval = 1000;
            //timProgram_3.Start();
        }

        /// <summary>
        /// 表示 timRefresh 觸發 Tick 事件處理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timRefresh_Tick(object sender, EventArgs e)
        {
            timRefresh.Stop();

            try
            {
                lblDateTime.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");

                #region DB、PLC連線狀態
                funShowConnect(clsSystem.gobjDB.ConnFlag, ref lblDBSts);
                //funShowConnect(clsSystem.gobjPLC.ConnectionFlag, ref lblMPLC1Sts);
                //funShowConnect(clsSystem.gobjPLC2.ConnectionFlag, ref lblMPLC2Sts);
                //funShowConnect(clsSystem.gobjPLC3.ConnectionFlag, ref lblMPLC3Sts);
                #endregion DB、PLC連線狀態
                //if (clsSystem.gobjPLC.ConnectionFlag)
                //{
                //    if (!timProgram.Enabled)
                //        timProgram.Start();
                //}
                //else
                //{
                //    timProgram.Stop();
                //}
                //if (clsSystem.gobjPLC2.ConnectionFlag)
                //{
                //    if(!timProgram_2.Enabled)
                //        timProgram_2.Start();
                //}
                //else
                //{
                //    timProgram_2.Stop();
                //}
                //if (clsSystem.gobjPLC3.ConnectionFlag)
                //{
                //    if (!timProgram_3.Enabled)
                //        timProgram_3.Start();
                //}
                //else
                //{
                //    timProgram_3.Stop();
                //}
                #region  更新Crane Mode/Sts
                if (clsSystem.intBegin == 0)
                {
                    funReadCraneMode();
                    funReadCraneSts();
                }
                #endregion  更新Crane Mode/Sts

                #region Auto Reconnect
                if (!chkAutoReconnect.Checked)
                {
                    btnReconnectDB.Enabled = !clsSystem.gobjDB.ConnFlag;
//                    btnReconnectPLC.Enabled = !clsSystem.gobjPLC.ConnectionFlag;
                }
                #endregion Auto Reconnect

                #region WriteAutoRun--註解
                //funWriteAutoRunBit("A01", chkAutoRunTest.Checked);
                //funWriteAutoRunBit("A02", chkAutoRunTest.Checked);
                //funWriteAutoRunBit("A03", chkAutoRunTest.Checked);
                //funWriteAutoRunBit("D04", chkAutoRunTest.Checked);
                #endregion WriteAutoRun

                #region HandShaking & Set PLC DateTime
                //if (clsSystem.gobjPLC.ConnectionFlag)
                //{
                //    if (!objBufferData.HandShaking)
                //        funWritePC2PLC_HandShake("1", 1);
                //    else
                //        funWritePC2PLC_HandShake("0", 1);

                //    funWritePLCSetDateTime(1);
                //}
                //if (clsSystem.gobjPLC2.ConnectionFlag)
                //{
                //    if (!objBufferData.HandShaking)
                //        funWritePC2PLC_HandShake("1", 2);
                //    else
                //        funWritePC2PLC_HandShake("0", 2);

                //    funWritePLCSetDateTime(2);
                //}
                if (clsSystem.gobjPLC3.ConnectionFlag)
                {
                    //if (!objBufferData.HandShaking)
                    //    funWritePC2PLC_HandShake("1", 3);
                    //else
                    //    funWritePC2PLC_HandShake("0", 3);

                    //funWritePLCSetDateTime(3);
                }
                #endregion HandShaking & Set PLC DateTime

                #region 檢查是否有做完的命令並Update字幕機Table



                #endregion 檢查是否有做完的命令並Update字幕機Table

                #region Release 暫存儲位

                #endregion Release 暫存儲位

                #region 站對站
                // funStnToStn();
                #endregion 站對站
            }
            catch (Exception ex)
            {
                var varObject = MethodBase.GetCurrentMethod();
                clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
            }
            finally
            {
                timRefresh.Start();
            }
        }


        /// <summary>
        /// 表示顯示SystemTrace之方法
        /// </summary>
        /// <param name="TraceListBox"></param>
        /// <param name="TraceLog"></param>
        /// <param name="WriteLog"></param>
        private void funShowSystemTrace(ListBox TraceListBox, clsTraceLogEventArgs TraceLog, bool WriteLog)
        {
            if (this.InvokeRequired)
            {
                delShowSystemTrace ShowSystemTrace = new delShowSystemTrace(funShowSystemTrace);
                this.Invoke(ShowSystemTrace, TraceListBox, TraceLog, WriteLog);
            }
            else
            {
                try
                {
                    string strLogMessage = TraceLog.LogMessage.PadRight(35, ' ');
                    switch (TraceLog.objTraceLog)
                    {
                        case enuTraceLog.System:
                            #region System
                            if (!string.IsNullOrWhiteSpace(TraceLog.BCRNo))
                            {
                                #region BCR相關
                                strLogMessage += " => BCRNo:<" + TraceLog.BCRNo + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.BCRSts))
                                    strLogMessage += "BCRSts:<" + TraceLog.BCRSts + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.BCRID))
                                    strLogMessage += "BCRID:<" + TraceLog.BCRID + "> ";
                                #endregion BCR相關
                            }
                            else if (!string.IsNullOrWhiteSpace(TraceLog.LeftCmdSno) || !string.IsNullOrWhiteSpace(TraceLog.RightCmdSno))
                            {
                                #region CmdSno相關
                                strLogMessage += " => CmdSno:<";
                                if (!string.IsNullOrWhiteSpace(TraceLog.LeftCmdSno) && !string.IsNullOrWhiteSpace(TraceLog.RightCmdSno))
                                    strLogMessage += TraceLog.LeftCmdSno + ", " + TraceLog.RightCmdSno + "> ";
                                else if (!string.IsNullOrWhiteSpace(TraceLog.LeftCmdSno))
                                    strLogMessage += TraceLog.LeftCmdSno + "> ";
                                else if (!string.IsNullOrWhiteSpace(TraceLog.RightCmdSno))
                                    strLogMessage += TraceLog.RightCmdSno + "> ";
                                else
                                    strLogMessage += "> ";

                                if (!string.IsNullOrWhiteSpace(TraceLog.SNO))
                                    strLogMessage += "SNO:<" + TraceLog.SNO + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.CmdSts))
                                    strLogMessage += "CmdSts:<" + TraceLog.CmdSts + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.CmdMode))
                                    strLogMessage += "CmdMode:<" + TraceLog.CmdMode + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.Trace))
                                    strLogMessage += "Trace:<" + TraceLog.Trace + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.StnNo))
                                    strLogMessage += "StnNo:<" + TraceLog.StnNo + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.CmdCraneNo))
                                    strLogMessage += "CmdCraneNo:<" + TraceLog.CmdCraneNo + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.LocID))
                                    strLogMessage += "LocID:<" + TraceLog.LocID + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.NewLocID))
                                    strLogMessage += "NewLocID:<" + TraceLog.NewLocID + "> ";
                                #endregion CmdSno相關
                            }
                            else if (!string.IsNullOrWhiteSpace(TraceLog.CraneNo))
                            {
                                #region Crane相關
                                strLogMessage += " => CraneNo:<" + TraceLog.CraneNo + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.CraneSts))
                                    strLogMessage += "CraneSts:<" + TraceLog.CraneSts + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.CraneStsLast))
                                    strLogMessage += "CraneStsLast:<" + TraceLog.CraneStsLast + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.CraneMode))
                                    strLogMessage += "CraneMode:<" + TraceLog.CraneMode + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.CraneModeLast))
                                    strLogMessage += "CraneModeLast:<" + TraceLog.CraneModeLast + "> ";
                                #endregion Crane相關
                            }
                            else if (!string.IsNullOrWhiteSpace(TraceLog.LocSts))
                            {
                                #region LocSts相關
                                strLogMessage += " => Loc:<" + TraceLog.LocID + "> ";
                                strLogMessage += "LocSts:<" + TraceLog.LocSts + "> ";
                                strLogMessage += "OldLocSts:<" + TraceLog.OldLocSts + "> ";
                                #endregion LocSts相關
                            }

                            if (strLastSystemTraceLog.LogMessage != TraceLog.LogMessage)
                            {
                                if (TraceListBox.Items.Count > 200)
                                    TraceListBox.Items.RemoveAt(0);

                                TraceListBox.Items.Add("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + strLogMessage);
                                TraceListBox.SelectedIndex = TraceListBox.Items.Count - 1;
                                strLastSystemTraceLog = TraceLog;

                                if (WriteLog)
                                    clsSystem.funWriteSystemTraceLog(strLogMessage);
                            }
                            #endregion System
                            break;
                        case enuTraceLog.MPLC:
                            #region MPLC
                            if (!string.IsNullOrWhiteSpace(TraceLog.BufferName))
                            {
                                strLogMessage += " => BuffwerName:<" + TraceLog.BufferName + "> ";
                                if (!string.IsNullOrWhiteSpace(TraceLog.AddressSection))
                                    strLogMessage += "AddressSection:<" + TraceLog.AddressSection + "> ";
                                if (TraceLog.PLCValues.Length > 0)
                                    strLogMessage += "PLCValues:<" + string.Join(", ", TraceLog.PLCValues) + "> ";
                            }

                            if (strLastMPLCTraceLog.LogMessage != TraceLog.LogMessage)
                            {
                                if (TraceListBox.Items.Count > 200)
                                    TraceListBox.Items.RemoveAt(0);

                                TraceListBox.Items.Add("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + strLogMessage);
                                TraceListBox.SelectedIndex = TraceListBox.Items.Count - 1;
                                strLastMPLCTraceLog = TraceLog;

                                if (WriteLog)
                                    clsSystem.funWriteMPLCTraceLog(strLogMessage);
                            }
                            #endregion MPLC
                            break;
                        case enuTraceLog.Alarm:
                            #region Alarm
                            if (TraceLog.AlarmClear)
                            {
                                if (TraceListBox.Items.Contains(TraceLog.LogMessage))
                                    TraceListBox.Items.Remove(TraceLog.LogMessage);

                                if (WriteLog)
                                    clsSystem.funWriteAlarmLog("Alarm Clear  => " + TraceLog.LogMessage);
                            }
                            else
                            {
                                if (!TraceListBox.Items.Contains(TraceLog.LogMessage))
                                    TraceListBox.Items.Add(TraceLog.LogMessage);

                                if (WriteLog)
                                    clsSystem.funWriteAlarmLog("Alarm Set => " + TraceLog.LogMessage);
                            }
                            #endregion Alarm
                            break;
                        case enuTraceLog.SQL:
                            if (WriteLog)
                                clsSystem.funWriteAlarmLog("SQL => " + TraceLog.LogMessage);
                            break;
                        case enuTraceLog.None:

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    var varObject = MethodBase.GetCurrentMethod();
                    clsSystem.funWriteExceptionLog(varObject.DeclaringType.FullName, varObject.Name, ex.Message);
                }
            }
        }
    }
}
