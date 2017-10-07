using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compilat
{
    class Program
    {
        static void Main(string[] args)
        {
            

            DirectoryInfo directory = new DirectoryInfo(@"codes");
            FileInfo[] files = directory.GetFiles("*.txt");

            string[] codeNames = new string[files.Length]; int currentCodeName = 0;

            for (int i = 0; i < files.Length; i++)
                codeNames[i] = files[i].Name.Remove(files[i].Name.LastIndexOf('.'));
            
            while (true)
            {
                #region uinput_command;
                string command = "";
                while (command.Length == 0)
                {
                    Console.Clear();
                    Console.WriteLine("↑ ↓ -- select file from list;\n q  -- exit.\n\n");
                    MISC.ConsoleWriteLine(" > Load \'"+codeNames[currentCodeName] + ".txt\'", ConsoleColor.Green);
                    try
                    {
                        string[] lines = System.IO.File.ReadAllLines(@"codes/" + codeNames[currentCodeName] + ".txt");
                        Console.WriteLine();
                        foreach (string line in lines)
                            MISC.ConsoleWriteLine(line, ConsoleColor.DarkGreen);
                    }
                    catch (Exception e)
                    {
                        MISC.ConsoleWriteLine("Error in code preview", ConsoleColor.DarkRed);
                    }

                    var ch = Console.ReadKey(false).Key;
                    switch (ch)
                    {
                        case ConsoleKey.UpArrow:
                            currentCodeName = (currentCodeName > 0) ? currentCodeName - 1 : codeNames.Length - 1;
                            break;
                        case ConsoleKey.DownArrow:
                            currentCodeName = (currentCodeName < codeNames.Length - 1) ? currentCodeName + 1 : 0;
                            break;
                        case ConsoleKey.Enter:
                            command = codeNames[currentCodeName];
                            Console.Clear();
                            break;
                        default:
                            break;
                    }
                }

                Console.WriteLine();
                if (command.ToLower() == "q")
                    break;
                #endregion;

                try
                {
                    string code = "";
                    string[] lines = System.IO.File.ReadAllLines(@"codes/" + command + ".txt");

                    foreach (string line in lines)
                    {
                        code += line + "\n";
                        Console.WriteLine(line);
                    }

                    ASTTree t = new ASTTree(code);
                    t.Trace();
                }
                catch (Exception e)
                {
                    Console.WriteLine(String.Format("Can not read file @/{0}.txt!", command));
                }
                finally
                {
                    Console.WriteLine("Press any key...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
//
// cicle FOR, WHILE, IF-ELSE
//for (a=10, b=1; a < 1000; a++, b*=2){\n    if (a < 500){\n        b = b * a - a / b;\n        a-= 0.5;\n    } else {\n         b = a*a*(a*a - b*b*b*b + a*b)*a*b;\n         a += 20 * b / b / b;\n    }\n    c = a;\n    while (c>0){\n        c -= b;\n        b *= 1.2;\n    }\n}\n