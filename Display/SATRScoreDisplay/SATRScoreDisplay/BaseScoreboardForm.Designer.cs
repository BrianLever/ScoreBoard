namespace SATRScoreDisplay
{
    partial class BaseScoreboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseScoreboard));
            this.CloseAppPicture = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.TimeofDayBtn = new System.Windows.Forms.Button();
            this.GameTimeBtn = new System.Windows.Forms.Button();
            this.MaximiseBtn = new System.Windows.Forms.PictureBox();
            this.CheckMissionTimer = new System.Windows.Forms.Timer(this.components);
            this.SecondTickTimer = new System.Windows.Forms.Timer(this.components);
            this.TestPhraseBtn = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.GameTimeSetBtn = new System.Windows.Forms.Button();
            this.RefrehGridTimer = new System.Windows.Forms.Timer(this.components);
            this.TimeLeftLabel = new System.Windows.Forms.Label();
            this.ScorePicture = new System.Windows.Forms.PictureBox();
            this.WaitSyncTimer = new System.Windows.Forms.Timer(this.components);
            this.SyncPicture = new System.Windows.Forms.PictureBox();
            this.PortOpen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaximiseBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScorePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SyncPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseAppPicture
            // 
            this.CloseAppPicture.BackColor = System.Drawing.Color.Transparent;
            this.CloseAppPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseAppPicture.BackgroundImage")));
            this.CloseAppPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseAppPicture.Location = new System.Drawing.Point(3696, 2069);
            this.CloseAppPicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CloseAppPicture.Name = "CloseAppPicture";
            this.CloseAppPicture.Size = new System.Drawing.Size(134, 137);
            this.CloseAppPicture.TabIndex = 3;
            this.CloseAppPicture.TabStop = false;
            this.toolTip1.SetToolTip(this.CloseAppPicture, "Close Application");
            this.CloseAppPicture.Click += new System.EventHandler(this.CloseAppPicture_Click);
            // 
            // TimeofDayBtn
            // 
            this.TimeofDayBtn.BackColor = System.Drawing.Color.Transparent;
            this.TimeofDayBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TimeofDayBtn.BackgroundImage")));
            this.TimeofDayBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TimeofDayBtn.FlatAppearance.BorderSize = 0;
            this.TimeofDayBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TimeofDayBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 44.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeofDayBtn.ForeColor = System.Drawing.Color.White;
            this.TimeofDayBtn.Location = new System.Drawing.Point(2715, 2077);
            this.TimeofDayBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TimeofDayBtn.Name = "TimeofDayBtn";
            this.TimeofDayBtn.Size = new System.Drawing.Size(453, 123);
            this.TimeofDayBtn.TabIndex = 12;
            this.TimeofDayBtn.Text = "00:00:00";
            this.TimeofDayBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.TimeofDayBtn, "Current Time");
            this.TimeofDayBtn.UseVisualStyleBackColor = false;
            // 
            // GameTimeBtn
            // 
            this.GameTimeBtn.BackColor = System.Drawing.Color.Transparent;
            this.GameTimeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GameTimeBtn.BackgroundImage")));
            this.GameTimeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GameTimeBtn.FlatAppearance.BorderSize = 0;
            this.GameTimeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GameTimeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 44.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameTimeBtn.ForeColor = System.Drawing.Color.White;
            this.GameTimeBtn.Location = new System.Drawing.Point(3218, 2077);
            this.GameTimeBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GameTimeBtn.Name = "GameTimeBtn";
            this.GameTimeBtn.Size = new System.Drawing.Size(453, 123);
            this.GameTimeBtn.TabIndex = 11;
            this.GameTimeBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.GameTimeBtn, "Time Left");
            this.GameTimeBtn.UseVisualStyleBackColor = false;
            // 
            // MaximiseBtn
            // 
            this.MaximiseBtn.BackColor = System.Drawing.Color.Transparent;
            this.MaximiseBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MaximiseBtn.BackgroundImage")));
            this.MaximiseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MaximiseBtn.Location = new System.Drawing.Point(3760, 0);
            this.MaximiseBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximiseBtn.Name = "MaximiseBtn";
            this.MaximiseBtn.Size = new System.Drawing.Size(75, 77);
            this.MaximiseBtn.TabIndex = 20;
            this.MaximiseBtn.TabStop = false;
            this.toolTip1.SetToolTip(this.MaximiseBtn, "Maximise the Screen");
            this.MaximiseBtn.Click += new System.EventHandler(this.MaximiseBtn_Click);
            // 
            // CheckMissionTimer
            // 
            this.CheckMissionTimer.Enabled = true;
            this.CheckMissionTimer.Interval = 5000;
            this.CheckMissionTimer.Tick += new System.EventHandler(this.CheckMissionTimer_Tick);
            // 
            // SecondTickTimer
            // 
            this.SecondTickTimer.Enabled = true;
            this.SecondTickTimer.Interval = 1000;
            this.SecondTickTimer.Tick += new System.EventHandler(this.SecondTickTimer_Tick);
            // 
            // TestPhraseBtn
            // 
            this.TestPhraseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestPhraseBtn.Location = new System.Drawing.Point(18, 3);
            this.TestPhraseBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TestPhraseBtn.Name = "TestPhraseBtn";
            this.TestPhraseBtn.Size = new System.Drawing.Size(207, 49);
            this.TestPhraseBtn.TabIndex = 13;
            this.TestPhraseBtn.Text = " Phrase ";
            this.TestPhraseBtn.UseVisualStyleBackColor = true;
            this.TestPhraseBtn.Click += new System.EventHandler(this.TestPhraseBtn_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.StatusLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.White;
            this.StatusLabel.Location = new System.Drawing.Point(18, 1815);
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(3780, 169);
            this.StatusLabel.TabIndex = 14;
            this.StatusLabel.Text = resources.GetString("StatusLabel.Text");
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.StatusLabel.Visible = false;
            this.StatusLabel.Click += new System.EventHandler(this.StatusLabel_Click);
            // 
            // GameTimeSetBtn
            // 
            this.GameTimeSetBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameTimeSetBtn.Location = new System.Drawing.Point(234, 0);
            this.GameTimeSetBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GameTimeSetBtn.Name = "GameTimeSetBtn";
            this.GameTimeSetBtn.Size = new System.Drawing.Size(135, 55);
            this.GameTimeSetBtn.TabIndex = 15;
            this.GameTimeSetBtn.Text = "Set Game Time";
            this.GameTimeSetBtn.UseVisualStyleBackColor = true;
            this.GameTimeSetBtn.Click += new System.EventHandler(this.GameTimeSetBtn_Click);
            // 
            // RefrehGridTimer
            // 
            this.RefrehGridTimer.Enabled = true;
            this.RefrehGridTimer.Interval = 5000;
            this.RefrehGridTimer.Tick += new System.EventHandler(this.RefrehGridTimer_Tick);
            // 
            // TimeLeftLabel
            // 
            this.TimeLeftLabel.AutoSize = true;
            this.TimeLeftLabel.BackColor = System.Drawing.Color.Transparent;
            this.TimeLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLeftLabel.ForeColor = System.Drawing.Color.White;
            this.TimeLeftLabel.Location = new System.Drawing.Point(1512, 263);
            this.TimeLeftLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TimeLeftLabel.Name = "TimeLeftLabel";
            this.TimeLeftLabel.Size = new System.Drawing.Size(629, 163);
            this.TimeLeftLabel.TabIndex = 17;
            this.TimeLeftLabel.Text = "00:14:00";
            this.TimeLeftLabel.Visible = false;
            // 
            // ScorePicture
            // 
            this.ScorePicture.BackColor = System.Drawing.Color.Transparent;
            this.ScorePicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ScorePicture.BackgroundImage")));
            this.ScorePicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ScorePicture.Location = new System.Drawing.Point(2464, 432);
            this.ScorePicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ScorePicture.Name = "ScorePicture";
            this.ScorePicture.Size = new System.Drawing.Size(1334, 754);
            this.ScorePicture.TabIndex = 18;
            this.ScorePicture.TabStop = false;
            this.ScorePicture.Visible = false;
            // 
            // WaitSyncTimer
            // 
            this.WaitSyncTimer.Interval = 3000;
            this.WaitSyncTimer.Tick += new System.EventHandler(this.WaitSyncTimer_Tick);
            // 
            // SyncPicture
            // 
            this.SyncPicture.BackColor = System.Drawing.Color.Transparent;
            this.SyncPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SyncPicture.BackgroundImage")));
            this.SyncPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SyncPicture.Location = new System.Drawing.Point(1656, 692);
            this.SyncPicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SyncPicture.Name = "SyncPicture";
            this.SyncPicture.Size = new System.Drawing.Size(380, 386);
            this.SyncPicture.TabIndex = 19;
            this.SyncPicture.TabStop = false;
            this.SyncPicture.Visible = false;
            // 
            // PortOpen
            // 
            this.PortOpen.Location = new System.Drawing.Point(394, 11);
            this.PortOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PortOpen.Name = "PortOpen";
            this.PortOpen.Size = new System.Drawing.Size(178, 45);
            this.PortOpen.TabIndex = 21;
            this.PortOpen.Text = "Check Port";
            this.PortOpen.UseVisualStyleBackColor = true;
            this.PortOpen.Visible = false;
            this.PortOpen.Click += new System.EventHandler(this.PortOpen_Click);
            // 
            // BaseScoreboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(3161, 1480);
            this.Controls.Add(this.PortOpen);
            this.Controls.Add(this.MaximiseBtn);
            this.Controls.Add(this.SyncPicture);
            this.Controls.Add(this.TimeLeftLabel);
            this.Controls.Add(this.GameTimeSetBtn);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.TestPhraseBtn);
            this.Controls.Add(this.TimeofDayBtn);
            this.Controls.Add(this.GameTimeBtn);
            this.Controls.Add(this.CloseAppPicture);
            this.Controls.Add(this.ScorePicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BaseScoreboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SATRScore";
            this.Load += new System.EventHandler(this.BaseScoreboard_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BaseScoreboard_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BaseScoreboard_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BaseScoreboard_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaximiseBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScorePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SyncPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox CloseAppPicture;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer CheckMissionTimer;
        private System.Windows.Forms.Button TimeofDayBtn;
        private System.Windows.Forms.Button GameTimeBtn;
        private System.Windows.Forms.Timer SecondTickTimer;
        private System.Windows.Forms.Button TestPhraseBtn;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button GameTimeSetBtn;
        private System.Windows.Forms.Timer RefrehGridTimer;
        private System.Windows.Forms.Label TimeLeftLabel;
        private System.Windows.Forms.PictureBox ScorePicture;
        private System.Windows.Forms.Timer WaitSyncTimer;
        private System.Windows.Forms.PictureBox SyncPicture;
        private System.Windows.Forms.PictureBox MaximiseBtn;
        private System.Windows.Forms.Button PortOpen;
    }
}