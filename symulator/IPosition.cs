using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vectors;

namespace symulator
{
    interface IPosition
    {
        float[] Coords { get; }

        double Distance(IPosition position);

        void ChangeByVector(IVector vector);
    }
}
