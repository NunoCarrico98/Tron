using System;

namespace Tron
{
    public class Player
    {
        protected PlayerDirections Direction { get; set; }
        public int Column { get; private set; }
        public int Row { get; private set; }
        public int Score { get; private set; }

        public Player(PlayerDirections direction, int column, int row)
        {
            Direction = direction;
            Column = column;
            Row = row;
            Score = 0;
        }

        public void ChangeDirection(ConsoleKeyInfo ki)
        {
            if (ki.Key == ConsoleKey.W || ki.Key == ConsoleKey.UpArrow)
                if (Direction != PlayerDirections.Down)
                    Direction = PlayerDirections.Up;
            if (ki.Key == ConsoleKey.S || ki.Key == ConsoleKey.DownArrow)
                if (Direction != PlayerDirections.Up)
                    Direction = PlayerDirections.Down;
            if (ki.Key == ConsoleKey.A || ki.Key == ConsoleKey.LeftArrow)
                if (Direction != PlayerDirections.Right)
                    Direction = PlayerDirections.Left;
            if (ki.Key == ConsoleKey.D || ki.Key == ConsoleKey.RightArrow)
                if (Direction != PlayerDirections.Left)
                    Direction = PlayerDirections.Right;
        }

        public virtual void Move()
        {
            if (Direction == PlayerDirections.Right)
                Column++;
            if (Direction == PlayerDirections.Left)
                Column--;
            if (Direction == PlayerDirections.Up)
                Row--;
            if (Direction == PlayerDirections.Down)
                Row++;
        }

        public bool DetectCollision(bool[,] gameWorld)
        {
            bool lost = false;
			int xdim = gameWorld.GetLength(0);
			int ydim = gameWorld.GetLength(1);

            if (Row >= xdim || Row < 0 || Column >= ydim || Column < 0)
                lost = true;
			else if (gameWorld[Row, Column])
				lost = true;
			
            return lost;
        }

		public void IncreaseScore()
		{
			Score++;
		}

		public void Reset(PlayerDirections direction, int row, int col)
		{
			Direction = direction;
			Row = row;
			Column = col;
		}

        public void ResetScore()
        {
            Score = 0;
        }
    }
}
