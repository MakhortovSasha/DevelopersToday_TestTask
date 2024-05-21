using DevelopersToday_TestTask.Parsers;
using DevelopersToday_TestTask.WorkWithMenu;
using DevelopersToday_TestTask.Processors;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DevelopersToday_TestTask.DB;
using static System.Net.Mime.MediaTypeNames;

namespace DevelopersToday_TestTask
{
    internal class Program
    {
        static List<MenuItem> menuItems= new List<MenuItem>();
        static List<Key> uniqueItemKeys = new List<Key>();
        static ModelContext context;
        static void Main(string[] args)
        {
            
            // DB test
            var settings = new DbSettings()
            {
                Password = "b",
                Server = "DESKTOP-AKO2KR8\\SQLEXPRESS",
                UserId = "UserName",
                Database = "DevToday",
            };
            context = new ModelContext(settings);

            

            menuItems.Add(new MenuItem { Name = "Choose file and start", Action=(()=> StartImportItems()) });
            menuItems.Add(new MenuItem { Name = "Exit", Action = (() => Environment.Exit(0))});
            MainMenu.Createmenu(menuItems);
            
        }

        private static void StartImportItems()
        {
            using(FileProcessor processor = new FileProcessor())
            {
                int index = 0;
                do
                {
                    index++;
                    List<DefaulModel> itemsSet = new List<DefaulModel>();
                    var eof = processor.TryLoadItems(ref itemsSet,ref uniqueItemKeys);
                    if (itemsSet.Count!=0)
                    context.AddModels(itemsSet);

                    if (eof) { break; }
                } while (true);

            }
            Console.WriteLine("Amount of the rows with unique dates and passangers: " + context.GetAmount().ToString());


        }
        
        private static bool LoadToSQL()
        {
            return false;
        }
        




    }

}
