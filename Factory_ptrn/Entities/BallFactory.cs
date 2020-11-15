using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_ptrn.Entities
{
    class BallFactory
    {
        public BallFactory()
        {

        }
        public Ball CreateNew()
        {

            return new Ball();
        }
    }
}
