using System;
using System.Configuration;
using System.Windows.Forms;
using LoTo.Core;

namespace Loto
{
    public partial class Main : Form
    {
        private LotoEngine _lotoEngine;
        private ILotoJukebox _lotoJukeBox;

        public Main()
        {
            var maxNumber = int.Parse(ConfigurationManager.AppSettings["MaxNumber"] ?? "0");
            var musicPath = ConfigurationManager.AppSettings["MusicPath"];

            InitializeComponent();
            SetupNumberDisplay(maxNumber);
            
            _lotoJukeBox = new LotoJukeBox(musicPath);
            _lotoEngine = new LotoEngine(maxNumber, _lotoJukeBox);
            _lotoEngine.NumberPicked += NumberPicked;
            
            ShowPlayButton();
        }

        private void SetupNumberDisplay(int maxNumber)
        {
            numberTracker.SetUp(maxNumber);
        }

        void NumberPicked(object sender, NumberPickEventArgs e)
        {
            SetPickedNumber(e.Number);
        }

        private void SetPickedNumber(int number)
        {
            numberTracker.NumberIsPicked(number);
            PickedNumber.Text = number.ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _lotoEngine.Run();
            ShowBingoButton();
        }
       
        private void ShowPlayButton()
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;
        }

        private void ShowBingoButton()
        {
            btnStart.Enabled = false;
            btnPause.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _lotoEngine.Pause();

            ShowPlayButton();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Starter et nytt spill?", "LoTo", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            StartNewGame();
        }

        private void StartNewGame()
        {
            ShowBingoButton();

            PickedNumber.Text = "?";
            numberTracker.Reset();
            _lotoEngine.Reset();
            _lotoEngine.Run();             
        }
    }
}
