using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;




namespace Spirograph {

    /// <summary>
    /// Circle Class
    /// Holds values need to compute the arcs
    /// M. Moody 10/2018
    /// </summary>
    class Circle {

        private int _radius;
        private double _circumference;


        public int Radius {
            get { return _radius; }
            private set { _radius = value; }
        }

        public double Circumference {
            get { return _circumference; }
            set { _circumference = value; }
        }

        /// <summary>
        /// Constructor for Circle
        /// </summary>
        /// <param name="radius">radius of the circle</param>
        public Circle(int radius) {
            Radius = radius;
            Circumference = 2 * Math.PI * radius;
        }
    }
}
