using System;

namespace Tron
{
    public class Player
    {
        public PlayerDirections Direction { get; private set; }
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
            else if (ki.Key == ConsoleKey.S || ki.Key == ConsoleKey.DownArrow)
                if (Direction != PlayerDirections.Up)
                    Direction = PlayerDirections.Down;
            else if (ki.Key == ConsoleKey.A || ki.Key == ConsoleKey.LeftArrow)
                if (Direction != PlayerDirections.Right)
                    Direction = PlayerDirections.Left;
            else if (ki.Key == ConsoleKey.D || ki.Key == ConsoleKey.RightArrow)
                if (Direction != PlayerDirections.Left)
                    Direction = PlayerDirections.Right;
        }

        public void Move()
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

        public bool DetectCollision(int xdim, int ydim)
        {
            bool lost = false;

            if (Row > ydim || Row < 0)
                lost = true;

            if (Column > xdim || Column < 0)
                lost = true;

            return lost;
        }
    }
}
