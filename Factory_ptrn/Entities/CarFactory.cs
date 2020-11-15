using Factory_ptrn.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_ptrn.Entities
{
    class CarFactory : IToyFactory
    {
        public Toy CreateNew()
        { return new Car(); }
    }
}
