using System;

namespace Tron
{
	/// <summary>
	/// Player that deals with all player operations.
	/// </summary>
    public class Player
    {
		/// <summary>
		/// Current player Direction.
		/// </summary>
        protected PlayerDirections Direction { get; set; }
		/// <summary>
		/// Current Player Column
		/// </summary>
        public int Column { get; private set; }
		/// <summary>
		/// Current Player Row
		/// </summary>
        public int Row { get; private set; }
		/// <summary>
		/// Current Player Score
		/// </summary>
        public int Score { get; private set; }

		/// <summary>
		/// Player Constructor to initiliase player properties
		/// </summary>
		/// <param name="direction">Initial player direction.</param>
		/// <param name="column">Initial player column.</param>
		/// <param name="row">Initial player row.</param>
		public Player(PlayerDirections direction, int column, int row)
        {
			// Initialise player properties
            Direction = direction;
            Column = column;
            Row = row;
            Score = 0;
        }

		/// <summary>
		/// Method that changes player diretions accoding to player input.
		/// </summary>
		/// <param name="ki">Current player input.</param>
        public void ChangeDirection(ConsoleKeyInfo ki)
        {
			// If player inputs a W (player 1) or an UpArrow (player 2) and 
			// player current player direction is not Down
			if (ki.Key == ConsoleKey.W || ki.Key == ConsoleKey.UpArrow)
                if (Direction != PlayerDirections.Down)
					// Change Direction to Up
                    Direction = PlayerDirections.Up;
			// If player inputs a S (player 1) or an DownArrow (player 2) and
			// player current player direction is not Up
			if (ki.Key == ConsoleKey.S || ki.Key == ConsoleKey.DownArrow)
                if (Direction != PlayerDirections.Up)
					// Change Direction to Down
					Direction = PlayerDirections.Down;
			// If player inputs an A (player 1) or an LeftArrow (player 2) and
			// player current player direction is not Right
			if (ki.Key == ConsoleKey.A || ki.Key == ConsoleKey.LeftArrow)
                if (Direction != PlayerDirections.Right)
					// Change Direction to Left
					Direction = PlayerDirections.Left;
			// If player inputs a D (player 1) or an RightArrow (player 2) and
			// player current player direction is not Left
			if (ki.Key == ConsoleKey.D || ki.Key == ConsoleKey.RightArrow)
                if (Direction != PlayerDirections.Left)
					// Change Direction to Right
					Direction = PlayerDirections.Right;
        }

		/// <summary>
		/// Method that moves the player depending on the Direction.
		/// </summary>
        public virtual void Move()
        {
			// If Direction is Right, go right on map
            if (Direction == PlayerDirections.Right)
                Column++;
			// If Direction is Left, go left on map
			if (Direction == PlayerDirections.Left)
                Column--;
			// If Direction is Up, go up on map
			if (Direction == PlayerDirections.Up)
                Row--;
			// If Direction is Down, go down on map
			if (Direction == PlayerDirections.Down)
                Row++;
        }

		/// <summary>
		/// Method that detects player collisions between themselves and the 
		/// world.
		/// </summary>
		/// <param name="gameWorld">Current world level.</param>
		/// <returns>Returns true if a collsion is detected.</returns>
        public bool DetectCollision(bool[,] gameWorld)
        {
			// Create bool to return and get world x and y size
            bool lost = false;
			int xdim = gameWorld.GetLength(0);
			int ydim = gameWorld.GetLength(1);

			// If player position is of the map
            if (Row >= xdim || Row < 0 || Column >= ydim || Column < 0)
                lost = true;
			// If player collides with another player
			else if (gameWorld[Row, Column])
				lost = true;
			
			// Return false if there no collision or true if there is a collision
            return lost;
        }

		/// <summary>
		/// Method to increase player score.
		/// </summary>
		public void IncreaseScore()
		{
			// Increase Score by 1
			Score++;
		}

		/// <summary>
		/// Method to reset player properties.
		/// </summary>
		/// <param name="direction">Initial player direction.</param>
		/// <param name="row">Initial player row.</param>
		/// <param name="col">Initial player column.</param>
		public virtual void Reset(PlayerDirections direction, int row, int col)
		{
			// Reset player properties
			Direction = direction;
			Row = row;
			Column = col;
		}

		/// <summary>
		/// Method to reset the player score.
		/// </summary>
        public void ResetScore()
        {
			// Reset Score
            Score = 0;
        }
    }
}
