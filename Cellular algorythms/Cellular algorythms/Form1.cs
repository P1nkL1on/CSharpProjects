using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CellularLib;

namespace Cellular_algorythms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private IField f1;
        private int size = 1;

        private void loadLevel(object sender, EventArgs e)
        {
            tickTimer.Enabled = true;
            loadingTimer.Enabled = false; loadingTimer.Dispose(); panel_trace.Location = new System.Drawing.Point(0, 0);
            f1 = new Field(150,150);
            f1.Randomise(new Random());

            size = ((f1.Width + f1.Height) / 2) / 50;
        }

        private Random rnd = new Random();
        private void tickLevel(object sender, EventArgs e)
        {

            //tickTimer.Enabled = false; 
            f1.Tick(1,rnd); tickTimer.Interval = 15*(size+1);
            using (Graphics G = panel_trace.CreateGraphics()) G.DrawImage(f1.getImage, new Rectangle(100,0,panel_trace.Width-100,panel_trace.Height));
            healthLabel.Text = string.Format("{0}% {1}% {2}% {3}%",Field.teamHealth[0],Field.teamHealth[1],Field.teamHealth[3],Field.teamHealth[4]);
            //MessageBox.Show("2");
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            panel_trace.Height = Height;
            panel_trace.Width = Width;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tickTimer.Enabled = false; loadingTimer.Enabled = true;
        }

        private void trackKillPower_Scroll(object sender, EventArgs e)
        {
            Consts._killRewardPower = trackKillPower.Value;
        }

        private void trackKillHealth_Scroll(object sender, EventArgs e)
        {
            Consts._killRewardHealth = trackKillHealth.Value;
        }

        private void trackColor_Scroll(object sender, EventArgs e)
        {
            Consts._colorIntensive = (byte)trackColor.Value;
        }

        private void trackDying_Scroll(object sender, EventArgs e)
        {
            Consts._dyingTickHealth = trackDying.Value / 100f;
            Consts._dyingTickPower = trackDying.Value / 3000f;
        }
    }
}
