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
        int x = 0;
        int y = 0;
        int vx = 0;
        int vy = 0;
        int gField = 0;
        Ellipse bodyShape = new Ellipse();

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

        Random r = new Random();
        public MainWindow()
        {
            InitializeComponent();

            Ellipse parentBody = new Ellipse();
            parentBody.Width = 100;
            parentBody.Height = 100;
            parentBody.Fill = Brushes.White;
            parentBody.MouseEnter += mouseOver;
            parentBody.MouseLeave += mouseLeave;

            Canvas.SetLeft(parentBody, 325);
            Canvas.SetTop(parentBody, 325);

            MyCanvas.Children.Add(parentBody);


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
            
            foreach (var activeEllipse in MyCanvas.Children.OfType<Ellipse>())
            {
                MessageBox.Show("hi");
                double x = Canvas.GetLeft(activeEllipse);
                double y = Canvas.GetTop(activeEllipse);

                Canvas.SetLeft(activeEllipse, (x + 5));
                Canvas.SetTop(activeEllipse, (y + 5));
                activeEllipse.Refresh();
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
