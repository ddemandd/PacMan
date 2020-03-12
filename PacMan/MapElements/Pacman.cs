using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    class Pacman : Cell
    {
        ConsoleColor _pacmanColor;
        Direction _direction;
        int _lifes;
        int _score;
        int _speed;

        public Pacman(GameField owner, Coord coord)
        {
            _pacmanColor = ConsoleColor.Yellow;
            _direction = Direction.Left;
            _lifes = DefaultSettings.PACMAN_LIFES;
            _owner = owner;
            _coord = coord;
            _score = 0;
            _speed = 1;
        }

        public void Move()
        { 
        
        }
    }
}
