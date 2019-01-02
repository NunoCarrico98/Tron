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
            // Set console window size
            Console.SetWindowSize(160, 45);

            // Render game view
            Renderer renderer = new Renderer();
			// Create a new input system
            InputSystem input = new InputSystem();

            // Initialize game
            Tron t = new Tron(Console.WindowHeight - 6, Console.WindowWidth/2 - 1, 
                renderer, input);

			// Cursor becomes invisible
            Console.CursorVisible = false;

            // Start game showing main menu first
            t.MainMenu();
		}
	}
}
