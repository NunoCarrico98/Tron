using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public void Draw()
		{
			Console.Clear();
			Console.WriteLine("It's a draw!!");
			Console.WriteLine("Press any key to play again...");
			Console.ReadKey();
		}

		public void Player1Wins()
		{
			Console.Clear();
			Console.WriteLine("Player 1 Wins!!");
			Console.WriteLine("Press any key to play again...");
			Console.ReadKey();
		}

		public void Player2Wins()
		{
			Console.Clear();
			Console.WriteLine("Player 2 Wins!!");
			Console.WriteLine("Press any key to play again...");
			Console.ReadKey();
		}

        public void ShowExitMessage()
        {
            Console.WriteLine("Press ENTER to quit.");
        }
	}
}
