namespace SATRScoreDisplay
{
    partial class GamersMonitorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GamersMonitorForm));
            this.ControlDataViewPanel = new System.Windows.Forms.Panel();
            this.GamerGrid = new System.Windows.Forms.DataGridView();
            this.TestResponseChk = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.PageTimer = new System.Windows.Forms.Timer(this.components);
            this.ALIAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Team = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Space1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ping = new System.Windows.Forms.DataGridViewImageColumn();
            this.Space2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kills = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShotsFired = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emulation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KToD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Objective = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Accuracy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SATRID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControlDataViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GamerGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ControlDataViewPanel
            // 
            this.ControlDataViewPanel.BackColor = System.Drawing.Color.Black;
            this.ControlDataViewPanel.Controls.Add(this.GamerGrid);
            this.ControlDataViewPanel.Location = new System.Drawing.Point(42, 284);
            this.ControlDataViewPanel.Name = "ControlDataViewPanel";
            this.ControlDataViewPanel.Size = new System.Drawing.Size(2476, 1040);
            this.ControlDataViewPanel.TabIndex = 22;
            // 
            // GamerGrid
            // 
            this.GamerGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.GamerGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GamerGrid.BackgroundColor = System.Drawing.Color.Black;
            this.GamerGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GamerGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GamerGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.GamerGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GamerGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GamerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GamerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ALIAS,
            this.Team,
            this.Space1,
            this.Ping,
            this.Space2,
            this.Kills,
            this.Hits,
            this.Deaths,
            this.ShotsFired,
            this.Emulation,
            this.KToD,
            this.Objective,
            this.Accuracy,
            this.SATRID});
            this.GamerGrid.Enabled = false;
            this.GamerGrid.GridColor = System.Drawing.Color.Black;
            this.GamerGrid.Location = new System.Drawing.Point(8, 8);
            this.GamerGrid.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.GamerGrid.MultiSelect = false;
            this.GamerGrid.Name = "GamerGrid";
            this.GamerGrid.ReadOnly = true;
            this.GamerGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GamerGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.GamerGrid.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Roboto Cn", 8F);
            dataGridViewCellStyle6.NullValue = null;
            this.GamerGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.GamerGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.GamerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GamerGrid.Size = new System.Drawing.Size(2476, 1023);
            this.GamerGrid.TabIndex = 19;
            this.GamerGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GamerGrid_CellContentClick_1);
            // 
            // TestResponseChk
            // 
            this.TestResponseChk.AutoSize = true;
            this.TestResponseChk.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestResponseChk.Location = new System.Drawing.Point(323, 17);
            this.TestResponseChk.Name = "TestResponseChk";
            this.TestResponseChk.Size = new System.Drawing.Size(105, 19);
            this.TestResponseChk.TabIndex = 23;
            this.TestResponseChk.Text = "Test Reponse";
            this.TestResponseChk.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Black;
            this.progressBar1.ForeColor = System.Drawing.Color.LemonChiffon;
            this.progressBar1.Location = new System.Drawing.Point(1119, 1350);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(624, 36);
            this.progressBar1.TabIndex = 24;
            this.progressBar1.Value = 50;
            this.progressBar1.Visible = false;
            // 
            // PageTimer
            // 
            this.PageTimer.Interval = 3000;
            this.PageTimer.Tick += new System.EventHandler(this.PageTimer_Tick);
            // 
            // ALIAS
            // 
            this.ALIAS.HeaderText = "";
            this.ALIAS.MinimumWidth = 380;
            this.ALIAS.Name = "ALIAS";
            this.ALIAS.ReadOnly = true;
            this.ALIAS.Width = 380;
            // 
            // Team
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Team.DefaultCellStyle = dataGridViewCellStyle3;
            this.Team.HeaderText = "";
            this.Team.MinimumWidth = 196;
            this.Team.Name = "Team";
            this.Team.ReadOnly = true;
            this.Team.Width = 196;
            // 
            // Space1
            // 
            this.Space1.HeaderText = "";
            this.Space1.MinimumWidth = 70;
            this.Space1.Name = "Space1";
            this.Space1.ReadOnly = true;
            this.Space1.Width = 70;
            // 
            // Ping
            // 
            this.Ping.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle4.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle4.NullValue")));
            this.Ping.DefaultCellStyle = dataGridViewCellStyle4;
            this.Ping.HeaderText = "";
            this.Ping.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Ping.MinimumWidth = 50;
            this.Ping.Name = "Ping";
            this.Ping.ReadOnly = true;
            this.Ping.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Space2
            // 
            this.Space2.HeaderText = "";
            this.Space2.MinimumWidth = 70;
            this.Space2.Name = "Space2";
            this.Space2.ReadOnly = true;
            this.Space2.Width = 70;
            // 
            // Kills
            // 
            this.Kills.HeaderText = "";
            this.Kills.MinimumWidth = 141;
            this.Kills.Name = "Kills";
            this.Kills.ReadOnly = true;
            this.Kills.Width = 141;
            // 
            // Hits
            // 
            this.Hits.HeaderText = "";
            this.Hits.MinimumWidth = 125;
            this.Hits.Name = "Hits";
            this.Hits.ReadOnly = true;
            this.Hits.Width = 125;
            // 
            // Deaths
            // 
            this.Deaths.HeaderText = "";
            this.Deaths.MinimumWidth = 180;
            this.Deaths.Name = "Deaths";
            this.Deaths.ReadOnly = true;
            this.Deaths.Width = 180;
            // 
            // ShotsFired
            // 
            this.ShotsFired.HeaderText = "";
            this.ShotsFired.MinimumWidth = 245;
            this.ShotsFired.Name = "ShotsFired";
            this.ShotsFired.ReadOnly = true;
            this.ShotsFired.Width = 245;
            // 
            // Emulation
            // 
            this.Emulation.HeaderText = "";
            this.Emulation.MinimumWidth = 390;
            this.Emulation.Name = "Emulation";
            this.Emulation.ReadOnly = true;
            this.Emulation.Width = 390;
            // 
            // KToD
            // 
            this.KToD.HeaderText = "";
            this.KToD.MinimumWidth = 156;
            this.KToD.Name = "KToD";
            this.KToD.ReadOnly = true;
            this.KToD.Width = 156;
            // 
            // Objective
            // 
            this.Objective.HeaderText = "";
            this.Objective.MinimumWidth = 140;
            this.Objective.Name = "Objective";
            this.Objective.ReadOnly = true;
            this.Objective.Width = 140;
            // 
            // Accuracy
            // 
            this.Accuracy.HeaderText = "";
            this.Accuracy.MinimumWidth = 150;
            this.Accuracy.Name = "Accuracy";
            this.Accuracy.ReadOnly = true;
            this.Accuracy.Width = 150;
            // 
            // SATRID
            // 
            this.SATRID.HeaderText = "";
            this.SATRID.MinimumWidth = 200;
            this.SATRID.Name = "SATRID";
            this.SATRID.ReadOnly = true;
            this.SATRID.Width = 200;
            // 
            // GamersMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(2560, 1440);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.TestResponseChk);
            this.Controls.Add(this.ControlDataViewPanel);
            this.Name = "GamersMonitorForm";
            this.Load += new System.EventHandler(this.GamersMonitorForm_Load);
            this.Controls.SetChildIndex(this.ControlDataViewPanel, 0);
            this.Controls.SetChildIndex(this.TestResponseChk, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.ControlDataViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GamerGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ControlDataViewPanel;
        private System.Windows.Forms.DataGridView GamerGrid;
        private System.Windows.Forms.CheckBox TestResponseChk;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer PageTimer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALIAS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team;
        private System.Windows.Forms.DataGridViewTextBoxColumn Space1;
        private System.Windows.Forms.DataGridViewImageColumn Ping;
        private System.Windows.Forms.DataGridViewTextBoxColumn Space2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kills;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hits;
        private System.Windows.Forms.DataGridViewTextBoxColumn Deaths;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShotsFired;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emulation;
        private System.Windows.Forms.DataGridViewTextBoxColumn KToD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Objective;
        private System.Windows.Forms.DataGridViewTextBoxColumn Accuracy;
        private System.Windows.Forms.DataGridViewTextBoxColumn SATRID;
    }
}
