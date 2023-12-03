using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace symulator.States
{
    class InfectedWithoutSymptoms : ISicknessState
    {
        private Person _person;

        public InfectedWithoutSymptoms(Person person)
        {
            _person = person;
            string colorHex = "#f5e911";
            _person.Color = (Color)ColorConverter.ConvertFromString(colorHex);
        }

        public void GetInfection(ISicknessState sicknessState)
        {
            throw new NotImplementedException();
        }

        public void SpreadInfection(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
