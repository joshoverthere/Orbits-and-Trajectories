using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;
using System.Timers;
using System.IO;

namespace Orbits_and_Trajectories
{
    public class gravitationalBody
    {


        public Ellipse UIShape = new Ellipse
        {
            Width = 100,
            Height = 100,
            Fill = Brushes.White,
        };

        int x = 0;
        int y = 0;
        int vx = 0;
        int vy = 0;
        int gField = 0;
        bool child = false;
    }


    public static class ExtensionMethods
    {
        private static readonly Action EmptyDelegate = delegate { };
        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, EmptyDelegate);
        }
    }

    public partial class MainWindow : Window
    {

        private List<double> resolveVectors(double vx, double vy, double x1, double y1, double x2, double y2, double g)
        {
            List<double> results = new List<double>();

            double gradient = -1 * (y2 - y1) / (x2 - x1);

            double angle = Math.Atan(gradient) * (180 / Math.PI);
            if (x1> x2)
            {
                angle += 180;
            }

            double xAccel = Math.Cos(angle * (Math.PI / 180)) * g;
            double yAccel = Math.Sin(angle * (Math.PI / 180)) * g;

            MessageBox.Show(xAccel.ToString() + yAccel.ToString());

            return results;


            



            MessageBox.Show(angle.ToString());
        }

        Random r = new Random();
        public MainWindow()
        {
            InitializeComponent();

            gravitationalBody parentBody = new gravitationalBody();
            //resolveVectors(5, 5, 200, 200, 100, 100, 9.8);


            Canvas.SetLeft(parentBody.UIShape, 100);
            Canvas.SetTop(parentBody.UIShape, 100);


            MyCanvas.Children.Add(parentBody.UIShape);
            


            //set timer for canvas refresh
            System.Timers.Timer _timer = new System.Timers.Timer(50);
            _timer.Enabled = true;
            _timer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
        }

        public void OnElapsedEvent(object source, ElapsedEventArgs e)
        {
            updateGrid();
        }

        public void updateGrid()
        {
            try
            {
                Dispatcher.Invoke(new Action(() => {
                    foreach (UIElement body in MyCanvas.Children)
                    {
                        double bodyx = Canvas.GetLeft(body);
                        double bodyy = Canvas.GetTop(body); 

                    }
                }));
                
            }
            catch (Exception e){
                MessageBox.Show(e.ToString());
            }
        }

        private void mouseOver(object sender, MouseEventArgs e)
        {
            Ellipse activeEllipse = sender as Ellipse;

            activeEllipse.Fill = Brushes.Gray;
            activeEllipse.Stroke = Brushes.Gray;
            activeEllipse.Refresh();
        }

        private void mouseLeave(object sender, MouseEventArgs e)
        {
            Ellipse activeEllipse = sender as Ellipse;

            activeEllipse.Fill = Brushes.White;
            activeEllipse.Stroke = Brushes.White;
            activeEllipse.Refresh();
        }


    }
    
}