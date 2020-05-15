using System;
using System.Collections.Generic;
using System.Text;
using BLLayer.MapElements;

namespace BLLayer.Factory
{
    abstract class Creator
    {
        public abstract Cell FactoryMethod(Coord coord);
    }
}
