namespace SATRScoreDisplay
{
    partial class MonitorOnlyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorOnlyForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CloseAppPicture = new System.Windows.Forms.PictureBox();
            this.CheckMissionChangeTimer = new System.Windows.Forms.Timer(this.components);
            this.GameTimeBtn = new System.Windows.Forms.Button();
            this.TimeofDayBtn = new System.Windows.Forms.Button();
            this.SecondTickTimer = new System.Windows.Forms.Timer(this.components);
            this.GameTimeSetBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseAppPicture
            // 
            this.CloseAppPicture.BackColor = System.Drawing.Color.Transparent;
            this.CloseAppPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseAppPicture.BackgroundImage")));
            this.CloseAppPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseAppPicture.Location = new System.Drawing.Point(2407, 1185);
            this.CloseAppPicture.Name = "CloseAppPicture";
            this.CloseAppPicture.Size = new System.Drawing.Size(125, 125);
            this.CloseAppPicture.TabIndex = 1;
            this.CloseAppPicture.TabStop = false;
            this.toolTip1.SetToolTip(this.CloseAppPicture, "Close Application");
            this.CloseAppPicture.Click += new System.EventHandler(this.CloseAppPicture_Click);
            // 
            // CheckMissionChangeTimer
            // 
            this.CheckMissionChangeTimer.Enabled = true;
            this.CheckMissionChangeTimer.Interval = 2000;
            this.CheckMissionChangeTimer.Tick += new System.EventHandler(this.CheckMissionChangeTimer_Tick);
            // 
            // GameTimeBtn
            // 
            this.GameTimeBtn.BackColor = System.Drawing.Color.Transparent;
            this.GameTimeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GameTimeBtn.BackgroundImage")));
            this.GameTimeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GameTimeBtn.FlatAppearance.BorderSize = 0;
            this.GameTimeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GameTimeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameTimeBtn.ForeColor = System.Drawing.Color.White;
            this.GameTimeBtn.Location = new System.Drawing.Point(2075, 1201);
            this.GameTimeBtn.Name = "GameTimeBtn";
            this.GameTimeBtn.Size = new System.Drawing.Size(302, 81);
            this.GameTimeBtn.TabIndex = 9;
            this.GameTimeBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.GameTimeBtn.UseVisualStyleBackColor = false;
            // 
            // TimeofDayBtn
            // 
            this.TimeofDayBtn.BackColor = System.Drawing.Color.Transparent;
            this.TimeofDayBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TimeofDayBtn.BackgroundImage")));
            this.TimeofDayBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TimeofDayBtn.FlatAppearance.BorderSize = 0;
            this.TimeofDayBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TimeofDayBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeofDayBtn.ForeColor = System.Drawing.Color.White;
            this.TimeofDayBtn.Location = new System.Drawing.Point(1740, 1201);
            this.TimeofDayBtn.Name = "TimeofDayBtn";
            this.TimeofDayBtn.Size = new System.Drawing.Size(302, 81);
            this.TimeofDayBtn.TabIndex = 10;
            this.TimeofDayBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TimeofDayBtn.UseVisualStyleBackColor = false;
            // 
            // SecondTickTimer
            // 
            this.SecondTickTimer.Enabled = true;
            this.SecondTickTimer.Interval = 1000;
            this.SecondTickTimer.Tick += new System.EventHandler(this.SecondTickTimer_Tick);
            // 
            // GameTimeSetBtn
            // 
            this.GameTimeSetBtn.Location = new System.Drawing.Point(2428, 1316);
            this.GameTimeSetBtn.Name = "GameTimeSetBtn";
            this.GameTimeSetBtn.Size = new System.Drawing.Size(104, 34);
            this.GameTimeSetBtn.TabIndex = 11;
            this.GameTimeSetBtn.Text = "Set Game Time";
            this.GameTimeSetBtn.UseVisualStyleBackColor = true;
            this.GameTimeSetBtn.Click += new System.EventHandler(this.GameTimeSetBtn_Click);
            // 
            // MonitorOnlyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(2560, 1440);
            this.Controls.Add(this.GameTimeSetBtn);
            this.Controls.Add(this.TimeofDayBtn);
            this.Controls.Add(this.GameTimeBtn);
            this.Controls.Add(this.CloseAppPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MonitorOnlyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Monitor Device";
            this.Load += new System.EventHandler(this.MonitorOnlyForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MonitorOnlyForm_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MonitorOnlyForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MonitorOnlyForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MonitorOnlyForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox CloseAppPicture;
        private System.Windows.Forms.Timer CheckMissionChangeTimer;
        private System.Windows.Forms.Button GameTimeBtn;
        private System.Windows.Forms.Button TimeofDayBtn;
        private System.Windows.Forms.Timer SecondTickTimer;
        private System.Windows.Forms.Button GameTimeSetBtn;
    }
}