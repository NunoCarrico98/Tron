using System;
using System.Threading;

namespace Tron
{
	/// <summary>
	/// Program Class.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Main Method.
		/// </summary>
		/// <param name="args">Command Line Arguments.</param>
		static void Main(string[] args)
		{
			// THESE ARE THE CORRECT VALUES FOR AN
			// EXCELLENT GAMEPLAY
			//    |
			//    V
            Console.SetWindowSize(160, 45);

            // TESTING PURPOSES
            //    |
            //    V
            //Console.SetWindowSize(70, 20);

            // Render game view
            Renderer renderer = new Renderer();
			// Create a new input system
            InputSystem input = new InputSystem();
			// Create and initialise input thread
            Thread inputThread = new Thread(input.GetUserInput);

            // Initialize game
            Tron t = new Tron(Console.WindowHeight - 6, Console.WindowWidth/2 - 1, 
                renderer, input);

			// Cursor becomes invisible
            Console.CursorVisible = false;

			// Start input thread
            inputThread.Start();

            // Start game passing a value of 50ms per frame
            t.MainMenu();

			// Wait for the input thread to join the "main" thread
            inputThread.Join();
		}
	}
}
