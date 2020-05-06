using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    abstract class Cell
    {
        protected Coord _coord;
        protected GameField _owner;
        protected char _viewCell;
        protected ConsoleColor _color;

        public char View
        {
            get { return _viewCell; }
        }

        public Coord Coord
        {
            get { return _coord; }
        }

        public ConsoleColor Color
        {
            get { return _color; }
        }

    }
}
