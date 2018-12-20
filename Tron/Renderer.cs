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
            StringBuilder sb = new StringBuilder(gameWorld.XDim * gameWorld.YDim +
                Environment.NewLine.Length * gameWorld.YDim);
			for (int i = 0; i < gameWorld.XDim; i++)
			{
				for (int j = 0; j < gameWorld.YDim; j++)
				{
					sb.Append(gameWorld[i, j] ? stepped : empty);
				}
                sb.Append(Environment.NewLine);
			}
			Console.SetCursorPosition(0, 0);
            Console.WriteLine(sb.ToString());
		}
	}
}
