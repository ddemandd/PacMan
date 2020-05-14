using System;

using BLLayer.Enums;

namespace BLLayer.MapElements
{
    class Food : Cell
    {
        public Food(Coord coord)
        {
            _coord = coord;
            _color = ConsoleColor.White;
            _viewCell = (char)ViewCell.Food;
        }
    }
}
