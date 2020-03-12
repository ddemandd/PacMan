using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    class GameField
    {
        Cell[,] _map;
        Enemy[] _enemys;
        Pacman _pacman;

        public GameField()
        {
            _map = new Cell[DefaultSettings.MAP_HEIGHT, DefaultSettings.MAP_WIDTH];
            _enemys = new Enemy[DefaultSettings.ENEMY_COUNT];
            _pacman = new Pacman(this, new Coord());
        }

        private void InitiolyzeEnemys()
        {
            for (int i = 0; i < _enemys.Length; i++)
            {
                _enemys[i] = new Enemy(ConsoleColor.Red, new Coord(), this);
            }
        }

        private void InitiolyzePacman()
        {
            _pacman = new Pacman(this, new Coord());
        }

        public void InitiolyzeField()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == _map.GetLength(0) - 1 || j == _map.GetLength(1) - 1)
                    {
                        _map[i, j] = new Wall(new Coord(i, j));
                    }
                    if (i % 2 == 0 && j % 5 != 1)
                    {
                        _map[i, j] = new Wall(new Coord(i, j));
                    }

                }
            }
        }

    }
}
