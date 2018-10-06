using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Spirograph {

    /// <summary>
    /// Spiro Class
    /// Contains needed items to make the spirograph
    /// Citation: Parametric equation of a circle https://www.mathopenref.com/coordparamcircle.html
    /// M. Moody 10/18
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

        /***************************************************************************************
         * Constructor for Spiro
         * 1. Creates a Spiro instance
         * 2. Creates the small and large circles
         * 3. Calculates all points where small circle will touch large circle
         * 4. Creates a list with those points
         * 5. Draws arc between the points
         * *************************************************************************************/
        public Spiro(Circle small, Circle large, int rotations) {

            SmallCircle = small;
            LargeCircle = large;

            PointsList = new List<Point>();


            //The arc will be drawn between points on the perimeter of the larger circle.
            //The length between each point is the circumference of the smaller circle.
            //The angle between points radians is: (circumference of small circle/ circumference of large circle) * 2 * pi
            //2pi radians in a full circle
            double radiansBetweenPoints = (2 * Math.PI) * (SmallCircle.Circumference / LargeCircle.Circumference);

            //I want to go around the large circle a selected # of times. The number of points needed to do this is
            //circumference of large circle / circumference of small circle * rotations
            int numPoints = (int)Math.Round(LargeCircle.Circumference / SmallCircle.Circumference) * rotations;

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

        //Create the Path object to draw the spirograph
        internal Path getPath(SolidColorBrush color) {
            Path path = new Path();
            path.Stroke = color;
            path.StrokeThickness = 2;

            StreamGeometry geo = new StreamGeometry();
            using (StreamGeometryContext streamGeoCtx = geo.Open()) {

                //Set start point of StreamGeometry path
                streamGeoCtx.BeginFigure(PointsList[0], true, false);

                //Draw arcs connecting all points in the circle object
                for (int i = 1; i < PointsList.Count; i++)
                    streamGeoCtx.ArcTo(PointsList[i], new Size(5, 5), 0, false, SweepDirection.Clockwise, true, true);
            }

            //recommended for speed
            geo.Freeze();

            path.Data = geo;

            return path;
        } 
    }
}
