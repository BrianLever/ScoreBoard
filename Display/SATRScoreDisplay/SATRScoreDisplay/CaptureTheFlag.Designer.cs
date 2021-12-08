namespace SATRScoreDisplay
{
    partial class CaptureTheFlagForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptureTheFlagForm));
            this.BravoProgress = new System.Windows.Forms.ProgressBar();
            this.AlphaProgress = new System.Windows.Forms.ProgressBar();
            this.AlphaPing = new System.Windows.Forms.PictureBox();
            this.BravoPing = new System.Windows.Forms.PictureBox();
            this.AlphaFlagsLabel = new System.Windows.Forms.Label();
            this.BattleLabel = new System.Windows.Forms.Label();
            this.BravoFlagsLabel = new System.Windows.Forms.Label();
            this.TestResponseChk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaPing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BravoPing)).BeginInit();
            this.SuspendLayout();
            // 
            // BravoProgress
            // 
            this.BravoProgress.BackColor = System.Drawing.Color.Black;
            this.BravoProgress.Enabled = false;
            this.BravoProgress.ForeColor = System.Drawing.Color.SteelBlue;
            this.BravoProgress.Location = new System.Drawing.Point(1002, 992);
            this.BravoProgress.Name = "BravoProgress";
            this.BravoProgress.Size = new System.Drawing.Size(563, 95);
            this.BravoProgress.Step = 1;
            this.BravoProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BravoProgress.TabIndex = 35;
            this.BravoProgress.Value = 46;
            // 
            // AlphaProgress
            // 
            this.AlphaProgress.BackColor = System.Drawing.Color.Black;
            this.AlphaProgress.Enabled = false;
            this.AlphaProgress.ForeColor = System.Drawing.Color.Red;
            this.AlphaProgress.Location = new System.Drawing.Point(1002, 891);
            this.AlphaProgress.Name = "AlphaProgress";
            this.AlphaProgress.Size = new System.Drawing.Size(563, 95);
            this.AlphaProgress.Step = 1;
            this.AlphaProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.AlphaProgress.TabIndex = 34;
            this.AlphaProgress.Value = 53;
            // 
            // AlphaPing
            // 
            this.AlphaPing.BackColor = System.Drawing.Color.Transparent;
            this.AlphaPing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AlphaPing.BackgroundImage")));
            this.AlphaPing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AlphaPing.Location = new System.Drawing.Point(78, 1113);
            this.AlphaPing.Name = "AlphaPing";
            this.AlphaPing.Size = new System.Drawing.Size(35, 35);
            this.AlphaPing.TabIndex = 33;
            this.AlphaPing.TabStop = false;
            // 
            // BravoPing
            // 
            this.BravoPing.BackColor = System.Drawing.Color.Transparent;
            this.BravoPing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BravoPing.BackgroundImage")));
            this.BravoPing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BravoPing.Location = new System.Drawing.Point(2448, 1113);
            this.BravoPing.Name = "BravoPing";
            this.BravoPing.Size = new System.Drawing.Size(35, 35);
            this.BravoPing.TabIndex = 31;
            this.BravoPing.TabStop = false;
            // 
            // AlphaFlagsLabel
            // 
            this.AlphaFlagsLabel.AutoSize = true;
            this.AlphaFlagsLabel.BackColor = System.Drawing.Color.Transparent;
            this.AlphaFlagsLabel.Font = new System.Drawing.Font("Roboto", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlphaFlagsLabel.ForeColor = System.Drawing.Color.Red;
            this.AlphaFlagsLabel.Location = new System.Drawing.Point(509, 877);
            this.AlphaFlagsLabel.Name = "AlphaFlagsLabel";
            this.AlphaFlagsLabel.Size = new System.Drawing.Size(159, 115);
            this.AlphaFlagsLabel.TabIndex = 30;
            this.AlphaFlagsLabel.Text = "12";
            // 
            // BattleLabel
            // 
            this.BattleLabel.AutoSize = true;
            this.BattleLabel.BackColor = System.Drawing.Color.Transparent;
            this.BattleLabel.Font = new System.Drawing.Font("Roboto Cn", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BattleLabel.ForeColor = System.Drawing.Color.White;
            this.BattleLabel.Location = new System.Drawing.Point(1229, 683);
            this.BattleLabel.Name = "BattleLabel";
            this.BattleLabel.Size = new System.Drawing.Size(99, 115);
            this.BattleLabel.TabIndex = 36;
            this.BattleLabel.Text = "1";
            // 
            // BravoFlagsLabel
            // 
            this.BravoFlagsLabel.AutoSize = true;
            this.BravoFlagsLabel.BackColor = System.Drawing.Color.Transparent;
            this.BravoFlagsLabel.Font = new System.Drawing.Font("Roboto", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BravoFlagsLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.BravoFlagsLabel.Location = new System.Drawing.Point(2122, 877);
            this.BravoFlagsLabel.Name = "BravoFlagsLabel";
            this.BravoFlagsLabel.Size = new System.Drawing.Size(159, 115);
            this.BravoFlagsLabel.TabIndex = 37;
            this.BravoFlagsLabel.Text = "12";
            // 
            // TestResponseChk
            // 
            this.TestResponseChk.AutoSize = true;
            this.TestResponseChk.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestResponseChk.Location = new System.Drawing.Point(252, 15);
            this.TestResponseChk.Name = "TestResponseChk";
            this.TestResponseChk.Size = new System.Drawing.Size(105, 19);
            this.TestResponseChk.TabIndex = 38;
            this.TestResponseChk.Text = "Test Reponse";
            this.TestResponseChk.UseVisualStyleBackColor = true;
            // 
            // CaptureTheFlagForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(2560, 1440);
            this.Controls.Add(this.TestResponseChk);
            this.Controls.Add(this.BravoFlagsLabel);
            this.Controls.Add(this.BattleLabel);
            this.Controls.Add(this.BravoProgress);
            this.Controls.Add(this.AlphaProgress);
            this.Controls.Add(this.AlphaPing);
            this.Controls.Add(this.BravoPing);
            this.Controls.Add(this.AlphaFlagsLabel);
            this.Name = "CaptureTheFlagForm";
            this.Load += new System.EventHandler(this.CaptureTheFlagForm_Load);
            this.Controls.SetChildIndex(this.AlphaFlagsLabel, 0);
            this.Controls.SetChildIndex(this.BravoPing, 0);
            this.Controls.SetChildIndex(this.AlphaPing, 0);
            this.Controls.SetChildIndex(this.AlphaProgress, 0);
            this.Controls.SetChildIndex(this.BravoProgress, 0);
            this.Controls.SetChildIndex(this.BattleLabel, 0);
            this.Controls.SetChildIndex(this.BravoFlagsLabel, 0);
            this.Controls.SetChildIndex(this.TestResponseChk, 0);
            ((System.ComponentModel.ISupportInitialize)(this.AlphaPing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BravoPing)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar BravoProgress;
        private System.Windows.Forms.ProgressBar AlphaProgress;
        private System.Windows.Forms.PictureBox AlphaPing;
        private System.Windows.Forms.PictureBox BravoPing;
        private System.Windows.Forms.Label AlphaFlagsLabel;
        private System.Windows.Forms.Label BattleLabel;
        private System.Windows.Forms.Label BravoFlagsLabel;
        private System.Windows.Forms.CheckBox TestResponseChk;
    }
}
