namespace BalanceADJ
{
    partial class NewKind
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
            this.txt_NewKindName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Pre_8V = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Pre_18V = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_NewKindSave = new System.Windows.Forms.Button();
            this.bt_Cancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Mud_No = new System.Windows.Forms.TextBox();
            this.txt_ProANo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_LaserSpecD = new System.Windows.Forms.TextBox();
            this.txt_LaserMaxPt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_LaserSpec = new System.Windows.Forms.TextBox();
            this.cbx_LaserNo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Pre_8V_Low = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_CorMass = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_MasterProANo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_NewKindName
            // 
            this.txt_NewKindName.Location = new System.Drawing.Point(95, 36);
            this.txt_NewKindName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_NewKindName.Name = "txt_NewKindName";
            this.txt_NewKindName.Size = new System.Drawing.Size(267, 25);
            this.txt_NewKindName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "机种名：";
            // 
            // txt_Pre_8V
            // 
            this.txt_Pre_8V.Location = new System.Drawing.Point(159, 136);
            this.txt_Pre_8V.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_Pre_8V.Name = "txt_Pre_8V";
            this.txt_Pre_8V.Size = new System.Drawing.Size(169, 25);
            this.txt_Pre_8V.TabIndex = 0;
            this.txt_Pre_8V.Text = "1.4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 141);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "高速电压初始值：";
            // 
            // txt_Pre_18V
            // 
            this.txt_Pre_18V.Location = new System.Drawing.Point(159, 186);
            this.txt_Pre_18V.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_Pre_18V.Name = "txt_Pre_18V";
            this.txt_Pre_18V.Size = new System.Drawing.Size(169, 25);
            this.txt_Pre_18V.TabIndex = 0;
            this.txt_Pre_18V.Text = "12";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 191);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "驱动电压初始值：";
            // 
            // bt_NewKindSave
            // 
            this.bt_NewKindSave.Location = new System.Drawing.Point(263, 674);
            this.bt_NewKindSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_NewKindSave.Name = "bt_NewKindSave";
            this.bt_NewKindSave.Size = new System.Drawing.Size(100, 29);
            this.bt_NewKindSave.TabIndex = 2;
            this.bt_NewKindSave.Text = "确定";
            this.bt_NewKindSave.UseVisualStyleBackColor = true;
            this.bt_NewKindSave.Click += new System.EventHandler(this.bt_NewKindSave_Click);
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.Location = new System.Drawing.Point(115, 674);
            this.bt_Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(100, 29);
            this.bt_Cancel.TabIndex = 2;
            this.bt_Cancel.Text = "返回";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 341);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "平衡泥识别符：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 241);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "产品A番号：";
            // 
            // txt_Mud_No
            // 
            this.txt_Mud_No.Location = new System.Drawing.Point(159, 336);
            this.txt_Mud_No.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_Mud_No.Name = "txt_Mud_No";
            this.txt_Mud_No.Size = new System.Drawing.Size(169, 25);
            this.txt_Mud_No.TabIndex = 3;
            this.txt_Mud_No.Text = "DW";
            // 
            // txt_ProANo
            // 
            this.txt_ProANo.Location = new System.Drawing.Point(159, 236);
            this.txt_ProANo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_ProANo.Name = "txt_ProANo";
            this.txt_ProANo.Size = new System.Drawing.Size(169, 25);
            this.txt_ProANo.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 541);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "面平衡下限值：";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 491);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "面平衡测定点数：";
            // 
            // txt_LaserSpecD
            // 
            this.txt_LaserSpecD.Location = new System.Drawing.Point(159, 534);
            this.txt_LaserSpecD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_LaserSpecD.Name = "txt_LaserSpecD";
            this.txt_LaserSpecD.Size = new System.Drawing.Size(169, 25);
            this.txt_LaserSpecD.TabIndex = 11;
            this.txt_LaserSpecD.Text = "0.0001";
            this.txt_LaserSpecD.Visible = false;
            // 
            // txt_LaserMaxPt
            // 
            this.txt_LaserMaxPt.Location = new System.Drawing.Point(159, 484);
            this.txt_LaserMaxPt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_LaserMaxPt.Name = "txt_LaserMaxPt";
            this.txt_LaserMaxPt.Size = new System.Drawing.Size(169, 25);
            this.txt_LaserMaxPt.TabIndex = 12;
            this.txt_LaserMaxPt.Text = "1000";
            this.txt_LaserMaxPt.TextChanged += new System.EventHandler(this.txt_LaserMaxPt_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 441);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "面平衡规格值：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 391);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "激光序号：";
            // 
            // txt_LaserSpec
            // 
            this.txt_LaserSpec.Location = new System.Drawing.Point(159, 434);
            this.txt_LaserSpec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_LaserSpec.Name = "txt_LaserSpec";
            this.txt_LaserSpec.Size = new System.Drawing.Size(169, 25);
            this.txt_LaserSpec.TabIndex = 7;
            this.txt_LaserSpec.Text = "0.150";
            // 
            // cbx_LaserNo
            // 
            this.cbx_LaserNo.AutoCompleteCustomSource.AddRange(new string[] {
            "01",
            "02"});
            this.cbx_LaserNo.FormattingEnabled = true;
            this.cbx_LaserNo.Location = new System.Drawing.Point(159, 386);
            this.cbx_LaserNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbx_LaserNo.Name = "cbx_LaserNo";
            this.cbx_LaserNo.Size = new System.Drawing.Size(169, 23);
            this.cbx_LaserNo.TabIndex = 15;
            this.cbx_LaserNo.Text = "01";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(337, 143);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 15);
            this.label10.TabIndex = 16;
            this.label10.Text = "V";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(337, 193);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "V";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(337, 438);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 15);
            this.label12.TabIndex = 17;
            this.label12.Text = "mm";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(337, 93);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 15);
            this.label15.TabIndex = 20;
            this.label15.Text = "V";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 91);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(127, 15);
            this.label16.TabIndex = 19;
            this.label16.Text = "低速电压初始值：";
            // 
            // txt_Pre_8V_Low
            // 
            this.txt_Pre_8V_Low.Location = new System.Drawing.Point(159, 86);
            this.txt_Pre_8V_Low.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_Pre_8V_Low.Name = "txt_Pre_8V_Low";
            this.txt_Pre_8V_Low.Size = new System.Drawing.Size(169, 25);
            this.txt_Pre_8V_Low.TabIndex = 18;
            this.txt_Pre_8V_Low.Text = "1.2";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(337, 592);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 15);
            this.label17.TabIndex = 23;
            this.label17.Text = "mg";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 591);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(97, 15);
            this.label18.TabIndex = 22;
            this.label18.Text = "动平衡规格：";
            // 
            // txt_CorMass
            // 
            this.txt_CorMass.Location = new System.Drawing.Point(159, 584);
            this.txt_CorMass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_CorMass.Name = "txt_CorMass";
            this.txt_CorMass.Size = new System.Drawing.Size(169, 25);
            this.txt_CorMass.TabIndex = 21;
            this.txt_CorMass.Text = "30";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(337, 539);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 15);
            this.label14.TabIndex = 17;
            this.label14.Text = "mm";
            this.label14.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 291);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 15);
            this.label13.TabIndex = 25;
            this.label13.Text = "点检A番号：";
            // 
            // txt_MasterProANo
            // 
            this.txt_MasterProANo.Location = new System.Drawing.Point(159, 286);
            this.txt_MasterProANo.Margin = new System.Windows.Forms.Padding(4);
            this.txt_MasterProANo.Name = "txt_MasterProANo";
            this.txt_MasterProANo.Size = new System.Drawing.Size(169, 25);
            this.txt_MasterProANo.TabIndex = 24;
            // 
            // NewKind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 734);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_MasterProANo);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txt_CorMass);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txt_Pre_8V_Low);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbx_LaserNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_LaserSpecD);
            this.Controls.Add(this.txt_LaserMaxPt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_LaserSpec);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Mud_No);
            this.Controls.Add(this.txt_ProANo);
            this.Controls.Add(this.bt_Cancel);
            this.Controls.Add(this.bt_NewKindSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Pre_18V);
            this.Controls.Add(this.txt_Pre_8V);
            this.Controls.Add(this.txt_NewKindName);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "NewKind";
            this.Text = "NewKind";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewKind_FormClosed);
            this.Load += new System.EventHandler(this.Kind_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_NewKindName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Pre_8V;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Pre_18V;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_NewKindSave;
        private System.Windows.Forms.Button bt_Cancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Mud_No;
        private System.Windows.Forms.TextBox txt_ProANo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_LaserSpecD;
        private System.Windows.Forms.TextBox txt_LaserMaxPt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_LaserSpec;
        private System.Windows.Forms.ComboBox cbx_LaserNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_Pre_8V_Low;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_CorMass;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_MasterProANo;
    }
}