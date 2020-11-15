using Factory_ptrn.Abstractions;
using Factory_ptrn.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_ptrn
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();
        private IToyFactory _factory;
        private Toy _nextToy;
        private List<Color> _presColor = new List<Color>() { Color.Blue,Color.Black,Color.Fuchsia};
        public  int colorChoiceIndex = 0;
        private IToyFactory Factory // online it was public
        {
            get { return _factory; }
            set 
            { 
                _factory = value;
                DisplayNext();
            }
        }

        public Form1()
        {
            InitializeComponent();
            btnBallColor.BackColor = Color.Blue;
            Factory = new BallFactory();
            

            
        }
        public void DisplayNext()
        {
            if (_nextToy!= null)
            {
                Controls.Remove(_nextToy);
            }
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }
        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy =  Factory.CreateNew();  
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var toy in _toys)
            {
                toy.MoveToy();
                if (toy.Left > maxPosition)
                    maxPosition = toy.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestBall = _toys[0];
                mainPanel.Controls.Remove(oldestBall);
                _toys.Remove(oldestBall);
            }
        }

        private void carBtn_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void ballBtn_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory { BallColor=btnBallColor.BackColor};
        }

        private void btnBallColor_Click(object sender, EventArgs e)
        {
            colorChoiceIndex++;
            if (colorChoiceIndex == _presColor.Count) colorChoiceIndex = 0;
            btnBallColor.BackColor = _presColor[colorChoiceIndex];
            ballBtn_Click(sender, e);
        }
    }
}
