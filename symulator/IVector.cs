using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vectors
{
    internal interface IVector
    {
        double Abs();

        double Cdot(IVector vector);

        double[] GetComponents();

        IVector Scale(double b);
    }
}
