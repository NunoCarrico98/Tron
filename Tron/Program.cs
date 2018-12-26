using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tron
{
	public class Program
	{
		static void Main(string[] args)
		{
            // THESE ARE THE CORRECT VALUES FOR AN
            // EXCELLENT GAMEPLAY
            //    |
            //    V
            //Console.SetWindowSize(160, 45);

            // TESTING PURPOSES
            //    |
            //    V
            Console.SetWindowSize(70, 20);

            // Render game view
            Renderer renderer = new Renderer();
            InputSystem input = new InputSystem();
            Thread inputThread = new Thread(input.GetUserInput);

            // Initialize game
            Tron t = new Tron(Console.WindowHeight - 6, Console.WindowWidth - 1, 
                renderer, input);

            Console.CursorVisible = false;

            inputThread.Start();

            // Start game passing a value of 50ms per frame
            t.MainMenu();

            inputThread.Join();
		}
	}
}
