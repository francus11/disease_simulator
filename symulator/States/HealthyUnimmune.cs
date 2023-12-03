using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace symulator.States
{
    class HealthyUnimmune : ISicknessState
    {
        private Person _person;

        public HealthyUnimmune(Person person)
        {
            _person = person;
            string colorHex = "#3f66f2";
            _person.Color = (Color)ColorConverter.ConvertFromString(colorHex);
        }

        public void GetInfection(ISicknessState sicknessState)
        {
            _person.SicknessState = sicknessState;
        }

        public void SpreadInfection(Person person)
        {
            
        }
    }
}
