namespace OnlineShopClient
{
    partial class ShopperForm
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
            this.btnPurchase = new System.Windows.Forms.Button();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.comboBoxList = new System.Windows.Forms.ComboBox();
            this.btnTimerSwitch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.listBoxOrders = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnPurchase
            // 
            this.btnPurchase.Enabled = false;
            this.btnPurchase.Location = new System.Drawing.Point(316, 188);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(80, 23);
            this.btnPurchase.TabIndex = 1;
            this.btnPurchase.Text = "Purchase";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_ClickAsync);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // comboBoxList
            // 
            this.comboBoxList.FormattingEnabled = true;
            this.comboBoxList.Location = new System.Drawing.Point(219, 12);
            this.comboBoxList.Name = "comboBoxList";
            this.comboBoxList.Size = new System.Drawing.Size(177, 21);
            this.comboBoxList.TabIndex = 0;
            this.comboBoxList.SelectedIndexChanged += new System.EventHandler(this.comboBoxList_SelectedIndexChanged);
            // 
            // btnTimerSwitch
            // 
            this.btnTimerSwitch.Location = new System.Drawing.Point(316, 159);
            this.btnTimerSwitch.Name = "btnTimerSwitch";
            this.btnTimerSwitch.Size = new System.Drawing.Size(80, 23);
            this.btnTimerSwitch.TabIndex = 3;
            this.btnTimerSwitch.Text = "Timer Switch";
            this.btnTimerSwitch.UseVisualStyleBackColor = true;
            this.btnTimerSwitch.Click += new System.EventHandler(this.btnTimerSwitch_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(230, 188);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // listBoxOrders
            // 
            this.listBoxOrders.FormattingEnabled = true;
            this.listBoxOrders.Location = new System.Drawing.Point(12, 12);
            this.listBoxOrders.Name = "listBoxOrders";
            this.listBoxOrders.Size = new System.Drawing.Size(201, 199);
            this.listBoxOrders.TabIndex = 4;
            // 
            // ShopperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 223);
            this.Controls.Add(this.listBoxOrders);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnTimerSwitch);
            this.Controls.Add(this.comboBoxList);
            this.Controls.Add(this.btnPurchase);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(425, 262);
            this.MinimumSize = new System.Drawing.Size(425, 262);
            this.Name = "ShopperForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shopper Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShopperForm_FormClosed);
            this.Load += new System.EventHandler(this.ShopperForm_LoadAsync);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.ComboBox comboBoxList;
        private System.Windows.Forms.Button btnTimerSwitch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListBox listBoxOrders;
    }
}