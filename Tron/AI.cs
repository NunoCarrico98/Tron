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
            CheckNearestEnemies();
            if (Direction == PlayerDirections.Left) CheckDirectionLeft();
            if (Direction == PlayerDirections.Right) CheckDirectionRight();
            if (Direction == PlayerDirections.Up) CheckDirectionUp();
            if (Direction == PlayerDirections.Down) CheckDirectionDown();
        }

        private void CheckNearestEnemies()
        {
            int numOfTiles = 15;
            int rowValue = 0;
            int colValue = 0;
            int actualRow = 0;
            int actualCol = 0;
            bool flag = false;

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

            actualRow = Row;
            actualCol = Column;

            for (int i = 0; i < numOfTiles; i++)
            {
                if (flag) break;
                flag = CheckNeighbours(actualRow, actualCol);

                actualRow += rowValue;
                actualCol += colValue;
            }
        }

        private bool CheckNeighbours(int row, int col)
        {
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

            value = IsEnemyNearby();
            return value;
        }

        private bool IsEnemyNearby()
        {
            bool value = false;

            if (Direction == PlayerDirections.Left)
                value = Array.Exists(isStepped, x =>
                x == true && x == isStepped[0] ||
                x == true && x == isStepped[1] ||
                x == true && x == isStepped[2] ||
                x == true && x == isStepped[3] ||
                x == true && x == isStepped[5]);
            else if (Direction == PlayerDirections.Right)
                value = Array.Exists(isStepped, x =>
                x == true && x == isStepped[3] ||
                x == true && x == isStepped[5] ||
                x == true && x == isStepped[6] ||
                x == true && x == isStepped[7] ||
                x == true && x == isStepped[8]);
            else if (Direction == PlayerDirections.Up)
                value = Array.Exists(isStepped, x =>
                x == true && x == isStepped[0] ||
                x == true && x == isStepped[1] ||
                x == true && x == isStepped[3] ||
                x == true && x == isStepped[6] ||
                x == true && x == isStepped[7]);
            else if (Direction == PlayerDirections.Down)
                value = Array.Exists(isStepped, x =>
                x == true && x == isStepped[1] ||
                x == true && x == isStepped[2] ||
                x == true && x == isStepped[5] ||
                x == true && x == isStepped[7] ||
                x == true && x == isStepped[8]);

            return value;
        }

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
                SetRandomDirectionUpOrDown();
            }
        }

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
                SetRandomDirectionUpOrDown();
            }
        }

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
                SetRandomDirectionLeftOrRight();
            }
        }

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
                SetRandomDirectionLeftOrRight();
            }
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

        private void SetRandomDirection(PlayerDirections dir1, PlayerDirections dir2)
        {
            if (rnd.NextDouble() <= 0.05f)
                Direction = dir1;
            else if (rnd.NextDouble() <= 0.05f)
                Direction = dir2;
        }

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
