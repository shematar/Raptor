namespace BalanceADJ
{
    partial class Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_KindSelect = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_KindSelect = new System.Windows.Forms.Button();
            this.bt_EditKind = new System.Windows.Forms.Button();
            this.bt_DeleteKind = new System.Windows.Forms.Button();
            this.bt_NewKind = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt_Rs232Set = new System.Windows.Forms.Button();
            this.bt_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbx_KindSelect
            // 
            this.cbx_KindSelect.FormattingEnabled = true;
            resources.ApplyResources(this.cbx_KindSelect, "cbx_KindSelect");
            this.cbx_KindSelect.Name = "cbx_KindSelect";
            this.cbx_KindSelect.SelectedIndexChanged += new System.EventHandler(this.cbx_KindSelect_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_KindSelect);
            this.groupBox1.Controls.Add(this.bt_EditKind);
            this.groupBox1.Controls.Add(this.bt_DeleteKind);
            this.groupBox1.Controls.Add(this.bt_NewKind);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // bt_KindSelect
            // 
            resources.ApplyResources(this.bt_KindSelect, "bt_KindSelect");
            this.bt_KindSelect.Name = "bt_KindSelect";
            this.bt_KindSelect.UseVisualStyleBackColor = true;
            this.bt_KindSelect.Click += new System.EventHandler(this.bt_KindSelect_Click);
            // 
            // bt_EditKind
            // 
            resources.ApplyResources(this.bt_EditKind, "bt_EditKind");
            this.bt_EditKind.Name = "bt_EditKind";
            this.bt_EditKind.UseVisualStyleBackColor = true;
            this.bt_EditKind.Click += new System.EventHandler(this.bt_EditKind_Click);
            // 
            // bt_DeleteKind
            // 
            resources.ApplyResources(this.bt_DeleteKind, "bt_DeleteKind");
            this.bt_DeleteKind.Name = "bt_DeleteKind";
            this.bt_DeleteKind.UseVisualStyleBackColor = true;
            this.bt_DeleteKind.Click += new System.EventHandler(this.bt_DeleteKind_Click);
            // 
            // bt_NewKind
            // 
            resources.ApplyResources(this.bt_NewKind, "bt_NewKind");
            this.bt_NewKind.Name = "bt_NewKind";
            this.bt_NewKind.UseVisualStyleBackColor = true;
            this.bt_NewKind.Click += new System.EventHandler(this.bt_NewKind_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_Rs232Set);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // bt_Rs232Set
            // 
            resources.ApplyResources(this.bt_Rs232Set, "bt_Rs232Set");
            this.bt_Rs232Set.Name = "bt_Rs232Set";
            this.bt_Rs232Set.UseVisualStyleBackColor = true;
            // 
            // bt_Cancel
            // 
            resources.ApplyResources(this.bt_Cancel, "bt_Cancel");
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
            // 
            // Setting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bt_Cancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbx_KindSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Setting";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Setting_FormClosed);
            this.Load += new System.EventHandler(this.Setting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbx_KindSelect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_EditKind;
        private System.Windows.Forms.Button bt_DeleteKind;
        private System.Windows.Forms.Button bt_NewKind;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bt_Rs232Set;
        private System.Windows.Forms.Button bt_KindSelect;
        private System.Windows.Forms.Button bt_Cancel;
    }
}