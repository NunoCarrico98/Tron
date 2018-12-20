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

			// Initialize game with a 20x20 grid
			Tron t = new Tron(20, 20, renderer, input);

			Thread inputThread = new Thread(input.GetUserInput);
			inputThread.Start();

			// Start game passing a value of 50ms per frame
			t.Gameloop(50);
		}
	}
}
