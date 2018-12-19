using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class DoubleBuffer2D<T>
    {
        private T[,] current;
        private T[,] next;
        public int XDim { get; }
        public int YDim { get; }

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
