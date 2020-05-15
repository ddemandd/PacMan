
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using BLLayer.Interfaces;
using BLLayer.MapElements;
using BLLayer.Enums;
using BLLayer.Delegates;

namespace BLLayer
{
    public class GameField : IOwner
    {
        #region Singleton

        private static GameField _field;

        public static GameField GetInstance()
        {
            if (_field == null)
            {
                _field = new GameField();
            }

            return _field;
        }

        #endregion

        #region Fields

        private Dictionary<Coord, Cell> _map;
        private Enemy[] _enemys;
        private Pacman _pacman;
        private int _score;
        private int _countFood;

        private UIGetUserDirectionDelegate _userDirection;
        private UIIsKeyAvalibleDelegate _keyAvalible;
        private UIWinLoseDelegate _printWin;
        private UIWinLoseDelegate _printLose;
        private UICellDelegate _printCell;
        private UICellDelegate _hideCell;
        private UIGameFieldDelegate _printGameField;
        private UIScoreDelegate _printScore;

        #endregion

        #region Events


        public event UIGetUserDirectionDelegate UserDirection
        {
            add
            {
                _userDirection += value;
            }
            remove
            {
                _userDirection -= value;
            }
        }

        public event UIIsKeyAvalibleDelegate KeyAvalible
        {
            add
            {
                _keyAvalible += value;
            }
            remove
            {
                _keyAvalible -= value;
            }
        }

        public event UIWinLoseDelegate PrintLose
        {
            add
            {
                _printLose += value;
            }
            remove
            {
                _printLose -= value;
            }
        }

        public event UIWinLoseDelegate PrintWin
        {
            add
            {
                _printWin += value;
            }
            remove
            {
                _printWin -= value;
            }
        }

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

        private GameField()
        {
            _map = new Dictionary<Coord, Cell>(DefaultSettings.MAP_SIZE); //Cell[DefaultSettings.MAP_HEIGHT, DefaultSettings.MAP_WIDTH];
            _enemys = new Enemy[DefaultSettings.ENEMY_COUNT];
            _pacman = new Pacman(this, new Coord());
            _score = 0;
            _countFood = 0;
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

        private void InitiolyzeFoodWall()
        {
            for (int i = 0; i < DefaultSettings.MAP_HEIGHT; i++)
            {
                for (int j = 0; j < DefaultSettings.MAP_WIDTH; j++)
                {
                    Coord currentCoord = new Coord(j, i);

                    if (i == 0 || j == 0 || i == DefaultSettings.MAP_HEIGHT - 1 || j == DefaultSettings.MAP_WIDTH - 1 || (i % 2 == 0 && j % 5 != 1))
                    {
                        _map.Add(currentCoord, new Wall(currentCoord));
                    }
                    else if(i != 0 && i != DefaultSettings.MAP_HEIGHT - 1 && j != 0 && j != DefaultSettings.MAP_WIDTH - 1 && i % 2 == 1 && j % 5 != 1)
                    {
                        _map.Add(currentCoord, new Food(currentCoord));
                        _countFood++;
                    }
                    else
                    {
                        _map.Add(currentCoord, null);
                    }
                }
            }
        }

        #endregion

        #region PrivateMethods

        private bool IsCellNullOrEmpty(Coord point)
        {
            return (_map[point] == null);
        }

        private bool IsCellCherry(Coord point)
        {
            return (_map[point] is Cherry);
        }

        private bool IsCellFood(Coord point)
        {
            return (_map[point] is Food);
        }

        private void ClearCell(Coord point)
        {
            _map[point] = null;
        }

        private bool CanPlay()
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

        #endregion

        bool IOwner.CanMoving(Coord currentPosition, Direction someDirection)
        {
            bool result = false;

            Cell tmp;
            Coord nextPosition = ((IOwner)this).GetNextCoord(currentPosition, someDirection);
            
            _map.TryGetValue(nextPosition, out tmp);

            if (!(tmp is Wall)) // ToDo: remove "is"
                {
                result = true;
            }

            return result;
        }

        Coord IOwner.GetNextCoord(Coord currentPos, Direction direct)
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

        void IOwner.TryEatFood(Coord point)
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

        bool IOwner.CheckEnemys(Coord point)
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

        bool IOwner.CheckPacman(Coord point)
        {
            bool result = false;

            if (_pacman.Coord.X == point.X && _pacman.Coord.Y == point.Y)
            {
                result = true;
            }

            return result;
        }

        void IOwner.PutPacmanToStartPosition()
        {
            _pacman.Coord = new Coord(DefaultSettings.PACMAN_START_POS_X, DefaultSettings.PACMAN_START_POS_Y);
            _pacman.Lifes--;
        }

        void IOwner.PutEnemyToStartPosition(Coord enemyPosition = null)
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

        bool IOwner.IsPacmanAngry()
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
                    Coord point = _enemys[i].Coord;

                    bool tmp = _map.ContainsKey(point);

                      if (_map[_enemys[i].Coord] == null)
                    {
                        _hideCell(_enemys[i]);
                    }
                    else
                    {
                        _printCell(_map[_enemys[i].Coord]);
                    }
                }

                for (int i = 0; i < _enemys.Length; i++)
                {
                    _enemys[i].Move();
                }


                if (_keyAvalible())
                {
                    Direction keyDirection = _userDirection();
                    _pacman.CheckChangeDirection(keyDirection); //получаем направление
                }

                _pacman.Move();

                _printCell(_pacman);

                for (int i = 0; i < _enemys.Length; i++)
                {
                    _printCell(_enemys[i]);
                }

            } while (CanPlay());

            if (_countFood == 0) 
            {
                //_printWin();
                _printWin.Invoke();
            }
            else
            {
                _printLose();
            }
        }
    }
}

