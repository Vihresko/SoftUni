using System;
using System.Linq;
namespace _01.DiagonalDifference
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[,] square = new int[n, n];

            for (int row = 0; row < n; row++)
            {
                int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int col = 0; col < n; col++)
                {
                    square[row, col] = input[col];
                }
            }

            int sumLeftDiagonal = 0;
            int sumRightDiagonal = 0;

            for (int i = 0; i < n; i++)
            {
                sumLeftDiagonal += square[i, i];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                sumRightDiagonal += square[i, n - i - 1];
            }
            
            int diff = Math.Abs(sumLeftDiagonal - sumRightDiagonal);
            Console.WriteLine(diff);
        }
    }
}
