using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanGame
{
    public class IntegerCoordinates /*: IComparable*/
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

        /*        public Location Sub(Location loc)
                {
                    if (loc == null) throw new ArgumentNullException("null location", nameof(loc));

                    var sub = new Location(X - loc.X, Y - loc.Y);
                    return sub;
                }

                public int CompareTo(object obj)
                {
                    if (obj == null) throw new ArgumentNullException("null location", nameof(obj));

                    var loc = (Location)obj;
                    return loc.X == X && loc.Y == Y ? 0 : -1;
                }*/
    }
}
