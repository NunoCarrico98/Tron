using System;
using System.Text;
using System.Threading;

namespace Tron
{
	/// <summary>
	/// Class that renders everything on screen.
	/// </summary>
	public class Renderer
	{
		/// <summary>
		/// Constant char representing a stepped tile
		/// </summary>
		private readonly char stepped = '■';
		/// <summary>
		/// Constant char representing an empty tile
		/// </summary>
		private readonly char empty = '.';

		/// <summary>
		/// Method to render the Main Menu
		/// </summary>
		public void ShowMainMenu()
		{
            Console.Clear();
			Console.WriteLine("----- WELCOME TO TRON -----");
			Console.WriteLine();
			Console.WriteLine("1. Two players mode");
			Console.WriteLine("2. AI mode");
			Console.WriteLine("3. Credits");
			Console.WriteLine("4. Controls");
			Console.WriteLine("5. Quit");
		}

		/// <summary>
		/// Method to render the credits
		/// </summary>
        public void ShowCredits()
        {
            Console.Clear();
            Console.WriteLine("----- WELCOME TO TRON -----");
            Console.WriteLine();
            Console.WriteLine("Made by:");
            Console.WriteLine("Nuno Carriço - https://github.com/NunoCarrico98");
            Console.WriteLine("Rui Martins - https://github.com/rui-martins");
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
        }

		/// <summary>
		/// Method to render the control info
		/// </summary>
		public void ShowControls()
		{
			Console.Clear();
			Console.WriteLine("----- WELCOME TO TRON -----");
			Console.WriteLine();
			Console.WriteLine("Controls:");
			Console.WriteLine();
			Console.WriteLine("   Player 1                      Player 2");
			Console.WriteLine("> W - Move Up              > Up Arrow - Move Up");
			Console.WriteLine("> S - Move Down            > Down Arrow - Move Up");
			Console.WriteLine("> A - Move Left            > Left Arrow - Move Up");
			Console.WriteLine("> D - Move Right           > Right Arrow - Move Up");
			Console.WriteLine();
			Console.WriteLine("If playing in singleplayer mode, the player 1 " +
				"controls are the default.");
			Console.WriteLine();
			Console.WriteLine("Press ENTER to continue...");
		}

		/// <summary>
		/// Method to render the level.
		/// </summary>
		/// <param name="gameWorld">Current state of level.</param>
		/// <param name="player1">Player 1.</param>
		/// <param name="player2">Player 2.</param>
        public void RenderGameWorld(bool[,] gameWorld, Player player1, Player player2)
		{
			// Get x and y size of the world
			int row = gameWorld.GetLength(0);
			int col = gameWorld.GetLength(1);

			// Create a stringbuilder with a specific size to efficiently 
			// join string elements
            StringBuilder sb = new StringBuilder(row * col + 
				(Environment.NewLine.Length *2) * col);
			// Join player Scores to stringbuilder
			sb.Append($"Player 1 - {player1.Score}     ||     Player 2 - {player2.Score}");
			sb.Append(Environment.NewLine);

			// Join the current state of the gameworld to stringbuilder
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					// If current checked tile is true, render 'stepped'. 
					// Otherwise, render 'empty'.
					sb.Append(gameWorld[i, j] ? stepped : empty);
					sb.Append(" ");
				}
                sb.Append(Environment.NewLine);
			}
			Console.SetCursorPosition(0, 0);

			// Render stringbuider to console
            Console.WriteLine(sb.ToString());
		}

		/// <summary>
		/// Method to center the cursor
		/// </summary>
		/// <param name="strLength">String lenght.</param>
		/// <param name="paragraphs">Number of paraghaphs</param>
        public void CenterCursor(int strLength, int paragraphs)
        {
            int cursorLeft = (Console.WindowWidth / 2) - (strLength / 2);
            int cursorTop = (Console.WindowHeight / 2) + paragraphs;
            Console.SetCursorPosition(cursorLeft, cursorTop);
        }

		/// <summary>
		/// Method that renders the message in case of a draw.
		/// </summary>
		public void RenderDraw()
        {
            Console.Clear();
            CenterCursor(12, 0);
            Console.WriteLine("It's a draw!!");
            Console.WriteLine();
            CenterCursor(30, 2);
            Console.WriteLine("Double tap any key to continue.");
            Console.ReadKey(true);
        }

		/// <summary>
		/// Method that renders the message in case player 1 wins.
		/// </summary>
		public void RenderPlayer1Wins()
        {
            Console.Clear();
            CenterCursor(14, 0);
            Console.WriteLine("Player 1 Wins!!");
            Console.WriteLine();
            CenterCursor(30, 2);
            Console.WriteLine("Double tap any key to continue.");
            Console.ReadKey(true);
		}

		/// <summary>
		/// Method that renders the message in case player 2 wins.
		/// </summary>
		public void RenderPlayer2Wins()
		{
            Console.Clear();
            CenterCursor(14, 0);
            Console.WriteLine("Player 2 Wins!!");
            Console.WriteLine();
            CenterCursor(30, 2);
            Console.WriteLine("Double tap any key to continue.");
            Console.ReadKey(true);
        }

		/// <summary>
		/// Render the match countdown.
		/// </summary>
        public void RenderMatchCountdown()
        {
            int cursorLeft = Console.BufferWidth / 2;
            int cursorTop = Console.CursorTop;
            for (int i = 3; i >= 1; i--)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.Write(i);
                Thread.Sleep(800);
            }
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(" ");
        }

		/// <summary>
		/// Render message before exiting game.
		/// </summary>
        public void ShowExitMessage()
        {
            Console.WriteLine("Press ENTER to quit.");
        }
	}
}
