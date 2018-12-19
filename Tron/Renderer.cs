using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class Renderer
    {
        public void ShowMainMenu()
        {
            Console.WriteLine("----- WELCOME TO TRON -----");
            Console.WriteLine();
            Console.WriteLine("1. Two players mode");
            Console.WriteLine("2. AI mode");
            Console.WriteLine("3. Quit");
        }
    }
}
