using System;
using System.Text;
using System.Threading;

namespace Tron
{
	public class Renderer
	{
		private readonly char stepped = '*';
		private readonly char empty = '-';

		public void ShowMainMenu()
		{
            Console.Clear();
			Console.WriteLine("----- WELCOME TO TRON -----");
			Console.WriteLine();
			Console.WriteLine("1. Two players mode");
			Console.WriteLine("2. AI mode");
			Console.WriteLine("3. Credits");
			Console.WriteLine("4. Quit");
		}

        public void ShowCredits()
        {
            Console.Clear();
            Console.WriteLine("----- WELCOME TO TRON -----");
            Console.WriteLine();
            Console.WriteLine("Made by:");
            Console.WriteLine("Nuno Carri�o - https://github.com/NunoCarrico98");
            Console.WriteLine("Rui Martins - https://github.com/rui-martins");
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
        }

        public void RenderGameWorld(bool[,] gameWorld, Player player1, Player player2)
		{
			int row = gameWorld.GetLength(0);
			int col = gameWorld.GetLength(1);

            StringBuilder sb = new StringBuilder(row * col + 
				(Environment.NewLine.Length *2) * col);
			sb.Append($"Player 1 - {player1.Score}   ||   Player 2 - {player2.Score}");
			sb.Append(Environment.NewLine);
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					sb.Append(gameWorld[i, j] ? stepped : empty);
				}
                sb.Append(Environment.NewLine);
			}
			Console.SetCursorPosition(0, 0);
            Console.WriteLine(sb.ToString());
		}

        public void CenterCursor(int strLength, int paragraphs)
        {
            int cursorLeft = (Console.WindowWidth / 2) - (strLength / 2);
            int cursorTop = (Console.WindowHeight / 2) + paragraphs;
            Console.SetCursorPosition(cursorLeft, cursorTop);
        }

		public void Draw()
        {
            Console.Clear();
            CenterCursor(12, 0);
            Console.WriteLine("It's a draw!!");
            Console.WriteLine();
            CenterCursor(30, 2);
            Console.WriteLine("Double tap any key to continue.");
            Console.ReadKey();
        }

		public void Player1Wins()
        {
            Console.Clear();
            CenterCursor(14, 0);
            Console.WriteLine("Player 1 Wins!!");
            Console.WriteLine();
            CenterCursor(30, 2);
            Console.WriteLine("Double tap any key to continue.");
            Console.ReadKey();
		}

		public void Player2Wins()
		{
            Console.Clear();
            CenterCursor(14, 0);
            Console.WriteLine("Player 2 Wins!!");
            Console.WriteLine();
            CenterCursor(30, 2);
            Console.WriteLine("Double tap any key to continue.");
            Console.ReadKey();
        }

        public void MatchCountdown()
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

        public void ShowExitMessage()
        {
            Console.WriteLine("Press ENTER to quit.");
        }
	}
}
