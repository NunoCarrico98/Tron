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
			// Render game view
			Renderer renderer = new Renderer();
            InputSystem input = new InputSystem();
            Thread inputThread = new Thread(input.GetUserInput);

			// Initialize game with a 100x20 grid
			Tron t = new Tron(20, 100, renderer, input);

            Console.CursorVisible = false;

            inputThread.Start();

            // Start game passing a value of 50ms per frame
            t.MainMenu();

            inputThread.Join();
		}
	}
}
