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
			Console.WriteLine("----- WELCOME TO TRON -----");
			Console.WriteLine();
			Console.WriteLine("1. Two players mode");
			Console.WriteLine("2. AI mode");
			Console.WriteLine("3. Credits");
			Console.WriteLine("4. Quit");
		}

		public void RenderGameWorld(DoubleBuffer2D<bool> gameWorld)
		{
			for (int i = 0; i < gameWorld.XDim; i++)
			{
				for (int j = 0; j < gameWorld.YDim; j++)
				{
					Console.Write(gameWorld[i, j] ? stepped : empty);
					Console.Write(' ');
				}
				Console.WriteLine();
			}

			Console.SetCursorPosition(0, 0);
		}
	}
}
