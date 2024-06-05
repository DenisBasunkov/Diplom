namespace Диспетчерская
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Заявки = new System.Windows.Forms.TabPage();
            this.ZaiavkaPDF = new System.Windows.Forms.WebBrowser();
            this.Отчет = new System.Windows.Forms.TabPage();
            this.OtchetPDF = new System.Windows.Forms.WebBrowser();
            this.Администрирование = new System.Windows.Forms.TabPage();
            this.AdminPDF = new System.Windows.Forms.WebBrowser();
            this.tabControl1.SuspendLayout();
            this.Заявки.SuspendLayout();
            this.Отчет.SuspendLayout();
            this.Администрирование.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Заявки);
            this.tabControl1.Controls.Add(this.Отчет);
            this.tabControl1.Controls.Add(this.Администрирование);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(20, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1330, 692);
            this.tabControl1.TabIndex = 0;
            // 
            // Заявки
            // 
            this.Заявки.Controls.Add(this.ZaiavkaPDF);
            this.Заявки.Location = new System.Drawing.Point(4, 22);
            this.Заявки.Name = "Заявки";
            this.Заявки.Padding = new System.Windows.Forms.Padding(3);
            this.Заявки.Size = new System.Drawing.Size(1322, 666);
            this.Заявки.TabIndex = 1;
            this.Заявки.Text = "Работы по заявкам";
            this.Заявки.UseVisualStyleBackColor = true;
            // 
            // ZaiavkaPDF
            // 
            this.ZaiavkaPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZaiavkaPDF.Location = new System.Drawing.Point(3, 3);
            this.ZaiavkaPDF.MinimumSize = new System.Drawing.Size(20, 20);
            this.ZaiavkaPDF.Name = "ZaiavkaPDF";
            this.ZaiavkaPDF.Size = new System.Drawing.Size(1316, 660);
            this.ZaiavkaPDF.TabIndex = 0;
            // 
            // Отчет
            // 
            this.Отчет.Controls.Add(this.OtchetPDF);
            this.Отчет.Location = new System.Drawing.Point(4, 22);
            this.Отчет.Name = "Отчет";
            this.Отчет.Size = new System.Drawing.Size(1322, 666);
            this.Отчет.TabIndex = 2;
            this.Отчет.Text = "Формирование отчета";
            this.Отчет.UseVisualStyleBackColor = true;
            // 
            // OtchetPDF
            // 
            this.OtchetPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OtchetPDF.Location = new System.Drawing.Point(0, 0);
            this.OtchetPDF.MinimumSize = new System.Drawing.Size(20, 20);
            this.OtchetPDF.Name = "OtchetPDF";
            this.OtchetPDF.Size = new System.Drawing.Size(1322, 666);
            this.OtchetPDF.TabIndex = 0;
            // 
            // Администрирование
            // 
            this.Администрирование.Controls.Add(this.AdminPDF);
            this.Администрирование.Location = new System.Drawing.Point(4, 22);
            this.Администрирование.Name = "Администрирование";
            this.Администрирование.Size = new System.Drawing.Size(1322, 666);
            this.Администрирование.TabIndex = 3;
            this.Администрирование.Text = "Администрирование";
            this.Администрирование.UseVisualStyleBackColor = true;
            // 
            // AdminPDF
            // 
            this.AdminPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AdminPDF.Location = new System.Drawing.Point(0, 0);
            this.AdminPDF.MinimumSize = new System.Drawing.Size(20, 20);
            this.AdminPDF.Name = "AdminPDF";
            this.AdminPDF.Size = new System.Drawing.Size(1322, 666);
            this.AdminPDF.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 772);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Инструкция";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.tabControl1.ResumeLayout(false);
            this.Заявки.ResumeLayout(false);
            this.Отчет.ResumeLayout(false);
            this.Администрирование.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Заявки;
        private System.Windows.Forms.TabPage Отчет;
        private System.Windows.Forms.TabPage Администрирование;
        private System.Windows.Forms.WebBrowser AdminPDF;
        private System.Windows.Forms.WebBrowser ZaiavkaPDF;
        private System.Windows.Forms.WebBrowser OtchetPDF;
    }
}