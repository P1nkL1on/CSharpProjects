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
            string Path = "";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            Path = saveFileDialog1.FileName;
            if (Path == "")
            {
                MessageBox.Show("Please name the file");
                return;
            }
            Game wawkiGame = new Game(new WawkiFactory(), Path, false);
            wawki = new GameController(wawkiGame);
            Draw();
        }

        private void loadGameBut_Click(object sender, EventArgs e)
        {
            string Path = "";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            Path = openFileDialog1.FileName;
            
            Game wawkiGame = new Game(new WawkiFactory(), Path, true);
            wawki = new GameController(wawkiGame);
            Draw();
        }

        private void saveGameBut_Click(object sender, EventArgs e)
        {
            if (wawki == null) return;
            wawki.SaveCurrGame();
            this.Close();
        }
    }
}
