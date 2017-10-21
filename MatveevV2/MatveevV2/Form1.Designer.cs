namespace MatveevV2
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.text_get = new System.Windows.Forms.TextBox();
            this.text_answer = new System.Windows.Forms.TextBox();
            this.button_generate = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_approve = new System.Windows.Forms.Button();
            this.button_nope = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text_get
            // 
            this.text_get.Enabled = false;
            this.text_get.Location = new System.Drawing.Point(13, 13);
            this.text_get.Multiline = true;
            this.text_get.Name = "text_get";
            this.text_get.Size = new System.Drawing.Size(294, 153);
            this.text_get.TabIndex = 100;
            this.text_get.Text = resources.GetString("text_get.Text");
            // 
            // text_answer
            // 
            this.text_answer.Enabled = false;
            this.text_answer.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text_answer.Location = new System.Drawing.Point(13, 172);
            this.text_answer.Multiline = true;
            this.text_answer.Name = "text_answer";
            this.text_answer.Size = new System.Drawing.Size(294, 354);
            this.text_answer.TabIndex = 99;
            // 
            // button_generate
            // 
            this.button_generate.Location = new System.Drawing.Point(13, 533);
            this.button_generate.Name = "button_generate";
            this.button_generate.Size = new System.Drawing.Size(75, 23);
            this.button_generate.TabIndex = 1;
            this.button_generate.Text = "Generate";
            this.button_generate.UseVisualStyleBackColor = true;
            this.button_generate.Click += new System.EventHandler(this.button_generate_Click);
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(232, 532);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Add.TabIndex = 0;
            this.button_Add.Text = "Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(314, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 513);
            this.panel1.TabIndex = 4;
            // 
            // button_approve
            // 
            this.button_approve.Location = new System.Drawing.Point(95, 533);
            this.button_approve.Name = "button_approve";
            this.button_approve.Size = new System.Drawing.Size(34, 23);
            this.button_approve.TabIndex = 3;
            this.button_approve.Text = "+";
            this.button_approve.UseVisualStyleBackColor = true;
            this.button_approve.Click += new System.EventHandler(this.button_approve_Click);
            // 
            // button_nope
            // 
            this.button_nope.Location = new System.Drawing.Point(135, 533);
            this.button_nope.Name = "button_nope";
            this.button_nope.Size = new System.Drawing.Size(34, 23);
            this.button_nope.TabIndex = 2;
            this.button_nope.Text = "-";
            this.button_nope.UseVisualStyleBackColor = true;
            this.button_nope.Click += new System.EventHandler(this.button_nope_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 574);
            this.Controls.Add(this.button_nope);
            this.Controls.Add(this.button_approve);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.button_generate);
            this.Controls.Add(this.text_answer);
            this.Controls.Add(this.text_get);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_get;
        private System.Windows.Forms.TextBox text_answer;
        private System.Windows.Forms.Button button_generate;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_approve;
        private System.Windows.Forms.Button button_nope;
    }
}

