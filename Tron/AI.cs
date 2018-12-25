using System;

namespace Tron
{
	public class AI : Player
	{
		private readonly bool[,] gameWorld;
		private readonly int xDim;
		private readonly int yDim;
		private bool[] isStepped;
		private Random rnd = new Random();

		public AI(bool[,] gameWorld, int xDim, int yDim, PlayerDirections direction, int column, int row)
			: base(direction, column, row)
		{
			this.xDim = xDim;
			this.yDim = yDim;
			this.gameWorld = gameWorld;

			// Array that saves if the positions next to the AI are stepped
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

		public override void Move()
		{
			CheckNeighours();
			if (Direction == PlayerDirections.Left) CheckDirectionLeft();
			if (Direction == PlayerDirections.Right) CheckDirectionRight();
			if (Direction == PlayerDirections.Up) CheckDirectionUp();
			if (Direction == PlayerDirections.Down) CheckDirectionDown();
			base.Move();
		}

		private void CheckNeighours()
		{
			// Up/Left of current position
			if (CheckIfWithinBounds(Row - 1, Column - 1))
				if (gameWorld[Row - 1, Column - 1])
					isStepped[0] = true;
			// Left of current position
			if (CheckIfWithinBounds(Row, Column - 1))
				if (gameWorld[Row, Column - 1])
					isStepped[1] = true;
			// Down/Left of current position
			if (CheckIfWithinBounds(Row + 1, Column - 1))
				if (gameWorld[Row + 1, Column - 1])
					isStepped[2] = true;
			// Up of current position
			if (CheckIfWithinBounds(Row - 1, Column))
				if (gameWorld[Row - 1, Column])
					isStepped[3] = true;
			// AI Player
			if (CheckIfWithinBounds(Row, Column))
				if (gameWorld[Row, Column])
					isStepped[4] = true;
			// Down of current position
			if (CheckIfWithinBounds(Row + 1, Column))
				if (gameWorld[Row + 1, Column])
					isStepped[5] = true;
			// Up/Right of current position
			if (CheckIfWithinBounds(Row - 1, Column + 1))
				if (gameWorld[Row - 1, Column + 1])
					isStepped[6] = true;
			// Right of current position
			if (CheckIfWithinBounds(Row, Column + 1))
				if (gameWorld[Row, Column + 1])
					isStepped[7] = true;
			// Down/Right of current position
			if (CheckIfWithinBounds(Row + 1, Column + 1))
				if (gameWorld[Row + 1, Column + 1])
					isStepped[8] = true;
		}

		private void CheckDirectionLeft()
		{
			// If Left is stepped
			if (isStepped[1])
				// If Up/Left and Down/Left are stepped
				if (isStepped[0] && isStepped[2])
				{
					// Random chance to turn up or down
					if (rnd.NextDouble() > 0.5f)
						Direction = PlayerDirections.Up;
					else
						Direction = PlayerDirections.Down;
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
		}

		private void CheckDirectionRight()
		{
			// If Right is stepped
			if (isStepped[7])
				// If Up/Right and Down/Right are stepped
				if (isStepped[6] && isStepped[8])
				{
					// Random chance to turn up or down
					if (rnd.NextDouble() > 0.5f)
						Direction = PlayerDirections.Up;
					else
						Direction = PlayerDirections.Down;
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
		}

		private void CheckDirectionUp()
		{
			// If Up is stepped
			if (isStepped[3])
				// If Up/Left and Up/Right are stepped
				if (isStepped[0] && isStepped[6])
				{
					// Random chance to turn up or down
					if (rnd.NextDouble() > 0.5f)
						Direction = PlayerDirections.Left;
					else
						Direction = PlayerDirections.Right;
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
		}

		private void CheckDirectionDown()
		{
			// If Up is stepped
			if (isStepped[5])
				// If Down/Left and Down/Right are stepped
				if (isStepped[2] && isStepped[8])
				{
					// Random chance to turn up or down
					if (rnd.NextDouble() > 0.5f)
						Direction = PlayerDirections.Left;
					else
						Direction = PlayerDirections.Right;
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
		}

		private bool CheckIfWithinBounds(int row, int column)
		{
			// If positions to check is not inside the level
			if (row >= xDim || row < 0 || column >= yDim || column < 0)
				return false;
			else return true;
		}
	}
}
