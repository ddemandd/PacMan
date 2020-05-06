using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    class Food : Cell
    {
        protected int _price;

        public Food(Coord coord)
        {
            _coord = coord;
            _color = ConsoleColor.White;
            _viewCell = (char)ViewCell.Food;
        }
    }
}
