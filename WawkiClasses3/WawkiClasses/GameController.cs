using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WawkiClasses
{
    public class GameController
    {
        
        private Game game;
        public GameController(Game game)
        {
            this.game = game;
        }
        public String CanBePlayed()
        {
            return game.CanBePlayed();
        }
        public void ChangeTurn(int To)
        {
            game.ChangeTurn(To);
        }
        public void StopChangingTurns()
        {
            game.StopChangingTurns();
        }
        public void DoABotTurn()
        {
            game.DoABotTurn();
        }
        public bool IsCurrentPlayerABot
        {
            get
            { return game.IsCurrentPlayerABot; }
        }
        public void RecieveClick(int X, int Y)
        {
            game.RecieveClick(X, Y);
        }
        public int TurnCount { get { return game.TurnCount; } }


        public GameField currentField
        {
            get { return game.CurrentField; }
        }
        public int CurrentPlayerTeam
        {
            get { return game.CurrentPlayerTeam; }
        }
        public List<Point> SelectedPoints { get { return game.SelectedPoints; } }
        public void SaveCurrGame()
        {
            FileSys.SaveCurrGame(game.currentField.figList, game.CurrentPlayerTeam, game.Path);
        }
        
    }
}
