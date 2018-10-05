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
/* path of spirograph
 x(t)=(R+r)cos(t) + p*cos((R+r)t/r) 
y(t)=(R+r)sin(t) + p*sin((R+r)t/r) 
r= small radius
R = large Radius
p = position

x	=	(a+b)cosphi-bcos((a+b)/bphi)	
y	=	(a+b)sinphi-bsin((a+b)/bphi).
*/
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
            if (!int.TryParse(txtNumPoints.Text, out int numPoints))
                MessageBox.Show("Please enter a number of points");
            else if (!int.TryParse(txtRadius.Text, out int radius))
                MessageBox.Show("Please enter a radius");
            else
                drawSpirograph(numPoints, radius);
        }

        /// <summary>
        /// drawSpirograph
        /// Creates a circle object and uses the point in the object to draw the spirograph
        /// Uses StreamGeometry class to draw in WPF
        /// </summary>
        /// <param name="numPoints"></param>
        /// <param name="radius"></param>
        private void drawSpirograph(int numPoints, int radius) {
            Circle newCircle = new Circle(numPoints, radius);

            Path path = new Path();
            path.Stroke = Brushes.Red;
            path.StrokeThickness = 2;

            StreamGeometry geo = new StreamGeometry();
            using (StreamGeometryContext ctx= geo.Open()) {

                //Set start point of StreamGeometry path
                ctx.BeginFigure(newCircle.Start, true, false);
 
                //Draw arcs connecting all points in the circle object
                for (int i = 1; i < numPoints; i++) 
                    ctx.ArcTo(newCircle.Points[i], new Size(10, 10), 0, false, SweepDirection.Clockwise, true, true);

                ctx.ArcTo(newCircle.Start, new Size(10, 10), 0, false, SweepDirection.Clockwise, true, true);
            }

            //recommended for speed
            geo.Freeze();

            path.Data = geo;

            //Draw the arcs to the stack panel
            StackPanel stkMain = new StackPanel();
            stkMain.Children.Add(path);

            gridSpiro.Children.Add(stkMain);
            
        }


    }
}
