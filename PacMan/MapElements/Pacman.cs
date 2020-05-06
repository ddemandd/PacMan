using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    class Pacman : Cell
    {
        private Direction _nextDirection;
        private Direction _direction;
        private int _lifes;
        private int _speed;

        public Pacman(GameField owner, Coord coord)
        {
            _color = ConsoleColor.Yellow;
            _direction = Direction.Left;
            _nextDirection = Direction.Default;
            _lifes = DefaultSettings.PACMAN_LIFES;
            _owner = owner;
            _coord = coord;
            _viewCell = (char)ViewCell.Pacman;
            _speed = DefaultSettings.SPEED;
        }

        public bool Angry { get; set; } = false;

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int Lifes
        {
            get { return _lifes; }
            set { _lifes = value; }
        }

        public Coord Coord
        {
            get { return _coord; }
            set { _coord = value; }
        }
        private void MoveNextPoint(Direction reverseDirection)
        {
            if (_nextDirection != Direction.Default)
            {
                if (_owner.CanMoving(_coord, _nextDirection))
                {
                    _direction = _nextDirection;
                    _nextDirection = Direction.Default;
                    _coord = _owner.GetNextCoord(_coord, _direction);
                    return;
                }
            }

            if (_owner.CanMoving(_coord, _direction))
            {
                _coord = _owner.GetNextCoord(_coord, _direction);
            }
            else
            {
                _direction = reverseDirection;
                _coord = _owner.GetNextCoord(_coord, _direction);
            }
        }

        public void Move()
        {
            switch (_direction)
            {
                case Direction.Up:
                    {
                        MoveNextPoint(Direction.Down);

                        break;
                    }
                case Direction.Down:
                    {
                        MoveNextPoint(Direction.Up);

                        break;
                    }
                case Direction.Left:
                    {
                        MoveNextPoint(Direction.Right);

                        break;
                    }
                case Direction.Right:
                    {
                        MoveNextPoint(Direction.Left);

                        break;
                    }
                default:
                    break;
            }

            if (_owner.CheckEnemys(_coord))
            {
                if (Angry)
                {
                    _owner.PutEnemyToStartPosition(_coord);
                }
                else
                {
                    _owner.PutEnemyToStartPosition();
                    _coord = new Coord(DefaultSettings.PACMAN_START_POS_X, DefaultSettings.PACMAN_START_POS_Y);
                    _lifes--;
                }
            }
            _owner.TryEatFood(_coord);
        }

        

        public void CheckChangeDirection()
        {
            Direction tmp = Direction.Default;

            if (Console.KeyAvailable) //проверка нажата ли кнопка пользователем
            {
                switch (Console.ReadKey().Key) //проверяем нажатаю кнопку на соответствие с имеющимся направлением и выбираем новое направление
                {
                    case ConsoleKey.LeftArrow:
                        {
                            tmp = Direction.Left;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            tmp = Direction.Up;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            tmp = Direction.Right;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            tmp = Direction.Down;
                            break;
                        }
                    default:
                        break;
                }
            }

            if (tmp != Direction.Default)
            {
                if (_owner.CanMoving(_coord, tmp))
                {
                    _direction = tmp;
                }
                else
                {
                    _nextDirection = tmp;
                }
            }
        }
    }
}
