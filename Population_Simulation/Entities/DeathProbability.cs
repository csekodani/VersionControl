using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population_Simulation.Entities
{
    public class DeathProbability
    {

        public Gender Gender { get; set; }
        public int Age { get; set; }

        public double Death_Probability { get; set; }

    }
}
