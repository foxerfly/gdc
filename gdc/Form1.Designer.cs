namespace gdc
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.change_gdc = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // change_gdc
            // 
            this.change_gdc.Location = new System.Drawing.Point(192, 243);
            this.change_gdc.Name = "change_gdc";
            this.change_gdc.Size = new System.Drawing.Size(295, 89);
            this.change_gdc.TabIndex = 0;
            this.change_gdc.Text = "升级GDC";
            this.change_gdc.UseVisualStyleBackColor = true;
            this.change_gdc.Click += new System.EventHandler(this.change_gdc_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(26, 34);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(641, 187);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "请将桌面【ERP快捷方式】拖入这儿，再点击【升级GDC】按钮";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 362);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.change_gdc);
            this.Name = "Form1";
            this.Text = "顶立信息部";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button change_gdc;
        private System.Windows.Forms.TextBox textBox1;
    }
}

