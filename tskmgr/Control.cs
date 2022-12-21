using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tskmgr
{
    public static class Control
    {
        public static int amount;
        public static int selector = 0;
        public static int selected;
        public static int damount = 0;
        
        public static int isrunning;
        public static List<string> options = new List<string>();
        public static Process[] processCollection;
        public static void arrows(int amount)
        {
            Console.SetCursorPosition(1, 2);
            Console.Write("1");
            Console.SetCursorPosition(1, 2);
            bool run = false;
            while (run != true)
            {
                ConsoleKeyInfo menuchoosekey = Console.ReadKey();
                string choosekey = (menuchoosekey.Key.ToString());
                switch (choosekey)
                {
                    case "UpArrow":
                        Console.SetCursorPosition(1, selector+2);
                        selector--;
                        break;
                    case "DownArrow":
                        Console.SetCursorPosition(1, selector+2);
                        selector++;
                        break;
                    case "Delete":
                        try
                        {
                            selector = selected;
                            foreach (var process in Process.GetProcessesByName(options[selected]))
                            {
                                process.Kill();
                            }
                            Console.SetCursorPosition(3, 2);
                            UI.ListProcesses();
                            processCollection = null;
                            Console.SetCursorPosition(0, 0);
                            arrows(damount);
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
                    case "Enter":
                        {
                            selected = selector;
                            if (choosekey == "Enter") selected = selector;
                            try
                            {
                                UI.ProcessInfo();
                                UI.ListProcesses();
                                Console.SetCursorPosition(0, 0);
                                arrows(damount);
                                break;
                            }
                            catch (Exception)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Access Denied");
                                Console.ForegroundColor = ConsoleColor.Blue;
                                do
                                {
                                    Console.WriteLine();
                                } while (Console.ReadKey(true).Key != ConsoleKey.Backspace);
                                UI.ListProcesses();
                                break;
                            }
                        }
                    case "R":
                        {
                            Console.Clear();
                            UI.Header();
                            UI.ListProcesses();
                            Console.SetCursorPosition(0, 0);
                            arrows(damount);
                            break;
                        }
                    case "_":
                        {
                            break;
                        }
                    case "Escape":
                        {
                            Environment.Exit(1);
                            break;
                        }
                }
                if (selector < 0)
                    selector = damount - 1;
                if (selector > damount - 1)
                {
                    selector = 0;
                    Console.SetCursorPosition(1, 0);
                    Console.SetCursorPosition(1, 2);
                }
                Console.Write("   ");
                Console.SetCursorPosition(1, 2);
                Console.SetCursorPosition(1, selector+2);
                Console.Write(selector + 1);
            }
        }
    }
}
