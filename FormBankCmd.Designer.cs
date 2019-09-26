namespace DT_WAP_SC
{
    partial class FormBankCmd
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
            this.btnclose = new HslCommunication.Controls.UserButton();
            this.btnSent = new HslCommunication.Controls.UserButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEQPID = new System.Windows.Forms.TextBox();
            this.txtLayer = new System.Windows.Forms.TextBox();
            this.lblLayer = new System.Windows.Forms.Label();
            this.lblShelfHeight = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPier = new System.Windows.Forms.Label();
            this.txtShelfHeight = new System.Windows.Forms.TextBox();
            this.txtShelfWidth = new System.Windows.Forms.TextBox();
            this.txtPlatform = new System.Windows.Forms.TextBox();
            this.txtPier = new System.Windows.Forms.TextBox();
            this.lblRow = new System.Windows.Forms.Label();
            this.txtRow = new System.Windows.Forms.TextBox();
            this.txtBase = new System.Windows.Forms.TextBox();
            this.lblBase = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel1.Controls.Add(this.btnclose, 2, 2);
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
            // btnclose
            // 
            this.btnclose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnclose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnclose.BackColor = System.Drawing.Color.Transparent;
            this.btnclose.CustomerInformation = "";
            this.btnclose.EnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.btnclose.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnclose.Location = new System.Drawing.Point(424, 523);
            this.btnclose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(104, 25);
            this.btnclose.TabIndex = 0;
            this.btnclose.UIText = "Close";
            this.btnclose.Click += new System.EventHandler(this.Btnclose_Click);
            // 
            // btnSent
            // 
            this.btnSent.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtEQPID, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtLayer, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblLayer, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblShelfHeight, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.lblPier, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.txtShelfHeight, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtShelfWidth, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.txtPlatform, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.txtPier, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblRow, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtRow, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtBase, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblBase, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(520, 488);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "EQP";
            // 
            // txtEQPID
            // 
            this.txtEQPID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEQPID.Location = new System.Drawing.Point(176, 11);
            this.txtEQPID.Name = "txtEQPID";
            this.txtEQPID.ReadOnly = true;
            this.txtEQPID.Size = new System.Drawing.Size(341, 25);
            this.txtEQPID.TabIndex = 1;
            // 
            // txtLayer
            // 
            this.txtLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLayer.Location = new System.Drawing.Point(176, 59);
            this.txtLayer.Name = "txtLayer";
            this.txtLayer.Size = new System.Drawing.Size(341, 25);
            this.txtLayer.TabIndex = 3;
            this.txtLayer.Text = "00";
            // 
            // lblLayer
            // 
            this.lblLayer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(3, 64);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(56, 15);
            this.lblLayer.TabIndex = 4;
            this.lblLayer.Text = "ToLayer";
            // 
            // lblShelfHeight
            // 
            this.lblShelfHeight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblShelfHeight.AutoSize = true;
            this.lblShelfHeight.Location = new System.Drawing.Point(3, 208);
            this.lblShelfHeight.Name = "lblShelfHeight";
            this.lblShelfHeight.Size = new System.Drawing.Size(75, 15);
            this.lblShelfHeight.TabIndex = 8;
            this.lblShelfHeight.Text = "ShelfHeight";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "ShelfWidth";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Platform";
            // 
            // lblPier
            // 
            this.lblPier.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPier.AutoSize = true;
            this.lblPier.Location = new System.Drawing.Point(3, 352);
            this.lblPier.Name = "lblPier";
            this.lblPier.Size = new System.Drawing.Size(30, 15);
            this.lblPier.TabIndex = 11;
            this.lblPier.Text = "Pier";
            // 
            // txtShelfHeight
            // 
            this.txtShelfHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShelfHeight.Location = new System.Drawing.Point(176, 203);
            this.txtShelfHeight.Name = "txtShelfHeight";
            this.txtShelfHeight.Size = new System.Drawing.Size(341, 25);
            this.txtShelfHeight.TabIndex = 12;
            this.txtShelfHeight.Text = "0";
            // 
            // txtShelfWidth
            // 
            this.txtShelfWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShelfWidth.Location = new System.Drawing.Point(176, 251);
            this.txtShelfWidth.Name = "txtShelfWidth";
            this.txtShelfWidth.Size = new System.Drawing.Size(341, 25);
            this.txtShelfWidth.TabIndex = 13;
            this.txtShelfWidth.Text = "0";
            // 
            // txtPlatform
            // 
            this.txtPlatform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlatform.Location = new System.Drawing.Point(176, 299);
            this.txtPlatform.Name = "txtPlatform";
            this.txtPlatform.Size = new System.Drawing.Size(341, 25);
            this.txtPlatform.TabIndex = 14;
            this.txtPlatform.Text = "0000";
            // 
            // txtPier
            // 
            this.txtPier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPier.Location = new System.Drawing.Point(176, 347);
            this.txtPier.Name = "txtPier";
            this.txtPier.Size = new System.Drawing.Size(341, 25);
            this.txtPier.TabIndex = 15;
            this.txtPier.Text = "0000";
            // 
            // lblRow
            // 
            this.lblRow.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRow.AutoSize = true;
            this.lblRow.Location = new System.Drawing.Point(3, 160);
            this.lblRow.Name = "lblRow";
            this.lblRow.Size = new System.Drawing.Size(49, 15);
            this.lblRow.TabIndex = 2;
            this.lblRow.Text = "ToRow";
            // 
            // txtRow
            // 
            this.txtRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRow.Location = new System.Drawing.Point(176, 155);
            this.txtRow.Name = "txtRow";
            this.txtRow.Size = new System.Drawing.Size(341, 25);
            this.txtRow.TabIndex = 5;
            this.txtRow.Text = "00";
            // 
            // txtBase
            // 
            this.txtBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBase.Location = new System.Drawing.Point(176, 107);
            this.txtBase.Name = "txtBase";
            this.txtBase.Size = new System.Drawing.Size(341, 25);
            this.txtBase.TabIndex = 4;
            this.txtBase.Text = "00";
            // 
            // lblBase
            // 
            this.lblBase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBase.AutoSize = true;
            this.lblBase.Location = new System.Drawing.Point(3, 112);
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size(49, 15);
            this.lblBase.TabIndex = 6;
            this.lblBase.Text = "ToBase";
            // 
            // FormBankCmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 553);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormBankCmd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bank";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormBankCmd_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private HslCommunication.Controls.UserButton btnclose;
        private HslCommunication.Controls.UserButton btnSent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEQPID;
        private System.Windows.Forms.Label lblRow;
        private System.Windows.Forms.TextBox txtLayer;
        private System.Windows.Forms.Label lblLayer;
        private System.Windows.Forms.TextBox txtRow;
        private System.Windows.Forms.Label lblBase;
        private System.Windows.Forms.TextBox txtBase;
        private System.Windows.Forms.Label lblShelfHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPier;
        private System.Windows.Forms.TextBox txtShelfHeight;
        private System.Windows.Forms.TextBox txtShelfWidth;
        private System.Windows.Forms.TextBox txtPlatform;
        private System.Windows.Forms.TextBox txtPier;
    }
}