﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    class Enemy : Cell
    {
        private Direction _direction;
        private Direction _nextDirection;
        private int _speed;

        public Enemy(ConsoleColor color, Coord coord, GameField owner)
        {
            _direction = Direction.Up;
            _color = color;
            _viewCell = (char)ViewCell.Enemy;
            _coord = coord;
            _owner = owner;
            _speed = DefaultSettings.SPEED;
        }

        public bool Angry { get; set; } = true;

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Coord Coord
        {
            get { return _coord; }
            set { _coord = value; }
        }

        private Direction GetRandomDirection()
        {
            Random tmp = new Random();

            int numb = tmp.Next(0, 1000) % 4 + 1;

            return (Direction)numb;

        }

        public void Move()
        {
            _nextDirection = GetRandomDirection();

            switch (_direction)
            {
                case Direction.Up:
                    {
                        MovingEnemy(Direction.Down);
                        break;
                    }
                case Direction.Down:
                    {
                        MovingEnemy(Direction.Up);
                        break;
                    }
                case Direction.Left:
                    {
                        MovingEnemy(Direction.Right);
                        break;
                    }
                case Direction.Right:
                    {
                        MovingEnemy(Direction.Left);
                        break;
                    }
                default:
                    break;
            }

            if (_owner.CheckPacman(_coord))
            {
                if (_owner.IsPacmanAngry())
                {
                    _owner.PutEnemyToStartPosition();
                }
                else
                {
                    _owner.PutEnemyToStartPosition();
                    _owner.PutPacmanToStartPosition();
                }
            }


        }

        private void MovingEnemy(Direction reverse)
        {
            if (_owner.CanMoving(_coord, _nextDirection) && reverse != _nextDirection)
            {
                _direction = _nextDirection;
            }
            else
            {
                if (!_owner.CanMoving(_coord, _direction))
                {
                    _direction = reverse;
                }
            }

            _coord = _owner.GetNextCoord(_coord, _direction);
        }
    }
}
