using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace symulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ellipse ellipse;
        private Simulation simulation;
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            simulation = new Simulation(30, 20, 10);
            StartSimulationLoop();
        }
        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            double move = Canvas.GetLeft(testEllipse) + 50;
            Canvas.SetLeft(testEllipse, 100);
            /*if (e.Key == Key.Space)
            {
                TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;

                // Jeśli transformacja to null, utwórz nową TranslateTransform
                if (translateTransform == null)
                {
                    translateTransform = new TranslateTransform();
                    ellipse.RenderTransform = translateTransform;
                }

                // Przesunięcie elipsy
                translateTransform.X += 10;
            }*/
        }

        private void Update(object sender, EventArgs e)
        {
            simulation.SimulateTimeLapse(TimeSpan.FromMilliseconds(100));
            RefreshView(simulation.Persons);          
        }

        private void RefreshView(List<Person> persons)
        {
            canva.Children.Clear();

            foreach (Person person in persons)
            {
                float multiplierX = 10;
                float multiplierY = 10;
                string hexColor = "FF00BB"; // Przykład koloru (możesz dostosować)

                Color ellipseColor = (Color)ColorConverter.ConvertFromString("#" + hexColor);

                Ellipse ellipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = new SolidColorBrush(person.Color)
                };

                // Oblicz pozycję elipsy na podstawie jej środka
                float[] position = person.Position.Coords;

                double centerX = (position[0] * multiplierX - ellipse.Width / 2);
                double centerY = (position[1] * multiplierY - ellipse.Height / 2);

                Canvas.SetLeft(ellipse, centerX);
                Canvas.SetTop(ellipse, centerY);

                canva.Children.Add(ellipse);
            }
        }

        private void StartSimulationLoop()
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(40);
            timer.Tick += Update;
            timer.Start();
        }
    }
    
}
