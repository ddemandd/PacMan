using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    class Enemy : Cell
    {
        ConsoleColor _enemyColor;
        Direction _direction;
        int _speed;

        public Enemy(ConsoleColor color, Coord coord, GameField owner)
        {
            _direction = Direction.Up;
            _enemyColor = color;
            _coord = coord;
            _owner = owner;
            _speed = 1;
        }

        public void Move()
        {

        }
    }
}
