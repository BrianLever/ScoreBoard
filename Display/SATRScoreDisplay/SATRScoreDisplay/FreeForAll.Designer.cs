namespace SATRScoreDisplay
{
    partial class FreeForAllForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FreeForAllForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ControlDataViewPanel = new System.Windows.Forms.Panel();
            this.GamerGrid = new System.Windows.Forms.DataGridView();
            this.ALIAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KillsWinner = new System.Windows.Forms.DataGridViewImageColumn();
            this.Kills = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HitsWinner = new System.Windows.Forms.DataGridViewImageColumn();
            this.Hits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeathsWinner = new System.Windows.Forms.DataGridViewImageColumn();
            this.Deaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KDWinner = new System.Windows.Forms.DataGridViewImageColumn();
            this.KToD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Padding1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ping = new System.Windows.Forms.DataGridViewImageColumn();
            this.Paddin2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emulation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestResponseChk = new System.Windows.Forms.CheckBox();
            this.PageTimer = new System.Windows.Forms.Timer(this.components);
            this.ControlDataViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GamerGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Black;
            this.progressBar1.ForeColor = System.Drawing.Color.LemonChiffon;
            this.progressBar1.Location = new System.Drawing.Point(1656, 2032);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(936, 55);
            this.progressBar1.TabIndex = 25;
            this.progressBar1.Value = 50;
            this.progressBar1.Visible = false;
            // 
            // ControlDataViewPanel
            // 
            this.ControlDataViewPanel.BackColor = System.Drawing.Color.Black;
            this.ControlDataViewPanel.Controls.Add(this.GamerGrid);
            this.ControlDataViewPanel.Location = new System.Drawing.Point(86, 534);
            this.ControlDataViewPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ControlDataViewPanel.Name = "ControlDataViewPanel";
            this.ControlDataViewPanel.Size = new System.Drawing.Size(3684, 1451);
            this.ControlDataViewPanel.TabIndex = 26;
            // 
            // GamerGrid
            // 
            this.GamerGrid.AllowUserToDeleteRows = false;
            this.GamerGrid.BackgroundColor = System.Drawing.Color.Black;
            this.GamerGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GamerGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GamerGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.GamerGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GamerGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GamerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GamerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ALIAS,
            this.KillsWinner,
            this.Kills,
            this.HitsWinner,
            this.Hits,
            this.DeathsWinner,
            this.Deaths,
            this.KDWinner,
            this.KToD,
            this.Padding1,
            this.Ping,
            this.Paddin2,
            this.Emulation});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GamerGrid.DefaultCellStyle = dataGridViewCellStyle4;
            this.GamerGrid.Enabled = false;
            this.GamerGrid.GridColor = System.Drawing.Color.Black;
            this.GamerGrid.Location = new System.Drawing.Point(-16, -156);
            this.GamerGrid.Margin = new System.Windows.Forms.Padding(15, 14, 15, 14);
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
            this.GamerGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            dataGridViewCellStyle6.NullValue = null;
            this.GamerGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.GamerGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.GamerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GamerGrid.Size = new System.Drawing.Size(3562, 1437);
            this.GamerGrid.TabIndex = 21;
            // 
            // ALIAS
            // 
            this.ALIAS.HeaderText = "";
            this.ALIAS.MinimumWidth = 310;
            this.ALIAS.Name = "ALIAS";
            this.ALIAS.ReadOnly = true;
            this.ALIAS.Width = 310;
            // 
            // KillsWinner
            // 
            this.KillsWinner.HeaderText = "";
            this.KillsWinner.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.KillsWinner.MinimumWidth = 60;
            this.KillsWinner.Name = "KillsWinner";
            this.KillsWinner.ReadOnly = true;
            this.KillsWinner.Width = 60;
            // 
            // Kills
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Kills.DefaultCellStyle = dataGridViewCellStyle2;
            this.Kills.HeaderText = "";
            this.Kills.MinimumWidth = 265;
            this.Kills.Name = "Kills";
            this.Kills.ReadOnly = true;
            this.Kills.Width = 265;
            // 
            // HitsWinner
            // 
            this.HitsWinner.HeaderText = "";
            this.HitsWinner.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.HitsWinner.MinimumWidth = 60;
            this.HitsWinner.Name = "HitsWinner";
            this.HitsWinner.ReadOnly = true;
            this.HitsWinner.Width = 60;
            // 
            // Hits
            // 
            this.Hits.HeaderText = "";
            this.Hits.MinimumWidth = 210;
            this.Hits.Name = "Hits";
            this.Hits.ReadOnly = true;
            this.Hits.Width = 210;
            // 
            // DeathsWinner
            // 
            this.DeathsWinner.HeaderText = "";
            this.DeathsWinner.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.DeathsWinner.MinimumWidth = 60;
            this.DeathsWinner.Name = "DeathsWinner";
            this.DeathsWinner.ReadOnly = true;
            this.DeathsWinner.Width = 60;
            // 
            // Deaths
            // 
            this.Deaths.HeaderText = "";
            this.Deaths.MinimumWidth = 360;
            this.Deaths.Name = "Deaths";
            this.Deaths.ReadOnly = true;
            this.Deaths.Width = 360;
            // 
            // KDWinner
            // 
            this.KDWinner.HeaderText = "";
            this.KDWinner.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.KDWinner.MinimumWidth = 60;
            this.KDWinner.Name = "KDWinner";
            this.KDWinner.ReadOnly = true;
            this.KDWinner.Width = 60;
            // 
            // KToD
            // 
            this.KToD.HeaderText = "";
            this.KToD.MinimumWidth = 185;
            this.KToD.Name = "KToD";
            this.KToD.ReadOnly = true;
            this.KToD.Width = 185;
            // 
            // Padding1
            // 
            this.Padding1.HeaderText = "";
            this.Padding1.MinimumWidth = 180;
            this.Padding1.Name = "Padding1";
            this.Padding1.ReadOnly = true;
            this.Padding1.Width = 180;
            // 
            // Ping
            // 
            this.Ping.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle3.NullValue")));
            this.Ping.DefaultCellStyle = dataGridViewCellStyle3;
            this.Ping.HeaderText = "";
            this.Ping.Image = ((System.Drawing.Image)(resources.GetObject("Ping.Image")));
            this.Ping.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Ping.MinimumWidth = 60;
            this.Ping.Name = "Ping";
            this.Ping.ReadOnly = true;
            this.Ping.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Ping.Width = 60;
            // 
            // Paddin2
            // 
            this.Paddin2.HeaderText = "";
            this.Paddin2.MinimumWidth = 195;
            this.Paddin2.Name = "Paddin2";
            this.Paddin2.ReadOnly = true;
            this.Paddin2.Width = 195;
            // 
            // Emulation
            // 
            this.Emulation.HeaderText = "";
            this.Emulation.MinimumWidth = 375;
            this.Emulation.Name = "Emulation";
            this.Emulation.ReadOnly = true;
            this.Emulation.Width = 375;
            // 
            // TestResponseChk
            // 
            this.TestResponseChk.AutoSize = true;
            this.TestResponseChk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestResponseChk.Location = new System.Drawing.Point(392, 18);
            this.TestResponseChk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TestResponseChk.Name = "TestResponseChk";
            this.TestResponseChk.Size = new System.Drawing.Size(160, 29);
            this.TestResponseChk.TabIndex = 27;
            this.TestResponseChk.Text = "Test Reponse";
            this.TestResponseChk.UseVisualStyleBackColor = true;
            // 
            // PageTimer
            // 
            this.PageTimer.Enabled = true;
            this.PageTimer.Interval = 5000;
            this.PageTimer.Tick += new System.EventHandler(this.PageTimer_Tick);
            // 
            // FreeForAllForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(2615, 1544);
            this.Controls.Add(this.TestResponseChk);
            this.Controls.Add(this.ControlDataViewPanel);
            this.Controls.Add(this.progressBar1);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "FreeForAllForm";
            this.Load += new System.EventHandler(this.FreeForAllForm_Load);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.ControlDataViewPanel, 0);
            this.Controls.SetChildIndex(this.TestResponseChk, 0);
            this.ControlDataViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GamerGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel ControlDataViewPanel;
        private System.Windows.Forms.DataGridView GamerGrid;
        private System.Windows.Forms.CheckBox TestResponseChk;
        private System.Windows.Forms.Timer PageTimer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALIAS;
        private System.Windows.Forms.DataGridViewImageColumn KillsWinner;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kills;
        private System.Windows.Forms.DataGridViewImageColumn HitsWinner;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hits;
        private System.Windows.Forms.DataGridViewImageColumn DeathsWinner;
        private System.Windows.Forms.DataGridViewTextBoxColumn Deaths;
        private System.Windows.Forms.DataGridViewImageColumn KDWinner;
        private System.Windows.Forms.DataGridViewTextBoxColumn KToD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Padding1;
        private System.Windows.Forms.DataGridViewImageColumn Ping;
        private System.Windows.Forms.DataGridViewTextBoxColumn Paddin2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emulation;
    }
}
