using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace symulator
{
    interface ISicknessState
    {
        void SpreadInfection(Person person);
        void GetInfection(ISicknessState sicknessState);
    }
}
