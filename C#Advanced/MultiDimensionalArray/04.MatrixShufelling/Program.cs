using System;
using System.Linq;
namespace _04.MatrixShuflling
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimension = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int n = dimension[0];
            int m = dimension[1];
            string[,] matrix = new string[dimension[0], dimension[1]];

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries).ToArray(); ;
                for (int col = 0; col < m; col++)
                {
                    matrix[i, col] = input[col];
                }
            }

            string inputCommand = string.Empty;
            while((inputCommand = Console.ReadLine()) != "END")
            {
                string[] cmds = inputCommand.Split(" ",StringSplitOptions.RemoveEmptyEntries).ToArray();
                if(cmds[0] == "swap" && cmds.Length == 5)
                {
                    int row1 = int.Parse(cmds[1]);
                    int col1 = int.Parse(cmds[2]);
                    int row2 = int.Parse(cmds[3]);
                    int col2 = int.Parse(cmds[4]);


                    bool isValidOne = row1 >= 0 && row1 < n && col1 >= 0 && col1 < m;
                    bool isValidTwo = row2 >= 0 && row2 < n && col2 >= 0 && col2 < m;

                    if(isValidOne && isValidTwo)
                    {
                        string Num1 = matrix[row1, col1];
                        string Num2 = matrix[row2, col2];

                        matrix[row1, col1] = Num2;
                        matrix[row2, col2] = Num1;

                        for (int row = 0; row < n; row++)
                        {
                            for (int col = 0; col < m; col++)
                            {
                                Console.Write(matrix[row, col] + " ");
                            }
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                    }
                    
                   
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }
        }
    }
}
