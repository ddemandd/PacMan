using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    class Coord
    {
        int _x;
        int _y;

        public Coord(int x = 0, int y = 0)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public override bool Equals(object obj)
        {
            return (Coord)obj == this;
        }

        public static bool operator ==(Coord firstCoord, Coord secondCoord)
        {
            bool result = false;

            if (ReferenceEquals(firstCoord, null) || ReferenceEquals(secondCoord, null))
            {
                result = ReferenceEquals(firstCoord, secondCoord);
            }
            else
            {
                result = (firstCoord.X == secondCoord.X && firstCoord.Y == secondCoord.Y);
            }

            return result;
        }

        public static bool operator !=(Coord firstCoord, Coord secondCoord)
        {
            return !(firstCoord == secondCoord);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(_x, _y).GetHashCode();
        }
    }
}
