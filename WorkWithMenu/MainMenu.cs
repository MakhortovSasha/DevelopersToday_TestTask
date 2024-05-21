using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersToday_TestTask.WorkWithMenu
{
    internal static class MainMenu
    {
        public static void Createmenu(List<MenuItem> menuItems)
        {
            Console.WriteLine("Choose option:");
            Console.WriteLine();

            int row = Console.CursorTop;
            int col = Console.CursorLeft;
            int index = 0;
            while (true)
            {
                DrawMenu(menuItems, row, col, index);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index < menuItems.Count - 1)
                            index++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (index > 0)
                            index--;
                        break;
                    case ConsoleKey.Enter:

                        if (menuItems[index].Action != null) menuItems[index].Action.Invoke();
                        break;
                }
            }
        }
        private static void DrawMenu(List<MenuItem> items, int row, int col, int index)
        {
            Console.SetCursorPosition(col, row);
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(items[i].Name);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}
