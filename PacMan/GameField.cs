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
            _pacman = new Pacman();
        }

        private void InitiolyzeEnemys()
        {
            for (int i = 0; i < _enemys.Length; i++)
            {
                _enemys[i] = new Enemy(ConsoleColor.Red, new Coord(), this);
            }
        }
    }
}
