﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    class Wall : Cell
    {
        public Wall(Coord coord)
        {
            _color = ConsoleColor.Blue;
            _coord = coord;
            _viewCell = (char)ViewCell.Wall;
        }
    }
}
