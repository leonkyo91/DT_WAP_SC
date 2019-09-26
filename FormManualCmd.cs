using ICommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DT_WAP_SC
{
    public partial class FormManualCmd : Form
    {
        public string function = string.Empty;
        public Dictionary<string, string> data = new Dictionary<string, string>();
        //private MsgModel msg = new MsgModel();
        
        public FormManualCmd()
        {
            InitializeComponent();
        }

        private void FormManualCmd_Load(object sender, EventArgs e)
        {
            txtFunctionname.Text = function;
            DataTable dt = new DataTable();
            dt.Columns.Add("KeyName");
            dt.Columns.Add("Bytes");
            dt.Columns.Add("Values");
            foreach(string key in MsgModel._commandType[function].Keys)
            {
                DataRow dr = dt.NewRow();
                dr["KeyName"] = key;
                dr["Bytes"] = MsgModel._commandType[function][key].ToString();
                dt.Rows.Add(dr);
            }
            grdData.DataSource = dt;

        }

        private void GrdData_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.Name.Equals("KeyName") || e.Column.Name.Equals("Bytes"))
            {
                e.Column.ReadOnly = true;
            }
            else
            {
                e.Column.ReadOnly = false;
            }
        }

        private void GrdData_DataSourceChanged(object sender, EventArgs e)
        {
            string triggerTime = DateTime.Now.ToString("HHmmss");
            foreach (DataGridViewRow dr in grdData.Rows)
            {
                if (dr.Cells[0].Value != null)
                    switch (dr.Cells["KeyName"].Value.ToString())
                    {
                        case "STX":
                            dr.Cells["Values"].Value = MsgModel._startPoint;
                            dr.ReadOnly = true;
                            break;
                        case "ETX":
                            dr.Cells["Values"].Value = MsgModel._endPoint;
                            dr.ReadOnly = true;
                            break;
                        case "TriggerTime":
                            dr.Cells["Values"].Value = triggerTime;
                            dr.ReadOnly = true;
                            break;
                        case "Command":
                            dr.Cells["Values"].Value = function;
                            dr.ReadOnly = true;
                            break;
                        case "EqpNo":
                            dr.Cells["Values"].Value = Equipment.eqpID;
                            dr.ReadOnly = true;
                            break;
                        case "SeqNo":
                            dr.Cells["Values"].Value = Equipment.seqno.ToString().PadLeft(4,'0');
                            dr.ReadOnly = true;
                            break;
                        case "BCC":
                            Dictionary<string, string> dt = new Dictionary<string, string>() { { "TriggerTime", triggerTime } };
                            byte[] bbc = new byte[2];
                            bbc = MsgModel.BCC(dt);
                            dr.Cells["Values"].Value = bbc[0].ToString() + bbc[1].ToString();
                            dr.ReadOnly = true;
                            break;
                        case "PlatformNo":
                            dr.Cells["Values"].Value = Equipment.eqpID;
                            dr.ReadOnly = true;
                            break;
                        case "ReSent":
                            dr.Cells["Values"].Value = "0";
                            //dr.ReadOnly = true;
                            break;
                        case "Pier":
                            dr.Cells["Values"].Value = "0000";
                            //dr.ReadOnly = true;
                            break;
                        case "ShelfHeight":
                            dr.Cells["Values"].Value = "0";
                            //dr.ReadOnly = true;
                            break;
                        case "ShelfWidth":
                            dr.Cells["Values"].Value = "0";
                            //dr.ReadOnly = true;
                            break;
                        default:
                            break;
                    }
            }
            
        }

        private void BtnSent_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dr in grdData.Rows)
            {
                if(string.IsNullOrEmpty(dr.Cells["Values"].Value.ToString()))
                {
                    string value = string.Empty;
                    for(int i = 0; i < int.Parse(dr.Cells["Bytes"].Value.ToString()); i++)
                    {
                        value += "0";
                    }
                    data.Add(dr.Cells["KeyName"].Value.ToString(), value);
                }
                else
                {
                    //string value = string.Empty;
                    //if (dr.Cells["Values"].Value.ToString().Length< int.Parse(dr.Cells["Bytes"].Value.ToString()))
                    //{

                    //}
                    data.Add(dr.Cells["KeyName"].Value.ToString(), dr.Cells["Values"].Value.ToString());
                }

            }

            this.DialogResult = DialogResult.OK;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
