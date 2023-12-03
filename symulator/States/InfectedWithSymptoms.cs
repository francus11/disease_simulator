using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace symulator.States
{
    class InfectedWithSymptoms : ISicknessState
    {
        private Person _person;

        public InfectedWithSymptoms(Person person)
        {
            _person = person;
            string colorHex = "#eb4034";
            _person.Color = (Color)ColorConverter.ConvertFromString(colorHex);
        }

        public void GetInfection(ISicknessState sicknessState)
        {

        }

        public void SpreadInfection(Person person)
        {
            
            Random random = new Random();
            double value = random.NextDouble();
            if (value < 0.5)
            {
                person.SicknessState = new InfectedWithoutSymptoms(person);
            }
            else
            {
                person.SicknessState = new InfectedWithSymptoms(person);
            }
                
                
        }

        
    }
}
