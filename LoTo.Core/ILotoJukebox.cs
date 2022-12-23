using System;

namespace LoTo.Core
{
    public interface ILotoJukebox
    {
        event EventHandler PlaybackEnded;

        void Play(int number);
        void Intro();
        void Pause();
        void Resume();
    }
}