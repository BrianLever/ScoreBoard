namespace SATRScoreDisplay
{
    partial class OnePointDomination
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnePointDomination));
            this.BattleLabel = new System.Windows.Forms.Label();
            this.AlphaTimeLabel = new System.Windows.Forms.Label();
            this.BravoTimeLabel = new System.Windows.Forms.Label();
            this.BravoPing = new System.Windows.Forms.PictureBox();
            this.AlphaProgress = new System.Windows.Forms.ProgressBar();
            this.BravoProgress = new System.Windows.Forms.ProgressBar();
            this.TestResponseChk = new System.Windows.Forms.CheckBox();
            this.TraceSendChk = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BravoPing)).BeginInit();
            this.SuspendLayout();
            // 
            // BattleLabel
            // 
            this.BattleLabel.AutoSize = true;
            this.BattleLabel.BackColor = System.Drawing.Color.Transparent;
            this.BattleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BattleLabel.ForeColor = System.Drawing.Color.White;
            this.BattleLabel.Location = new System.Drawing.Point(1232, 656);
            this.BattleLabel.Name = "BattleLabel";
            this.BattleLabel.Size = new System.Drawing.Size(98, 108);
            this.BattleLabel.TabIndex = 17;
            this.BattleLabel.Text = "1";
            // 
            // AlphaTimeLabel
            // 
            this.AlphaTimeLabel.AutoSize = true;
            this.AlphaTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.AlphaTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlphaTimeLabel.ForeColor = System.Drawing.Color.White;
            this.AlphaTimeLabel.Location = new System.Drawing.Point(283, 911);
            this.AlphaTimeLabel.Name = "AlphaTimeLabel";
            this.AlphaTimeLabel.Size = new System.Drawing.Size(417, 108);
            this.AlphaTimeLabel.TabIndex = 18;
            this.AlphaTimeLabel.Text = "00:00:00";
            // 
            // BravoTimeLabel
            // 
            this.BravoTimeLabel.AutoSize = true;
            this.BravoTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.BravoTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BravoTimeLabel.ForeColor = System.Drawing.Color.White;
            this.BravoTimeLabel.Location = new System.Drawing.Point(1890, 911);
            this.BravoTimeLabel.Name = "BravoTimeLabel";
            this.BravoTimeLabel.Size = new System.Drawing.Size(417, 108);
            this.BravoTimeLabel.TabIndex = 19;
            this.BravoTimeLabel.Text = "00:00:00";
            // 
            // BravoPing
            // 
            this.BravoPing.BackColor = System.Drawing.Color.Transparent;
            this.BravoPing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BravoPing.BackgroundImage")));
            this.BravoPing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BravoPing.Location = new System.Drawing.Point(1296, 841);
            this.BravoPing.Name = "BravoPing";
            this.BravoPing.Size = new System.Drawing.Size(35, 35);
            this.BravoPing.TabIndex = 21;
            this.BravoPing.TabStop = false;
            this.BravoPing.Click += new System.EventHandler(this.BravoPing_Click);
            // 
            // AlphaProgress
            // 
            this.AlphaProgress.BackColor = System.Drawing.Color.Black;
            this.AlphaProgress.Enabled = false;
            this.AlphaProgress.ForeColor = System.Drawing.Color.Red;
            this.AlphaProgress.Location = new System.Drawing.Point(1001, 931);
            this.AlphaProgress.Name = "AlphaProgress";
            this.AlphaProgress.Size = new System.Drawing.Size(563, 95);
            this.AlphaProgress.Step = 1;
            this.AlphaProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.AlphaProgress.TabIndex = 23;
            this.AlphaProgress.Value = 53;
            // 
            // BravoProgress
            // 
            this.BravoProgress.BackColor = System.Drawing.Color.Black;
            this.BravoProgress.Enabled = false;
            this.BravoProgress.ForeColor = System.Drawing.Color.SteelBlue;
            this.BravoProgress.Location = new System.Drawing.Point(1001, 1032);
            this.BravoProgress.Name = "BravoProgress";
            this.BravoProgress.Size = new System.Drawing.Size(563, 95);
            this.BravoProgress.Step = 1;
            this.BravoProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BravoProgress.TabIndex = 24;
            this.BravoProgress.Value = 46;
            // 
            // TestResponseChk
            // 
            this.TestResponseChk.AutoSize = true;
            this.TestResponseChk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestResponseChk.Location = new System.Drawing.Point(269, 42);
            this.TestResponseChk.Name = "TestResponseChk";
            this.TestResponseChk.Size = new System.Drawing.Size(113, 20);
            this.TestResponseChk.TabIndex = 25;
            this.TestResponseChk.Text = "Test Reponse";
            this.TestResponseChk.UseVisualStyleBackColor = true;
            // 
            // TraceSendChk
            // 
            this.TraceSendChk.AutoSize = true;
            this.TraceSendChk.Location = new System.Drawing.Point(397, 7);
            this.TraceSendChk.Name = "TraceSendChk";
            this.TraceSendChk.Size = new System.Drawing.Size(82, 17);
            this.TraceSendChk.TabIndex = 27;
            this.TraceSendChk.Text = "Trace Send";
            this.TraceSendChk.UseVisualStyleBackColor = true;
            this.TraceSendChk.Visible = false;
            this.TraceSendChk.CheckedChanged += new System.EventHandler(this.TraceSendChk_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(485, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Close/Open RF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OnePointDomination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1386, 788);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TraceSendChk);
            this.Controls.Add(this.TestResponseChk);
            this.Controls.Add(this.BravoProgress);
            this.Controls.Add(this.AlphaProgress);
            this.Controls.Add(this.BravoPing);
            this.Controls.Add(this.BravoTimeLabel);
            this.Controls.Add(this.AlphaTimeLabel);
            this.Controls.Add(this.BattleLabel);
            this.Name = "OnePointDomination";
            this.Load += new System.EventHandler(this.OnePointDomination_Load);
            this.Controls.SetChildIndex(this.BattleLabel, 0);
            this.Controls.SetChildIndex(this.AlphaTimeLabel, 0);
            this.Controls.SetChildIndex(this.BravoTimeLabel, 0);
            this.Controls.SetChildIndex(this.BravoPing, 0);
            this.Controls.SetChildIndex(this.AlphaProgress, 0);
            this.Controls.SetChildIndex(this.BravoProgress, 0);
            this.Controls.SetChildIndex(this.TestResponseChk, 0);
            this.Controls.SetChildIndex(this.TraceSendChk, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.BravoPing)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label BattleLabel;
        private System.Windows.Forms.Label AlphaTimeLabel;
        private System.Windows.Forms.Label BravoTimeLabel;
        private System.Windows.Forms.PictureBox BravoPing;
        private System.Windows.Forms.ProgressBar AlphaProgress;
        private System.Windows.Forms.ProgressBar BravoProgress;
        private System.Windows.Forms.CheckBox TestResponseChk;
        private System.Windows.Forms.CheckBox TraceSendChk;
        private System.Windows.Forms.Button button1;
    }
}
