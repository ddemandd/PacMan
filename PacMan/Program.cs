using System;

using BLLayer;
using BLLayer.Delegates;

namespace PacMan
{
    class Program
    {
        static void Main(string[] args)
        {
            GameField start = new GameField();
            UI print = new UI();

            start.ShowGameField += print.PrintField;
            start.ShowCell += print.ShowCell;
            start.HideCell += print.HideCell;
            start.ShowScore += print.PrintScore;
            start.PrintWin += print.PrintWin;
            start.PrintLose += print.PrintLose;
            start.KeyAvalible += print.IsKeyAvalible;
            start.UserDirection += print.GetDirection;

            start.Run();

            Console.ReadKey();
        }
    }
}
