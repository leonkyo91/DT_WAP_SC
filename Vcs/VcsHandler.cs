using CmdCode;
using ICommon;
using mirle.stone.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Vcs
{
    public class VcsHandler
    {
        public event EventHandler<MsgEventArg> Run;
        public event EventHandler<MsgEventArg> Report;
        public delegate MsgEventArg MsgArg(MsgEventArg mx);
        //public MsgEventArg msg = new MsgEventArg();
        Dictionary<string, string> values = new Dictionary<string, string>();
        private bool cmdack = false;
        Thread t1;
        protected virtual bool SCWork(object obj)
        {
            MsgEventArg e = obj as MsgEventArg;
            if (Run != null)
            {
                Run(this, e);
                StringBuilder str = new StringBuilder();
                foreach(string s in e._value.Keys)
                {
                    str.Append(s + ":" + e._value[s]+"\n");
                }
                Log.Warn("[" + e._cmdCode + "]-"+ str.ToString());
            }
            return true;
        }
        protected virtual MsgEventArg ToVcs(object obj)
        {
            MsgEventArg e = obj as MsgEventArg;
            if (Report != null)
            {
                Report(this, e);
                StringBuilder str = new StringBuilder();
                foreach (string s in e._value.Keys)
                {
                    str.Append(s + ":" + e._value[s] + "\n");
                }
                Log.Warn("[" + e._cmdCode + "]-" + str.ToString());
            }
            return e;
        }

        public ES01_reply ReportEQPStatus(ES01 es)
        {
            ES01_reply re = new ES01_reply();
            try
            {
                //need to sync system time
                Dictionary<string, string> val = new Dictionary<string, string>();
                val.Add("eqpid", es.eqpid);
                val.Add("report_time", es.report_time);
                val.Add("eqstate", es.eqstate);
                val.Add("linkmode", es.linkmode);
                val.Add("operationmode", es.operationmode);
                if (Equipment.eqpstatus.Substring(5, 1).Equals("1"))
                    val.Add("plcbattery", "NOMAL");
                else if (Equipment.eqpstatus.Substring(6, 1).Equals("1"))
                    val.Add("plcbattery", "LOW");
                val.Add("battery", es.battery);
                val.Add("palletid", es.palletid);
                val.Add("isempty", es.isempty);
                val.Add("idt", es.idt);
                MsgEventArg msg = new MsgEventArg();
                msg._value = val;
                ToVcs(msg);

                //reply
                re.ack = "0";
                re.desc = "Accept";
                return re;
            }
            catch (Exception ex)
            {
                re.ack = "1";
                re.desc = ex.Message;
                return re;
                throw;
            }
        }
        public ES02_reply ReportAlarm(ES02 es)
        {
            ES02_reply re = new ES02_reply();
            try
            {
                //need to sync system time
                Dictionary<string, string> val = new Dictionary<string, string>();
                val.Add("errcode", es.errcode);
                val.Add("errdesc", es.errdesc);
                MsgEventArg msg = new MsgEventArg();
                msg._value = val;
                ToVcs(msg);

                //reply
                re.ack = "0";
                re.desc = "Accept";
                return re;
            }
            catch (Exception ex)
            {
                re.ack = "1";
                re.desc = ex.Message;
                return re;
                throw;
            }
        }
        public ES03_reply HeartBeat(ES03 es)
        {
            ES03_reply re = new ES03_reply();
            try
            {
                //need to sync system time
                Dictionary<string, string> val = new Dictionary<string, string>();
                val.Add("eqpid", es.eqpid);
                val.Add("report_time", es.report_time);
                val.Add("heartbeat", es.heartbeat);

                MsgEventArg msg = new MsgEventArg();
                msg._value = val;
                ToVcs(msg);

                //reply
                
                re.ack = "0";
                re.desc = "Accept";
                return re;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ER01_reply ReportMove(ER01 es)
        {
            ER01_reply re = new ER01_reply();
            try
            {
                //report move
                Dictionary<string, string> val = new Dictionary<string, string>();
                val.Add("eqpid", es.eqpid);
                val.Add("report_time", es.report_time);
                val.Add("movestate", es.movestate);
                MsgEventArg msg = new MsgEventArg();
                msg._value = val;
                ToVcs(msg);

                //reply

                return re;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ER02_reply ReportCarrierTransferState(ER02 es)
        {
            ER02_reply re = new ER02_reply();
            try
            {
                //report carrier transfer state
                Dictionary<string, string> val = new Dictionary<string, string>();
                val.Add("eqpid", es.eqpid);
                val.Add("report_time", es.report_time);
                val.Add("transferstate", es.transferstate);
                MsgEventArg msg = new MsgEventArg();
                msg._value = val;
                ToVcs(msg);

                //reply

                return re;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ER03_reply ReportChargeProgress(ER03 es)
        {
            ER03_reply re = new ER03_reply();
            try
            {
                //report charge progress 
                Dictionary<string, string> val = new Dictionary<string, string>();
                val.Add("eqpid", es.eqpid);
                val.Add("report_time", es.report_time);
                val.Add("progress", es.progress);
                MsgEventArg msg = new MsgEventArg();
                msg._value = val;
                ToVcs(msg);

                //reply
                re.eqpid = Equipment.eqpID;
                re.ack = "";
                re.desc = "";
                return re;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ER04_reply ReportChargeState(ER04 es)
        {
            ER04_reply re = new ER04_reply();
            try
            {
                //report charge state 
                Dictionary<string, string> val = new Dictionary<string, string>();
                val.Add("eqpid", es.eqpid);
                val.Add("report_time", es.report_time);
                val.Add("chargestate", es.chargestate);
                MsgEventArg msg = new MsgEventArg();
                ToVcs(msg);
                
                //reply

                return re;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public HC01_reply DateTimeSync(HC01 hc)
        {
            HC01_reply re = new HC01_reply();
            try
            {
                //need to sync system time
                Dictionary<string, string> val = new Dictionary<string, string>();
                re.ack = "0";
                re.desc = "Accept";
                if (hc.eqpid == Equipment.eqpID)
                {
                    val.Add("synctime", hc.synctime);
                    MsgEventArg msg = new MsgEventArg();
                    msg._cmdCode = "HC01";
                    msg._value = val;
                    SCWork(msg);
                }
                else
                {
                    re.ack = "1";
                    re.desc = "EqpID not match.";
                }
                //report
                re.eqpid = Equipment.eqpID;
                re.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                re.systimetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                return re;
            }
            catch (Exception ex)
            {
                re.eqpid = Equipment.eqpID;
                re.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                re.ack = "1";
                re.desc = ex.Message;
                re.systimetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                return re;
                //throw;
            }
            
        }
        public HC02_reply Move(HC02 hc)
        {
            HC02_reply re = new HC02_reply();
            try
            {
                //call action move 
                Dictionary<string, string> val = new Dictionary<string, string>();
                val.Add("MCkey", hc.cmdid);
                val.Add("ShelfHeight", "0");
                val.Add("ShelfWidth", "0");
                val.Add("Platform", Equipment.eqpID);
                val.Add("Pier", "0000");
                val.Add("TRow", hc.to);
                val.Add("TBase", hc.to);
                val.Add("TLayer", hc.to);
                MsgEventArg msg = new MsgEventArg();
                msg._cmdCode = "HC02";
                SCWork(msg);
                
                //report
                re.eqpid = Equipment.eqpID;
                re.cmdid = hc.cmdid;
                re.ack = "0";
                re.desc = "Accept";
                return re;
            }
            catch (Exception ex)
            {
                re.eqpid = Equipment.eqpID;
                re.cmdid = hc.cmdid;
                re.ack = "1";
                re.desc = ex.Message;
                return re;
                //throw;
            }
        }
        public HC03_reply Transfer(HC03 hc)
        {
            HC03_reply re = new HC03_reply();
            try
            {
                //call action transfer
                MsgEventArg msg = new MsgEventArg();
                msg._cmdCode = "HC03";
                SCWork(msg);

                //report
                re.eqpid = Equipment.eqpID;
                re.cmdid = hc.cmdid;
                re.ack = "0";
                re.desc = "Accept";
                return re;
            }
            catch (Exception ex)
            {
                re.eqpid = Equipment.eqpID;
                re.cmdid = hc.cmdid;
                re.ack = "1";
                re.desc = ex.Message;
                return re;
                throw;
            }
        }
        public HC04_reply Scan(HC04 hc)
        {
            HC04_reply re = new HC04_reply();
            try
            {
                //call action scan
                MsgEventArg msg = new MsgEventArg();
                msg._cmdCode = "HC04";
                SCWork(msg);

                //report
                re.eqpid = Equipment.eqpID;
                re.result = Equipment.barCode;
                re.ack = "0";
                re.desc = "Accept";
                return re;
            }
            catch (Exception ex)
            {
                re.eqpid = Equipment.eqpID;
                re.result = "";
                re.ack = "1";
                re.desc = ex.Message;
                return re;
                //throw;
            }
        }
        public HC05_reply Position(HC05 hc)
        {
            HC05_reply re = new HC05_reply();
            try
            {
                //report position
                //MsgEventArg msg = new MsgEventArg();
                //msg._cmdCode = "HC05";
                //SCWork(msg);

                //report
                re.eqpid = Equipment.eqpID;
                re.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                re.position = Equipment.row + "-" + Equipment.bas + "-" + Equipment.layer;
                re.ack = "0";
                re.desc = "Accept";
                return re;
            }
            catch (Exception ex)
            {
                re.eqpid = Equipment.eqpID;
                re.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                re.position = Equipment.row + "-" + Equipment.bas + "-" + Equipment.layer;
                re.ack = "1";
                re.desc = ex.Message;
                return re;
                throw;
            }
        }
        public HC06_reply TerminalDisplayMsg(HC06 hc)
        {
            HC06_reply re = new HC06_reply();
            try
            {
                //call Terminal display
                re.ack = "0";
                re.desc = "Accept";
                if (hc.eqpid == Equipment.eqpID)
                {
                    MsgEventArg msg = new MsgEventArg();
                    msg._cmdCode = "HC06";
                    msg._message = hc.displaymsg;
                    SCWork(msg);
                }
                else
                {
                    re.ack = "1";
                    re.desc = "Eqp ID not match.";
                }
                //report
                re.eqpid = Equipment.eqpID;
                re.ack = "0";
                re.desc = "Accept";
                return re;
            }
            catch (Exception ex)
            {
                re.eqpid = Equipment.eqpID;
                re.ack = "1";
                re.desc = ex.Message;
                return re;
                throw;
            }
        }
        public HC07_reply QueryEQPStatus(HC07 hc)
        {
            HC07_reply re = new HC07_reply();
            try
            {
                //Query eqp status
                //MsgEventArg msg = new MsgEventArg();
                //msg._cmdCode = "HC07";
                //SCWork(msg);

                //report
                re.eqpid = Equipment.eqpID;
                re.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                re.ack = "0";
                re.desc = "Accept";
                re.eqstate = "";
                re.linkmode = "";
                re.operationmode = "";
                if(Equipment.eqpstatus.Substring(5,1).Equals("1"))
                    re.plcbattery = "NOMAL";
                else if(Equipment.eqpstatus.Substring(6, 1).Equals("1"))
                    re.plcbattery = "LOW";
                re.battery = Equipment.battery;
                re.palletid = "";
                if(Equipment.loadStatus.Equals("1"))
                    re.isempty = "load ";
                else
                    re.isempty = "not load ";
                re.idt = "";
                return re;
            }
            catch (Exception ex)
            {
                re.eqpid = Equipment.eqpID;
                re.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                re.ack = "1";
                re.desc = ex.Message;
                re.eqstate = "";
                re.linkmode = "";
                re.operationmode = "";
                if (Equipment.eqpstatus.Substring(5, 1).Equals("1"))
                    re.plcbattery = "NOMAL";
                else if (Equipment.eqpstatus.Substring(6, 1).Equals("1"))
                    re.plcbattery = "LOW";
                re.battery = Equipment.battery;
                re.palletid = "";
                if (Equipment.loadStatus.Equals("1"))
                    re.isempty = "load ";
                else
                    re.isempty = "not load ";
                re.idt = "";
                return re;
                throw;
            }
        }
        public HC08_reply QueryAllAlarm(HC08 hc)
        {
            HC08_reply re = new HC08_reply();
            try
            {
                //call Terminal display
                //MsgEventArg msg = new MsgEventArg();
                //msg._cmdCode = "HC08";
                //SCWork(msg);

                //report
                re.eqpid = Equipment.eqpID;
                re.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                re.awstate = "";
                re.awlevel = "";
                re.errcode = Equipment.eqpErrorCode;
                re.errdesc = "";

                return re;
            }
            catch (Exception ex)
            {
                re.eqpid = Equipment.eqpID;
                re.report_time = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
                re.awstate = "";
                re.awlevel = "";
                re.errcode = "";
                re.errdesc = "";

                return re;
                throw;
            }
        }
        public HC09_reply Charge(HC09 hc)
        {
            HC09_reply re = new HC09_reply();
            try
            {
                Dictionary<string, string> val = new Dictionary<string, string>();
                if (hc.eqpid == Equipment.eqpID)
                {
                    //call charge
                    val.Add("action", hc.action);
                    MsgEventArg msg = new MsgEventArg();
                    msg._cmdCode = "HC09";
                    SCWork(msg);
                    re.ack = "0";
                    re.desc = "Accept";
                }
                else
                {
                    re.ack = "1";
                    re.desc = "Eqp ID not match.[" + hc.eqpid + "]";
                }
                //report
                re.eqpid = Equipment.eqpID;
                
                return re;
            }
            catch (Exception ex)
            {
                re.eqpid = Equipment.eqpID;
                re.ack = "1";
                re.desc = ex.Message;
                return re;
                throw;
            }
        }

        public void Dispatch(string cmdcode, Dictionary<string, string> dic)
        {
            try
            {
                MsgEventArg ms = new MsgEventArg();
                ms._cmdCode = cmdcode;
                ms._value = dic;
                SCWork(ms);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void waitack()
        {
            while (!cmdack)
            {
            }

        }
        public bool HandelVcsMsg(byte[] bt)
        {
            try
            {
                //string data = Encoding.ASCII.GetString(bt);
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(data);
                //XDocument xml = XDocument.Parse(data);
                //var xmldic = xml.Descendants();
                //Dictionary<string, string> cont = new Dictionary<string, string>();
                //Dictionary<string, Dictionary<string, Dictionary<string, string>>> xmlvalue = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
                //XmlNodeList xmlnode = doc.GetElementsByTagName("*");
                //foreach (var v in xmlnode)
                //{

                //}
                //Dispatch("", cont);
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }
        
        
    }
}
