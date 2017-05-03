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
            this.saveGameBut = new System.Windows.Forms.Button();
            this.loadGameBut = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.TurnHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // TurnHistory
            // 
            this.TurnHistory.Location = new System.Drawing.Point(1024, 27);
            this.TurnHistory.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.TurnHistory.Maximum = 1;
            this.TurnHistory.Name = "TurnHistory";
            this.TurnHistory.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TurnHistory.Size = new System.Drawing.Size(101, 868);
            this.TurnHistory.TabIndex = 0;
            this.TurnHistory.Value = 1;
            this.TurnHistory.Scroll += new System.EventHandler(this.TurnHistory_Scroll);
            this.TurnHistory.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TurnHistory_MouseUp);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(835, 27);
            this.btnStart.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(175, 51);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "New Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // saveGameBut
            // 
            this.saveGameBut.Location = new System.Drawing.Point(835, 140);
            this.saveGameBut.Name = "saveGameBut";
            this.saveGameBut.Size = new System.Drawing.Size(175, 85);
            this.saveGameBut.TabIndex = 2;
            this.saveGameBut.Text = "Save Current + Quit";
            this.saveGameBut.UseVisualStyleBackColor = true;
            this.saveGameBut.Click += new System.EventHandler(this.saveGameBut_Click);
            // 
            // loadGameBut
            // 
            this.loadGameBut.Location = new System.Drawing.Point(835, 88);
            this.loadGameBut.Name = "loadGameBut";
            this.loadGameBut.Size = new System.Drawing.Size(175, 46);
            this.loadGameBut.TabIndex = 3;
            this.loadGameBut.Text = "Load Game";
            this.loadGameBut.UseVisualStyleBackColor = true;
            this.loadGameBut.Click += new System.EventHandler(this.loadGameBut_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 921);
            this.Controls.Add(this.loadGameBut);
            this.Controls.Add(this.saveGameBut);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.TurnHistory);
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
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
        private System.Windows.Forms.Button saveGameBut;
        private System.Windows.Forms.Button loadGameBut;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

