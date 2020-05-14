using System;

using BLLayer.Enums;

namespace BLLayer.MapElements
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
