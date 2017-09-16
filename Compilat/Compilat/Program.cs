﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    class Program
    {
        static void Main(string[] args)
        {
            ASTTree t = new ASTTree("for (a=10, b=1; a < 1000; a++, b*=2){\n    if (a < 500){\n        b = b * a - a / b;\n        a-= 0.5;\n    } else {\n         b = a*a*(a*a - b*b*b*b + a*b)*a*b;\n         a += 20 * b / b / b;\n    }\n    c = a;\n    while (c>0){\n        c -= b;\n        b *= 1.2;\n    }\n}\n");    
            //("0 == 1-1000 / (500+500) * (500 + 501) / 1001");
            t.Trace();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
// cicle FOR, WHILE, IF-ELSE
//for (a=10, b=1; a < 1000; a++, b*=2){\n    if (a < 500){\n        b = b * a - a / b;\n        a-= 0.5;\n    } else {\n         b = a*a*(a*a - b*b*b*b + a*b)*a*b;\n         a += 20 * b / b / b;\n    }\n    c = a;\n    while (c>0){\n        c -= b;\n        b *= 1.2;\n    }\n}\n