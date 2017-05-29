namespace Cellular_algorythms
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
            this.components = new System.ComponentModel.Container();
            this.panel_trace = new System.Windows.Forms.Panel();
            this.loadingTimer = new System.Windows.Forms.Timer(this.components);
            this.tickTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.healthLabel = new System.Windows.Forms.Label();
            this.trackKillHealth = new System.Windows.Forms.TrackBar();
            this.trackKillPower = new System.Windows.Forms.TrackBar();
            this.trackColor = new System.Windows.Forms.TrackBar();
            this.trackDying = new System.Windows.Forms.TrackBar();
            this.panel_trace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackKillHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackKillPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDying)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_trace
            // 
            this.panel_trace.Controls.Add(this.trackDying);
            this.panel_trace.Controls.Add(this.trackColor);
            this.panel_trace.Controls.Add(this.trackKillPower);
            this.panel_trace.Controls.Add(this.trackKillHealth);
            this.panel_trace.Controls.Add(this.healthLabel);
            this.panel_trace.Controls.Add(this.button1);
            this.panel_trace.Location = new System.Drawing.Point(13, 13);
            this.panel_trace.Name = "panel_trace";
            this.panel_trace.Size = new System.Drawing.Size(779, 490);
            this.panel_trace.TabIndex = 0;
            // 
            // loadingTimer
            // 
            this.loadingTimer.Enabled = true;
            this.loadingTimer.Interval = 1000;
            this.loadingTimer.Tick += new System.EventHandler(this.loadLevel);
            // 
            // tickTimer
            // 
            this.tickTimer.Interval = 30;
            this.tickTimer.Tick += new System.EventHandler(this.tickLevel);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-1, -2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "Restart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // healthLabel
            // 
            this.healthLabel.AutoSize = true;
            this.healthLabel.Location = new System.Drawing.Point(32, 0);
            this.healthLabel.Name = "healthLabel";
            this.healthLabel.Size = new System.Drawing.Size(40, 13);
            this.healthLabel.TabIndex = 2;
            this.healthLabel.Text = "0 0 0 0";
            // 
            // trackKillHealth
            // 
            this.trackKillHealth.Location = new System.Drawing.Point(0, 31);
            this.trackKillHealth.Maximum = 100;
            this.trackKillHealth.Name = "trackKillHealth";
            this.trackKillHealth.Size = new System.Drawing.Size(93, 45);
            this.trackKillHealth.TabIndex = 3;
            this.trackKillHealth.TickFrequency = 10;
            this.trackKillHealth.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackKillHealth.Value = 50;
            this.trackKillHealth.Scroll += new System.EventHandler(this.trackKillHealth_Scroll);
            // 
            // trackKillPower
            // 
            this.trackKillPower.Location = new System.Drawing.Point(0, 62);
            this.trackKillPower.Maximum = 50;
            this.trackKillPower.Name = "trackKillPower";
            this.trackKillPower.Size = new System.Drawing.Size(93, 45);
            this.trackKillPower.TabIndex = 4;
            this.trackKillPower.TickFrequency = 10;
            this.trackKillPower.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackKillPower.Value = 4;
            this.trackKillPower.Scroll += new System.EventHandler(this.trackKillPower_Scroll);
            // 
            // trackColor
            // 
            this.trackColor.Location = new System.Drawing.Point(0, 291);
            this.trackColor.Maximum = 255;
            this.trackColor.Minimum = 1;
            this.trackColor.Name = "trackColor";
            this.trackColor.Size = new System.Drawing.Size(93, 45);
            this.trackColor.TabIndex = 5;
            this.trackColor.TickFrequency = 50;
            this.trackColor.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackColor.Value = 150;
            this.trackColor.Scroll += new System.EventHandler(this.trackColor_Scroll);
            // 
            // trackDying
            // 
            this.trackDying.Location = new System.Drawing.Point(-1, 99);
            this.trackDying.Maximum = 100;
            this.trackDying.Name = "trackDying";
            this.trackDying.Size = new System.Drawing.Size(93, 45);
            this.trackDying.TabIndex = 6;
            this.trackDying.TickFrequency = 10;
            this.trackDying.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackDying.Value = 30;
            this.trackDying.Scroll += new System.EventHandler(this.trackDying_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 515);
            this.Controls.Add(this.panel_trace);
            this.Name = "Form1";
            this.Text = "Main";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.panel_trace.ResumeLayout(false);
            this.panel_trace.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackKillHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackKillPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDying)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_trace;
        private System.Windows.Forms.Timer loadingTimer;
        private System.Windows.Forms.Timer tickTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label healthLabel;
        private System.Windows.Forms.TrackBar trackKillHealth;
        private System.Windows.Forms.TrackBar trackKillPower;
        private System.Windows.Forms.TrackBar trackColor;
        private System.Windows.Forms.TrackBar trackDying;
    }
}

