using System;

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

            start.Run();



            Console.ReadKey();
        }
    }
}
