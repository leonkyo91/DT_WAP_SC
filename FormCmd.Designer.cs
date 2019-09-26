namespace DT_WAP_SC
{
    partial class FormCmd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new HslCommunication.Controls.UserButton();
            this.btnSent = new HslCommunication.Controls.UserButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTLayer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFLayer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEQP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtshelfheight = new System.Windows.Forms.TextBox();
            this.txtshelfwidth = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPlatform = new System.Windows.Forms.TextBox();
            this.txtPier = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFRow = new System.Windows.Forms.TextBox();
            this.txtFBase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTRow = new System.Windows.Forms.TextBox();
            this.txtTBase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSent, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(532, 553);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.CustomerInformation = "";
            this.btnClose.EnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnClose.Location = new System.Drawing.Point(424, 523);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.UIText = "Close";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnSent
            // 
            this.btnSent.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSent.BackColor = System.Drawing.Color.Transparent;
            this.btnSent.CustomerInformation = "";
            this.btnSent.EnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.btnSent.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnSent.Location = new System.Drawing.Point(312, 523);
            this.btnSent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSent.Name = "btnSent";
            this.btnSent.Size = new System.Drawing.Size(104, 25);
            this.btnSent.TabIndex = 1;
            this.btnSent.UIText = "Sent";
            this.btnSent.Click += new System.EventHandler(this.BtnSent_Click);
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 3);
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(526, 512);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66667F));
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.txtTLayer, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtFLayer, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtEQP, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.txtshelfheight, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.txtshelfwidth, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.txtPlatform, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.txtPier, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtFRow, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtFBase, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.txtTRow, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.txtTBase, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 13;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(520, 488);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 289);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "ShelfHeight";
            // 
            // txtTLayer
            // 
            this.txtTLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTLayer.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTLayer.Location = new System.Drawing.Point(176, 161);
            this.txtTLayer.Name = "txtTLayer";
            this.txtTLayer.Size = new System.Drawing.Size(341, 25);
            this.txtTLayer.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "To Layer";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Layer";
            // 
            // txtFLayer
            // 
            this.txtFLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFLayer.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtFLayer.Location = new System.Drawing.Point(176, 38);
            this.txtFLayer.Name = "txtFLayer";
            this.txtFLayer.Size = new System.Drawing.Size(341, 25);
            this.txtFLayer.TabIndex = 1;
            this.txtFLayer.Text = "00";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(136, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "EQP";
            // 
            // txtEQP
            // 
            this.txtEQP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEQP.Location = new System.Drawing.Point(176, 3);
            this.txtEQP.Name = "txtEQP";
            this.txtEQP.ReadOnly = true;
            this.txtEQP.Size = new System.Drawing.Size(341, 25);
            this.txtEQP.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 330);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "ShelfWidth";
            // 
            // txtshelfheight
            // 
            this.txtshelfheight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtshelfheight.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtshelfheight.Location = new System.Drawing.Point(176, 284);
            this.txtshelfheight.Name = "txtshelfheight";
            this.txtshelfheight.Size = new System.Drawing.Size(341, 25);
            this.txtshelfheight.TabIndex = 16;
            this.txtshelfheight.Text = "0";
            // 
            // txtshelfwidth
            // 
            this.txtshelfwidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtshelfwidth.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtshelfwidth.Location = new System.Drawing.Point(176, 325);
            this.txtshelfwidth.Name = "txtshelfwidth";
            this.txtshelfwidth.Size = new System.Drawing.Size(341, 25);
            this.txtshelfwidth.TabIndex = 17;
            this.txtshelfwidth.Text = "0";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 371);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "Platform";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 412);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 15);
            this.label11.TabIndex = 19;
            this.label11.Text = "Pier";
            // 
            // txtPlatform
            // 
            this.txtPlatform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlatform.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPlatform.Location = new System.Drawing.Point(176, 366);
            this.txtPlatform.Name = "txtPlatform";
            this.txtPlatform.Size = new System.Drawing.Size(341, 25);
            this.txtPlatform.TabIndex = 20;
            this.txtPlatform.Text = "0000";
            // 
            // txtPier
            // 
            this.txtPier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPier.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPier.Location = new System.Drawing.Point(176, 407);
            this.txtPier.Name = "txtPier";
            this.txtPier.Size = new System.Drawing.Size(341, 25);
            this.txtPier.TabIndex = 21;
            this.txtPier.Text = "0000";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "From Row";
            // 
            // txtFRow
            // 
            this.txtFRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFRow.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtFRow.Location = new System.Drawing.Point(176, 120);
            this.txtFRow.Name = "txtFRow";
            this.txtFRow.Size = new System.Drawing.Size(341, 25);
            this.txtFRow.TabIndex = 3;
            this.txtFRow.Text = "00";
            // 
            // txtFBase
            // 
            this.txtFBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFBase.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtFBase.Location = new System.Drawing.Point(176, 79);
            this.txtFBase.Name = "txtFBase";
            this.txtFBase.Size = new System.Drawing.Size(341, 25);
            this.txtFBase.TabIndex = 2;
            this.txtFBase.Text = "00";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "From Base";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "To Row";
            // 
            // txtTRow
            // 
            this.txtTRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTRow.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTRow.Location = new System.Drawing.Point(176, 243);
            this.txtTRow.Name = "txtTRow";
            this.txtTRow.Size = new System.Drawing.Size(341, 25);
            this.txtTRow.TabIndex = 6;
            // 
            // txtTBase
            // 
            this.txtTBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTBase.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTBase.Location = new System.Drawing.Point(176, 202);
            this.txtTBase.Name = "txtTBase";
            this.txtTBase.Size = new System.Drawing.Size(341, 25);
            this.txtTBase.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "To Base";
            // 
            // FormCmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 553);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormCmd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Move";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormMoveCmd_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private HslCommunication.Controls.UserButton btnClose;
        private HslCommunication.Controls.UserButton btnSent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtTLayer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFBase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFLayer;
        private System.Windows.Forms.TextBox txtFRow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTRow;
        private System.Windows.Forms.TextBox txtTBase;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEQP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtshelfheight;
        private System.Windows.Forms.TextBox txtshelfwidth;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPlatform;
        private System.Windows.Forms.TextBox txtPier;
    }
}