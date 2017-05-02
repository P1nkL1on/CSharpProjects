using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WawkiClasses
{
    public abstract class GameBotView
    {
        public abstract GameField CurrentField { get; }
        public abstract List<Point> WhereToGo(Figure who);
        public abstract List<Point> SelectedPointsSet { set; }
        public abstract sbyte CurrentPlayerTeam { get; }
    }

    public class Game : GameBotView
    {
        List<Point> selectedPoints;
        public GameField currentField;
        TurnVault vault;
        TurnState state;
        Player[] players;
        List<List<Point>> shouldBeRemoved;

        public override List<Point> SelectedPointsSet
        {
            set { selectedPoints = value; }
        }

        public override GameField CurrentField
        {
            get { return this.currentField; }
        }

        public List<Point> SelectedPoints { get { return selectedPoints; } }

        public Game(GameFactory factory)
        {
            state = TurnState.nothingSelected;
            selectedPoints = new List<Point>();
            vault = new TurnVault();
            currentField = factory.CreateField();
            shouldBeRemoved = new List<List<Point>>();

            //creating players
            WawkiPlayer pl1 = new WawkiPlayer(1, false); WawkiPlayer pl2 = new WawkiPlayer(2, true);
            players = new Player[2] { pl1, pl2 };
            players[0].Active = true;
            //stop creating players. Genious
        }

        #region turnHistory

        public int TurnCount { get { return vault.Count; } }
        private int turnNumber = 0;
        public void ChangeTurn(int To)
        {
            state = TurnState.nothingSelected;
            selectedPoints.Clear();
            shouldBeRemoved.Clear();
            currentField = vault[To - 1];
            turnNumber = To - 1;
        }
        public void StopChangingTurns()
        {
            if ((TurnCount - turnNumber) % 2 == 1)
                for (int i = 0; i < players.Length; i++)
                    players[i].Active = !players[i].Active;
            if (turnNumber <= 0) { players[0].Active = true; players[1].Active = false; }
            vault.RemoveFrom(turnNumber);
            CheckDamka();
        }

        #endregion

        //is current player a bot or not
        public bool IsCurrentPlayerABot
        {
            get
            {
                //bool res = false;
                for (int i = 0; i < players.Length; i++)
                    if (players[i].Active)
                    {
                        if (players[i].IsBot) return true;
                    }
                return false;
            }
        }
        //recieve a click from MainForm
        public void RecieveClick(int X, int Y)
        {
            ActionPoint(MouseClickAdapter.Tranform(X, Y));
        }
        //выполняет действия с выбранной точкой.
        private void ActionPoint(Point where)
        {
            if (where.X < 0 || where.X > 7 || where.Y < 0 || where.Y > 7)
            {
                //clicked out of field. Drop all selected things, set state to nothing selected
                selectedPoints.Clear();
                state = TurnState.nothingSelected;
                return;
            }
            else
            {
                Figure selectedFigure = currentField.WhoIsOn(where);

                //IF NONE SELECTED
                if (state == TurnState.nothingSelected || state == TurnState.emptyGridSelected || state == TurnState.enemyFigureSelected)
                {
                    if (selectedFigure == null)
                        state = TurnState.emptyGridSelected;  //selected an empty
                    else
                    {
                        if (selectedFigure.Team == CurrentPlayerTeam)
                        {
                            selectedPoints.Clear();
                            selectedPoints = WhereToGo(selectedFigure);
                            selectedPoints.Add(selectedFigure.Position);
                            state = TurnState.friendlyFigureSelected;
                            //Мы имеем список всех клеток, куда она может пойти и ее саму в конце
                            //select your figure
                        }
                        else
                        {
                            state = TurnState.enemyFigureSelected;  //select enemy
                            selectedPoints.Clear();
                            selectedPoints.Add(selectedFigure.Position);//add this figure to a selectedPoints
                        }
                    }
                    return;
                }

                //IF FRIEND SELECTED and pressed other point
                if (state == TurnState.friendlyFigureSelected && selectedPoints.Count > 0)
                {
                    Figure previousSelected = currentField.WhoIsOn(selectedPoints[selectedPoints.Count - 1]);
                    //garanted a friendly figure
                    if (selectedFigure == null && WhereToGo(previousSelected).IndexOf(where) >= 0 /*можем пойти в этиу клетку*/)
                    {
                        /*MAKE A TURN GREAT AGAIN*/
                        selectedPoints = WhereToGo(previousSelected);
                        state = TurnState.makeATurn;
                        DoATurn(previousSelected, where);
                        return;
                    }
                    if (selectedFigure != null)
                    {
                        //not null selected
                        if (selectedFigure.Position == previousSelected.Position)
                        {
                            //еще один клик на выбраннуб пешку
                            selectedPoints.Clear();
                            state = TurnState.nothingSelected;
                            return;
                        }
                        if (selectedFigure.Team == previousSelected.Team)
                        {
                            //выбираем другую пешку своей же команды. При этом список переделывается
                            selectedPoints.Clear();
                            selectedPoints = WhereToGo(selectedFigure);
                            selectedPoints.Add(selectedFigure.Position);
                            state = TurnState.friendlyFigureSelected;
                            return;
                        }
                    }
                }
            }
        }
        //определяет игрока, который сейчас ходит
        public override sbyte CurrentPlayerTeam
        {
            get
            {
                sbyte res = 0;
                for (int i = 0; i < players.Length; i++)
                    if (players[i].Active) res = players[i].Team;

                return res;
            }
        }

        //Do A BOT Turn
        public void DoABotTurn()
        {
            Player pl;
            if (players[0].Active) pl = players[0]; else pl = players[1];
            Figure f; Point p;
            pl.RandomTurn((GameBotView)this, out f, out p);

            if (f != null)
                DoATurn(f, p);
        }
        //DO A TURN GREAT AGAIN!!!
        private void DoATurn(Figure who, Point where)
        {
            //put a current turn ionto history
            vault.PushTurn(currentField.Clone());
            RemoveFromPath(where);
            currentField.MoveFromTo(who.Position, where);
            CheckDamka(who);
            currentField.ReScreen();

            //currentField

            state = TurnState.nothingSelected;
            selectedPoints.Clear();

            //swap a turn
            for (int i = 0; i < players.Length; i++)
                players[i].Active = !players[i].Active;
        }
        //проверить не стал ли кто-нить дамкой
        private void CheckDamka()
        {
            for (int i = 0; i < currentField.FigureCount; i++)
                if (currentField.GetFigureBy(i).State == FigureState.usuall &&
                   ((currentField.GetFigureBy(i).Position.Y == 7 && currentField.GetFigureBy(i).Team == 1) ||
                   (currentField.GetFigureBy(i).Position.Y == 0 && currentField.GetFigureBy(i).Team == 2)))
                {
                    currentField.GetFigureBy(i).State = FigureState.damka;
                }
        }
        private void CheckDamka(Figure who)
        {
            if (who.State == FigureState.usuall &&
                   ((who.Position.Y == 7 && who.Team == 1) ||
                   (who.Position.Y == 0 && who.Team == 2)))
            {
                who.State = FigureState.damka;
            }
        }

        //убрать все фишки, которые стоят на пути, выбранном из массива листов should be removed.
        private void RemoveFromPath(Point whereGo)
        {
            int N = -1;
            //по всем предложенным вариантам ходьбы
            for (int i = 0; i < selectedPoints.Count; i++)
                if (selectedPoints[i].X == whereGo.X && selectedPoints[i].Y == whereGo.Y) { N = i; break; }
            if (N >= 0)
                for (int i = 0; i < shouldBeRemoved[N].Count; i++)
                    if (currentField.WhoIsOn(shouldBeRemoved[N][i]) != null)
                        currentField.WhoIsOn(shouldBeRemoved[N][i]).State = FigureState.dead;
            //kill this guy!
            currentField.ClearDead();
        }

        //стэк пути, который накапливается и тд
        Stack<Point> path;
        //Определяет весь набор точек, в который может отправиться данная фигура.
        public override List<Point> WhereToGo(Figure who)
        {
            path = new Stack<Point>();
            shouldBeRemoved.Clear();
            List<Point> final = new List<Point>();
            #region comments
            //int team = (int)who.Team / 2 - 1;  //1 - -1
            //Point[] go = new Point[] { new Point(1, 1), new Point(-1, 1), new Point(1, -1), new Point(-1, -1) };
            //for (int i = 0; i < 4; i++)
            //{

            //}
            //final.Add(new Point(who.Position.X + 1, who.Position.Y + 1)); shouldBeRemoved.Add(new List<Point>());
            //final.Add(new Point(who.Position.X - 1, who.Position.Y - 1)); shouldBeRemoved.Add(new List<Point>());
            //final.Add(new Point(who.Position.X - 1, who.Position.Y + 1)); shouldBeRemoved.Add(new List<Point>());

            //List<Point> last = new List<Point>(); last.Add(new Point(who.Position.X + 1, who.Position.Y - 1));
            //final.Add(new Point(who.Position.X + 2, who.Position.Y - 2)); shouldBeRemoved.Add(last);
            #endregion


            //napravleniya
            Point[] go = new Point[] { new Point(1, 1), new Point(-1, 1), new Point(1, -1), new Point(-1, -1) };

            if (who.State == FigureState.usuall)
            {
                #region usuall
                for (int i = 0; i < 4; i++)
                {
                    Figure onMyWay = currentField.WhoIsOn(Summ(who, go[i]));
                    //simple step
                    if (onMyWay == null && ((i < 2 && who.Team == 1) || (i >= 2 && who.Team == 2)))
                    { final.Add(Summ(who, go[i])); shouldBeRemoved.Add(new List<Point>()); }
                    //kill a guy
                    if (onMyWay != null && onMyWay.Team == who.Team)
                        continue;   //cannot go throw a fridn
                    //самое интересное
                    path.Clear();//restart your path
                    if (onMyWay != null && onMyWay.Team != who.Team && onMyWay.State != FigureState.dead
                        && path.ToList().IndexOf(onMyWay.Position) < 0
                        && currentField.WhoIsOn(Summ(who, Summ(go[i], go[i]))) == null
                        && Summ(who, Summ(go[i], go[i])).X >= 0
                        && Summ(who, Summ(go[i], go[i])).X <= 7
                        && Summ(who, Summ(go[i], go[i])).Y >= 0
                        && Summ(who, Summ(go[i], go[i])).Y <= 7)
                    {//kill and count some more
                        final.Add(Summ(who, Summ(go[i], go[i])));
                        List<Point> bito = new List<Point>(); path.Push(onMyWay.Position);
                        shouldBeRemoved.Add(path.ToList());//add all bito to a list

                        //psevdo recursion
                        int tr = 0; int C = 0; int vetvi = 0;
                        bool finish = false; Point currentPosition = Summ(who, Summ(go[i], go[i]));
                        do
                        {
                            tr++;
                            for (int j = 0; j < 4; j++)
                            {
                                Figure nextF = currentField.WhoIsOn(Summ(currentPosition, go[j]));
                                Figure next2F = currentField.WhoIsOn(Summ(currentPosition, Summ(go[j], go[j])));
                                //if there is another 
                                finish = !(nextF == null || nextF.Team == who.Team  //клетка пустая, мы ведь не можем просто подвинуться после удара // или там стоит друг
                                    || (nextF.Team != who.Team && next2F != null)   //спереди враг, ноза ним закрыто
                                    || (nextF.Team != who.Team && path.ToList().IndexOf(nextF.Position) >= 0)); //уже битая шашка
                                if (!finish) continue;
                                //условие завершения, если оно выполняется для всех четырех, то конец вайлы\а
                                if (nextF.Team != who.Team && next2F == null
                                    && path.ToList().IndexOf(nextF.Position) < 0)//спереди враг, он еще не был бит, за ним свободно
                                {
                                    if (final.IndexOf(Summ(currentPosition, Summ(go[j], go[j]))) < 0
                                        && Summ(currentPosition, Summ(go[j], go[j])).X >= 0 && Summ(currentPosition, Summ(go[j], go[j])).X <= 7
                                        && Summ(currentPosition, Summ(go[j], go[j])).Y >= 0 && Summ(currentPosition, Summ(go[j], go[j])).Y <= 7)
                                    //if there is no such
                                    {
                                        path.Push(nextF.Position);
                                        final.Add(Summ(currentPosition, Summ(go[j], go[j])));
                                        shouldBeRemoved.Add(path.ToList());
                                        vetvi++;    //количество ветвей в данном месте

                                        //path.Pop();
                                    }
                                }
                            }
                            if (/*-vetvi - 1 + C < 0*/ final.Count > C + 1)
                            {
                                currentPosition = final[/*final.Count - vetvi - 1 + */C + 1];
                                C++;
                                //path.Pop();
                            }
                            else break;
                        } while (tr < 250 && (!finish || final.Count > C + 1));

                    }
                }
                #endregion
            }
            if (who.State == FigureState.damka)
            {
                #region firstly
                //selectedPoints.Clear();// selectedPoints.Add(who.Position);
                path.Clear(); shouldBeRemoved.Clear();
                for (int i = 0; i < 4; i++)
                {
                    byte canGo = 0; path.Clear();
                    for (int step = 1; step < 8; step++)
                        if (canGo >= 2 || (
                            who.Position.X + step * go[i].X < 0 || who.Position.X + step * go[i].X > 7 ||
                            who.Position.Y + step * go[i].Y < 0 || who.Position.Y + step * go[i].Y > 7
                            )) break;
                        else
                        {
                            Figure pregrada = currentField.WhoIsOn(new Point(who.Position.X + step * go[i].X, who.Position.Y + step * go[i].Y));
                            if (pregrada == null)
                            {
                                canGo = 0;
                                final.Add(new Point(who.Position.X + step * go[i].X, who.Position.Y + step * go[i].Y));
                                shouldBeRemoved.Add(path.ToList());
                                //pusto
                            }
                            else
                                if (pregrada.Team == who.Team) canGo += 2;
                                else
                                { canGo++; path.Push(pregrada.Position); }  //add enemy to path killed
                        }
                }
                #endregion
                //now in final there are all points
                #region secondly
                int start = 0;
            Again:
                int L = final.Count;
                for (int k = start; k < L; k++)
                {
                    if (shouldBeRemoved[k].Count > 0)
                    {//stepping from this point you have almost 1 kill

                        for (int i = 0; i < 4; i++)
                        {
                            int podhod = 0; Figure pregrada = null;
                            List<Point> newPath = shouldBeRemoved[k];//new kill path
                            for (int step = 1; step < 8; step++)
                            {
                                if (final[k].X + step * go[i].X < 0 || final[k].X + step * go[i].X > 7 ||   //ыходит за пределы
                                    final[k].Y + step * go[i].Y < 0 || final[k].Y + step * go[i].Y > 7
                                    || final.IndexOf(new Point(final[k].X + step * go[i].X,
                                        final[k].Y + step * go[i].Y)) >= 0//уже было
                                        ) break;//get lost
                                if (podhod == 1 && pregrada != null && newPath.IndexOf(pregrada.Position) < 0)
                                    newPath.Add(pregrada.Position);//

                                pregrada = currentField.WhoIsOn(
                                    new Point(final[k].X + step * go[i].X, final[k].Y + step * go[i].Y));
                                if (pregrada == null)
                                {
                                    if (podhod == 0)//еще не бил никого
                                        continue;
                                    else
                                    {   //добавляем шашку, которая уже была
                                        final.Add(new Point(final[k].X + step * go[i].X, final[k].Y + step * go[i].Y));
                                        shouldBeRemoved.Add(newPath);
                                    }
                                }
                                if (pregrada != null)
                                {
                                    if (pregrada.Team == who.Team) break;//нашел друга, сразу стоп
                                    else
                                    {
                                        if (podhod == 0) { podhod++; /**/ continue; }    //нашел первого врага, активируем
                                        else { break; } //нашел врага, а потом еще один враг. Блок
                                    }
                                }
                            }
                        }
                    }
                }


                if (final.Count > L)
                    { start = L; goto Again; }
                #endregion
            }
            return final;
        }

        private Point Summ(Point A, Point B)
        { return new Point(A.X + B.X, A.Y + B.Y); }

        private Point Summ(Figure A, Point B)
        { return new Point(A.Position.X + B.X, A.Position.Y + B.Y); }
        //figure filters
        figureFilter Team1 = fig => fig.Team == 1;
        figureFilter Team2 = fig => fig.Team == 2;
        figureFilter damkaonly = fig => fig.State == FigureState.damka;
        figureFilter usuallonly = fig => fig.State == FigureState.usuall;
    }

    enum TurnState
    {
        nothingSelected = 0,
        emptyGridSelected = 1,
        friendlyFigureSelected = 2,
        enemyFigureSelected = 3,
        makeATurn = 4
    }
}
