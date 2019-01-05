namespace Corporation
{
    partial class ServicesForm
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonUndef = new System.Windows.Forms.RadioButton();
            this.radioButton1275 = new System.Windows.Forms.RadioButton();
            this.radioButton12 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton0 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonTotal = new System.Windows.Forms.RadioButton();
            this.radioButtonContent = new System.Windows.Forms.RadioButton();
            this.radioButtonRoaming = new System.Windows.Forms.RadioButton();
            this.radioButtonSpecial = new System.Windows.Forms.RadioButton();
            this.radioButtonMain = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButtonDiscount = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(21, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(480, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonUndef);
            this.groupBox1.Controls.Add(this.radioButton1275);
            this.groupBox1.Controls.Add(this.radioButton12);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton0);
            this.groupBox1.Location = new System.Drawing.Point(232, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 159);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Коэффициент";
            // 
            // radioButtonUndef
            // 
            this.radioButtonUndef.AutoSize = true;
            this.radioButtonUndef.Location = new System.Drawing.Point(7, 112);
            this.radioButtonUndef.Name = "radioButtonUndef";
            this.radioButtonUndef.Size = new System.Drawing.Size(96, 17);
            this.radioButtonUndef.TabIndex = 4;
            this.radioButtonUndef.TabStop = true;
            this.radioButtonUndef.Text = "Не определён";
            this.radioButtonUndef.UseVisualStyleBackColor = true;
            // 
            // radioButton1275
            // 
            this.radioButton1275.AutoSize = true;
            this.radioButton1275.Location = new System.Drawing.Point(7, 89);
            this.radioButton1275.Name = "radioButton1275";
            this.radioButton1275.Size = new System.Drawing.Size(52, 17);
            this.radioButton1275.TabIndex = 3;
            this.radioButton1275.TabStop = true;
            this.radioButton1275.Text = "1,275";
            this.radioButton1275.UseVisualStyleBackColor = true;
            // 
            // radioButton12
            // 
            this.radioButton12.AutoSize = true;
            this.radioButton12.Location = new System.Drawing.Point(7, 66);
            this.radioButton12.Name = "radioButton12";
            this.radioButton12.Size = new System.Drawing.Size(40, 17);
            this.radioButton12.TabIndex = 3;
            this.radioButton12.TabStop = true;
            this.radioButton12.Text = "1,2";
            this.radioButton12.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 43);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(31, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton0
            // 
            this.radioButton0.AutoSize = true;
            this.radioButton0.Location = new System.Drawing.Point(7, 20);
            this.radioButton0.Name = "radioButton0";
            this.radioButton0.Size = new System.Drawing.Size(31, 17);
            this.radioButton0.TabIndex = 0;
            this.radioButton0.TabStop = true;
            this.radioButton0.Text = "0";
            this.radioButton0.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonDiscount);
            this.groupBox2.Controls.Add(this.radioButtonTotal);
            this.groupBox2.Controls.Add(this.radioButtonContent);
            this.groupBox2.Controls.Add(this.radioButtonRoaming);
            this.groupBox2.Controls.Add(this.radioButtonSpecial);
            this.groupBox2.Controls.Add(this.radioButtonMain);
            this.groupBox2.Location = new System.Drawing.Point(21, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 159);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Тип услуги";
            // 
            // radioButtonTotal
            // 
            this.radioButtonTotal.AutoSize = true;
            this.radioButtonTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonTotal.Location = new System.Drawing.Point(3, 131);
            this.radioButtonTotal.Name = "radioButtonTotal";
            this.radioButtonTotal.Size = new System.Drawing.Size(198, 19);
            this.radioButtonTotal.TabIndex = 6;
            this.radioButtonTotal.TabStop = true;
            this.radioButtonTotal.Text = "Обобщение (группировка)";
            this.radioButtonTotal.UseVisualStyleBackColor = true;
            // 
            // radioButtonContent
            // 
            this.radioButtonContent.AutoSize = true;
            this.radioButtonContent.Location = new System.Drawing.Point(3, 85);
            this.radioButtonContent.Name = "radioButtonContent";
            this.radioButtonContent.Size = new System.Drawing.Size(66, 17);
            this.radioButtonContent.TabIndex = 6;
            this.radioButtonContent.TabStop = true;
            this.radioButtonContent.Text = "Контент";
            this.radioButtonContent.UseVisualStyleBackColor = true;
            // 
            // radioButtonRoaming
            // 
            this.radioButtonRoaming.AutoSize = true;
            this.radioButtonRoaming.Location = new System.Drawing.Point(3, 62);
            this.radioButtonRoaming.Name = "radioButtonRoaming";
            this.radioButtonRoaming.Size = new System.Drawing.Size(68, 17);
            this.radioButtonRoaming.TabIndex = 6;
            this.radioButtonRoaming.TabStop = true;
            this.radioButtonRoaming.Text = "Роуминг";
            this.radioButtonRoaming.UseVisualStyleBackColor = true;
            // 
            // radioButtonSpecial
            // 
            this.radioButtonSpecial.AutoSize = true;
            this.radioButtonSpecial.Location = new System.Drawing.Point(3, 39);
            this.radioButtonSpecial.Name = "radioButtonSpecial";
            this.radioButtonSpecial.Size = new System.Drawing.Size(92, 17);
            this.radioButtonSpecial.TabIndex = 6;
            this.radioButtonSpecial.TabStop = true;
            this.radioButtonSpecial.Text = "Специальная";
            this.radioButtonSpecial.UseVisualStyleBackColor = true;
            // 
            // radioButtonMain
            // 
            this.radioButtonMain.AutoSize = true;
            this.radioButtonMain.Location = new System.Drawing.Point(3, 16);
            this.radioButtonMain.Name = "radioButtonMain";
            this.radioButtonMain.Size = new System.Drawing.Size(75, 17);
            this.radioButtonMain.TabIndex = 0;
            this.radioButtonMain.TabStop = true;
            this.radioButtonMain.Text = "Основная";
            this.radioButtonMain.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(426, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioButtonDiscount
            // 
            this.radioButtonDiscount.AutoSize = true;
            this.radioButtonDiscount.Location = new System.Drawing.Point(3, 108);
            this.radioButtonDiscount.Name = "radioButtonDiscount";
            this.radioButtonDiscount.Size = new System.Drawing.Size(62, 17);
            this.radioButtonDiscount.TabIndex = 7;
            this.radioButtonDiscount.TabStop = true;
            this.radioButtonDiscount.Text = "Скидка";
            this.radioButtonDiscount.UseVisualStyleBackColor = true;
            // 
            // ServicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 273);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox1);
            this.Name = "ServicesForm";
            this.Text = "ServicesForm";
            this.Load += new System.EventHandler(this.ServicesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonUndef;
        private System.Windows.Forms.RadioButton radioButton1275;
        private System.Windows.Forms.RadioButton radioButton12;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton0;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonTotal;
        private System.Windows.Forms.RadioButton radioButtonContent;
        private System.Windows.Forms.RadioButton radioButtonRoaming;
        private System.Windows.Forms.RadioButton radioButtonSpecial;
        private System.Windows.Forms.RadioButton radioButtonMain;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButtonDiscount;
    }
}