using System;
using System.Collections.Generic;
using System.Diagnostics;
using tskmgr;

namespace TaskManager
{
    class Program
    {   
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.CursorVisible = false;
            UI.ListProcesses();
            Console.SetCursorPosition(0, 0);
            Control.arrows(Control.damount);
        }
    }
}