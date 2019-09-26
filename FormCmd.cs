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
    public partial class FormCmd : Form
    {
        public Dictionary<string, string> data = new Dictionary<string, string>();
        public string function = string.Empty;
        public FormCmd()
        {
            InitializeComponent();
        }

        private void FormMoveCmd_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = function;
                if (function.Equals("Move"))
                {
                    txtFLayer.ReadOnly = true;
                    txtFBase.ReadOnly = true;
                    txtFRow.ReadOnly = true;
                }
                else
                {
                    txtFLayer.ReadOnly = false;
                    txtFBase.ReadOnly = false;
                    txtFRow.ReadOnly = false;
                }

                if (!string.IsNullOrEmpty(Equipment.eqpID))
                {
                    txtEQP.Text = Equipment.eqpID;
                    txtPlatform.Text = Equipment.eqpID;
                }
                txtFLayer.Text = Equipment.layer;
                txtFRow.Text = Equipment.row;
                txtFBase.Text = Equipment.bas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSent_Click(object sender, EventArgs e)
        {
            try
            {
                data.Add("FLayer", txtFLayer.Text);
                data.Add("FRow", txtFRow.Text);
                data.Add("FBase", txtFBase.Text);
                data.Add("TLayer", txtTLayer.Text);
                data.Add("TRow", txtTRow.Text);
                data.Add("TBase", txtTBase.Text);
                data.Add("ShelfHeight", txtshelfheight.Text);
                data.Add("ShelfWidth", txtshelfwidth.Text);
                data.Add("Platform", txtPlatform.Text);
                data.Add("Pier", txtPier.Text);
                data.Add("MCkey", "0000");
                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
