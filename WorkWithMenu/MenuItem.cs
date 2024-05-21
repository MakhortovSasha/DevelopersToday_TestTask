using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersToday_TestTask.WorkWithMenu
{
    internal class MenuItem
    {
        public string Name { get; set; }
        public Action Action { get; set; }
        public bool Visible = true;
    }
}
