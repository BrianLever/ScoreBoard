namespace SATRScore
{
    partial class MissionSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MissionSelectForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BackBtn = new System.Windows.Forms.PictureBox();
            this.MissionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Mission1Box = new System.Windows.Forms.PictureBox();
            this.Mission1Label = new System.Windows.Forms.Label();
            this.CloseAppPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).BeginInit();
            this.MissionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mission1Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(517, 654);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(304, 58);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // BackBtn
            // 
            this.BackBtn.BackColor = System.Drawing.Color.Transparent;
            this.BackBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BackBtn.BackgroundImage")));
            this.BackBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackBtn.ErrorImage = null;
            this.BackBtn.InitialImage = null;
            this.BackBtn.Location = new System.Drawing.Point(12, 654);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(304, 58);
            this.BackBtn.TabIndex = 4;
            this.BackBtn.TabStop = false;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // MissionsPanel
            // 
            this.MissionsPanel.BackColor = System.Drawing.Color.Transparent;
            this.MissionsPanel.Controls.Add(this.Mission1Box);
            this.MissionsPanel.Controls.Add(this.Mission1Label);
            this.MissionsPanel.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MissionsPanel.ForeColor = System.Drawing.Color.Transparent;
            this.MissionsPanel.Location = new System.Drawing.Point(36, 197);
            this.MissionsPanel.Name = "MissionsPanel";
            this.MissionsPanel.Size = new System.Drawing.Size(1269, 439);
            this.MissionsPanel.TabIndex = 5;
            this.MissionsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MissionsPanel_Paint);
            // 
            // Mission1Box
            // 
            this.Mission1Box.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Mission1Box.BackgroundImage")));
            this.Mission1Box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Mission1Box.Location = new System.Drawing.Point(3, 3);
            this.Mission1Box.Name = "Mission1Box";
            this.Mission1Box.Size = new System.Drawing.Size(130, 130);
            this.Mission1Box.TabIndex = 0;
            this.Mission1Box.TabStop = false;
            // 
            // Mission1Label
            // 
            this.Mission1Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.Mission1Label.AutoSize = true;
            this.Mission1Label.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mission1Label.Location = new System.Drawing.Point(139, 0);
            this.Mission1Label.MinimumSize = new System.Drawing.Size(200, 0);
            this.Mission1Label.Name = "Mission1Label";
            this.Mission1Label.Size = new System.Drawing.Size(258, 136);
            this.Mission1Label.TabIndex = 7;
            this.Mission1Label.Text = "SINGLE POINT DOMINATION";
            this.Mission1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseAppPicture
            // 
            this.CloseAppPicture.BackColor = System.Drawing.Color.Transparent;
            this.CloseAppPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseAppPicture.BackgroundImage")));
            this.CloseAppPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseAppPicture.Location = new System.Drawing.Point(1258, 642);
            this.CloseAppPicture.Name = "CloseAppPicture";
            this.CloseAppPicture.Size = new System.Drawing.Size(80, 80);
            this.CloseAppPicture.TabIndex = 11;
            this.CloseAppPicture.TabStop = false;
            this.CloseAppPicture.Click += new System.EventHandler(this.CloseAppPicture_Click);
            // 
            // MissionSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.CloseAppPicture);
            this.Controls.Add(this.MissionsPanel);
            this.Controls.Add(this.BackBtn);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MissionSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.MissionSelectForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MissionSelectForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MissionSelectForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MissionSelectForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).EndInit();
            this.MissionsPanel.ResumeLayout(false);
            this.MissionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mission1Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox BackBtn;
        private System.Windows.Forms.FlowLayoutPanel MissionsPanel;
        private System.Windows.Forms.PictureBox Mission1Box;
        private System.Windows.Forms.Label Mission1Label;
        private System.Windows.Forms.PictureBox CloseAppPicture;
    }
}