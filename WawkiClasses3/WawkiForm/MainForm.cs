using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WawkiClasses;

namespace WawkiForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private GameController wawki;
        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void test(object sender, MouseEventArgs e)
        {
            if (wawki != null)
            {
                if (wawki.CanBePlayed().Length == 0)
                {
                    if (!wawki.IsCurrentPlayerABot)
                        wawki.DoABotTurn();
                    else
                        wawki.RecieveClick(e.X, e.Y);
                }
                else
                    MessageBox.Show(wawki.CanBePlayed(), "Message");
                Draw();
                UpdateBar();
            }
        }

        private void test2(object sender, MouseEventArgs e)
        {

        }

        private void TurnHistory_Scroll(object sender, EventArgs e)
        { wawki.ChangeTurn(TurnHistory.Value); Draw(); }

        private void Draw()
        { Graphics G = this.CreateGraphics(); Drawing.DrawGame(G, wawki); }
        private void UpdateBar()
        { TurnHistory.Maximum = Math.Max(wawki.TurnCount, 0); TurnHistory.Value = wawki.TurnCount; }

        private void TurnHistory_MouseUp(object sender, MouseEventArgs e)
        { wawki.StopChangingTurns(); UpdateBar(); }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Game wawkiGame = new Game(new TestFactory());
            wawki = new GameController(wawkiGame);
            Draw();
        }
    }
}
