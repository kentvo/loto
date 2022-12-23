using System;
using System.Threading;
using NAudio.Wave;

namespace LoTo.Core
{
  
    public class LotoJukeBox: ILotoJukebox
    {
        private readonly string _path;
        private DirectSoundOut _player = new DirectSoundOut();

        public event EventHandler PlaybackEnded;

        public LotoJukeBox(string path)
        {
            _path = path;
            _player.PlaybackStopped += PlaybackStopped;
        }

        void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (PlaybackEnded != null)
                PlaybackEnded(this, e);
        }

        public void Play(int number)
        {
            var wfr = new WaveFileReader(GetFile(number));
            WaveChannel32 wc = new WaveChannel32(wfr) {PadWithZeroes = false};
            _player.Init(wc);

            Play();            
        }

        public void Intro()
        {
            var introFile = _path + "intro.wav";
            var wfr = new WaveFileReader(introFile);
            WaveChannel32 wc = new WaveChannel32(wfr) { PadWithZeroes = false };
            _player.Init(wc);

            Play();
        }

        private string GetFile(int numbers)
        {
            return _path + numbers.ToString("D3") + ".wav";
        }

        private void Play()
        {
            _player.Play();
        }

        public void Pause()
        {
            _player.Pause();
        }

        public void Resume()
        {
            Play();
        }
    }
}