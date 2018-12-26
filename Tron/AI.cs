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
            ChangeDirection();
            base.Move();
        }

        public void ChangeDirection()
        {
            ResetCheckArray();
            CheckNeighours();
            if (Direction == PlayerDirections.Left) CheckDirectionLeft();
            if (Direction == PlayerDirections.Right) CheckDirectionRight();
            if (Direction == PlayerDirections.Up) CheckDirectionUp();
            if (Direction == PlayerDirections.Down) CheckDirectionDown();
        }

        private void CheckNeighours()
        {
            // Up/Left of current position
            if (CheckIfWithinBounds(Row - 1, Column - 1, 0))
                if (gameWorld[Row - 1, Column - 1])
                    isStepped[0] = true;
            // Left of current position
            if (CheckIfWithinBounds(Row, Column - 1, 1))
                if (gameWorld[Row, Column - 1])
                    isStepped[1] = true;
            // Down/Left of current position
            if (CheckIfWithinBounds(Row + 1, Column - 1, 2))
                if (gameWorld[Row + 1, Column - 1])
                    isStepped[2] = true;
            // Up of current position
            if (CheckIfWithinBounds(Row - 1, Column, 3))
                if (gameWorld[Row - 1, Column])
                    isStepped[3] = true;
            // AI Player
            if (CheckIfWithinBounds(Row, Column, 4))
                if (gameWorld[Row, Column])
                    isStepped[4] = true;
            // Down of current position
            if (CheckIfWithinBounds(Row + 1, Column, 5))
                if (gameWorld[Row + 1, Column])
                    isStepped[5] = true;
            // Up/Right of current position
            if (CheckIfWithinBounds(Row - 1, Column + 1, 6))
                if (gameWorld[Row - 1, Column + 1])
                    isStepped[6] = true;
            // Right of current position
            if (CheckIfWithinBounds(Row, Column + 1, 7))
                if (gameWorld[Row, Column + 1])
                    isStepped[7] = true;
            // Down/Right of current position
            if (CheckIfWithinBounds(Row + 1, Column + 1, 8))
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
        }

        private void CheckDirectionRight()
        {
            // If Right is stepped
            if (isStepped[7])
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
        }

        private void CheckDirectionUp()
        {
            // If Up is stepped
            if (isStepped[3])
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
        }

        private void CheckDirectionDown()
        {
            // If Down is stepped
            if (isStepped[5])
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
        }

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

        private void ResetCheckArray()
        {
            for (int i = 0; i < isStepped.Length; i++)
                isStepped[i] = false;
        }

        public override void Reset(PlayerDirections direction, int row, int col)
        {
            base.Reset(direction, row, col);
            ResetCheckArray();
        }
    }
}
