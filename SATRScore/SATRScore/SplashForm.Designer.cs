namespace SATRScore
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.VersionLabel = new System.Windows.Forms.Label();
            this.lblRFConnect = new System.Windows.Forms.Label();
            this.ForceConnectionChk = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.Location = new System.Drawing.Point(0, 0);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(1366, 768);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            this.PictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.VersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.ForeColor = System.Drawing.Color.White;
            this.VersionLabel.Location = new System.Drawing.Point(535, 605);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(129, 29);
            this.VersionLabel.TabIndex = 4;
            this.VersionLabel.Text = "version 1.0";
            // 
            // lblRFConnect
            // 
            this.lblRFConnect.AutoSize = true;
            this.lblRFConnect.BackColor = System.Drawing.Color.Transparent;
            this.lblRFConnect.Location = new System.Drawing.Point(537, 649);
            this.lblRFConnect.Name = "lblRFConnect";
            this.lblRFConnect.Size = new System.Drawing.Size(124, 13);
            this.lblRFConnect.TabIndex = 5;
            this.lblRFConnect.Text = "Connecting to RF USB...";
            // 
            // ForceConnectionChk
            // 
            this.ForceConnectionChk.AutoSize = true;
            this.ForceConnectionChk.BackColor = System.Drawing.Color.Transparent;
            this.ForceConnectionChk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ForceConnectionChk.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ForceConnectionChk.Location = new System.Drawing.Point(12, 12);
            this.ForceConnectionChk.Name = "ForceConnectionChk";
            this.ForceConnectionChk.Size = new System.Drawing.Size(12, 11);
            this.ForceConnectionChk.TabIndex = 6;
            this.ForceConnectionChk.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(30, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Force USB RF connection";
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ForceConnectionChk);
            this.Controls.Add(this.lblRFConnect);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.PictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.SplashScreen_Activated);
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.Shown += new System.EventHandler(this.SplashScreen_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label lblRFConnect;
        private System.Windows.Forms.CheckBox ForceConnectionChk;
        private System.Windows.Forms.Label label1;
    }
}