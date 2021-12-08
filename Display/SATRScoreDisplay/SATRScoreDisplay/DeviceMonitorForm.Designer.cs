namespace SATRScoreDisplay
{
    partial class DeviceMonitorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceMonitorForm));
            this.StatusText = new System.Windows.Forms.Label();
            this.TestBox = new System.Windows.Forms.TextBox();
            this.ControlDataViewPanel = new System.Windows.Forms.Panel();
            this.DeviceGrid = new System.Windows.Forms.DataGridView();
            this.TestResponseChk = new System.Windows.Forms.CheckBox();
            this.RoleBadge = new System.Windows.Forms.DataGridViewImageColumn();
            this.DeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALIAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Team = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Padding1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ping = new System.Windows.Forms.DataGridViewImageColumn();
            this.Padding2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SATRID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControlDataViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusText
            // 
            this.StatusText.AutoSize = true;
            this.StatusText.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusText.Location = new System.Drawing.Point(854, 1322);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(64, 23);
            this.StatusText.TabIndex = 18;
            this.StatusText.Text = "Test S";
            this.StatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusText.Visible = false;
            // 
            // TestBox
            // 
            this.TestBox.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestBox.Location = new System.Drawing.Point(858, 1348);
            this.TestBox.Multiline = true;
            this.TestBox.Name = "TestBox";
            this.TestBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TestBox.Size = new System.Drawing.Size(850, 80);
            this.TestBox.TabIndex = 20;
            this.TestBox.Visible = false;
            // 
            // ControlDataViewPanel
            // 
            this.ControlDataViewPanel.BackColor = System.Drawing.Color.Black;
            this.ControlDataViewPanel.Controls.Add(this.DeviceGrid);
            this.ControlDataViewPanel.Location = new System.Drawing.Point(49, 313);
            this.ControlDataViewPanel.Name = "ControlDataViewPanel";
            this.ControlDataViewPanel.Size = new System.Drawing.Size(2476, 873);
            this.ControlDataViewPanel.TabIndex = 21;
            // 
            // DeviceGrid
            // 
            this.DeviceGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            this.DeviceGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DeviceGrid.BackgroundColor = System.Drawing.Color.Black;
            this.DeviceGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DeviceGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DeviceGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.DeviceGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DeviceGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DeviceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DeviceGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RoleBadge,
            this.DeviceName,
            this.ALIAS,
            this.Team,
            this.Score,
            this.Padding1,
            this.Ping,
            this.Padding2,
            this.Status,
            this.SATRID});
            this.DeviceGrid.Enabled = false;
            this.DeviceGrid.GridColor = System.Drawing.Color.Black;
            this.DeviceGrid.Location = new System.Drawing.Point(0, 0);
            this.DeviceGrid.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.DeviceGrid.MultiSelect = false;
            this.DeviceGrid.Name = "DeviceGrid";
            this.DeviceGrid.ReadOnly = true;
            this.DeviceGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DeviceGrid.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto Cn", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.NullValue = null;
            this.DeviceGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DeviceGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DeviceGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DeviceGrid.Size = new System.Drawing.Size(2476, 873);
            this.DeviceGrid.TabIndex = 17;
            this.DeviceGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DeviceGrid_CellContentClick_1);
            // 
            // TestResponseChk
            // 
            this.TestResponseChk.AutoSize = true;
            this.TestResponseChk.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestResponseChk.Location = new System.Drawing.Point(764, 12);
            this.TestResponseChk.Name = "TestResponseChk";
            this.TestResponseChk.Size = new System.Drawing.Size(105, 19);
            this.TestResponseChk.TabIndex = 22;
            this.TestResponseChk.Text = "Test Reponse";
            this.TestResponseChk.UseVisualStyleBackColor = true;
            this.TestResponseChk.CheckedChanged += new System.EventHandler(this.TestResponseChk_CheckedChanged);
            // 
            // RoleBadge
            // 
            this.RoleBadge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RoleBadge.HeaderText = "";
            this.RoleBadge.MinimumWidth = 100;
            this.RoleBadge.Name = "RoleBadge";
            this.RoleBadge.ReadOnly = true;
            this.RoleBadge.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // DeviceName
            // 
            this.DeviceName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DeviceName.HeaderText = "";
            this.DeviceName.MinimumWidth = 470;
            this.DeviceName.Name = "DeviceName";
            this.DeviceName.ReadOnly = true;
            this.DeviceName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DeviceName.Width = 470;
            // 
            // ALIAS
            // 
            this.ALIAS.HeaderText = "";
            this.ALIAS.MinimumWidth = 400;
            this.ALIAS.Name = "ALIAS";
            this.ALIAS.ReadOnly = true;
            this.ALIAS.Width = 400;
            // 
            // Team
            // 
            this.Team.HeaderText = "";
            this.Team.MinimumWidth = 370;
            this.Team.Name = "Team";
            this.Team.ReadOnly = true;
            this.Team.Width = 370;
            // 
            // Score
            // 
            this.Score.HeaderText = "";
            this.Score.MinimumWidth = 170;
            this.Score.Name = "Score";
            this.Score.ReadOnly = true;
            this.Score.Width = 170;
            // 
            // Padding1
            // 
            this.Padding1.HeaderText = "";
            this.Padding1.MinimumWidth = 40;
            this.Padding1.Name = "Padding1";
            this.Padding1.ReadOnly = true;
            this.Padding1.Width = 40;
            // 
            // Ping
            // 
            this.Ping.HeaderText = "";
            this.Ping.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Ping.MinimumWidth = 75;
            this.Ping.Name = "Ping";
            this.Ping.ReadOnly = true;
            this.Ping.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Ping.Width = 75;
            // 
            // Padding2
            // 
            this.Padding2.HeaderText = "";
            this.Padding2.MinimumWidth = 85;
            this.Padding2.Name = "Padding2";
            this.Padding2.ReadOnly = true;
            this.Padding2.Width = 85;
            // 
            // Status
            // 
            this.Status.HeaderText = "";
            this.Status.MinimumWidth = 550;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 550;
            // 
            // SATRID
            // 
            this.SATRID.HeaderText = "";
            this.SATRID.MinimumWidth = 200;
            this.SATRID.Name = "SATRID";
            this.SATRID.ReadOnly = true;
            this.SATRID.Width = 200;
            // 
            // DeviceMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 38F);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(2580, 1460);
            this.Controls.Add(this.TestResponseChk);
            this.Controls.Add(this.ControlDataViewPanel);
            this.Controls.Add(this.TestBox);
            this.Controls.Add(this.StatusText);
            this.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.Name = "DeviceMonitorForm";
            this.Load += new System.EventHandler(this.DeviceMonitorForm_Load);
            this.Click += new System.EventHandler(this.DeviceMonitorForm_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DeviceMonitorForm_Paint);
            this.Enter += new System.EventHandler(this.DeviceMonitorForm_Enter);
            this.Controls.SetChildIndex(this.StatusText, 0);
            this.Controls.SetChildIndex(this.TestBox, 0);
            this.Controls.SetChildIndex(this.ControlDataViewPanel, 0);
            this.Controls.SetChildIndex(this.TestResponseChk, 0);
            this.ControlDataViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label StatusText;
        private System.Windows.Forms.TextBox TestBox;
        private System.Windows.Forms.Panel ControlDataViewPanel;
        private System.Windows.Forms.DataGridView DeviceGrid;
        private System.Windows.Forms.CheckBox TestResponseChk;
        private System.Windows.Forms.DataGridViewImageColumn RoleBadge;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALIAS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
        private System.Windows.Forms.DataGridViewTextBoxColumn Padding1;
        private System.Windows.Forms.DataGridViewImageColumn Ping;
        private System.Windows.Forms.DataGridViewTextBoxColumn Padding2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn SATRID;
    }
}
