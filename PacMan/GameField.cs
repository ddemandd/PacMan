﻿using PacMan.Delegates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PacMan
{
    class GameField
    {
        #region Fields

        private Cell[,] _map;
        private Enemy[] _enemys;
        private Pacman _pacman;
        private int _score;
        private int _countFood;

        private UICellDelegate _printCell;
        private UICellDelegate _hideCell;
        private UIGameFieldDelegate _printGameField;
        private UIScoreDelegate _printScore;

        #endregion

        #region Events

        public event UICellDelegate ShowCell
        {
            add 
            {
                _printCell += value;
            }
            remove 
            {
                _printCell -= value;
            }
        }
        
        public event UICellDelegate HideCell
        {
            add
            {
                _hideCell += value;
            }
            remove
            {
                _hideCell -= value;
            }
        }

        public event UIGameFieldDelegate ShowGameField
        {
            add
            {
                _printGameField += value;
            }
            remove
            {
                _printGameField -= value;
            }
        }

        public event UIScoreDelegate ShowScore
        {
            add
            {
                _printScore += value;
            }
            remove
            {
                _printScore -= value;
            }
        }


        #endregion

        #region CTOR

        public GameField()
        {
            _map = new Cell[DefaultSettings.MAP_HEIGHT, DefaultSettings.MAP_WIDTH];
            _enemys = new Enemy[DefaultSettings.ENEMY_COUNT];
            _pacman = new Pacman(this, new Coord());
            _score = 0;
            _countFood = 0;
        }

        #endregion

        #region Properties

        public Cell[,] Map
        {
            get { return (Cell[,])_map.Clone(); }
            private set { _map = value; }
        }

        public Pacman Pacman
        {
            get { return _pacman; }
        }

        #endregion

        #region Initiolyze

        private void InitiolyzeEnemys()
        {
            for (int i = 0; i < _enemys.Length; i++)
            {
                _enemys[i] = new Enemy(ConsoleColor.Red, new Coord(DefaultSettings.ENEMY_START_POS_X, DefaultSettings.ENEMY_START_POS_Y - i), this);
            }
        }

        private void InitiolyzePacman()
        {
            _pacman = new Pacman(this, new Coord(DefaultSettings.PACMAN_START_POS_X, DefaultSettings.PACMAN_START_POS_Y));
        }

        public void InitiolyzeFoodWall()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == _map.GetLength(0) - 1 || j == _map.GetLength(1) - 1)
                    {
                        _map[i, j] = new Wall(new Coord(j, i));
                    }
                    if (i % 2 == 0 && j % 5 != 1)
                    {
                        _map[i, j] = new Wall(new Coord(j, i));
                    }
                    else if(i != 0 && i != _map.GetLength(0) - 1 && j != 0 && j != _map.GetLength(1) - 1 && i % 2 == 1 && j % 5 != 1)
                    {
                        _map[i, j] = new Food(new Coord(j, i));
                        _countFood++;
                    }
                }
            }
        }

        #endregion

        public bool CanMoving(Coord currentPosition, Direction someDirection)
        {
            bool result = false;

            Coord nextPosition = GetNextCoord(currentPosition, someDirection);

            if (nextPosition.Y < DefaultSettings.MAP_HEIGHT - 1 && nextPosition.X < DefaultSettings.MAP_WIDTH - 1 && nextPosition.X > 0 && nextPosition.Y > 0)
            {
                if (!(_map[nextPosition.Y, nextPosition.X] is Wall))
                {
                    result = true;
                }
            }

            return result;
        }

        public Coord GetNextCoord(Coord currentPos, Direction direct)
        {
            Coord point = new Coord(currentPos.X, currentPos.Y);

            switch (direct)
            {
                case Direction.Up:
                    {
                        point.Y--;
                        break;
                    }
                case Direction.Down:
                    {
                        point.Y++;
                        break;
                    }
                case Direction.Left:
                    {
                        point.X--;
                        break;
                    }
                case Direction.Right:
                    {
                        point.X++;
                        break;
                    }
                default:
                    break;
            }

            return point;
        }

        private bool IsCellNullOrEmpty(Coord point)
        {
            return (_map[point.Y, point.X] == null);
        }

        public Direction[] GetFreeDirection(Coord position)
        {
            Direction[] directions = new Direction[3];

           

            return directions;
        }

        private bool IsCellCherry(Coord point)
        {
            return (_map[point.Y, point.X] is Cherry);
        }

        private bool IsCellFood(Coord point)
        {
            return (_map[point.Y, point.X] is Food);
        }

        private void ClearCell(Coord point)
        {
            _map[point.Y, point.X] = null;
        }

        public void TryEatFood(Coord point)
        {
            if (!IsCellNullOrEmpty(point))
            {
                if (IsCellCherry(point))
                {
                    _pacman.Angry = true;
                    _pacman.Speed = DefaultSettings.BONUS_SPEED;

                    for (int i = 0; i < _enemys.Length; i++)
                    {
                        _enemys[i].Angry = false;
                        _enemys[i].Speed = DefaultSettings.PENALTY_SPEED;
                    }
                }
                else if (IsCellFood(point))
                {
                    _score += DefaultSettings.PRICE_OF_FOOD;
                    _countFood--;
                    _printScore(_score);
                    }

                ClearCell(point);
            }
        }

        public bool CheckEnemys(Coord point)
        {
            bool result = false;

            for (int i = 0; i < _enemys.Length; i++)
            {
                if (_enemys[i].Coord.X == point.X && _enemys[i].Coord.Y == point.Y)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public bool CheckPacman(Coord point)
        {
            bool result = false;

            if (_pacman.Coord.X == point.X && _pacman.Coord.Y == point.Y)
            {
                result = true;
            }

            return result;
        }

        public void PutPacmanToStartPosition()
        {
            _pacman.Coord = new Coord(DefaultSettings.PACMAN_START_POS_X, DefaultSettings.PACMAN_START_POS_Y);
            _pacman.Lifes--;
        }

        public void PutEnemyToStartPosition(Coord enemyPosition = null)
        {
            for (int i = 0; i < _enemys.Length; i++)
            {
                if (enemyPosition == null)
                {
                    _enemys[i].Coord = new Coord(DefaultSettings.ENEMY_START_POS_X, DefaultSettings.ENEMY_START_POS_Y - i);
                }
                else
                {
                    if (_enemys[i].Coord == enemyPosition)
                    {
                        _enemys[i].Coord = new Coord(DefaultSettings.ENEMY_START_POS_X, DefaultSettings.ENEMY_START_POS_Y);
                    }
                }
            }
        }

        public bool CanPlay()
        {
            bool result = false;

            if (_pacman.Lifes > 0)
            {
                if (_countFood > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool IsPacmanAngry()
        {
            bool result = false;

            if (_pacman.Angry)
            {
                result = true;
            }

            return result;
        }

        public void Run()
        {
            InitiolyzeFoodWall();
            InitiolyzePacman();
            InitiolyzeEnemys();

            _printGameField(_map);
            _printCell(_pacman);
            _printScore(_score);

            for (int i = 0; i < _enemys.Length; i++)
            {
                _printCell(_enemys[i]);
            }
             
            do
            {
                Thread.Sleep(300);

                _hideCell(_pacman);

                for (int i = 0; i < _enemys.Length; i++)
                {

                    if (_map[_enemys[i].Coord.Y, _enemys[i].Coord.X] == null)
                    {
                        _hideCell(_enemys[i]);
                    }
                    else
                    {
                        _printCell(_map[_enemys[i].Coord.Y, _enemys[i].Coord.X]);
                    }
                }

                for (int i = 0; i < _enemys.Length; i++)
                {
                    _enemys[i].Move();
                }

                _pacman.CheckChangeDirection(); //получаем направление
                _pacman.Move();

                _printCell(_pacman);

                for (int i = 0; i < _enemys.Length; i++)
                {
                    _printCell(_enemys[i]);
                }

            } while (CanPlay());

            if (_countFood == 0) 
            {
                Console.WriteLine("win");
            }
            else
            {
                Console.WriteLine("gg");
            }
        }
    }
}

