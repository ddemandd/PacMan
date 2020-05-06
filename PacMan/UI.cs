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

        public void PrintField(Cell[,] source)
        {
            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    if (source[i, j] != null)
                    {
                        Console.ForegroundColor = source[i, j].Color;

                        Console.Write(source[i, j].View);
                        //ShowCell(source[i, j]);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
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
            Console.SetCursorPosition(someCell.Coord.X, someCell.Coord.Y); 

            Console.Write(" ");

            Console.SetCursorPosition(someCell.Coord.X, someCell.Coord.Y);
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
