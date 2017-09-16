using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GameOfLife91
{
    class Program
    {
        static bool INFINITE_GAME = true;

        static Field f = null;// = new Field(new Random(), 20, 20, false) ;
        static int wid, hei, pause;
        static bool PAUSE = false;
        static void Main(string[] args)
        {

            Console.WriteLine("Infinite game? y/n");
            string  s = Console.ReadLine() + "";
            INFINITE_GAME = (s == "y");

            Console.WriteLine("Width / Height / Pause (ms)");
            wid = int.Parse(Console.ReadLine());
            hei = int.Parse(Console.ReadLine());
            pause = int.Parse(Console.ReadLine());

            Thread th = new Thread(StepWorld);
            Thread draw = new Thread(DrawWorld);

            th.IsBackground = true; draw.IsBackground = true;
            th.Start(); draw.Start();

            // constrols
            while (true)
            {
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    // nothing
                    case ConsoleKey.Escape:
                        break;
                    // ZOOM N MOVEMENT
                    case ConsoleKey.OemPlus:
                        f.Zoom /= 2;
                        break;
                    case ConsoleKey.OemMinus:
                        f.Zoom *= 2;
                        break;
                    case ConsoleKey.LeftArrow:
                        f.FromWid -= f.Zoom;
                        break;
                    case ConsoleKey.RightArrow:
                        f.FromWid += f.Zoom;
                        break;
                    case ConsoleKey.UpArrow:
                        f.FromHei -= f.Zoom;
                        break;
                    case ConsoleKey.DownArrow:
                        f.FromHei += f.Zoom;
                        break;

                    // TIME SPEED
                    case ConsoleKey.I:
                        pause = Math.Max(10, pause - 10);
                        break;
                    case ConsoleKey.O:
                        pause += 10;
                        break;

                    // PAUSE
                    case ConsoleKey.P:
                        if (!f.isDead() || INFINITE_GAME)
                            PAUSE = !PAUSE;
                        if (PAUSE)
                        {
                            // set cursor
                            Consts.point_wid = f.FromHei;
                            Consts.point_hei = f.FromWid;
                        }
                        break;

                    // GAME FINISH
                    case ConsoleKey.R:
                        f = new Field(new Random(), hei, wid, !INFINITE_GAME);
                        break;

                    // ON PAUSE
                    case ConsoleKey.A:
                        if (PAUSE) { Consts.point_wid--; if (Consts.point_wid < 0) Consts.point_wid = wid - 1; }
                        break;
                    case ConsoleKey.D:
                        if (PAUSE) { Consts.point_wid++; if (Consts.point_wid > wid - 1) Consts.point_wid = 0; }
                        break;
                    case ConsoleKey.W:
                        if (PAUSE) { Consts.point_hei--; if (Consts.point_hei < 0) Consts.point_hei = hei - 1; }
                        break;
                    case ConsoleKey.S:
                        if (PAUSE) { Consts.point_hei++; if (Consts.point_hei > hei - 1) Consts.point_hei = 0; }
                        break;
                    case ConsoleKey.U:
                        if (PAUSE) { f.set_point(Consts.point_hei, Consts.point_wid, !f.get_point(Consts.point_hei, Consts.point_wid)); }
                        break;
                    default:
                        break;
                }
            }
        }

        static void StepWorld()
        {
            Random rnd = new Random();
            do
            {
                Console.Clear();
                f = new Field(rnd, hei, wid, !INFINITE_GAME);
                Console.WriteLine(f.ToString());
                while (!f.isDead() || INFINITE_GAME)
                {
                    Thread.Sleep(pause);
                    if (!PAUSE) f.UpdateMap();
                };

                Console.WriteLine(String.Format(">Dead.\n"));
                Thread.Sleep(5000);


            } while (true);
        }

        static void DrawWorld()
        {
            while (true)
            {
                if (f != null && (!f.isDead() || INFINITE_GAME))
                {
                    Console.Clear();
                    Console.WriteLine(String.Format(" Update every {0} ms", pause));
                    
                    Console.WriteLine(f.ToString());
                    
                    if (PAUSE)
                        if (PAUSE) WriteLine(">GAME IS PAUSED", ConsoleColor.Red);
                }
                Thread.Sleep(Math.Min(100, pause));
            }
        }


        static void WriteLine(String what, ConsoleColor clr)
        {
            Console.ForegroundColor = clr;
            Console.WriteLine(what);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
