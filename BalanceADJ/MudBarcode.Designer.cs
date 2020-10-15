namespace BalanceADJ
{
    partial class form_MudBarcode
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
            this.txt_MudCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_MudCode
            // 
            this.txt_MudCode.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_MudCode.Location = new System.Drawing.Point(12, 58);
            this.txt_MudCode.Name = "txt_MudCode";
            this.txt_MudCode.Size = new System.Drawing.Size(700, 62);
            this.txt_MudCode.TabIndex = 0;
            this.txt_MudCode.TextChanged += new System.EventHandler(this.txt_MudCode_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 48);
            this.label1.TabIndex = 2;
            this.label1.Text = "请扫描平衡泥二维码";
            // 
            // form_MudBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 132);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_MudCode);
            this.Name = "form_MudBarcode";
            this.Text = "MudBarcode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.form_MudBarcode_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_MudCode;
        private System.Windows.Forms.Label label1;
    }
}