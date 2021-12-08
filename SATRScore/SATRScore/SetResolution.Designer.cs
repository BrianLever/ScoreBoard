namespace SATRScore
{
    partial class SetResForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetResForm));
            this.CancelBtn = new System.Windows.Forms.Button();
            this.OKBtn = new System.Windows.Forms.Button();
            this.x_Res = new System.Windows.Forms.TextBox();
            this.y_Res = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GetCurrentBtn = new System.Windows.Forms.Button();
            this.MaxResBtn = new System.Windows.Forms.Button();
            this.LapTopBtn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.MonitorList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDownGrid;
            this.CancelBtn.BackColor = System.Drawing.Color.DimGray;
            this.CancelBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CancelBtn.Font = new System.Drawing.Font("Roboto Cn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.ForeColor = System.Drawing.Color.White;
            this.CancelBtn.Image = ((System.Drawing.Image)(resources.GetObject("CancelBtn.Image")));
            this.CancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelBtn.Location = new System.Drawing.Point(178, 113);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(112, 41);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CancelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // OKBtn
            // 
            this.OKBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDownGrid;
            this.OKBtn.BackColor = System.Drawing.Color.DimGray;
            this.OKBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OKBtn.Font = new System.Drawing.Font("Roboto Cn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKBtn.ForeColor = System.Drawing.Color.White;
            this.OKBtn.Image = ((System.Drawing.Image)(resources.GetObject("OKBtn.Image")));
            this.OKBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OKBtn.Location = new System.Drawing.Point(60, 113);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(112, 41);
            this.OKBtn.TabIndex = 4;
            this.OKBtn.Text = "OK";
            this.OKBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OKBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.OKBtn.UseVisualStyleBackColor = false;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // x_Res
            // 
            this.x_Res.Location = new System.Drawing.Point(178, 42);
            this.x_Res.Name = "x_Res";
            this.x_Res.Size = new System.Drawing.Size(100, 20);
            this.x_Res.TabIndex = 6;
            this.toolTip1.SetToolTip(this.x_Res, "Screen width");
            this.x_Res.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.x_Res_KeyPress);
            // 
            // y_Res
            // 
            this.y_Res.Location = new System.Drawing.Point(178, 68);
            this.y_Res.Name = "y_Res";
            this.y_Res.Size = new System.Drawing.Size(100, 20);
            this.y_Res.TabIndex = 7;
            this.toolTip1.SetToolTip(this.y_Res, "Screen Height");
            this.y_Res.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.y_Res_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Cn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(106, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "x pixels";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Cn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(106, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "y pixels";
            // 
            // GetCurrentBtn
            // 
            this.GetCurrentBtn.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GetCurrentBtn.Location = new System.Drawing.Point(60, 9);
            this.GetCurrentBtn.Name = "GetCurrentBtn";
            this.GetCurrentBtn.Size = new System.Drawing.Size(230, 27);
            this.GetCurrentBtn.TabIndex = 10;
            this.GetCurrentBtn.Text = "Get Current Resolution";
            this.toolTip1.SetToolTip(this.GetCurrentBtn, "Set the resolution to that of the current monitor");
            this.GetCurrentBtn.UseVisualStyleBackColor = true;
            this.GetCurrentBtn.Click += new System.EventHandler(this.GetCurrentBtn_Click);
            // 
            // MaxResBtn
            // 
            this.MaxResBtn.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxResBtn.Location = new System.Drawing.Point(12, 160);
            this.MaxResBtn.Name = "MaxResBtn";
            this.MaxResBtn.Size = new System.Drawing.Size(130, 27);
            this.MaxResBtn.TabIndex = 11;
            this.MaxResBtn.Text = "Max Res.";
            this.toolTip1.SetToolTip(this.MaxResBtn, "Set Resolution of the 2560 x  1440");
            this.MaxResBtn.UseVisualStyleBackColor = true;
            this.MaxResBtn.Click += new System.EventHandler(this.MaxResBtn_Click);
            // 
            // LapTopBtn
            // 
            this.LapTopBtn.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LapTopBtn.Location = new System.Drawing.Point(160, 161);
            this.LapTopBtn.Name = "LapTopBtn";
            this.LapTopBtn.Size = new System.Drawing.Size(130, 27);
            this.LapTopBtn.TabIndex = 12;
            this.LapTopBtn.Text = "Laptop Res";
            this.toolTip1.SetToolTip(this.LapTopBtn, "Set to 1366 x 768 typically used on a laptop");
            this.LapTopBtn.UseVisualStyleBackColor = true;
            this.LapTopBtn.Click += new System.EventHandler(this.LapTopBtn_Click);
            // 
            // MonitorList
            // 
            this.MonitorList.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonitorList.FormattingEnabled = true;
            this.MonitorList.ItemHeight = 19;
            this.MonitorList.Location = new System.Drawing.Point(317, 29);
            this.MonitorList.Name = "MonitorList";
            this.MonitorList.Size = new System.Drawing.Size(164, 156);
            this.MonitorList.TabIndex = 13;
            this.toolTip1.SetToolTip(this.MonitorList, "Double Click to set this resolution ");
            this.MonitorList.DoubleClick += new System.EventHandler(this.MonitorList_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(313, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 19);
            this.label3.TabIndex = 14;
            this.label3.Text = "Monitor List";
            // 
            // SetResForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(498, 200);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MonitorList);
            this.Controls.Add(this.LapTopBtn);
            this.Controls.Add(this.MaxResBtn);
            this.Controls.Add(this.GetCurrentBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.y_Res);
            this.Controls.Add(this.x_Res);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetResForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Main Scoreboard Screen Resolution";
            this.toolTip1.SetToolTip(this, "Set the resolution to be used by SATRDisplay.");
            this.Load += new System.EventHandler(this.SetResForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.TextBox x_Res;
        private System.Windows.Forms.TextBox y_Res;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button GetCurrentBtn;
        private System.Windows.Forms.Button MaxResBtn;
        private System.Windows.Forms.Button LapTopBtn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListBox MonitorList;
        private System.Windows.Forms.Label label3;
    }
}