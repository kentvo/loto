using System;

namespace LoTo.Core
{
    public class NumberPickEventArgs : EventArgs
    {
        public int Number { get; set; }
    }

    public class LotoEngine
    {
        private bool started = false;
        private int currentNumber = 0;
        private IShuffleEngine _shuffleEngine;
        private readonly ILotoJukebox _player;
        public event EventHandler<NumberPickEventArgs> NumberPicked;
        
        public LotoEngine(int maxNumbers, ILotoJukebox player )
        {
            _shuffleEngine = new ShuffleEngine(maxNumbers);
            _player = player;
            _player.PlaybackEnded += PlaybackEnded;
        }

        void PlaybackEnded(object sender, EventArgs e)
        {
            if (_shuffleEngine.IsDone())
                return;

            if (currentNumber > 0)
                RaiseNumberPickedEvent(currentNumber);

            currentNumber = _shuffleEngine.GetNextNumber();
            _player.Play(currentNumber);
        }

        public void Reset()
        {
            started = false;
            currentNumber = 0;
            _shuffleEngine.Reset();
        }

        public void Run()
        {
            if (!started)
            {
                started = true;
                _player.Intro();                
            }                
            else
                _player.Resume();            
        }

        public void Pause()
        {
            _player.Pause();
        }

        public void Resume()
        {
            _player.Resume();
        }

        private void RaiseNumberPickedEvent(int number)
        {
            if(NumberPicked != null)
                NumberPicked.Invoke(this, new NumberPickEventArgs { Number = number});
        }
    }
}