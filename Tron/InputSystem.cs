using System;
using System.Collections.Concurrent;

namespace Tron
{
	public class InputSystem
	{
		private ConsoleKeyInfo Ki { get; set; }

		public event Action<ConsoleKeyInfo> Player1KeysPressed;
		public event Action<ConsoleKeyInfo> Player2KeysPressed;

		public void GetUserInput()
		{
			while (true)
			{
                Ki = Console.ReadKey();

				if (Ki.Key == ConsoleKey.W || Ki.Key == ConsoleKey.S || 
					Ki.Key == ConsoleKey.A || Ki.Key == ConsoleKey.D)
					OnPlayer1KeysPressed();

				if (Ki.Key == ConsoleKey.UpArrow || Ki.Key == ConsoleKey.DownArrow ||
					Ki.Key == ConsoleKey.LeftArrow || Ki.Key == ConsoleKey.RightArrow)
					OnPlayer2KeysPressed();

				if (Ki.Key == ConsoleKey.Escape) break;
			}
		}

		protected virtual void OnPlayer1KeysPressed()
		{
			Player1KeysPressed?.Invoke(Ki);
		}

		protected virtual void OnPlayer2KeysPressed()
		{
			Player2KeysPressed?.Invoke(Ki);
		}
	}
}
