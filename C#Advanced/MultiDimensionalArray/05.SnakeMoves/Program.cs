using System;
using System.Collections.Generic;
using System.Linq;

namespace _05.SnakeMoves
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimensionParameters = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int n = dimensionParameters[0];
            int m = dimensionParameters[1];
            char[,] matrix = new char[n, m]; 
            string snake = Console.ReadLine();
            Queue<char> mover = new Queue<char>(snake.ToCharArray()); 
            int size = snake.Length;

            for (int row = 0; row < n; row++)
            {
                if(row % 2 == 0)
                {
                    for (int col = 0; col < m; col++)
                    {
                        char currentChar = mover.Peek();
                        matrix[row, col] = mover.Dequeue();
                        mover.Enqueue(currentChar);
                        
                    }
                }
                else
                {
                    for (int col = m - 1; col >= 0; col--)
                    {
                        char currentChar = mover.Peek();
                        matrix[row, col] = mover.Dequeue();
                        mover.Enqueue(currentChar);
                    }
                }
            }

            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < m; col++)
                {
                    Console.Write(matrix[row,col]);
                }
                Console.WriteLine();
            }
        }
    }
}
