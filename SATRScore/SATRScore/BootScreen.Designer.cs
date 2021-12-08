namespace SATRScore
{
    partial class BootScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BootScreen));
            this.genreBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sATRScoreDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sATRScoreDataSet = new SATRScore.SATRScoreDataSet();
            this.MissionBadge = new System.Windows.Forms.PictureBox();
            this.ConfigBtn = new System.Windows.Forms.Button();
            this.SyncBtn = new System.Windows.Forms.Button();
            this.ChangeMissionBtn = new System.Windows.Forms.Button();
            this.GoLiveBTN = new System.Windows.Forms.Button();
            this.REbuildDBBtn = new System.Windows.Forms.Button();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.MissionLabel = new System.Windows.Forms.Label();
            this.CloseTip = new System.Windows.Forms.ToolTip(this.components);
            this.CloseAppPicture = new System.Windows.Forms.PictureBox();
            this.genreTableAdapter = new SATRScore.SATRScoreDataSetTableAdapters.GenreTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.genreBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sATRScoreDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sATRScoreDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MissionBadge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // genreBindingSource
            // 
            this.genreBindingSource.DataMember = "Genre";
            this.genreBindingSource.DataSource = this.sATRScoreDataSetBindingSource;
            // 
            // sATRScoreDataSetBindingSource
            // 
            this.sATRScoreDataSetBindingSource.DataSource = this.sATRScoreDataSet;
            this.sATRScoreDataSetBindingSource.Position = 0;
            // 
            // sATRScoreDataSet
            // 
            this.sATRScoreDataSet.DataSetName = "SATRScoreDataSet";
            this.sATRScoreDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MissionBadge
            // 
            this.MissionBadge.BackColor = System.Drawing.Color.Transparent;
            this.MissionBadge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MissionBadge.Enabled = false;
            this.MissionBadge.InitialImage = null;
            this.MissionBadge.Location = new System.Drawing.Point(763, 189);
            this.MissionBadge.Margin = new System.Windows.Forms.Padding(0);
            this.MissionBadge.MinimumSize = new System.Drawing.Size(400, 400);
            this.MissionBadge.Name = "MissionBadge";
            this.MissionBadge.Size = new System.Drawing.Size(400, 400);
            this.MissionBadge.TabIndex = 6;
            this.MissionBadge.TabStop = false;
            this.MissionBadge.Paint += new System.Windows.Forms.PaintEventHandler(this.MissionBadge_Paint);
            // 
            // ConfigBtn
            // 
            this.ConfigBtn.BackColor = System.Drawing.Color.Transparent;
            this.ConfigBtn.BackgroundImage = global::SATRScore.Properties.Resources.ConfigBtn;
            this.ConfigBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ConfigBtn.FlatAppearance.BorderSize = 0;
            this.ConfigBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConfigBtn.Location = new System.Drawing.Point(2, 511);
            this.ConfigBtn.Name = "ConfigBtn";
            this.ConfigBtn.Size = new System.Drawing.Size(755, 97);
            this.ConfigBtn.TabIndex = 4;
            this.ConfigBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ConfigBtn.UseVisualStyleBackColor = false;
            this.ConfigBtn.Click += new System.EventHandler(this.ConfigBtn_Click);
            // 
            // SyncBtn
            // 
            this.SyncBtn.BackColor = System.Drawing.Color.Transparent;
            this.SyncBtn.BackgroundImage = global::SATRScore.Properties.Resources.SyncButton;
            this.SyncBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SyncBtn.FlatAppearance.BorderSize = 0;
            this.SyncBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SyncBtn.Location = new System.Drawing.Point(2, 405);
            this.SyncBtn.Name = "SyncBtn";
            this.SyncBtn.Size = new System.Drawing.Size(755, 100);
            this.SyncBtn.TabIndex = 3;
            this.SyncBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SyncBtn.UseVisualStyleBackColor = false;
            this.SyncBtn.Click += new System.EventHandler(this.SyncBtn_Click);
            // 
            // ChangeMissionBtn
            // 
            this.ChangeMissionBtn.BackColor = System.Drawing.Color.Transparent;
            this.ChangeMissionBtn.BackgroundImage = global::SATRScore.Properties.Resources.ChangeBtn;
            this.ChangeMissionBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ChangeMissionBtn.FlatAppearance.BorderSize = 0;
            this.ChangeMissionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangeMissionBtn.Location = new System.Drawing.Point(2, 307);
            this.ChangeMissionBtn.Name = "ChangeMissionBtn";
            this.ChangeMissionBtn.Size = new System.Drawing.Size(755, 92);
            this.ChangeMissionBtn.TabIndex = 2;
            this.ChangeMissionBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ChangeMissionBtn.UseVisualStyleBackColor = false;
            this.ChangeMissionBtn.Click += new System.EventHandler(this.ChangeMissionBtn_Click);
            // 
            // GoLiveBTN
            // 
            this.GoLiveBTN.BackgroundImage = global::SATRScore.Properties.Resources.GoLiveButton;
            this.GoLiveBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GoLiveBTN.FlatAppearance.BorderSize = 0;
            this.GoLiveBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GoLiveBTN.Location = new System.Drawing.Point(2, 206);
            this.GoLiveBTN.Name = "GoLiveBTN";
            this.GoLiveBTN.Size = new System.Drawing.Size(755, 95);
            this.GoLiveBTN.TabIndex = 0;
            this.GoLiveBTN.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.CloseTip.SetToolTip(this.GoLiveBTN, "Reset Device/Gamer Stats/Start Mission Option (no timer)");
            this.GoLiveBTN.UseVisualStyleBackColor = true;
            this.GoLiveBTN.Click += new System.EventHandler(this.GoLiveBTN_Click);
            // 
            // REbuildDBBtn
            // 
            this.REbuildDBBtn.Location = new System.Drawing.Point(1166, 529);
            this.REbuildDBBtn.Name = "REbuildDBBtn";
            this.REbuildDBBtn.Size = new System.Drawing.Size(104, 39);
            this.REbuildDBBtn.TabIndex = 7;
            this.REbuildDBBtn.Text = "Rebuild DB";
            this.REbuildDBBtn.UseVisualStyleBackColor = true;
            this.REbuildDBBtn.Visible = false;
            this.REbuildDBBtn.Click += new System.EventHandler(this.REbuildDBBtn_Click);
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 1000;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // MissionLabel
            // 
            this.MissionLabel.BackColor = System.Drawing.Color.Transparent;
            this.MissionLabel.Font = new System.Drawing.Font("Modern No. 20", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MissionLabel.ForeColor = System.Drawing.Color.LawnGreen;
            this.MissionLabel.Location = new System.Drawing.Point(763, 589);
            this.MissionLabel.Name = "MissionLabel";
            this.MissionLabel.Size = new System.Drawing.Size(400, 52);
            this.MissionLabel.TabIndex = 9;
            this.MissionLabel.Text = "MissionLabel";
            this.MissionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseTip
            // 
            this.CloseTip.Popup += new System.Windows.Forms.PopupEventHandler(this.CloseTip_Popup);
            // 
            // CloseAppPicture
            // 
            this.CloseAppPicture.BackColor = System.Drawing.Color.Transparent;
            this.CloseAppPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseAppPicture.BackgroundImage")));
            this.CloseAppPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseAppPicture.Location = new System.Drawing.Point(1177, 574);
            this.CloseAppPicture.Name = "CloseAppPicture";
            this.CloseAppPicture.Size = new System.Drawing.Size(80, 80);
            this.CloseAppPicture.TabIndex = 10;
            this.CloseAppPicture.TabStop = false;
            this.CloseTip.SetToolTip(this.CloseAppPicture, "Close Application");
            this.CloseAppPicture.Click += new System.EventHandler(this.CloseAppPicture_Click);
            // 
            // genreTableAdapter
            // 
            this.genreTableAdapter.ClearBeforeFill = true;
            // 
            // BootScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1269, 661);
            this.Controls.Add(this.MissionLabel);
            this.Controls.Add(this.REbuildDBBtn);
            this.Controls.Add(this.MissionBadge);
            this.Controls.Add(this.ConfigBtn);
            this.Controls.Add(this.SyncBtn);
            this.Controls.Add(this.ChangeMissionBtn);
            this.Controls.Add(this.GoLiveBTN);
            this.Controls.Add(this.CloseAppPicture);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BootScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SATRScore - Boot Screen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BootScreen_FormClosed);
            this.Load += new System.EventHandler(this.BootScreen_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BootScreen_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BootScreen_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BootScreen_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.genreBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sATRScoreDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sATRScoreDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MissionBadge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAppPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource sATRScoreDataSetBindingSource;
        private SATRScoreDataSet sATRScoreDataSet;
        private System.Windows.Forms.BindingSource genreBindingSource;
        private SATRScoreDataSetTableAdapters.GenreTableAdapter genreTableAdapter;
        private System.Windows.Forms.Button GoLiveBTN;
        private System.Windows.Forms.Button ChangeMissionBtn;
        private System.Windows.Forms.Button SyncBtn;
        private System.Windows.Forms.Button ConfigBtn;
        private System.Windows.Forms.PictureBox MissionBadge;
        private System.Windows.Forms.Button REbuildDBBtn;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label MissionLabel;
        private System.Windows.Forms.ToolTip CloseTip;
        private System.Windows.Forms.PictureBox CloseAppPicture;
    }
}