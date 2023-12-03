using symulator.States;
using System;
using System.Collections.Generic;
using vectors;

namespace symulator
{
    class Simulation
    {
        private float _width;
        private float _height;
        private float _EllipseDiameter = 2;

        private Random random = new Random();
        private List<Person> _persons;

        public List<Person> Persons { get { return _persons; } }

        public Simulation(float width, float height, int amount)
        {
            _width = width;
            _height = height;
            _persons = GeneratePersons(amount);
        }

        public void SimulateTimeLapse(TimeSpan time)
        {
            MovePersons(time);
            CollisionDetection();
            AddNewPersons();
        }

        private void AddNewPersons()
        {
            int amount = _persons.Count;

            double propability = -0.075 * amount + 1.75;

            double rand = random.NextDouble();

            if (rand < propability)
            {
                _persons.Add(GeneratePersonOnEdge());
            }
        }

        private Person GeneratePersonOnEdge()
        {
            /*
             * draw position for x. first determine if  it will be edge of the x axis of between ends. If center, get random position and for y draw edge
             * If x is an edge, generate value for y axis
             * Vectors must be directed to the inside of the area
            */
            const double precision = 0.1;

            double minPosition = precision;
            double maxXPosition = _width - precision;
            double maxYPosition = _height - precision;

            Position2D position = null;
            int startAngle = 0;
            Vector2D vector;
            Person result;

            /*
             * 0 - left
             * 1 - middle
             * 2 - right
            */
            int choose = random.Next(0, 3);
            switch (choose)
            {
                case 0:
                    {
                        double yPosition = random.NextDouble() * maxYPosition;
                        startAngle = -89;
                        position = new Position2D((float)minPosition, (float)yPosition);
                        break;
                    }
                case 1:
                    {
                        double xPosition = random.NextDouble() * maxXPosition;

                        int rand = random.Next(0, 2);
                        if (rand == 0)
                        {
                            startAngle = 181;
                            position = new Position2D((float)xPosition, (float)minPosition);
                        }
                        else
                        {
                            startAngle = 1;
                            position = new Position2D((float)xPosition, (float)maxYPosition);
                        }

                        break;
                    }
                case 2:
                    {
                        double yPosition = random.NextDouble() * maxYPosition;
                        startAngle = 91;
                        position = new Position2D((float)maxXPosition, (float)yPosition);
                        break; 
                    }
            }

            int angle = random.Next(startAngle, 189);
            vector = GenerateVector(angle, RandomVelocity());

            result = new Person(vector, position);
            result.SicknessState = RandomSicknessState(result);
            return result;
        }

        private void MovePersons(TimeSpan time)
        {
            foreach (Person person in _persons)
            {
                person.Move(time);
            }

        }

        private void CollisionDetection()
        {
            List<Person> temp = new List<Person>();
            foreach (Person person in _persons)
            {
                List<Person> personsTemp = new List<Person>(_persons);
                personsTemp.Remove(person);
                foreach (Person person2 in personsTemp)
                {
                    CollisionWithAnotherPerson(person, person2);
                }
                if (CollisionWithWall(person))
                {
                    temp.Add(person);
                }
            }

            foreach (Person person in temp)
            {
                _persons.Remove(person);
            }
        }

        private bool CollisionWithWall(Person person)
        {
            float[] coords = person.Position.Coords;

            if (coords[0] <= 0 || coords[0] >= _width)
            {
                double value = random.NextDouble();
                if (value < 0.5)
                {
                    var components = person.Vector.GetComponents();
                    components[0] *= -1;
                    person.Vector = new Vector2D(components[0], components[1]);
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else if (coords[1] <= 0 || coords[1] >= _height)
            {
                double value = random.NextDouble();
                if (value < 0.5)
                {
                    var components = person.Vector.GetComponents();
                    components[1] *= -1;
                    person.Vector = new Vector2D(components[0], components[1]);
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        private void CollisionWithAnotherPerson(Person person1, Person person2)
        {
            double distance = person1.Position.Distance(person2.Position);
            if (distance <= _EllipseDiameter)
            {
                float[] components = new float[2];
                float[] position1 = person1.Position.Coords;
                float[] position2 = person2.Position.Coords;
                components[0] = (position2[0] - position1[0]);
                components[1] = (position2[1] - position1[1]);

                Vector2D vector = new Vector2D(components[0], components[1]);
                person1.Vector = vector.Scale(-1.0 / distance).Scale(RandomVelocity());
            }
        }

        private Vector2D GenerateRandomVector()
        {
            double velocity = RandomVelocity();
            double deg = random.Next(0, 360);

            return GenerateVector(deg, velocity);
        }

        private Vector2D GenerateVector(double degree, double velocity)
        {
            double rad = degree * (Math.PI / 180.0);

            double vectorX = Math.Cos(rad) * velocity;
            double vectorY = Math.Sin(rad) * velocity;

            return new Vector2D(vectorX, vectorY);
        }

        private double RandomVelocity()
        {
            return (double)random.Next(1, 25) / 10;
        }

        private Position2D RandomPosition()
        {

            float x = random.Next(0, (int)(_width * 100)) / 100;
            float y = random.Next(0, (int)(_height * 100)) / 100;

            return new Position2D(x, y);
        }

        private List<Person> GeneratePersons(int amount)
        {
            List<Person> result = new List<Person>();

            for (int i = 0; i < amount; i++)
            {
                result.Add(CreatePerson(GenerateRandomVector(), RandomPosition()));
            }

            return result;
        }

        private ISicknessState RandomSicknessState(Person person)
        {
            List<Type> sicknessStates = new List<Type>
            {
                typeof(HealthyImmune),
                typeof(HealthyUnimmune),
                typeof(InfectedWithoutSymptoms),
                typeof(InfectedWithSymptoms)
            };

            // Lista konstruktorów dla każdej klasy
            List<Func<ISicknessState>> constructors = new List<Func<ISicknessState>>
            {
                () => new HealthyImmune(person),
                () => new HealthyUnimmune(person),
                () => new InfectedWithoutSymptoms(person),
                () => new InfectedWithSymptoms(person)
            };

            // Lista prawdopodobieństw dla poszczególnych stanów
            List<double> probabilities = new List<double>
            {
                0.4,
                0.3,
                0.1,
                0.2
            };

            double randomNumber = random.NextDouble();

            // Sumowanie prawdopodobieństw, aby określić, który stan zwrócić
            double cumulativeProbability = 0.0;
            for (int i = 0; i < sicknessStates.Count; i++)
            {
                cumulativeProbability += probabilities[i];
                if (randomNumber < cumulativeProbability)
                {
                    // Tworzenie obiektu danego stanu za pomocą odpowiedniego konstruktora
                    return constructors[i]();
                }
            }

            return new HealthyImmune(person);
        }

        private Person CreatePerson(Vector2D vector, Position2D position)
        {
            Person person = new Person(vector, position);

            person.SicknessState = RandomSicknessState(person);

            return person;
        }
    }
}
