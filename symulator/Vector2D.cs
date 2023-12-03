using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vectors
{
    internal class Vector2D : IVector
    {
        private double x;
        private double y;

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }   

        public virtual double Abs()
        {
            double result = 0;
            double[] vector = GetComponents();

            foreach (var component in vector)
            {
                result += component * component;
            }
            
            result = Math.Sqrt(result);

            return result;
        }

        public virtual double Cdot(IVector vector)
        {
            var values = vector.GetComponents();

            double result = x * values[0] + y * values[1];

            return result;
        }

        public virtual double[] GetComponents()
        {
            double[] result = new double[2];
            result[0] = x;
            result[1] = y;

            return result;
        }

        public IVector Scale(double b)
        {
            return new Vector2D(x * b, y * b);
        }
    }
}
