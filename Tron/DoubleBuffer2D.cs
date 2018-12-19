using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class DoubleBuffer2D<T>
    {
        // Current buffer
        private T[,] current;
        // Next buffer
        private T[,] next;

        /// <summary>
        /// X Dimension of the grid.
        /// </summary>
        public int XDim { get; }
        /// <summary>
        /// Y Dimension of the grid.
        /// </summary>
        public int YDim { get; }

        /// <summary>
        /// Double Buffer constructor.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public DoubleBuffer2D(int x, int y)
        {
            XDim = x;
            YDim = y;
            current = new T[x, y];
            next = new T[x, y];
            Clear();
        }

        public T this[int x, int y]
        {
            get => current[x, y];
            set => next[x, y] = value;
        }

        public void Clear()
        {
            Array.Clear(next, 0, next.Length);
        }

        public void Swap()
        {
            T[,] temp = current;
            current = next;
            next = temp;
        }
    }
}
