using BLLayer.MapElements;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLLayer.Factory
{
    class FoodCreator : Creator
    {
        public override Cell FactoryMethod(Coord coord)
        {
            return new Food(coord);
        }
    }
}
