using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Spirograph {

    /// <summary>
    /// Spirograph Class
    /// Contains needed items to make the spirograph
    /// Citation: Parametric equation of a circle https://www.mathopenref.com/coordparamcircle.html
    /// </summary>
    class Spiro {

        private Circle _largeCircle;    
        private Circle _smallCircle;
        private List<Point> _pointsList;

        public Circle LargeCircle {
            get { return _largeCircle; }
            set { _largeCircle = value; }
        }

        public Circle SmallCircle {
            get { return _smallCircle; }
            set { _smallCircle = value; }
        }

        public List<Point> PointsList {
            get { return _pointsList; }
            set { _pointsList = value; }
        }

        public Spiro(Circle small, Circle large) {

            SmallCircle = small;
            LargeCircle = large;

            PointsList = new List<Point>();


            //The arc will be drawn between points on the perimeter of the larger circle.
            //The length between each point is the circumference of the smaller circle.
            //The angle between points radians is: (circumference of small circle/ circumference of large circle) * 2 * pi
            //2pi radians in a full circle
            double radiansBetweenPoints = (2 * Math.PI) * (SmallCircle.Circumference / LargeCircle.Circumference);

            //I want to go around the large circle 10 times. The number of points needed to do this is
            //circumference of large circle / circumference of small circle * 10
            int numPoints = (int)Math.Round(LargeCircle.Circumference / SmallCircle.Circumference) * 10;

            //Add points on circle to the array
            //The formula for the x and y coordinated can be found using the parametric equation
            //x = radius * cos(angle)
            //y = radius * sin(angle)
            int xOffset = LargeCircle.Radius + 180;
            int yOffset = LargeCircle.Radius + 100;
            for (int i = 0; i < numPoints; i++) {
                int x = xOffset + (int)Math.Round(LargeCircle.Radius * Math.Cos(radiansBetweenPoints * i));
                int y = yOffset + (int)Math.Round(LargeCircle.Radius * Math.Sin(radiansBetweenPoints * i));
                Point point = new Point(x, y);
                PointsList.Add(point);
            }
        }

        internal Path getPath() {
            Path path = new Path();
            path.Stroke = Brushes.Red;
            path.StrokeThickness = 2;

            StreamGeometry geo = new StreamGeometry();
            using (StreamGeometryContext streamGeoCtx = geo.Open()) {

                //Set start point of StreamGeometry path
                streamGeoCtx.BeginFigure(PointsList[0], true, false);

                //Draw arcs connecting all points in the circle object
                for (int i = 1; i < PointsList.Count; i++)
                    streamGeoCtx.ArcTo(PointsList[i], new Size(10, 10), 0, false, SweepDirection.Clockwise, true, true);
            }

            //recommended for speed
            geo.Freeze();

            path.Data = geo;

            return path;
        } 
    }
}
