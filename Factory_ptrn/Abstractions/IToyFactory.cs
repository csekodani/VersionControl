using Factory_ptrn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_ptrn.Abstractions
{
    public interface IToyFactory
    {
         Toy CreateNew();
    }
}
