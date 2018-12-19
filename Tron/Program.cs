using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class Program
    {
        static void Main(string[] args)
        {
            Renderer renderer = new Renderer();
            Tron t = new Tron(20, 20, renderer);

            t.Gameloop();
        }
    }
}
