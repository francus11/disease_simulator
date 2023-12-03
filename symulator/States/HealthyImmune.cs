using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace symulator.States
{
    class HealthyImmune : ISicknessState
    {
        private Person _person;

        public HealthyImmune(Person person)
        {
            _person = person;
            string colorHex = "#1ad636";
            _person.Color = (Color)ColorConverter.ConvertFromString(colorHex);
        }

        public void GetInfection(ISicknessState sicknessState)
        {
        }

        public void SpreadInfection(Person person)
        {
        }
    }
}
