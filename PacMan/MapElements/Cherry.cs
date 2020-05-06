using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    class Cherry : Food
    {
        public Cherry(Coord coord)
            : base(coord)
        {
            _color = ConsoleColor.Green;
            _viewCell = (char)ViewCell.Cherry;
        }
    }
}
