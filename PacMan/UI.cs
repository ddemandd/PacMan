using System;
using System.Collections.Generic;
using System.Text;

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


            //Cell tmp;

            //for (int i = 0; i < DefaultSettings.MAP_HEIGHT; i++)
            //{
            //    for (int j = 0; j < DefaultSettings.MAP_WIDTH; j++)
            //    {
            //        tmp = source[new Coord(j, i)];

            //        if (tmp != null)
            //        {
            //            Console.ForegroundColor = tmp.Color;

            //            Console.Write(tmp.View);
            //            //ShowCell(source[i, j]);
            //        }
            //        else
            //        {
            //            Console.Write(" ");
            //        }
            //    }
            //    Console.WriteLine();
            //}

            //Console.ForegroundColor = ConsoleColor.Gray;
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

        
    }
}
