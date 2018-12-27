using System;

namespace Tron
{
	/// <summary>
	/// Class that receives the player input.
	/// </summary>
	public class InputSystem
	{
		/// <summary>
		/// Current Player input.
		/// </summary>
		public ConsoleKeyInfo Ki { get; private set; }

		/// <summary>
		/// Event to send key pressed to player 1 to deal with.
		/// </summary>
		public event Action<ConsoleKeyInfo> Player1KeysPressed;
		/// <summary>
		/// Event to send key pressed to player 2 to deal with.
		/// </summary>
		public event Action<ConsoleKeyInfo> Player2KeysPressed;

		/// <summary>
		/// Method that receives player input and calls events accordingly.
		/// </summary>
		public void GetUserInput()
		{
			// Infinite loop
			while (true)
			{
				// Get player input
                Ki = Console.ReadKey(true);

				// If it's a WASD input, send key to player 1
				if (Ki.Key == ConsoleKey.W || Ki.Key == ConsoleKey.S || 
					Ki.Key == ConsoleKey.A || Ki.Key == ConsoleKey.D)
					OnPlayer1KeysPressed();

				// If it's an ARROW input, send key to player 2
				if (Ki.Key == ConsoleKey.UpArrow || Ki.Key == ConsoleKey.DownArrow ||
					Ki.Key == ConsoleKey.LeftArrow || Ki.Key == ConsoleKey.RightArrow)
					OnPlayer2KeysPressed();
			}
		}

		/// <summary>
		/// Reset player input to default.
		/// </summary>
        public void ResetUserInput()
        {
            Ki = default(ConsoleKeyInfo);
        }

		/// <summary>
		/// Check if the events have listeners.
		/// </summary>
		/// <returns></returns>
        public bool[] CheckEvents()
        {
			// Bool array that saves if the events have listeners
            bool[] eventsContent = new bool[2];

			// If events are not null, set bools to true
            if (Player1KeysPressed != null) eventsContent[0] = true;
            if (Player2KeysPressed != null) eventsContent[1] = true;

			// Return the array with the bools
			return eventsContent;
        }

		/// <summary>
		/// Method to invoke the event to the connected to the player 1.
		/// </summary>
		protected virtual void OnPlayer1KeysPressed()
		{
			// If event is not null, invoke it
			Player1KeysPressed?.Invoke(Ki);
		}

		/// <summary>
		/// Method to invoke the event to the connected to the player 2.
		/// </summary>
		protected virtual void OnPlayer2KeysPressed()
		{
			// If event is not null, invoke it
			Player2KeysPressed?.Invoke(Ki);
		}
	}
}
