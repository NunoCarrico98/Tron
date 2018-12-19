using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class KeyReader
    {
        private ConsoleKeyInfo _lastPressedKey;
        public event Action<ConsoleKeyInfo> ReadPressedKey;

        public void ReadKey()
        {
            while (true)
            {
                _lastPressedKey = Console.ReadKey(true);
                OnReadPressedKey();
            }
        }

        protected virtual void OnReadPressedKey()
        {
            ReadPressedKey?.Invoke(_lastPressedKey);
        }
    }
}
