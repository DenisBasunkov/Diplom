namespace Диспетчерская
{
    partial class Registrat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registrat));
            this.panel1 = new System.Windows.Forms.Panel();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.label4 = new System.Windows.Forms.Label();
            this.Prof = new MetroFramework.Controls.MetroComboBox();
            this.bunifuProgressBar1 = new Bunifu.Framework.UI.BunifuProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Names = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.PasDuo = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.Pas = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.Log = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.bunifuImageButton1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 50);
            this.panel1.TabIndex = 1;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(318, 4);
            this.bunifuImageButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(38, 41);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 1;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(77, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 32);
            this.label4.TabIndex = 2;
            this.label4.Text = "Регистрация";
            // 
            // Prof
            // 
            this.Prof.BackColor = System.Drawing.Color.White;
            this.Prof.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Prof.FormattingEnabled = true;
            this.Prof.ItemHeight = 24;
            this.Prof.Items.AddRange(new object[] {
            "Оператор",
            "Диспетчер АВР"});
            this.Prof.Location = new System.Drawing.Point(8, 495);
            this.Prof.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Prof.Name = "Prof";
            this.Prof.Size = new System.Drawing.Size(333, 30);
            this.Prof.TabIndex = 16;
            this.Prof.UseCustomBackColor = true;
            this.Prof.UseCustomForeColor = true;
            this.Prof.UseSelectable = true;
            this.Prof.UseStyleColors = true;
            // 
            // bunifuProgressBar1
            // 
            this.bunifuProgressBar1.BackColor = System.Drawing.Color.Silver;
            this.bunifuProgressBar1.BorderRadius = 5;
            this.bunifuProgressBar1.Location = new System.Drawing.Point(14, 249);
            this.bunifuProgressBar1.Margin = new System.Windows.Forms.Padding(5);
            this.bunifuProgressBar1.MaximumValue = 100;
            this.bunifuProgressBar1.Name = "bunifuProgressBar1";
            this.bunifuProgressBar1.ProgressColor = System.Drawing.Color.Teal;
            this.bunifuProgressBar1.Size = new System.Drawing.Size(323, 17);
            this.bunifuProgressBar1.TabIndex = 15;
            this.bunifuProgressBar1.Value = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(7, 454);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 32);
            this.label6.TabIndex = 10;
            this.label6.Text = "Профиль";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(7, 362);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 32);
            this.label5.TabIndex = 11;
            this.label5.Text = "Имя оператора";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(7, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 32);
            this.label3.TabIndex = 12;
            this.label3.Text = "Повтор пароля";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(7, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 32);
            this.label2.TabIndex = 13;
            this.label2.Text = "Пароль";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(7, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 32);
            this.label1.TabIndex = 14;
            this.label1.Text = "Логин";
            // 
            // Names
            // 
            this.Names.BorderColorFocused = System.Drawing.Color.Black;
            this.Names.BorderColorIdle = System.Drawing.Color.Black;
            this.Names.BorderColorMouseHover = System.Drawing.Color.Black;
            this.Names.BorderThickness = 3;
            this.Names.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Names.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.Names.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Names.isPassword = false;
            this.Names.Location = new System.Drawing.Point(8, 398);
            this.Names.Margin = new System.Windows.Forms.Padding(5);
            this.Names.Name = "Names";
            this.Names.Size = new System.Drawing.Size(333, 52);
            this.Names.TabIndex = 6;
            this.Names.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // PasDuo
            // 
            this.PasDuo.BorderColorFocused = System.Drawing.Color.Black;
            this.PasDuo.BorderColorIdle = System.Drawing.Color.Black;
            this.PasDuo.BorderColorMouseHover = System.Drawing.Color.Black;
            this.PasDuo.BorderThickness = 3;
            this.PasDuo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasDuo.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.PasDuo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PasDuo.isPassword = true;
            this.PasDuo.Location = new System.Drawing.Point(8, 306);
            this.PasDuo.Margin = new System.Windows.Forms.Padding(5);
            this.PasDuo.Name = "PasDuo";
            this.PasDuo.Size = new System.Drawing.Size(333, 52);
            this.PasDuo.TabIndex = 7;
            this.PasDuo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // Pas
            // 
            this.Pas.BorderColorFocused = System.Drawing.Color.Black;
            this.Pas.BorderColorIdle = System.Drawing.Color.Black;
            this.Pas.BorderColorMouseHover = System.Drawing.Color.Black;
            this.Pas.BorderThickness = 3;
            this.Pas.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Pas.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.Pas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Pas.isPassword = true;
            this.Pas.Location = new System.Drawing.Point(8, 189);
            this.Pas.Margin = new System.Windows.Forms.Padding(5);
            this.Pas.Name = "Pas";
            this.Pas.Size = new System.Drawing.Size(333, 52);
            this.Pas.TabIndex = 8;
            this.Pas.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Pas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Pas_KeyPress);
            // 
            // Log
            // 
            this.Log.BorderColorFocused = System.Drawing.Color.Black;
            this.Log.BorderColorIdle = System.Drawing.Color.Black;
            this.Log.BorderColorMouseHover = System.Drawing.Color.Black;
            this.Log.BorderThickness = 3;
            this.Log.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Log.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.Log.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Log.isPassword = false;
            this.Log.Location = new System.Drawing.Point(8, 93);
            this.Log.Margin = new System.Windows.Forms.Padding(5);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(333, 52);
            this.Log.TabIndex = 9;
            this.Log.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.panel1;
            this.bunifuDragControl1.Vertical = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(8, 540);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(333, 60);
            this.button1.TabIndex = 17;
            this.button1.Text = "Регистрация";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Registrat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 612);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Prof);
            this.Controls.Add(this.bunifuProgressBar1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Names);
            this.Controls.Add(this.PasDuo);
            this.Controls.Add(this.Pas);
            this.Controls.Add(this.Log);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Registrat";
            this.Text = "Registrat";
            this.Load += new System.EventHandler(this.Registrat_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroComboBox Prof;
        private Bunifu.Framework.UI.BunifuProgressBar bunifuProgressBar1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuMetroTextbox Names;
        private Bunifu.Framework.UI.BunifuMetroTextbox PasDuo;
        private Bunifu.Framework.UI.BunifuMetroTextbox Pas;
        private Bunifu.Framework.UI.BunifuMetroTextbox Log;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Button button1;
    }
}