using System;
using System.Collections.Generic;
using System.Linq;

namespace _08.Bombs
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = new int[n, n];

            for (int row = 0; row < n; row++)
            {
                int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int col = 0; col < n; col++)
                {
                    matrix[row, col] = input[col];
                }
            }

            string[] bombsInput = Console.ReadLine().Split();
            List<List<int>> targets = new List<List<int>>();
            for (int i = 0; i < bombsInput.Length; i++)
            {
                int[] bombIndex = bombsInput[i].Split(',').Select(int.Parse).ToArray();
                int bombRow = bombIndex[0];
                int bombCol = bombIndex[1];
                targets.Clear();

                if (matrix[bombRow, bombCol] <= 0)
                {
                    continue;
                }

                int[] left = new int[] { bombRow, bombCol - 1 };
                targets.Add(left.ToList());
                int[] right = new int[] { bombRow, bombCol + 1 };
                targets.Add(right.ToList());
                int[] topLeft = new int[] { bombRow - 1, bombCol - 1 };
                targets.Add(topLeft.ToList());
                int[] downLeft = new int[] { bombRow + 1, bombCol - 1 };
                targets.Add(downLeft.ToList());
                int[] topRight = new int[] { bombRow - 1, bombCol + 1 };
                targets.Add(topRight.ToList());
                int[] downRight = new int[] { bombRow + 1, bombCol + 1 };
                targets.Add(downRight.ToList());
                int[] top = new int[] { bombRow - 1, bombCol };
                targets.Add(top.ToList());
                int[] down = new int[] { bombRow + 1, bombCol };
                targets.Add(down.ToList());

                for (int b = 0; b < targets.Count; b++)
                {
                    if(targets[b][0] >= 0 && targets[b][0] < n && targets[b][1] >= 0 && targets[b][1] < n)
                    {

                        if(matrix[targets[b][0],targets[b][1]] > 0)
                        {
                            matrix[targets[b][0], targets[b][1]] -= matrix[bombRow, bombCol];
                        }
                       
                    }
                }
            }

            foreach (var bomb in bombsInput)
            {
                int[] convert = bomb.Split(',').Select(int.Parse).ToArray();
                int row = convert[0];
                int col = convert[1];
                matrix[row, col] = 0;
            }

            long sumOfAlive = 0;
            int countOfAlive = 0;
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    if(matrix[row,col] > 0)
                    {
                        countOfAlive++;
                        sumOfAlive += matrix[row, col];
                    }
                }
            }

            Console.WriteLine($"Alive cells: {countOfAlive}");
            Console.WriteLine($"Sum: {sumOfAlive}");
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    Console.Write(matrix[row,col] + " ");
                }
                Console.WriteLine();
            }
            
        }
    }
}
