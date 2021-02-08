using System;
using System.Linq;

namespace _06.JaggedArrayManipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            double[][] matrix = new double[n][];
            for (int row = 0; row < n; row++)
            {
                double[] fullfillMatrix = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
                matrix[row] = fullfillMatrix;
            }
            for (int row = 0; row < n-1; row++)
            {
                if(matrix[row].Length == matrix[row + 1].Length)
                {
                    matrix[row] = matrix[row].Select(x => x = x * 2).ToArray();
                    matrix[row + 1] = matrix[row+1].Select(x => x = x * 2).ToArray();

                }
                else
                {
                    matrix[row] = matrix[row].Select(x => x = x / 2).ToArray();
                    matrix[row + 1] = matrix[row + 1].Select(x => x = x / 2).ToArray();

                }
            }
            string input = string.Empty;
            while((input = Console.ReadLine()) != "End")
            {
                string[] cmds = input.Split();
                int rowForWrite = int.Parse(cmds[1]);
                int colForWrite = int.Parse(cmds[2]);
                int value = int.Parse(cmds[3]);

                if(rowForWrite >= 0 && rowForWrite < n && colForWrite >= 0 && colForWrite < matrix[rowForWrite].Length)
                {
                    
                    if(cmds[0] == "Add")
                    {
                        matrix[rowForWrite][colForWrite] += value;
                    }
                    else
                    {
                        matrix[rowForWrite][colForWrite] -= value;
                    }
                }
            }
            for (int row = 0; row < n; row++)
            {
                Console.WriteLine(string.Join(" ", matrix[row]));
            }
        }
    }
}
