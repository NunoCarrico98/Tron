using System;

namespace Tron
{
	/// <summary>
	/// Class that controls the AI player.
	/// </summary>
    public class AI : Player
    {
		/// <summary>
		/// Reference to the game world.
		/// </summary>
        private readonly bool[,] gameWorld;
		/// <summary>
		/// X size of the game world.
		/// </summary>
        private readonly int xDim;
		/// <summary>
		/// X size of the game world.
		/// </summary>
		private readonly int yDim;
		/// <summary>
		/// Array that saves if the positions next to the AI are stepped.
		/// </summary>
		private bool[] isStepped;
		/// <summary>
		/// Instance of the random class.
		/// </summary>
        private Random rnd = new Random();

        public AI(bool[,] gameWorld, int xDim, int yDim, PlayerDirections direction, int column, int row)
            : base(direction, column, row)
        {
			/// Initialise references to world and x and y sizes
            this.xDim = xDim;
            this.yDim = yDim;
            this.gameWorld = gameWorld;

            // Array positions corresponding to the verification array
            // isStepped[0] = up left
            // isStepped[1] = left
            // isStepped[2] = down left
            // isStepped[3] = up
            // isStepped[4] = AI player position
            // isStepped[5] = down
            // isStepped[6] = up right
            // isStepped[7] = right
            // isStepped[8] = down right
            isStepped = new bool[9];
        }

		/// <summary>
		/// Override the Move method from Player so AI can change directions.
		/// </summary>
        public override void Move()
        {
            ChangeDirection();
            base.Move();
        }

		/// <summary>
		/// Method that changes AI player Direction.
		/// </summary>
        public void ChangeDirection()
        {
			// Reset isStepped Array
            ResetCheckArray();
			// Check if there neighbour tiles are stepped
            CheckNearestEnemies();
			// Change Direction according to the neighbours that surround the AI
            if (Direction == PlayerDirections.Left) CheckDirectionLeft();
            if (Direction == PlayerDirections.Right) CheckDirectionRight();
            if (Direction == PlayerDirections.Up) CheckDirectionUp();
            if (Direction == PlayerDirections.Down) CheckDirectionDown();
        }

        /// <summary>
        /// Method that checks for the nearest 
        /// enemies in the direction the player is headed.
        /// </summary>
        private void CheckNearestEnemies()
        {
            // Maximum number of tiles to check ahead of the player position
            int numOfTiles = 15;

            // Vars that receive a value depending on the actual direction, 
            // allowing us to move across the array.
            int rowValue = 0;
            int colValue = 0;

            // Vars that receive the actual row / column we're searching on
            // The default is the tile we're on
            int actualRow = Row;
            int actualCol = Column;

            // Flag that breaks the cycle once we find an enemy tile
            bool flag = false;

            // Set the row and column values according to the actual direction
            switch (Direction)
            {
                case PlayerDirections.Left:
                    colValue = -1;
                    rowValue = 0;
                    break;
                case PlayerDirections.Right:
                    colValue = 1;
                    rowValue = 0;
                    break;
                case PlayerDirections.Down:
                    colValue = 0;
                    rowValue = 1;
                    break;
                case PlayerDirections.Up:
                    colValue = 0;
                    rowValue = -1;
                    break;
            }

            // Cycle until our maximum number of tiles is reached
            for (int i = 0; i < numOfTiles; i++)
            {
                // If flag is true, break out of cycle because we have found an enemy
                if (flag) break;

                // Flag receives false or true, depending if we have found an enemy
                flag = CheckNeighbours(actualRow, actualCol);

                // Receive the values of our next tile to check
                actualRow += rowValue;
                actualCol += colValue;
            }
        }

        /// <summary>
        /// Method that checks our nearest neighbours.
        /// </summary>
        /// <param name="row">Row to check.</param>
        /// <param name="col">Column to check.</param>
        /// <returns>Returns true if an enemy is found, false if not.</returns>
        private bool CheckNeighbours(int row, int col)
        {
            // Var that holds value to return
            bool value = false;

            // Up/Left of current position
            if (CheckIfWithinBounds(row - 1, col - 1, 0))
                if (gameWorld[row - 1, col - 1])
                    isStepped[0] = true;
            // Left of current position
            if (CheckIfWithinBounds(row, col - 1, 1))
                if (gameWorld[row, col - 1])
                    isStepped[1] = true;
            // Down/Left of current position
            if (CheckIfWithinBounds(row + 1, col - 1, 2))
                if (gameWorld[row + 1, col - 1])
                    isStepped[2] = true;
            // Up of current position
            if (CheckIfWithinBounds(row - 1, col, 3))
                if (gameWorld[row - 1, col])
                    isStepped[3] = true;
            // Down of current position
            if (CheckIfWithinBounds(row + 1, col, 5))
                if (gameWorld[row + 1, col])
                    isStepped[5] = true;
            // Up/Right of current position
            if (CheckIfWithinBounds(row - 1, col + 1, 6))
                if (gameWorld[row - 1, col + 1])
                    isStepped[6] = true;
            // Right of current position
            if (CheckIfWithinBounds(row, col + 1, 7))
                if (gameWorld[row, col + 1])
                    isStepped[7] = true;
            // Down/Right of current position
            if (CheckIfWithinBounds(row + 1, col + 1, 8))
                if (gameWorld[row + 1, col + 1])
                    isStepped[8] = true;

            // Receive true or false, depending if enemy is found
            value = IsEnemyNearby();

            // Return value
            return value;
        }

		/// <summary>
		/// Method that checks if the AI has an enenmy nearby.
		/// </summary>
		/// <returns>Returns true is and enemy is found.</returns>
        private bool IsEnemyNearby()
        {
            // Var to hold value to return
            bool value = false;

            // If current direction is left, we only care about the enemies
            // on top, down, up/left, left and down/left
            // If any of these positions are true, set value to true
            if (Direction == PlayerDirections.Left)
                value = Array.Exists(isStepped, x =>
                x == true && x == isStepped[0] ||
                x == true && x == isStepped[1] ||
                x == true && x == isStepped[2] ||
                x == true && x == isStepped[3] ||
                x == true && x == isStepped[5]);
            // If current direction is right, we only care about the enemies
            // on top, down, up/right, right and down/right
            // If any of these positions are true, set value to true
            else if (Direction == PlayerDirections.Right)
                value = Array.Exists(isStepped, x =>
                x == true && x == isStepped[3] ||
                x == true && x == isStepped[5] ||
                x == true && x == isStepped[6] ||
                x == true && x == isStepped[7] ||
                x == true && x == isStepped[8]);
            // If current direction is up, we only care about the enemies
            // on top/left, left, top, right/top, right
            // If any of these positions are true, set value to true
            else if (Direction == PlayerDirections.Up)
                value = Array.Exists(isStepped, x =>
                x == true && x == isStepped[0] ||
                x == true && x == isStepped[1] ||
                x == true && x == isStepped[3] ||
                x == true && x == isStepped[6] ||
                x == true && x == isStepped[7]);
            // If current direction is down, we only care about the enemies
            // on left, down/left, down, right, down/right
            // If any of these positions are true, set value to true
            else if (Direction == PlayerDirections.Down)
                value = Array.Exists(isStepped, x =>
                x == true && x == isStepped[1] ||
                x == true && x == isStepped[2] ||
                x == true && x == isStepped[5] ||
                x == true && x == isStepped[7] ||
                x == true && x == isStepped[8]);

            // Return value
            return value;
        }

		/// <summary>
		/// Check neibours and change direction when current direction is Left.
		/// </summary>
        private void CheckDirectionLeft()
        {
            // If Left is stepped
            if (isStepped[1])
            {
                // If Up/Left and Down/Left are stepped
                if (isStepped[0] && isStepped[2])
                {
                    if (isStepped[3])
                        Direction = PlayerDirections.Down;
                    else if (isStepped[5])
                        Direction = PlayerDirections.Up;
                    else
                        // Random chance to turn up or down
                        Direction = (rnd.NextDouble() > 0.5f)
                            ? PlayerDirections.Up : PlayerDirections.Down;
                }
                // If Up/Left is stepped
                else if (isStepped[0])
                    Direction = PlayerDirections.Down;
                // If Down/Left is stepped
                else if (isStepped[2])
                    Direction = PlayerDirections.Up;
                // If Up is stepped
                else if (isStepped[3])
                    Direction = PlayerDirections.Down;
                // If Down is stepped
                else if (isStepped[5])
                    Direction = PlayerDirections.Up;
                else
                    Direction = (rnd.NextDouble() > 0.5f)
                        ? PlayerDirections.Up : PlayerDirections.Down;
            }
            else
            {
				// If there is no front obstacles, there is a chance that the 
				// AI will change direction randomly
                SetRandomDirectionUpOrDown();
            }
        }

		/// <summary>
		/// Check neibours and change direction when current direction is Right.
		/// </summary>
		private void CheckDirectionRight()
        {
            // If Right is stepped
            if (isStepped[7])
            {
                // If Up/Right and Down/Right are stepped
                if (isStepped[6] && isStepped[8])
                {
                    if (isStepped[3])
                        Direction = PlayerDirections.Down;
                    else if (isStepped[5])
                        Direction = PlayerDirections.Up;
                    else
                        // Random chance to turn up or down
                        Direction = (rnd.NextDouble() > 0.5f)
                            ? PlayerDirections.Up : PlayerDirections.Down;
                }
                // If Up/Right is stepped
                else if (isStepped[6])
                    Direction = PlayerDirections.Down;
                // If Down/Right is stepped
                else if (isStepped[8])
                    Direction = PlayerDirections.Up;
                // If Up is stepped
                else if (isStepped[3])
                    Direction = PlayerDirections.Down;
                // If Down is stepped
                else if (isStepped[5])
                    Direction = PlayerDirections.Up;
                else
                    Direction = (rnd.NextDouble() > 0.5f)
                        ? PlayerDirections.Up : PlayerDirections.Down;
            }
            else
            {
				// If there is no front obstacles, there is a chance that the 
				// AI will change direction randomly
				SetRandomDirectionUpOrDown();
            }
        }

		/// <summary>
		/// Check neibours and change direction when current direction is Up.
		/// </summary>
		private void CheckDirectionUp()
        {
            // If Up is stepped
            if (isStepped[3])
            {
                // If Up/Left and Up/Right are stepped
                if (isStepped[0] && isStepped[6])
                {
                    if (isStepped[1])
                        Direction = PlayerDirections.Right;
                    else if (isStepped[7])
                        Direction = PlayerDirections.Left;
                    else
                        // Random chance to turn left or right
                        Direction = (rnd.NextDouble() > 0.5f)
                            ? PlayerDirections.Left : PlayerDirections.Right;
                }
                // If Up/Left is stepped
                else if (isStepped[0])
                    Direction = PlayerDirections.Right;
                // If Up/Right is stepped
                else if (isStepped[2])
                    Direction = PlayerDirections.Left;
                // If Left is stepped
                else if (isStepped[1])
                    Direction = PlayerDirections.Right;
                // If Right is stepped
                else if (isStepped[7])
                    Direction = PlayerDirections.Left;
                else
                    Direction = (rnd.NextDouble() > 0.5f)
                        ? PlayerDirections.Left : PlayerDirections.Right;
            }
            else
            {
				// If there is no front obstacles, there is a chance that the 
				// AI will change direction randomly
				SetRandomDirectionLeftOrRight();
            }
        }

		/// <summary>
		/// Check neibours and change direction when current direction is Down.
		/// </summary>
		private void CheckDirectionDown()
        {
            // If Down is stepped
            if (isStepped[5])
            {
                // If Down/Left and Down/Right are stepped
                if (isStepped[2] && isStepped[8])
                {
                    if (isStepped[1])
                        Direction = PlayerDirections.Right;
                    else if (isStepped[7])
                        Direction = PlayerDirections.Left;
                    else
                        // Random chance to turn left or right
                        Direction = (rnd.NextDouble() > 0.5f)
                            ? PlayerDirections.Left : PlayerDirections.Right;
                }
                // If Down/Left is stepped
                else if (isStepped[2])
                    Direction = PlayerDirections.Right;
                // If Down/Right is stepped
                else if (isStepped[8])
                    Direction = PlayerDirections.Left;
                // If Left is stepped
                else if (isStepped[1])
                    Direction = PlayerDirections.Right;
                // If Right is stepped
                else if (isStepped[7])
                    Direction = PlayerDirections.Left;
                else
                    Direction = (rnd.NextDouble() > 0.5f)
                        ? PlayerDirections.Left : PlayerDirections.Right;
            }
            else
            {
				// If there is no front obstacles, there is a chance that the 
				// AI will change direction randomly
				SetRandomDirectionLeftOrRight();
            }
        }

		/// <summary>
		/// Method that verifies if certain coordinates are within the game world.
		/// </summary>
		/// <param name="row">Row to check.</param>
		/// <param name="column">Column to check.</param>
		/// <param name="i">Current isStepped member being verified.</param>
		/// <returns>Returns true if is within bounds.</returns>
        private bool CheckIfWithinBounds(int row, int column, int i)
        {
            // If positions to check is not inside the level
            if (row >= xDim || row < 0 || column >= yDim || column < 0)
            {
                isStepped[i] = true;
                return false;
            }
            else return true;
        }

		/// <summary>
		/// Method that sets a random direction according to a certain probability.
		/// </summary>
		/// <param name="dir1">One of the possible Directions.</param>
		/// <param name="dir2">Other Direction.</param>
        private void SetRandomDirection(PlayerDirections dir1, PlayerDirections dir2)
        {
			// Probability to change direction is 5% for each direction 
            if (rnd.NextDouble() <= 0.05f)
                Direction = dir1;
            else if (rnd.NextDouble() <= 0.05f)
                Direction = dir2;
        }

		/// <summary>
		/// Method that sets the direction randomly to Left or Right.
		/// </summary>
        private void SetRandomDirectionLeftOrRight()
        {
            // If Left and Right is stepped
            if (isStepped[1] && isStepped[7])
                Direction = Direction;
            // If Left is stepped
            else if (isStepped[1])
                SetRandomDirection(PlayerDirections.Right, PlayerDirections.Right);
            // If Right is stepped
            else if (isStepped[7])
                SetRandomDirection(PlayerDirections.Left, PlayerDirections.Left);
            else
                SetRandomDirection(PlayerDirections.Left, PlayerDirections.Right);
        }

		/// <summary>
		/// Method that sets the direction randomly to Up or Down.
		/// </summary>
		private void SetRandomDirectionUpOrDown()
        {
            // If Up and Down is stepped
            if (isStepped[3] && isStepped[5])
                Direction = Direction;
            // If Up is stepped
            if (isStepped[3])
                SetRandomDirection(PlayerDirections.Down, PlayerDirections.Down);
            // If Down is stepped
            else if (isStepped[5])
                SetRandomDirection(PlayerDirections.Up, PlayerDirections.Up);
            else
                SetRandomDirection(PlayerDirections.Up, PlayerDirections.Down);
        }

		/// <summary>
		/// Reset the array that saves if the positions next to the AI are stepped.
		/// </summary>
		private void ResetCheckArray()
        {
            for (int i = 0; i < isStepped.Length; i++)
                isStepped[i] = false;
        }

		/// <summary>
		/// Override the Reset Method from the player to be able to reset the 
		/// isStepped array.
		/// </summary>
		/// <param name="direction">Initial player direction.</param>
		/// <param name="row">Initial player row.</param>
		/// <param name="col">Initial player column.</param>
		public override void Reset(PlayerDirections direction, int row, int col)
        {
            base.Reset(direction, row, col);
            ResetCheckArray();
        }
    }
}
