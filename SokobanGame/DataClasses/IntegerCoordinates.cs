using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanGame
{
    public class IntegerCoordinates 
    {
        public int X { get; set; }
        public int Y { get; set; }

        public IntegerCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IntegerCoordinates(IntegerCoordinates coordinates)
        {
            if (coordinates == null)
                throw new ArgumentNullException("null coordinates", nameof(coordinates));

            X = coordinates.X;
            Y = coordinates.Y;
        }
    }
}
