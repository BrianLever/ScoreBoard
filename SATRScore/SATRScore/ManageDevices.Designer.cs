namespace SATRScore
{
    partial class ManageDevicesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageDevicesForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "Plan"}, -1);
            this.BackBtn = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DeviceList = new System.Windows.Forms.ListView();
            this.Alias = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Role = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SATRID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SearchBtn = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.DeleteAllBtn = new System.Windows.Forms.PictureBox();
            this.RandonBtnAllocation = new System.Windows.Forms.PictureBox();
            this.WaitRFTransmissionTimer = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.CloseAppPicture = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.DeviceCntLabel = new System.Windows.Forms.Label();
            this.GamersLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeleteAllBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RandonBtnAllocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // BackBtn
            // 
            this.BackBtn.BackColor = System.Drawing.Color.Transparent;
            this.BackBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BackBtn.BackgroundImage")));
            this.BackBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackBtn.ErrorImage = null;
            this.BackBtn.InitialImage = null;
            this.BackBtn.Location = new System.Drawing.Point(26, 679);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(304, 58);
            this.BackBtn.TabIndex = 8;
            this.BackBtn.TabStop = false;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(531, 679);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(304, 58);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // DeviceList
            // 
            this.DeviceList.BackColor = System.Drawing.Color.Black;
            this.DeviceList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DeviceList.CheckBoxes = true;
            this.DeviceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Alias,
            this.Role,
            this.SATRID});
            this.DeviceList.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceList.ForeColor = System.Drawing.Color.White;
            this.DeviceList.FullRowSelect = true;
            this.DeviceList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.StateImageIndex = 0;
            this.DeviceList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.DeviceList.Location = new System.Drawing.Point(73, 321);
            this.DeviceList.Name = "DeviceList";
            this.DeviceList.Size = new System.Drawing.Size(1221, 352);
            this.DeviceList.TabIndex = 9;
            this.toolTip1.SetToolTip(this.DeviceList, "<DELETE KEY> deletes device; doublc click to change Alias");
            this.DeviceList.UseCompatibleStateImageBehavior = false;
            this.DeviceList.View = System.Windows.Forms.View.Details;
            this.DeviceList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.DeviceList_ItemCheck);
            this.DeviceList.SelectedIndexChanged += new System.EventHandler(this.DeviceList_SelectedIndexChanged);
            this.DeviceList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DeviceList_KeyDown);
            this.DeviceList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DeviceList_MouseDoubleClick);
            // 
            // Alias
            // 
            this.Alias.Text = "Alias";
            this.Alias.Width = 450;
            // 
            // Role
            // 
            this.Role.Text = "Role";
            this.Role.Width = 585;
            // 
            // SATRID
            // 
            this.SATRID.Text = "SATR ID";
            this.SATRID.Width = 100;
            // 
            // SearchBtn
            // 
            this.SearchBtn.BackColor = System.Drawing.Color.Transparent;
            this.SearchBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SearchBtn.BackgroundImage")));
            this.SearchBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SearchBtn.ErrorImage = null;
            this.SearchBtn.InitialImage = null;
            this.SearchBtn.Location = new System.Drawing.Point(884, 679);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(304, 58);
            this.SearchBtn.TabIndex = 10;
            this.SearchBtn.TabStop = false;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.Transparent;
            this.toolTip1.ForeColor = System.Drawing.Color.White;
            // 
            // DeleteAllBtn
            // 
            this.DeleteAllBtn.BackColor = System.Drawing.Color.Transparent;
            this.DeleteAllBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DeleteAllBtn.BackgroundImage")));
            this.DeleteAllBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.DeleteAllBtn.Location = new System.Drawing.Point(1300, 321);
            this.DeleteAllBtn.Name = "DeleteAllBtn";
            this.DeleteAllBtn.Size = new System.Drawing.Size(55, 50);
            this.DeleteAllBtn.TabIndex = 11;
            this.DeleteAllBtn.TabStop = false;
            this.toolTip1.SetToolTip(this.DeleteAllBtn, "Delete all devices");
            this.DeleteAllBtn.Click += new System.EventHandler(this.DeleteAllBtn_Click);
            // 
            // RandonBtnAllocation
            // 
            this.RandonBtnAllocation.BackColor = System.Drawing.Color.Transparent;
            this.RandonBtnAllocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RandonBtnAllocation.BackgroundImage")));
            this.RandonBtnAllocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RandonBtnAllocation.Location = new System.Drawing.Point(1300, 387);
            this.RandonBtnAllocation.Name = "RandonBtnAllocation";
            this.RandonBtnAllocation.Size = new System.Drawing.Size(55, 50);
            this.RandonBtnAllocation.TabIndex = 12;
            this.RandonBtnAllocation.TabStop = false;
            this.toolTip1.SetToolTip(this.RandonBtnAllocation, "Allocate devices Random ALIAS names based on Device Role");
            this.RandonBtnAllocation.Click += new System.EventHandler(this.RandonBtnAllocation_Click);
            // 
            // WaitRFTransmissionTimer
            // 
            this.WaitRFTransmissionTimer.Interval = 200;
            this.WaitRFTransmissionTimer.Tick += new System.EventHandler(this.WaitRFTransmissionTimer_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.LemonChiffon;
            this.progressBar1.Location = new System.Drawing.Point(283, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(835, 23);
            this.progressBar1.TabIndex = 13;
            this.progressBar1.Visible = false;
            // 
            // CloseAppPicture
            // 
            this.CloseAppPicture.BackColor = System.Drawing.Color.Transparent;
            this.CloseAppPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseAppPicture.BackgroundImage")));
            this.CloseAppPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseAppPicture.Location = new System.Drawing.Point(1286, 673);
            this.CloseAppPicture.Name = "CloseAppPicture";
            this.CloseAppPicture.Size = new System.Drawing.Size(68, 73);
            this.CloseAppPicture.TabIndex = 14;
            this.CloseAppPicture.TabStop = false;
            this.CloseAppPicture.Click += new System.EventHandler(this.CloseAppPicture_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Admin Lock?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DeviceCntLabel
            // 
            this.DeviceCntLabel.AutoSize = true;
            this.DeviceCntLabel.BackColor = System.Drawing.Color.Transparent;
            this.DeviceCntLabel.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceCntLabel.ForeColor = System.Drawing.Color.White;
            this.DeviceCntLabel.Location = new System.Drawing.Point(345, 679);
            this.DeviceCntLabel.Name = "DeviceCntLabel";
            this.DeviceCntLabel.Size = new System.Drawing.Size(100, 24);
            this.DeviceCntLabel.TabIndex = 16;
            this.DeviceCntLabel.Text = "DEVICES";
            // 
            // GamersLabel
            // 
            this.GamersLabel.AutoSize = true;
            this.GamersLabel.BackColor = System.Drawing.Color.Transparent;
            this.GamersLabel.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GamersLabel.ForeColor = System.Drawing.Color.White;
            this.GamersLabel.Location = new System.Drawing.Point(345, 713);
            this.GamersLabel.Name = "GamersLabel";
            this.GamersLabel.Size = new System.Drawing.Size(97, 24);
            this.GamersLabel.TabIndex = 17;
            this.GamersLabel.Text = "GAMERS";
            // 
            // ManageDevicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1359, 745);
            this.Controls.Add(this.GamersLabel);
            this.Controls.Add(this.DeviceCntLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CloseAppPicture);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.RandonBtnAllocation);
            this.Controls.Add(this.DeleteAllBtn);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.DeviceList);
            this.Controls.Add(this.BackBtn);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageDevicesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Activated += new System.EventHandler(this.ManageDevicesForm_Activated);
            this.Load += new System.EventHandler(this.ManageDevicesForm_Load);
            this.Shown += new System.EventHandler(this.ManageDevicesForm_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ManageDevicesForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ManageDevicesForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ManageDevicesForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeleteAllBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RandonBtnAllocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BackBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView DeviceList;
        private System.Windows.Forms.ColumnHeader Alias;
        private System.Windows.Forms.ColumnHeader Role;
        private System.Windows.Forms.ColumnHeader SATRID;
        private System.Windows.Forms.PictureBox SearchBtn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox DeleteAllBtn;
        private System.Windows.Forms.PictureBox RandonBtnAllocation;
        private System.Windows.Forms.Timer WaitRFTransmissionTimer;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox CloseAppPicture;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label DeviceCntLabel;
        private System.Windows.Forms.Label GamersLabel;
    }
}