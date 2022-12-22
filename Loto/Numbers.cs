using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Loto
{
    public partial class Numbers : UserControl
    {
        private List<Label> _labels;
        private int _maxNumber;

        public Numbers()
        {
            InitializeComponent();
        }

        public void SetUp(int maxNumber)
        {
            _maxNumber = maxNumber;
            _labels = new List<Label>(maxNumber);
            int numberOfColums = 10;
            int numberOfRows = maxNumber/10;
            int labelWidth = tableLayoutPanel1.Size.Width / numberOfColums;
            int labelHeigth = tableLayoutPanel1.Size.Height/numberOfRows;            

            for (int i = 0; i < maxNumber; i++)
            {
                var label = new Label
                    {
                        Text = (i+1).ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        BackColor = Color.Gainsboro,
                        Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Regular, GraphicsUnit.Point, 0),
                        Size = new Size(labelWidth, labelHeigth)
                        
                    };
                _labels.Add(label);
                tableLayoutPanel1.Controls.Add(label,i / numberOfColums, i % numberOfColums);
            }

        }

        public void NumberIsPicked(int number)
        {
            SetNumberLabelBlueWhite(number);
            AddNumbersToSelectedNumbers(number);
        }

        public void Reset()
        {
            selectedNumbers.Text = string.Empty;

            for (int i = 0; i < _maxNumber; i++)
            {
                _labels[i].BackColor = Color.Gainsboro;
                _labels[i].ForeColor = Color.Black;
            }            
        }

        private void SetNumberLabelBlueWhite(int number)
        {
            var label = GetLabel(number);
            if (label == null)
                return;
            label.BackColor = Color.Blue;
            label.ForeColor = Color.White;            
        }

        private void AddNumbersToSelectedNumbers(int number)
        {
            if (selectedNumbers.Text.Length > 0)
                selectedNumbers.Text += "-";
            selectedNumbers.Text += number;
        }

        private Label GetLabel(int number)
        {
            if (number > 0 && number <= _maxNumber)
                return _labels[number-1];
            return null;
        }
    }
}
