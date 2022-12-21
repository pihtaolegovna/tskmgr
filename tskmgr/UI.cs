using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tskmgr
{
    public static class UI
    {
        static int top;
        public static void ListProcesses()
        {
            Control.processCollection = null;
            Console.Clear();
            Control.options.Clear();
            Header();
            Console.SetCursorPosition(0, 4);
            Control.damount = 0;
            top = 2;
            Control.processCollection = Process.GetProcesses();
            foreach (Process p in Control.processCollection)
            {
                Console.SetCursorPosition(0, top);
                top += 1;
                Console.WriteLine("│    {0}", p.ProcessName);
                Control.options.Add(p.ProcessName);
                Console.SetCursorPosition(50, top-1);
                try
                {
                    Console.Write("{0}{1}", (p.PagedMemorySize64 / 1000000), ("MB"));
                    Console.SetCursorPosition(106, top - 1);
                    Console.Write("│");
                }
                catch
                {
                    Console.Write("Access Denied");
                }
                Control.damount++;
            }
            Console.WriteLine();
            Console.WriteLine("└─────────────────────────────────────────────────────────────────────────────────────────────────────────┘");
        }
        public static void ProcessInfo()
        {
            Console.Clear();
            Header();
            Console.WriteLine("Имя процесса: {0}", Control.options[Control.selected]);
            foreach (var process in Process.GetProcessesByName(Control.options[Control.selected]))
            {
                Console.WriteLine("Process start time: {0}", (process.StartTime));
                Console.WriteLine("Total Process time: {0}", (process.TotalProcessorTime));
                Console.WriteLine("Memory Size: {0}{1}", (process.PagedMemorySize64 / 1000000), ("MB"));
            }
            ConsoleKeyInfo Key;
            do
            {
                Key = Console.ReadKey(true);
                if (Key.Key == ConsoleKey.D)
                {
                    try
                    {
                        foreach (var process in Process.GetProcessesByName(Control.options[Control.selected]))
                        {
                            process.Kill();
                        }
                        break;
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Нет прав доступа для завершения процесса");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.ReadKey();
                        UI.ListProcesses();
                        break;
                    }
                }
            } while (Key.Key != ConsoleKey.Backspace);
        }
        public static void Header()
        {
            Console.WriteLine("┌ Task Manager v. 0.1 │ Arrows For Navigate │   Enter for info   │ D to Kill Process │ R for Refresh List ┐");
            Console.WriteLine("└─────────────────────┴─────────────────────┴────────────────────┴───────────────────┴────────────────────┘");
        }
    }
}
