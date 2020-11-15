using Factory_ptrn.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_ptrn.Entities
{
    class BallFactory : IToyFactory
    {
        public Color BallColor { get; set; }
        public BallFactory()
        {

        }
        public Toy CreateNew()
        {

            return new Ball(BallColor);
        }
    }
}
