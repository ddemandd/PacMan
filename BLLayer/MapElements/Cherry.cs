using System;

using BLLayer.Enums;

namespace BLLayer.MapElements
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
