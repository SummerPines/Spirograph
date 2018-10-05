using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



//Citation: Parametric equation of a circle https://www.mathopenref.com/coordparamcircle.html
namespace Spirograph {

    /// <summary>
    /// Circle Class
    /// Contains all points that the arcs will be drawn to
    /// M. Moody 10/2018
    /// </summary>
    class Circle {

        private Point[] _points;

        //The array of points needed to create th spirograph
        public Point[] Points {
            get { return _points; }
            private set { _points = value; }
        }

        //These 2 are not needed - take them out later
        public Point Start {
            get { return Points[0]; }
        }

        public Point Next {
            get { return Points[2]; }
        }

        /// <summary>
        /// Constructor for Circle
        /// </summary>
        /// <param name="numPoints">number of points to plot</param>
        /// <param name="radius">radis of the circle</param>
        public Circle(int numPoints, int radius) {

            Points = new Point[numPoints];

            //2PI radians in a full circle
            double radiansBetweenPoints = (2*Math.PI) / numPoints;

            for (int i = 0; i < numPoints; i++) {

                //Add point on circle to the array
                int x = 3*radius + (int)Math.Round(radius * Math.Cos(radiansBetweenPoints * i));
                int y = 50+radius + (int)Math.Round(radius * Math.Sin(radiansBetweenPoints * i));
                Point point = new Point(x, y);
                Points[i] = point;
            }



        }
    }
}
