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
            // MonitorOnlyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(2560, 1440);
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
    }
}