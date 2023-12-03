using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vectors;

namespace symulator
{
    class Position2D : IPosition
    {
        private float _x;
        private float _y;

        public Position2D(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public float[] Coords
        {
            get
            {
                float[] result = new float[2];
                result[0] = _x;
                result[1] = _y;

                return result;
            }
        }

        public void ChangeByVector(IVector vector)
        {
            var components = vector.GetComponents();
            _x += (float)components[0];
            _y += (float)components[1];
        }

        public double Distance(IPosition position)
        {
            float[] coordsThis = Coords;
            float[] coordsOther = position.Coords; 

            double differenceX = coordsThis[0] - coordsOther[0];
            double differenceY = coordsThis[1] - coordsOther[1];

            return Math.Sqrt(differenceX * differenceX + differenceY * differenceY);
        }
    }
}
