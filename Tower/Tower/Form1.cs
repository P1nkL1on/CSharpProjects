using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower
{
    public partial class Form1 : Form
    {
        Viewer vv;

        public Form1()
        {
            InitializeComponent();
            Field ff = new Field(new Random(), 1, 1, true);

            vv = new Viewer();
            Tower tw = new Tower(ff);

            vv.AddItem(tw);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 2; i++ ) vv.Update();
            vv.Draw(pan);
        }
    }
}
