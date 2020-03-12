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

    }
}
