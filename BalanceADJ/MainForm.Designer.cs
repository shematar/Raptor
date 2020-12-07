namespace BalanceADJ
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgv_Balance = new System.Windows.Forms.DataGridView();
            this.Type2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VibAngl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VibAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorAngl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cht_FaceWave = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bt_Start = new System.Windows.Forms.Button();
            this.bt_Setting = new System.Windows.Forms.Button();
            this.timer_wave = new System.Windows.Forms.Timer(this.components);
            this.lb_Voiltage = new System.Windows.Forms.Label();
            this.trackBar_Vol = new System.Windows.Forms.TrackBar();
            this.txtBox_message = new System.Windows.Forms.TextBox();
            this.lbl_Kind = new System.Windows.Forms.Label();
            this.lbl_CurrentKind = new System.Windows.Forms.Label();
            this.bt_test = new System.Windows.Forms.Button();
            this.bt_Power = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_FaceResult = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_AdjResult = new System.Windows.Forms.Label();
            this.lb_OkNg = new System.Windows.Forms.Label();
            this.cb_HighSpeed = new System.Windows.Forms.CheckBox();
            this.bt_Init = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_FcMax = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lb_FcMin = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbx_MasterChk = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbx_Balance = new System.Windows.Forms.CheckBox();
            this.cbx_FaceChk = new System.Windows.Forms.CheckBox();
            this.cbx_IsManul = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timer_Face = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Balance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cht_FaceWave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Vol)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_Balance
            // 
            this.dgv_Balance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Balance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type2,
            this.RPM,
            this.VibAngl,
            this.VibAmnt,
            this.CorAngl,
            this.CorMass});
            this.dgv_Balance.Location = new System.Drawing.Point(21, 62);
            this.dgv_Balance.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_Balance.Name = "dgv_Balance";
            this.dgv_Balance.RowTemplate.Height = 23;
            this.dgv_Balance.Size = new System.Drawing.Size(860, 130);
            this.dgv_Balance.TabIndex = 99;
            // 
            // Type2
            // 
            this.Type2.HeaderText = "Type";
            this.Type2.Name = "Type2";
            this.Type2.ReadOnly = true;
            // 
            // RPM
            // 
            this.RPM.HeaderText = "RPM";
            this.RPM.Name = "RPM";
            this.RPM.ReadOnly = true;
            // 
            // VibAngl
            // 
            this.VibAngl.HeaderText = "Vib.Angl";
            this.VibAngl.Name = "VibAngl";
            this.VibAngl.ReadOnly = true;
            // 
            // VibAmnt
            // 
            this.VibAmnt.HeaderText = "Vib.Amnt";
            this.VibAmnt.Name = "VibAmnt";
            this.VibAmnt.ReadOnly = true;
            // 
            // CorAngl
            // 
            this.CorAngl.HeaderText = "Cor.Angl";
            this.CorAngl.Name = "CorAngl";
            this.CorAngl.ReadOnly = true;
            // 
            // CorMass
            // 
            this.CorMass.HeaderText = "Cor.Mass";
            this.CorMass.Name = "CorMass";
            this.CorMass.ReadOnly = true;
            // 
            // cht_FaceWave
            // 
            this.cht_FaceWave.BackColor = System.Drawing.Color.Black;
            this.cht_FaceWave.Location = new System.Drawing.Point(21, 256);
            this.cht_FaceWave.Margin = new System.Windows.Forms.Padding(4);
            this.cht_FaceWave.Name = "cht_FaceWave";
            this.cht_FaceWave.Size = new System.Drawing.Size(1332, 370);
            this.cht_FaceWave.TabIndex = 1;
            this.cht_FaceWave.Text = "FaceWaveChart";
            // 
            // bt_Start
            // 
            this.bt_Start.Location = new System.Drawing.Point(356, 195);
            this.bt_Start.Margin = new System.Windows.Forms.Padding(4);
            this.bt_Start.Name = "bt_Start";
            this.bt_Start.Size = new System.Drawing.Size(100, 29);
            this.bt_Start.TabIndex = 6;
            this.bt_Start.Text = "开始运行";
            this.bt_Start.UseVisualStyleBackColor = true;
            this.bt_Start.Click += new System.EventHandler(this.bt_Start_Click);
            // 
            // bt_Setting
            // 
            this.bt_Setting.Location = new System.Drawing.Point(248, 195);
            this.bt_Setting.Margin = new System.Windows.Forms.Padding(4);
            this.bt_Setting.Name = "bt_Setting";
            this.bt_Setting.Size = new System.Drawing.Size(100, 29);
            this.bt_Setting.TabIndex = 7;
            this.bt_Setting.Text = "设置";
            this.bt_Setting.UseVisualStyleBackColor = true;
            this.bt_Setting.Click += new System.EventHandler(this.bt_Setting_Click);
            // 
            // timer_wave
            // 
            this.timer_wave.Tick += new System.EventHandler(this.timer_wave_Tick);
            // 
            // lb_Voiltage
            // 
            this.lb_Voiltage.AutoSize = true;
            this.lb_Voiltage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Voiltage.Location = new System.Drawing.Point(16, 28);
            this.lb_Voiltage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Voiltage.Name = "lb_Voiltage";
            this.lb_Voiltage.Size = new System.Drawing.Size(59, 20);
            this.lb_Voiltage.TabIndex = 8;
            this.lb_Voiltage.Text = "0.000";
            // 
            // trackBar_Vol
            // 
            this.trackBar_Vol.Location = new System.Drawing.Point(37, 51);
            this.trackBar_Vol.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar_Vol.Maximum = 10000;
            this.trackBar_Vol.Name = "trackBar_Vol";
            this.trackBar_Vol.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_Vol.Size = new System.Drawing.Size(56, 145);
            this.trackBar_Vol.TabIndex = 0;
            this.trackBar_Vol.Value = 5000;
            this.trackBar_Vol.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trackBar_Vol_KeyDown);
            // 
            // txtBox_message
            // 
            this.txtBox_message.Location = new System.Drawing.Point(889, 642);
            this.txtBox_message.Margin = new System.Windows.Forms.Padding(4);
            this.txtBox_message.MaxLength = 0;
            this.txtBox_message.Multiline = true;
            this.txtBox_message.Name = "txtBox_message";
            this.txtBox_message.ReadOnly = true;
            this.txtBox_message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBox_message.Size = new System.Drawing.Size(464, 70);
            this.txtBox_message.TabIndex = 10;
            this.txtBox_message.TextChanged += new System.EventHandler(this.txtBox_message_TextChanged);
            // 
            // lbl_Kind
            // 
            this.lbl_Kind.AutoSize = true;
            this.lbl_Kind.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Kind.Location = new System.Drawing.Point(24, 11);
            this.lbl_Kind.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Kind.Name = "lbl_Kind";
            this.lbl_Kind.Size = new System.Drawing.Size(177, 48);
            this.lbl_Kind.TabIndex = 11;
            this.lbl_Kind.Text = "当前机种:";
            // 
            // lbl_CurrentKind
            // 
            this.lbl_CurrentKind.AutoSize = true;
            this.lbl_CurrentKind.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_CurrentKind.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbl_CurrentKind.Location = new System.Drawing.Point(219, 11);
            this.lbl_CurrentKind.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_CurrentKind.Name = "lbl_CurrentKind";
            this.lbl_CurrentKind.Size = new System.Drawing.Size(140, 48);
            this.lbl_CurrentKind.TabIndex = 12;
            this.lbl_CurrentKind.Text = "Raptor";
            // 
            // bt_test
            // 
            this.bt_test.Location = new System.Drawing.Point(773, 26);
            this.bt_test.Margin = new System.Windows.Forms.Padding(4);
            this.bt_test.Name = "bt_test";
            this.bt_test.Size = new System.Drawing.Size(100, 29);
            this.bt_test.TabIndex = 13;
            this.bt_test.Text = "面平衡";
            this.bt_test.UseVisualStyleBackColor = true;
            this.bt_test.Visible = false;
            this.bt_test.Click += new System.EventHandler(this.bt_ReInit_Click);
            // 
            // bt_Power
            // 
            this.bt_Power.Location = new System.Drawing.Point(557, 26);
            this.bt_Power.Margin = new System.Windows.Forms.Padding(4);
            this.bt_Power.Name = "bt_Power";
            this.bt_Power.Size = new System.Drawing.Size(100, 29);
            this.bt_Power.TabIndex = 14;
            this.bt_Power.Text = "PowerOn";
            this.bt_Power.UseVisualStyleBackColor = true;
            this.bt_Power.Visible = false;
            this.bt_Power.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(21, 222);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 27);
            this.label2.TabIndex = 15;
            this.label2.Text = "面平衡结果";
            // 
            // lb_FaceResult
            // 
            this.lb_FaceResult.AutoSize = true;
            this.lb_FaceResult.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_FaceResult.Location = new System.Drawing.Point(171, 222);
            this.lb_FaceResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_FaceResult.Name = "lb_FaceResult";
            this.lb_FaceResult.Size = new System.Drawing.Size(65, 27);
            this.lb_FaceResult.TabIndex = 15;
            this.lb_FaceResult.Text = "0.000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(245, 222);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 27);
            this.label3.TabIndex = 15;
            this.label3.Text = "mm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(88, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 20);
            this.label1.TabIndex = 100;
            this.label1.Text = "V";
            // 
            // lb_AdjResult
            // 
            this.lb_AdjResult.BackColor = System.Drawing.SystemColors.Control;
            this.lb_AdjResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_AdjResult.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_AdjResult.Location = new System.Drawing.Point(21, 642);
            this.lb_AdjResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_AdjResult.Name = "lb_AdjResult";
            this.lb_AdjResult.Size = new System.Drawing.Size(860, 71);
            this.lb_AdjResult.TabIndex = 101;
            this.lb_AdjResult.Text = "待机中";
            this.lb_AdjResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_OkNg
            // 
            this.lb_OkNg.BackColor = System.Drawing.SystemColors.Control;
            this.lb_OkNg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_OkNg.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_OkNg.Location = new System.Drawing.Point(284, 28);
            this.lb_OkNg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_OkNg.Name = "lb_OkNg";
            this.lb_OkNg.Size = new System.Drawing.Size(171, 150);
            this.lb_OkNg.TabIndex = 102;
            this.lb_OkNg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_HighSpeed
            // 
            this.cb_HighSpeed.AutoSize = true;
            this.cb_HighSpeed.Location = new System.Drawing.Point(33, 204);
            this.cb_HighSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.cb_HighSpeed.Name = "cb_HighSpeed";
            this.cb_HighSpeed.Size = new System.Drawing.Size(59, 19);
            this.cb_HighSpeed.TabIndex = 103;
            this.cb_HighSpeed.Text = "快速";
            this.cb_HighSpeed.UseVisualStyleBackColor = true;
            // 
            // bt_Init
            // 
            this.bt_Init.Location = new System.Drawing.Point(449, 26);
            this.bt_Init.Margin = new System.Windows.Forms.Padding(4);
            this.bt_Init.Name = "bt_Init";
            this.bt_Init.Size = new System.Drawing.Size(100, 29);
            this.bt_Init.TabIndex = 104;
            this.bt_Init.Text = "初始化";
            this.bt_Init.UseVisualStyleBackColor = true;
            this.bt_Init.Visible = false;
            this.bt_Init.Click += new System.EventHandler(this.bt_Init_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(355, 222);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 27);
            this.label4.TabIndex = 105;
            this.label4.Text = "MAX:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(628, 222);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 27);
            this.label5.TabIndex = 105;
            this.label5.Text = "MIN:";
            // 
            // lb_FcMax
            // 
            this.lb_FcMax.AutoSize = true;
            this.lb_FcMax.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_FcMax.Location = new System.Drawing.Point(431, 222);
            this.lb_FcMax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_FcMax.Name = "lb_FcMax";
            this.lb_FcMax.Size = new System.Drawing.Size(65, 27);
            this.lb_FcMax.TabIndex = 106;
            this.lb_FcMax.Text = "0.000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(508, 222);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 27);
            this.label7.TabIndex = 15;
            this.label7.Text = "mm";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(791, 222);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 27);
            this.label8.TabIndex = 15;
            this.label8.Text = "mm";
            // 
            // lb_FcMin
            // 
            this.lb_FcMin.AutoSize = true;
            this.lb_FcMin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_FcMin.Location = new System.Drawing.Point(704, 222);
            this.lb_FcMin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_FcMin.Name = "lb_FcMin";
            this.lb_FcMin.Size = new System.Drawing.Size(65, 27);
            this.lb_FcMin.TabIndex = 106;
            this.lb_FcMin.Text = "0.000";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbx_MasterChk);
            this.groupBox1.Controls.Add(this.lb_OkNg);
            this.groupBox1.Controls.Add(this.bt_Start);
            this.groupBox1.Controls.Add(this.bt_Setting);
            this.groupBox1.Controls.Add(this.lb_Voiltage);
            this.groupBox1.Controls.Add(this.trackBar_Vol);
            this.groupBox1.Controls.Add(this.cb_HighSpeed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(889, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(464, 234);
            this.groupBox1.TabIndex = 107;
            this.groupBox1.TabStop = false;
            // 
            // cbx_MasterChk
            // 
            this.cbx_MasterChk.AutoSize = true;
            this.cbx_MasterChk.Location = new System.Drawing.Point(147, 201);
            this.cbx_MasterChk.Name = "cbx_MasterChk";
            this.cbx_MasterChk.Size = new System.Drawing.Size(59, 19);
            this.cbx_MasterChk.TabIndex = 110;
            this.cbx_MasterChk.Text = "点检";
            this.cbx_MasterChk.UseVisualStyleBackColor = true;
            this.cbx_MasterChk.CheckedChanged += new System.EventHandler(this.cbx_MasterChk_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbx_Balance);
            this.groupBox2.Controls.Add(this.cbx_FaceChk);
            this.groupBox2.Controls.Add(this.cbx_IsManul);
            this.groupBox2.Location = new System.Drawing.Point(127, 28);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(128, 149);
            this.groupBox2.TabIndex = 109;
            this.groupBox2.TabStop = false;
            // 
            // cbx_Balance
            // 
            this.cbx_Balance.AutoSize = true;
            this.cbx_Balance.Enabled = false;
            this.cbx_Balance.Location = new System.Drawing.Point(20, 125);
            this.cbx_Balance.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_Balance.Name = "cbx_Balance";
            this.cbx_Balance.Size = new System.Drawing.Size(74, 19);
            this.cbx_Balance.TabIndex = 108;
            this.cbx_Balance.Text = "动平衡";
            this.cbx_Balance.UseVisualStyleBackColor = true;
            this.cbx_Balance.CheckedChanged += new System.EventHandler(this.cbx_Balance_CheckedChanged);
            // 
            // cbx_FaceChk
            // 
            this.cbx_FaceChk.AutoSize = true;
            this.cbx_FaceChk.Enabled = false;
            this.cbx_FaceChk.Location = new System.Drawing.Point(20, 74);
            this.cbx_FaceChk.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_FaceChk.Name = "cbx_FaceChk";
            this.cbx_FaceChk.Size = new System.Drawing.Size(74, 19);
            this.cbx_FaceChk.TabIndex = 107;
            this.cbx_FaceChk.Text = "面平衡";
            this.cbx_FaceChk.UseVisualStyleBackColor = true;
            this.cbx_FaceChk.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // cbx_IsManul
            // 
            this.cbx_IsManul.AutoSize = true;
            this.cbx_IsManul.Location = new System.Drawing.Point(20, 22);
            this.cbx_IsManul.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_IsManul.Name = "cbx_IsManul";
            this.cbx_IsManul.Size = new System.Drawing.Size(59, 19);
            this.cbx_IsManul.TabIndex = 106;
            this.cbx_IsManul.Text = "手动";
            this.cbx_IsManul.UseVisualStyleBackColor = true;
            this.cbx_IsManul.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(665, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 105;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer_Face
            // 
            this.timer_Face.Interval = 8000;
            this.timer_Face.Tick += new System.EventHandler(this.timer_Face_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 725);
            this.Controls.Add(this.lb_FcMin);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lb_FcMax);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb_AdjResult);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bt_test);
            this.Controls.Add(this.bt_Init);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bt_Power);
            this.Controls.Add(this.lb_FaceResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_CurrentKind);
            this.Controls.Add(this.lbl_Kind);
            this.Controls.Add(this.txtBox_message);
            this.Controls.Add(this.cht_FaceWave);
            this.Controls.Add(this.dgv_Balance);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Balance ADJ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_Close);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Balance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cht_FaceWave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Vol)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type2;
        private System.Windows.Forms.DataGridViewTextBoxColumn RPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn VibAngl;
        private System.Windows.Forms.DataGridViewTextBoxColumn VibAmnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorAngl;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorMass;
        private System.Windows.Forms.DataVisualization.Charting.Chart cht_FaceWave;
        private System.Windows.Forms.Button bt_Start;
        private System.Windows.Forms.Button bt_Setting;
        private System.Windows.Forms.Timer timer_wave;
        private System.Windows.Forms.Label lb_Voiltage;
        private System.Windows.Forms.TrackBar trackBar_Vol;
        private System.Windows.Forms.TextBox txtBox_message;
        private System.Windows.Forms.Label lbl_Kind;
        private System.Windows.Forms.Label lbl_CurrentKind;
        private System.Windows.Forms.Button bt_test;
        private System.Windows.Forms.Button bt_Power;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_FaceResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_AdjResult;
        private System.Windows.Forms.Label lb_OkNg;
        private System.Windows.Forms.CheckBox cb_HighSpeed;
        private System.Windows.Forms.Button bt_Init;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_FcMax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lb_FcMin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer_Face;
        private System.Windows.Forms.CheckBox cbx_IsManul;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbx_Balance;
        private System.Windows.Forms.CheckBox cbx_FaceChk;
        private System.Windows.Forms.CheckBox cbx_MasterChk;

    }
}

