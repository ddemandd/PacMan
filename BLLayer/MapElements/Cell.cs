using System;

using BLLayer.Interfaces;

namespace BLLayer.MapElements
{
    public abstract class Cell
    {
        protected Coord _coord;
        protected IOwner _owner;
        protected char _viewCell;
        protected ConsoleColor _color;

        public char View
        {
            get { return _viewCell; }
        }

        public Coord Coord
        {
            get { return _coord; }
            set { _coord = value; }
        }

        public ConsoleColor Color
        {
            get { return _color; }
        }
    }
}
