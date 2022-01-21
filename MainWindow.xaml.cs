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

        public double vx = 0;
        public double vy = 0;
        public int gField = 0;
        public bool isChild = false;
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
            double yAccel = - Math.Sin(angle * (Math.PI / 180)) * g; //essential to add negative sign since the coordinate system goes increases from top to bottom

            results.Add(xAccel);
            results.Add(yAccel);

            return results;
        }

        Random r = new Random();
        List<gravitationalBody> gravitationalBodies = new List<gravitationalBody>();
        public MainWindow()
        {
            InitializeComponent();

            gravitationalBody parentBody = new gravitationalBody();
            gravitationalBody childBody = new gravitationalBody();
            gravitationalBody childBody2 = new gravitationalBody();

            parentBody.UIShape.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"C:/Users/thejo/Downloads/earth2.jpg", UriKind.Absolute))
            };

            childBody.UIShape.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"C:/Users/thejo/Downloads/moon2.jpg", UriKind.Absolute))
            };

            gravitationalBodies.Add(parentBody);
            gravitationalBodies.Add(childBody);
            gravitationalBodies.Add(childBody2);

            childBody.vx = -30;
            childBody.vy = 0;

            childBody2.vx = 10;
            childBody2.vy = -5;

            childBody.UIShape.Width = 25;
            childBody.UIShape.Height = 25;
            childBody2.UIShape.Width = 10;
            childBody2.UIShape.Height = 10;

            childBody.isChild = true;
            childBody2.isChild = true;

            //parentBody.UIShape.MouseEnter += mouseOver;
            //parentBody.UIShape.MouseLeave += mouseLeave;

            //childBody.UIShape.MouseEnter += mouseOver;
            //childBody.UIShape.MouseLeave += mouseLeave;

            Canvas.SetLeft(parentBody.UIShape, 350);
            Canvas.SetTop(parentBody.UIShape, 350);

            Canvas.SetLeft(childBody.UIShape, 500);
            Canvas.SetTop(childBody.UIShape, 500);

            Canvas.SetLeft(childBody2.UIShape, 200);
            Canvas.SetTop(childBody2.UIShape, 200);



            MyCanvas.Children.Add(parentBody.UIShape);
            MyCanvas.Children.Add(childBody.UIShape);
            MyCanvas.Children.Add(childBody2.UIShape);
            


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
                    foreach (gravitationalBody body in gravitationalBodies)
                    {
                        body.UIShape.Refresh();
                        if (body.isChild)
                        {
                            double bodyx = Canvas.GetLeft(body.UIShape);
                            double bodyy = Canvas.GetTop(body.UIShape);

                            double xDistance = 400 - bodyx;
                            double yDistance = 400 - bodyy;

                            if (xDistance < 0)
                            {
                                xDistance = -xDistance;
                            }
                            if (yDistance < 0)
                            {
                                yDistance = -yDistance;
                            }

                            double distanceBetweenBodies = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
                            distanceBetweenBodies /= 150;

                            double g = 1 / (Math.Pow(distanceBetweenBodies, 2));
                            g *= 9.81;
                            //MessageBox.Show(g.ToString());
                            
                            List<double> accelVals = new List<double>();
                            accelVals = resolveVectors(body.vx, body.vy, bodyx, bodyy, 400, 400, 2);

                            //MessageBox.Show(bodyx + ", " + bodyy + " with vx: " + body.vx + " and vy: " + body.vy + " will accelerate " + accelVals[0] + " in x and " + accelVals[1] + " in y. Distance: " + distanceBetweenBodies + " making a force of " + g);
                            
                            body.vx += accelVals[0];
                            body.vy += accelVals[1];
                            
                            
                            bodyx += body.vx;
                            bodyy += body.vy;
                            

                            Canvas.SetLeft(body.UIShape, bodyx);
                            Canvas.SetTop(body.UIShape, bodyy);
                            
                        }
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