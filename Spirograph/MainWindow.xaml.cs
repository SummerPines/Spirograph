﻿using System;
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
            if (!int.TryParse(txtSmallRadius.Text, out int smallRad))
                MessageBox.Show("Please enter a radius for the small circle");
            else if (!int.TryParse(txtLargeRadius.Text, out int largeRad))
                MessageBox.Show("Please enter a radius for the large circle");
            else
                drawSpirograph(smallRad, largeRad);
        }

        /// <summary>
        /// drawSpirograph
        /// Creates a circle object and uses the point in the object to draw the spirograph
        /// Uses StreamGeometry class to draw in WPF
        /// </summary>
        /// <param name="smallRad">the radius of the smaller circle</param>
        /// <param name="radius">the radius of the outer circle</param>
        private void drawSpirograph(int smallRad, int largeRad) {
            Circle smallCircle = new Circle(smallRad);
            Circle largeCircle = new Circle(largeRad);
            Spiro spiro = new Spiro(smallCircle, largeCircle);

            Path path = spiro.getPath();

            //Draw the arcs to the stack panel
            stkMain.Children.Clear();
            stkMain.Children.Add(path);           
        }
    }
}
