using System;
using System.Collections.Generic;

using BLLayer.MapElements;
using BLLayer;
using BLLayer.Enums;

namespace PacMan
{
    class UI
    {
        public UI()
        {
            Console.CursorVisible = false;
        }

        public void PrintField(Dictionary<Coord, Cell> source)
        {
            foreach (Coord item in source.Keys)
            {
                Cell tmp = source[item];

                if (tmp != null)
                {
                    ShowCell(tmp);
                }
                else
                {
                    PrintEmptyCell(item);
                }
            }
        }

        public void ShowCell(Cell someCell)
        {
            Console.SetCursorPosition(someCell.Coord.X, someCell.Coord.Y);

            Console.ForegroundColor = someCell.Color;

            Console.Write(someCell.View);

            Console.SetCursorPosition(someCell.Coord.X, someCell.Coord.Y);

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void HideCell(Cell someCell)
        {
            PrintEmptyCell(someCell.Coord);
        }

        private void PrintEmptyCell(Coord point)
        {
            Console.SetCursorPosition(point.X, point.Y);

            Console.Write(" ");

            Console.SetCursorPosition(point.X, point.Y);
        }

        public void PrintScore(int score)
        {
            Console.SetCursorPosition(DefaultSettings.SCORE_POSITION_X, DefaultSettings.SCORE_POSITION_Y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SCORE: {0}", score);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void PrintWin()
        {
            Console.SetCursorPosition(DefaultSettings.RESULT_POSITION_X, DefaultSettings.RESULT_POSITION_Y);
            Console.WriteLine("Magestic !!");
        }

        public void PrintLose()
        {
            Console.SetCursorPosition(DefaultSettings.RESULT_POSITION_X, DefaultSettings.RESULT_POSITION_Y);
            Console.WriteLine("Game Over");
        }

        public bool IsKeyAvalible()
        {
            return (Console.KeyAvailable);
        }

        public Direction GetDirection()
        {
            Direction tmp = Direction.NonDirection;

            ConsoleKey key = Console.ReadKey().Key;

            switch (key) //проверяем нажатаю кнопку на соответствие с имеющимся направлением и выбираем новое направление
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

            return tmp;
        }
    }
}
