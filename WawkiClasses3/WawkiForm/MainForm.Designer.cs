namespace WawkiForm
{
    partial class MainForm
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
            this.TurnHistory = new System.Windows.Forms.TrackBar();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TurnHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // TurnHistory
            // 
            this.TurnHistory.Location = new System.Drawing.Point(439, 12);
            this.TurnHistory.Maximum = 1;
            this.TurnHistory.Name = "TurnHistory";
            this.TurnHistory.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TurnHistory.Size = new System.Drawing.Size(45, 389);
            this.TurnHistory.TabIndex = 0;
            this.TurnHistory.Value = 1;
            this.TurnHistory.Scroll += new System.EventHandler(this.TurnHistory_Scroll);
            this.TurnHistory.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TurnHistory_MouseUp);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(358, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start new";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 413);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.TurnHistory);
            this.Name = "MainForm";
            this.Text = "Шашки";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.test);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.test2);
            ((System.ComponentModel.ISupportInitialize)(this.TurnHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar TurnHistory;
        private System.Windows.Forms.Button btnStart;
    }
}

