using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatveevV2
{
    public partial class Form1 : Form
    {
        MatGen mg;
        public Form1()
        {
            InitializeComponent();
            mg = new MatGen();
        }

        private void button_generate_Click(object sender, EventArgs e)
        {
            button_approve.Enabled = true;
            button_nope.Enabled = true;
            button_generate.Enabled = false;

            text_answer.Text = mg.Generate();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            mg.AddText(text_get.Text);
            button_Add.Enabled = false;
        }

        private void button_approve_Click(object sender, EventArgs e)
        {
            button_generate.Enabled = true;
            button_approve.Enabled = false;
            button_nope.Enabled = false;
            mg.Approve(160.0f);
            mg.DrawGraf(panel1);


            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"success.txt", true))
            {
                file.WriteLine(text_answer.Text);
            }
            button_generate_Click(sender, e);
        }

        private void button_nope_Click(object sender, EventArgs e)
        {
            button_generate.Enabled = true;
            button_nope.Enabled = false;
            button_approve.Enabled = false;
            mg.Approve(-80.0f);
            //mg.DrawGraf(panel1);

            button_generate_Click(sender, e);
        }
    }
}
