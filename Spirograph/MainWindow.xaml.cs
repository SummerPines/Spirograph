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


//Citation: StreamGometry use from   https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/how-to-create-a-shape-using-a-streamgeometry
//Citation: Use of slider from https://www.wpf-tutorial.com/misc-controls/the-slider-control/

namespace Spirograph {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// M. Moody 10/2018
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        /// <summary>
        /// button handler for main button to create spirograph
        /// Check for input errors
        /// </summary>
        private void btnMake_Click(object sender, RoutedEventArgs e) {
            int largeRad = (int)sldLargeRad.Value;
            int smallRad = (int)sldSmallRad.Value;
            int rotations = (int)sldRoataions.Value;

            SolidColorBrush color = getColor();
            
            drawSpirograph(smallRad, largeRad, rotations, color);
        }

        /// <summary>
        /// drawSpirograph
        /// Creates a circle object and uses the point in the object to draw the spirograph
        /// Uses StreamGeometry class to draw in WPF
        /// </summary>
        /// <param name="smallRad">the radius of the smaller circle</param>
        /// <param name="radius">the radius of the outer circle</param>
        private void drawSpirograph(int smallRad, int largeRad, int rotations, SolidColorBrush color) {
            Circle smallCircle = new Circle(smallRad);
            Circle largeCircle = new Circle(largeRad);
            Spiro spiro = new Spiro(smallCircle, largeCircle, rotations);

            Path path = spiro.getPath(color);

            //Draw the arcs to the stack panel
            stkMain.Children.Clear();
            stkMain.Children.Add(path);           
        }

        //Read the textboxes to determine brush color
        private SolidColorBrush getColor() {
            SolidColorBrush color;

            if (rbBlue.IsChecked == true)
                color = Brushes.CornflowerBlue;
            else if (rbCoral.IsChecked == true)
                color = Brushes.Salmon;
            else if (rbGreen.IsChecked == true)
                color = Brushes.LimeGreen;
            else if (rbLavender.IsChecked == true)
                color = Brushes.Violet;
            else if (rbLtBlue.IsChecked == true)
                color = Brushes.LightBlue;
            else if (rbOrange.IsChecked == true)
                color = Brushes.Orange;
            else
                color = Brushes.Black;

            return color;
        }
    }
}
