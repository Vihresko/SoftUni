using System;
using System.Linq;

namespace _02.SquaresInMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimension = Console.ReadLine().Split().Select(int.Parse).ToArray();
            string[,] mainFigure = new string[dimension[0], dimension[1]];

            int count = 0;

            for (int i = 0; i < dimension[0]; i++)
            {
                string[] input = Console.ReadLine().Split();
                for (int col = 0; col < dimension[1]; col++)
                {
                    mainFigure[i, (dimension[0] - (dimension[0] - col))] = input[col];
                }
               
            }
            
            for (int row = 0; row < mainFigure.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < mainFigure.GetLength(1)-1; col++)
                {
                    string currentPoint = mainFigure[row, col];
                    if(currentPoint == mainFigure[row, col + 1])
                    {
                        if(currentPoint == mainFigure[row+1, col])
                        {
                            if(currentPoint == mainFigure[row+1, col + 1])
                            {
                                count++;
                            }
                        }
                    }

                }
                
            }
            Console.WriteLine(count);
        }
    }
}
