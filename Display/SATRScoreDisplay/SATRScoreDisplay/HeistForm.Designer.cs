namespace SATRScoreDisplay
{
    partial class HeistForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeistForm));
            this.BravoPing = new System.Windows.Forms.PictureBox();
            this.AlphaBalanceLabel = new System.Windows.Forms.Label();
            this.BattleLabel = new System.Windows.Forms.Label();
            this.BravoBalanceLabel = new System.Windows.Forms.Label();
            this.AlphaPing = new System.Windows.Forms.PictureBox();
            this.BravoProgress = new System.Windows.Forms.ProgressBar();
            this.AlphaProgress = new System.Windows.Forms.ProgressBar();
            this.TestResponseChk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.BravoPing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaPing)).BeginInit();
            this.SuspendLayout();
            // 
            // BravoPing
            // 
            this.BravoPing.BackColor = System.Drawing.Color.Transparent;
            this.BravoPing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BravoPing.BackgroundImage")));
            this.BravoPing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BravoPing.Location = new System.Drawing.Point(2442, 1114);
            this.BravoPing.Name = "BravoPing";
            this.BravoPing.Size = new System.Drawing.Size(35, 35);
            this.BravoPing.TabIndex = 25;
            this.BravoPing.TabStop = false;
            // 
            // AlphaBalanceLabel
            // 
            this.AlphaBalanceLabel.BackColor = System.Drawing.Color.Transparent;
            this.AlphaBalanceLabel.Font = new System.Drawing.Font("Roboto Cn", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlphaBalanceLabel.ForeColor = System.Drawing.Color.White;
            this.AlphaBalanceLabel.Location = new System.Drawing.Point(379, 926);
            this.AlphaBalanceLabel.Name = "AlphaBalanceLabel";
            this.AlphaBalanceLabel.Size = new System.Drawing.Size(228, 77);
            this.AlphaBalanceLabel.TabIndex = 23;
            this.AlphaBalanceLabel.Text = "726";
            this.AlphaBalanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BattleLabel
            // 
            this.BattleLabel.AutoSize = true;
            this.BattleLabel.BackColor = System.Drawing.Color.Transparent;
            this.BattleLabel.Font = new System.Drawing.Font("Roboto Cn", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BattleLabel.ForeColor = System.Drawing.Color.White;
            this.BattleLabel.Location = new System.Drawing.Point(1229, 687);
            this.BattleLabel.Name = "BattleLabel";
            this.BattleLabel.Size = new System.Drawing.Size(99, 115);
            this.BattleLabel.TabIndex = 22;
            this.BattleLabel.Text = "1";
            // 
            // BravoBalanceLabel
            // 
            this.BravoBalanceLabel.BackColor = System.Drawing.Color.Transparent;
            this.BravoBalanceLabel.Font = new System.Drawing.Font("Roboto Cn", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BravoBalanceLabel.ForeColor = System.Drawing.Color.White;
            this.BravoBalanceLabel.Location = new System.Drawing.Point(1964, 926);
            this.BravoBalanceLabel.Name = "BravoBalanceLabel";
            this.BravoBalanceLabel.Size = new System.Drawing.Size(255, 77);
            this.BravoBalanceLabel.TabIndex = 26;
            this.BravoBalanceLabel.Text = "12002";
            this.BravoBalanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AlphaPing
            // 
            this.AlphaPing.BackColor = System.Drawing.Color.Transparent;
            this.AlphaPing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AlphaPing.BackgroundImage")));
            this.AlphaPing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AlphaPing.Location = new System.Drawing.Point(72, 1114);
            this.AlphaPing.Name = "AlphaPing";
            this.AlphaPing.Size = new System.Drawing.Size(35, 35);
            this.AlphaPing.TabIndex = 27;
            this.AlphaPing.TabStop = false;
            // 
            // BravoProgress
            // 
            this.BravoProgress.BackColor = System.Drawing.Color.Black;
            this.BravoProgress.Enabled = false;
            this.BravoProgress.ForeColor = System.Drawing.Color.SteelBlue;
            this.BravoProgress.Location = new System.Drawing.Point(996, 993);
            this.BravoProgress.Name = "BravoProgress";
            this.BravoProgress.Size = new System.Drawing.Size(563, 95);
            this.BravoProgress.Step = 1;
            this.BravoProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BravoProgress.TabIndex = 29;
            this.BravoProgress.Value = 46;
            // 
            // AlphaProgress
            // 
            this.AlphaProgress.BackColor = System.Drawing.Color.Black;
            this.AlphaProgress.Enabled = false;
            this.AlphaProgress.ForeColor = System.Drawing.Color.Red;
            this.AlphaProgress.Location = new System.Drawing.Point(996, 892);
            this.AlphaProgress.Name = "AlphaProgress";
            this.AlphaProgress.Size = new System.Drawing.Size(563, 95);
            this.AlphaProgress.Step = 1;
            this.AlphaProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.AlphaProgress.TabIndex = 28;
            this.AlphaProgress.Value = 53;
            // 
            // TestResponseChk
            // 
            this.TestResponseChk.AutoSize = true;
            this.TestResponseChk.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestResponseChk.Location = new System.Drawing.Point(252, 12);
            this.TestResponseChk.Name = "TestResponseChk";
            this.TestResponseChk.Size = new System.Drawing.Size(105, 19);
            this.TestResponseChk.TabIndex = 30;
            this.TestResponseChk.Text = "Test Reponse";
            this.TestResponseChk.UseVisualStyleBackColor = true;
            // 
            // HeistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(2560, 1440);
            this.Controls.Add(this.TestResponseChk);
            this.Controls.Add(this.BravoProgress);
            this.Controls.Add(this.AlphaProgress);
            this.Controls.Add(this.AlphaPing);
            this.Controls.Add(this.BravoBalanceLabel);
            this.Controls.Add(this.BravoPing);
            this.Controls.Add(this.AlphaBalanceLabel);
            this.Controls.Add(this.BattleLabel);
            this.Name = "HeistForm";
            this.Load += new System.EventHandler(this.HeistForm_Load);
            this.Controls.SetChildIndex(this.BattleLabel, 0);
            this.Controls.SetChildIndex(this.AlphaBalanceLabel, 0);
            this.Controls.SetChildIndex(this.BravoPing, 0);
            this.Controls.SetChildIndex(this.BravoBalanceLabel, 0);
            this.Controls.SetChildIndex(this.AlphaPing, 0);
            this.Controls.SetChildIndex(this.AlphaProgress, 0);
            this.Controls.SetChildIndex(this.BravoProgress, 0);
            this.Controls.SetChildIndex(this.TestResponseChk, 0);
            ((System.ComponentModel.ISupportInitialize)(this.BravoPing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaPing)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BravoPing;
        private System.Windows.Forms.Label AlphaBalanceLabel;
        private System.Windows.Forms.Label BattleLabel;
        private System.Windows.Forms.Label BravoBalanceLabel;
        private System.Windows.Forms.PictureBox AlphaPing;
        private System.Windows.Forms.ProgressBar BravoProgress;
        private System.Windows.Forms.ProgressBar AlphaProgress;
        private System.Windows.Forms.CheckBox TestResponseChk;
    }
}
