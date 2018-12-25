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

		public AI(bool[,] gameWorld,int xDim, int yDim, PlayerDirections direction, int column, int row)
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
			// up left
			if (gameWorld[Row - 1, Column - 1])
				isStepped[0] = true;
			// left
			if (gameWorld[Row, Column - 1])
				isStepped[1] = true;
			// down left
			if (gameWorld[Row + 1, Column - 1])
				isStepped[2] = true;
			// down left
			if (gameWorld[Row - 1, Column])
				isStepped[3] = true;
			// AI Player
			if (gameWorld[Row, Column])
				isStepped[4] = true;
			// down
			if (gameWorld[Row + 1, Column])
				isStepped[5] = true;
			// up right
			if (gameWorld[Row - 1, Column + 1])
				isStepped[6] = true;
			// right
			if (gameWorld[Row, Column + 1])
				isStepped[7] = true;
			// down right
			if (gameWorld[Row + 1, Column + 1])
				isStepped[8] = true;
		}

		private void CheckDirectionLeft()
		{
			// if left is stepped
			if (isStepped[1])
				// if up/left and down/left are stepped
				if (isStepped[0] && isStepped[2])
				{
					// Random chance to turn up or down
					if (rnd.NextDouble() > 0.5f)
						Direction = PlayerDirections.Up;
					else
						Direction = PlayerDirections.Down;
				}
				// if up/left is stepped
				else if (isStepped[0])
					Direction = PlayerDirections.Down;
				// if down/left is stepped
				else if (isStepped[2])
					Direction = PlayerDirections.Up;
				// if up is stepped
				else if (isStepped[3])
					Direction = PlayerDirections.Down;
				// if down is stepped
				else if (isStepped[5])
					Direction = PlayerDirections.Up;
		}

		private void CheckDirectionRight()
		{
			// if right is stepped
			if (isStepped[7])
				// if up/right and down/right are stepped
				if (isStepped[6] && isStepped[8])
				{
					// Random chance to turn up or down
					if (rnd.NextDouble() > 0.5f)
						Direction = PlayerDirections.Up;
					else
						Direction = PlayerDirections.Down;
				}
				// if up/right is stepped
				else if (isStepped[6])
					Direction = PlayerDirections.Down;
				// if down/right is stepped
				else if (isStepped[8])
					Direction = PlayerDirections.Up;
				// if up is stepped
				else if (isStepped[3])
					Direction = PlayerDirections.Down;
				// if down is stepped
				else if (isStepped[5])
					Direction = PlayerDirections.Up;
		}

		private void CheckDirectionUp()
		{
			// if up is stepped
			if (isStepped[3])
				// if up/left and up/right are stepped
				if (isStepped[0] && isStepped[6])
				{
					// Random chance to turn up or down
					if (rnd.NextDouble() > 0.5f)
						Direction = PlayerDirections.Left;
					else
						Direction = PlayerDirections.Right;
				}
				// if up/left is stepped
				else if (isStepped[0])
					Direction = PlayerDirections.Right;
				// if up/right is stepped
				else if (isStepped[2])
					Direction = PlayerDirections.Left;
				// if left is stepped
				else if (isStepped[1])
					Direction = PlayerDirections.Right;
				// if right is stepped
				else if (isStepped[7])
					Direction = PlayerDirections.Left;
		}

		private void CheckDirectionDown()
		{
			// if up is stepped
			if (isStepped[5])
				// if down/left and down/right are stepped
				if (isStepped[2] && isStepped[8])
				{
					// Random chance to turn up or down
					if (rnd.NextDouble() > 0.5f)
						Direction = PlayerDirections.Left;
					else
						Direction = PlayerDirections.Right;
				}
				// if down/left is stepped
				else if (isStepped[2])
					Direction = PlayerDirections.Right;
				// if down/right is stepped
				else if (isStepped[8])
					Direction = PlayerDirections.Left;
				// if left is stepped
				else if (isStepped[1])
					Direction = PlayerDirections.Right;
				// if right is stepped
				else if (isStepped[7])
					Direction = PlayerDirections.Left;
		}

		private bool CheckIfWithinBounds()
		{
			if (Row >=  xDim || Row < 0 || Column >= yDim || Column < 0)
				return false;
			else return true;
		}
	}
}
