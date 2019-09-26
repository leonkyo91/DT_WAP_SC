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
    public partial class FormBankCmd : Form
    {
        public string function { get; set; }
        public Dictionary<string, string> dc = new Dictionary<string, string>();
        public FormBankCmd()
        {
            InitializeComponent();
        }

        private void FormBankCmd_Load(object sender, EventArgs e)
        {
            this.Text = function;
            txtEQPID.Text = Equipment.eqpID;
            txtPlatform.Text = Equipment.eqpID;
            if (function.Equals("BankIn"))
            {
                lblLayer.Text = "To Layer";
                lblRow.Text = "To Row";
                lblBase.Text = "To Base";
            }
            else
            {
                lblLayer.Text = "From Layer";
                lblRow.Text = "From Row";
                lblBase.Text = "From Base";
            }
        }

        private void BtnSent_Click(object sender, EventArgs e)
        {
            try
            {
                dc.Add("TLayer", txtLayer.Text);
                dc.Add("TRow", txtRow.Text);
                dc.Add("TBase", txtBase.Text);
                dc.Add("ShelfHeight", txtShelfHeight.Text);
                dc.Add("ShelfWidth", txtShelfWidth.Text);
                dc.Add("Platform", txtPlatform.Text);
                dc.Add("Pier", txtPier.Text);
                dc.Add("MCkey", "0000");
                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private void Btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
