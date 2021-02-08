using System;
using System.Linq;

namespace _03.MAximalSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimension = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int[,] mainFigure = new int[dimension[0], dimension[1]];
            int sumOfCurrent = 0;
            int[]indexOfMax = new int[] { -1  , -1 };

            int maxSum = int.MinValue;

            for (int i = 0; i < dimension[0]; i++)
            {
                int[] input = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int col = 0; col < dimension[1]; col++)
                {
                    mainFigure[i, col] = input[col];
                }
            }
            for (int row = 0; row < mainFigure.GetLength(0) - 2; row++)
            {
                for (int col = 0; col < mainFigure.GetLength(1) - 2 ; col++)
                {
                   
                  sumOfCurrent = mainFigure[row, col] + mainFigure[row, col +1] + mainFigure[row, col+2]
                                       + mainFigure[row+1, col] + mainFigure[row+1, col+1] + mainFigure[row+1, col +2]
                                       + mainFigure[row + 2, col] + mainFigure[row + 2, col + 1] + mainFigure[row + 2, col + 2];
                    if (sumOfCurrent > maxSum)
                    {
                        maxSum = sumOfCurrent;
                        indexOfMax[0] = row;
                        indexOfMax[1] = col;
                    }
                }
            }
            Console.WriteLine($"Sum = {maxSum}");
            for (int row = indexOfMax[0]; row < indexOfMax[0] + 3; row++)
            {
                for (int col = indexOfMax[1]; col < indexOfMax[1] + 3; col++)
                {
                    Console.Write(mainFigure[row,col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
