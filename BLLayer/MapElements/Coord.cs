
namespace BLLayer
{
    public class Coord
    {
        public Coord(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

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
            return (X | (Y << 4));
        }
    }
}
