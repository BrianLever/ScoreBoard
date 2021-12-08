namespace SATRScore
{
    partial class SynchroniseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SynchroniseForm));
            this.BackBtn = new System.Windows.Forms.PictureBox();
            this.HomeBtn = new System.Windows.Forms.PictureBox();
            this.DeviceTypePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Mission1Box = new System.Windows.Forms.PictureBox();
            this.Mission1Label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TestResponseBtn = new System.Windows.Forms.Button();
            this.SyncTimer = new System.Windows.Forms.Timer(this.components);
            this.CloseAppPicture = new System.Windows.Forms.PictureBox();
            this.SyncPicture = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ProgressTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HomeBtn)).BeginInit();
            this.DeviceTypePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mission1Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SyncPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // BackBtn
            // 
            this.BackBtn.BackColor = System.Drawing.Color.Transparent;
            this.BackBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BackBtn.BackgroundImage")));
            this.BackBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackBtn.ErrorImage = null;
            this.BackBtn.InitialImage = null;
            this.BackBtn.Location = new System.Drawing.Point(15, 659);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(304, 58);
            this.BackBtn.TabIndex = 9;
            this.BackBtn.TabStop = false;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // HomeBtn
            // 
            this.HomeBtn.BackColor = System.Drawing.Color.Transparent;
            this.HomeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("HomeBtn.BackgroundImage")));
            this.HomeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HomeBtn.ErrorImage = null;
            this.HomeBtn.InitialImage = null;
            this.HomeBtn.Location = new System.Drawing.Point(540, 659);
            this.HomeBtn.Name = "HomeBtn";
            this.HomeBtn.Size = new System.Drawing.Size(304, 58);
            this.HomeBtn.TabIndex = 10;
            this.HomeBtn.TabStop = false;
            this.HomeBtn.Click += new System.EventHandler(this.HomeBtn_Click);
            // 
            // DeviceTypePanel
            // 
            this.DeviceTypePanel.BackColor = System.Drawing.Color.Transparent;
            this.DeviceTypePanel.Controls.Add(this.Mission1Box);
            this.DeviceTypePanel.Controls.Add(this.Mission1Label);
            this.DeviceTypePanel.Controls.Add(this.pictureBox1);
            this.DeviceTypePanel.Location = new System.Drawing.Point(12, 204);
            this.DeviceTypePanel.Name = "DeviceTypePanel";
            this.DeviceTypePanel.Size = new System.Drawing.Size(1326, 440);
            this.DeviceTypePanel.TabIndex = 11;
            this.DeviceTypePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DeviceTypePanel_Paint);
            // 
            // Mission1Box
            // 
            this.Mission1Box.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Mission1Box.BackgroundImage")));
            this.Mission1Box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mission1Box.Location = new System.Drawing.Point(3, 3);
            this.Mission1Box.Name = "Mission1Box";
            this.Mission1Box.Size = new System.Drawing.Size(160, 160);
            this.Mission1Box.TabIndex = 8;
            this.Mission1Box.TabStop = false;
            this.Mission1Box.Click += new System.EventHandler(this.Mission1Box_Click);
            // 
            // Mission1Label
            // 
            this.Mission1Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.Mission1Label.AutoSize = true;
            this.Mission1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mission1Label.ForeColor = System.Drawing.Color.Transparent;
            this.Mission1Label.Location = new System.Drawing.Point(169, 0);
            this.Mission1Label.MinimumSize = new System.Drawing.Size(200, 0);
            this.Mission1Label.Name = "Mission1Label";
            this.Mission1Label.Size = new System.Drawing.Size(200, 166);
            this.Mission1Label.TabIndex = 9;
            this.Mission1Label.Text = "MEDIC BOX";
            this.Mission1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Mission1Label.Click += new System.EventHandler(this.Mission1Label_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(375, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // TestResponseBtn
            // 
            this.TestResponseBtn.Location = new System.Drawing.Point(869, 689);
            this.TestResponseBtn.Name = "TestResponseBtn";
            this.TestResponseBtn.Size = new System.Drawing.Size(97, 28);
            this.TestResponseBtn.TabIndex = 10;
            this.TestResponseBtn.Text = "test Response";
            this.TestResponseBtn.UseVisualStyleBackColor = true;
            this.TestResponseBtn.Click += new System.EventHandler(this.TestResponseBtn_Click);
            // 
            // SyncTimer
            // 
            this.SyncTimer.Interval = 3000;
            this.SyncTimer.Tick += new System.EventHandler(this.SyncTimer_Tick);
            // 
            // CloseAppPicture
            // 
            this.CloseAppPicture.BackColor = System.Drawing.Color.Transparent;
            this.CloseAppPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseAppPicture.BackgroundImage")));
            this.CloseAppPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseAppPicture.Location = new System.Drawing.Point(1258, 650);
            this.CloseAppPicture.Name = "CloseAppPicture";
            this.CloseAppPicture.Size = new System.Drawing.Size(80, 80);
            this.CloseAppPicture.TabIndex = 12;
            this.CloseAppPicture.TabStop = false;
            this.CloseAppPicture.Click += new System.EventHandler(this.CloseAppPicture_Click);
            // 
            // SyncPicture
            // 
            this.SyncPicture.BackColor = System.Drawing.Color.Transparent;
            this.SyncPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SyncPicture.BackgroundImage")));
            this.SyncPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SyncPicture.Location = new System.Drawing.Point(549, 239);
            this.SyncPicture.Name = "SyncPicture";
            this.SyncPicture.Size = new System.Drawing.Size(253, 251);
            this.SyncPicture.TabIndex = 21;
            this.SyncPicture.TabStop = false;
            this.SyncPicture.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(338, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.RightToLeftLayout = true;
            this.progressBar1.Size = new System.Drawing.Size(684, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 22;
            this.progressBar1.Value = 50;
            this.progressBar1.Visible = false;
            // 
            // ProgressTimer
            // 
            this.ProgressTimer.Interval = 2500;
            this.ProgressTimer.Tick += new System.EventHandler(this.ProgresTimer_Tick);
            // 
            // SynchroniseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.SyncPicture);
            this.Controls.Add(this.CloseAppPicture);
            this.Controls.Add(this.DeviceTypePanel);
            this.Controls.Add(this.HomeBtn);
            this.Controls.Add(this.TestResponseBtn);
            this.Controls.Add(this.BackBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SynchroniseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Synchronise Devices";
            this.Load += new System.EventHandler(this.SynchroniseForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SynchroniseForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SynchroniseForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SynchroniseForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HomeBtn)).EndInit();
            this.DeviceTypePanel.ResumeLayout(false);
            this.DeviceTypePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mission1Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SyncPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BackBtn;
        private System.Windows.Forms.PictureBox HomeBtn;
        private System.Windows.Forms.FlowLayoutPanel DeviceTypePanel;
        private System.Windows.Forms.PictureBox Mission1Box;
        private System.Windows.Forms.Label Mission1Label;
        private System.Windows.Forms.Button TestResponseBtn;
        private System.Windows.Forms.Timer SyncTimer;
        private System.Windows.Forms.PictureBox CloseAppPicture;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox SyncPicture;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer ProgressTimer;
    }
}