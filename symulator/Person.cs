using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using vectors;

namespace symulator
{
    class Person
    {
        private IVector _vector;
        private IPosition _position;
        private ISicknessState _sicknessState;
        private System.Windows.Media.Color _color;

        public IPosition Position { get { return _position; } }
        public IVector Vector { get { return _vector; } set { _vector = value; } }

        public Color Color { get { return _color; } set { _color = value; } }

        public ISicknessState SicknessState
        {
            get { return _sicknessState; } 
            set { _sicknessState = value; }
        }

        public Person(IVector vector, IPosition position)
        {
            _vector = vector;
            _position = position;
        }

        public void Move(TimeSpan timeSpan)
        {
            double time = timeSpan.TotalSeconds;
            IVector vector = _vector.Scale(time);
            _position.ChangeByVector(vector);
        }

        public void SpreadInfection(Person person)
        {
            _sicknessState.SpreadInfection(person);
        }

        public void GetInfection(ISicknessState sicknessState)
        {
            _sicknessState.GetInfection(sicknessState);
        }
    }
}
