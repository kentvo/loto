using System;

namespace LoTo.Core
{
    public interface IShuffleEngine
    {
        int GetNextNumber();
        int[] GetAllNumbers();
        bool IsDone();
        void Reset();
    }

    public class ShuffleEngine : IShuffleEngine
    {
        private int[] _numbers;
        private int _nextNumber;

        public ShuffleEngine(int maxNumber)
        {
            _numbers = new int[maxNumber];
            Reset();
        }

        public void Reset()
        {
            _nextNumber = 0;
            InitNumber();
            Shuffle();
        }

        public int GetNextNumber()
        {
            var i = _nextNumber++;
            if (_nextNumber > GetLastIndex())
                i = GetLastIndex();

            return _numbers[i];
        }

        private int GetLastIndex()
        {
            return _numbers.Length - 1;
        }

        public int[] GetAllNumbers()
        {
            return _numbers;
        }

        public bool IsDone()
        {
            return _nextNumber == _numbers.Length;
        }

        private void Shuffle()
        {
            var rng = new Random();
            int n = _numbers.Length;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var value = _numbers[k];
                _numbers[k] = _numbers[n];
                _numbers[n] = value;
            }
        }

        private void InitNumber()
        {
            int n = _numbers.Length;
            for (int i = 0; i < n; i++)
            {
                _numbers[i] = i + 1;
            }
        }
    }
}