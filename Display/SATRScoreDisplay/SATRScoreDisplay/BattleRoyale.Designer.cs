namespace SATRScoreDisplay
{
    partial class BattleRoyale
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleRoyale));
            this.TestResponseChk = new System.Windows.Forms.CheckBox();
            this.TeamGrid = new System.Windows.Forms.DataGridView();
            this.ControlDataViewPanel = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Trophy = new System.Windows.Forms.DataGridViewImageColumn();
            this.Team = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Space1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SurvivorWin = new System.Windows.Forms.DataGridViewImageColumn();
            this.Survivors = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Space2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PINGS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Space3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KillsWinner = new System.Windows.Forms.DataGridViewImageColumn();
            this.Kills = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Space4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HitsWinner = new System.Windows.Forms.DataGridViewImageColumn();
            this.Hits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Space5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeathsWinner = new System.Windows.Forms.DataGridViewImageColumn();
            this.Deaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Space6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WinnersShots = new System.Windows.Forms.DataGridViewImageColumn();
            this.ShotsFired = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WinnerKD = new System.Windows.Forms.DataGridViewImageColumn();
            this.KToD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TeamGrid)).BeginInit();
            this.ControlDataViewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestResponseChk
            // 
            this.TestResponseChk.AutoSize = true;
            this.TestResponseChk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestResponseChk.Location = new System.Drawing.Point(327, 37);
            this.TestResponseChk.Name = "TestResponseChk";
            this.TestResponseChk.Size = new System.Drawing.Size(113, 20);
            this.TestResponseChk.TabIndex = 25;
            this.TestResponseChk.Text = "Test Reponse";
            this.TestResponseChk.UseVisualStyleBackColor = true;
            // 
            // TeamGrid
            // 
            this.TeamGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.TeamGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.TeamGrid.BackgroundColor = System.Drawing.Color.Black;
            this.TeamGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TeamGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.TeamGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.TeamGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TeamGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.TeamGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TeamGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Trophy,
            this.Team,
            this.Space1,
            this.SurvivorWin,
            this.Survivors,
            this.Space2,
            this.PINGS,
            this.Space3,
            this.KillsWinner,
            this.Kills,
            this.Space4,
            this.HitsWinner,
            this.Hits,
            this.Space5,
            this.DeathsWinner,
            this.Deaths,
            this.Space6,
            this.WinnersShots,
            this.ShotsFired,
            this.WinnerKD,
            this.KToD});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TeamGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.TeamGrid.Enabled = false;
            this.TeamGrid.GridColor = System.Drawing.Color.Black;
            this.TeamGrid.Location = new System.Drawing.Point(8, 8);
            this.TeamGrid.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.TeamGrid.MultiSelect = false;
            this.TeamGrid.Name = "TeamGrid";
            this.TeamGrid.ReadOnly = true;
            this.TeamGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TeamGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.TeamGrid.RowHeadersVisible = false;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            dataGridViewCellStyle7.NullValue = null;
            this.TeamGrid.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.TeamGrid.RowTemplate.Height = 50;
            this.TeamGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TeamGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TeamGrid.Size = new System.Drawing.Size(2476, 1023);
            this.TeamGrid.TabIndex = 19;
            this.TeamGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TeamGrid_CellContentClick);
            // 
            // ControlDataViewPanel
            // 
            this.ControlDataViewPanel.BackColor = System.Drawing.Color.Black;
            this.ControlDataViewPanel.Controls.Add(this.TeamGrid);
            this.ControlDataViewPanel.Location = new System.Drawing.Point(26, 199);
            this.ControlDataViewPanel.Name = "ControlDataViewPanel";
            this.ControlDataViewPanel.Size = new System.Drawing.Size(2476, 632);
            this.ControlDataViewPanel.TabIndex = 24;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Black;
            this.progressBar1.ForeColor = System.Drawing.Color.LemonChiffon;
            this.progressBar1.Location = new System.Drawing.Point(1152, 1350);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(624, 36);
            this.progressBar1.TabIndex = 26;
            this.progressBar1.Value = 50;
            this.progressBar1.Visible = false;
            // 
            // Trophy
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Trophy.DefaultCellStyle = dataGridViewCellStyle3;
            this.Trophy.HeaderText = "";
            this.Trophy.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Trophy.MinimumWidth = 70;
            this.Trophy.Name = "Trophy";
            this.Trophy.ReadOnly = true;
            this.Trophy.Width = 70;
            // 
            // Team
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Team.DefaultCellStyle = dataGridViewCellStyle4;
            this.Team.HeaderText = "";
            this.Team.MinimumWidth = 400;
            this.Team.Name = "Team";
            this.Team.ReadOnly = true;
            this.Team.Width = 400;
            // 
            // Space1
            // 
            this.Space1.HeaderText = "";
            this.Space1.MinimumWidth = 130;
            this.Space1.Name = "Space1";
            this.Space1.ReadOnly = true;
            this.Space1.Width = 130;
            // 
            // SurvivorWin
            // 
            this.SurvivorWin.HeaderText = "";
            this.SurvivorWin.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.SurvivorWin.MinimumWidth = 70;
            this.SurvivorWin.Name = "SurvivorWin";
            this.SurvivorWin.ReadOnly = true;
            this.SurvivorWin.Width = 70;
            // 
            // Survivors
            // 
            this.Survivors.HeaderText = "";
            this.Survivors.MinimumWidth = 140;
            this.Survivors.Name = "Survivors";
            this.Survivors.ReadOnly = true;
            this.Survivors.Width = 140;
            // 
            // Space2
            // 
            this.Space2.HeaderText = "";
            this.Space2.MinimumWidth = 140;
            this.Space2.Name = "Space2";
            this.Space2.ReadOnly = true;
            this.Space2.Width = 140;
            // 
            // PINGS
            // 
            this.PINGS.HeaderText = "";
            this.PINGS.MinimumWidth = 150;
            this.PINGS.Name = "PINGS";
            this.PINGS.ReadOnly = true;
            this.PINGS.Width = 150;
            // 
            // Space3
            // 
            this.Space3.HeaderText = "";
            this.Space3.MinimumWidth = 65;
            this.Space3.Name = "Space3";
            this.Space3.ReadOnly = true;
            this.Space3.Width = 65;
            // 
            // KillsWinner
            // 
            this.KillsWinner.HeaderText = "";
            this.KillsWinner.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.KillsWinner.MinimumWidth = 70;
            this.KillsWinner.Name = "KillsWinner";
            this.KillsWinner.ReadOnly = true;
            this.KillsWinner.Width = 70;
            // 
            // Kills
            // 
            this.Kills.HeaderText = "";
            this.Kills.MinimumWidth = 140;
            this.Kills.Name = "Kills";
            this.Kills.ReadOnly = true;
            this.Kills.Width = 140;
            // 
            // Space4
            // 
            this.Space4.HeaderText = "";
            this.Space4.MinimumWidth = 10;
            this.Space4.Name = "Space4";
            this.Space4.ReadOnly = true;
            this.Space4.Width = 10;
            // 
            // HitsWinner
            // 
            this.HitsWinner.HeaderText = "";
            this.HitsWinner.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.HitsWinner.MinimumWidth = 70;
            this.HitsWinner.Name = "HitsWinner";
            this.HitsWinner.ReadOnly = true;
            this.HitsWinner.Width = 70;
            // 
            // Hits
            // 
            this.Hits.HeaderText = "";
            this.Hits.MinimumWidth = 140;
            this.Hits.Name = "Hits";
            this.Hits.ReadOnly = true;
            this.Hits.Width = 140;
            // 
            // Space5
            // 
            this.Space5.HeaderText = "";
            this.Space5.MinimumWidth = 10;
            this.Space5.Name = "Space5";
            this.Space5.ReadOnly = true;
            this.Space5.Width = 10;
            // 
            // DeathsWinner
            // 
            this.DeathsWinner.HeaderText = "";
            this.DeathsWinner.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.DeathsWinner.MinimumWidth = 70;
            this.DeathsWinner.Name = "DeathsWinner";
            this.DeathsWinner.ReadOnly = true;
            this.DeathsWinner.Width = 70;
            // 
            // Deaths
            // 
            this.Deaths.HeaderText = "";
            this.Deaths.MinimumWidth = 200;
            this.Deaths.Name = "Deaths";
            this.Deaths.ReadOnly = true;
            this.Deaths.Width = 200;
            // 
            // Space6
            // 
            this.Space6.HeaderText = "";
            this.Space6.MinimumWidth = 10;
            this.Space6.Name = "Space6";
            this.Space6.ReadOnly = true;
            this.Space6.Width = 10;
            // 
            // WinnersShots
            // 
            this.WinnersShots.HeaderText = "";
            this.WinnersShots.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.WinnersShots.MinimumWidth = 70;
            this.WinnersShots.Name = "WinnersShots";
            this.WinnersShots.ReadOnly = true;
            this.WinnersShots.Width = 70;
            // 
            // ShotsFired
            // 
            this.ShotsFired.HeaderText = "";
            this.ShotsFired.MinimumWidth = 270;
            this.ShotsFired.Name = "ShotsFired";
            this.ShotsFired.ReadOnly = true;
            this.ShotsFired.Width = 270;
            // 
            // WinnerKD
            // 
            this.WinnerKD.HeaderText = "";
            this.WinnerKD.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.WinnerKD.MinimumWidth = 70;
            this.WinnerKD.Name = "WinnerKD";
            this.WinnerKD.ReadOnly = true;
            this.WinnerKD.Width = 70;
            // 
            // KToD
            // 
            this.KToD.HeaderText = "";
            this.KToD.MinimumWidth = 210;
            this.KToD.Name = "KToD";
            this.KToD.ReadOnly = true;
            this.KToD.Width = 210;
            // 
            // BattleRoyale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1386, 788);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.TestResponseChk);
            this.Controls.Add(this.ControlDataViewPanel);
            this.Name = "BattleRoyale";
            this.Load += new System.EventHandler(this.BattleRoyale_Load);
            this.Controls.SetChildIndex(this.ControlDataViewPanel, 0);
            this.Controls.SetChildIndex(this.TestResponseChk, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.TeamGrid)).EndInit();
            this.ControlDataViewPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox TestResponseChk;
        private System.Windows.Forms.DataGridView TeamGrid;
        private System.Windows.Forms.Panel ControlDataViewPanel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewImageColumn Trophy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team;
        private System.Windows.Forms.DataGridViewTextBoxColumn Space1;
        private System.Windows.Forms.DataGridViewImageColumn SurvivorWin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Survivors;
        private System.Windows.Forms.DataGridViewTextBoxColumn Space2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PINGS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Space3;
        private System.Windows.Forms.DataGridViewImageColumn KillsWinner;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kills;
        private System.Windows.Forms.DataGridViewTextBoxColumn Space4;
        private System.Windows.Forms.DataGridViewImageColumn HitsWinner;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hits;
        private System.Windows.Forms.DataGridViewTextBoxColumn Space5;
        private System.Windows.Forms.DataGridViewImageColumn DeathsWinner;
        private System.Windows.Forms.DataGridViewTextBoxColumn Deaths;
        private System.Windows.Forms.DataGridViewTextBoxColumn Space6;
        private System.Windows.Forms.DataGridViewImageColumn WinnersShots;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShotsFired;
        private System.Windows.Forms.DataGridViewImageColumn WinnerKD;
        private System.Windows.Forms.DataGridViewTextBoxColumn KToD;
    }
}
