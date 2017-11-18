using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DungStruct;

namespace MMO
{
    public partial class Screen : Form
    {
        Manager mg;


        public Screen()
        {
            InitializeComponent();
            mg = new Manager();
            WindowState = FormWindowState.Maximized;
        }

        private void OnPaintEvent(object sender, PaintEventArgs e)
        {
            mg.Draw(ViewPanel);
        }

    }
}
