using System;
using System.Linq;

namespace _09.Miner
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            string[,] field = new string[n, n];
            string[] movements = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int[] currentPosition = new int[2];

            for (int row = 0; row < n; row++)
            {
                string[] fieldConstructLine = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                for (int col = 0; col < n; col++)
                {
                    field[row, col] = fieldConstructLine[col];
                    if(field[row,col] == "s")
                    {
                        currentPosition[0] = row;
                        currentPosition[1] = col;
                    }
                }
            }
            
            int countOfCoals = 0;
            bool isGameOver = false;
            for (int move = 0; move < movements.Length; move++)
            {
                string direction = movements[move];
                if(IsValidNextMove(field, direction, currentPosition))
                {
                    int[] lastPosition = currentPosition;
                    int currentRow = currentPosition[0];
                    int currentCol = currentPosition[1];
                    string elementOfNewPosition = string.Empty;
                    switch (direction)
                    {
                        case "up":
                            elementOfNewPosition = field[currentRow - 1, currentCol];
                            currentPosition[0] = currentRow - 1;
                            currentPosition[1] = currentCol;
                            break;
                        case "down":
                            elementOfNewPosition = field[currentRow + 1, currentCol];
                            currentPosition[0] = currentRow + 1;
                            currentPosition[1] = currentCol;
                            break;
                        case "left":
                            elementOfNewPosition = field[currentRow, currentCol - 1];
                            currentPosition[0] = currentRow;
                            currentPosition[1] = currentCol - 1;
                            break;
                        case "right":
                            elementOfNewPosition = field[currentRow, currentCol + 1];
                            currentPosition[0] = currentRow;
                            currentPosition[1] = currentCol + 1;
                            break;
                        default:
                            break;
                    }

                    if(elementOfNewPosition == "e")
                    {
                        field[lastPosition[0], lastPosition[1]] = "*";
                        Console.WriteLine($"Game over! ({currentPosition[0]}, {currentPosition[1]})");
                        isGameOver = true;
                        break;
                    }
                    else if(elementOfNewPosition == "c")
                    {
                        countOfCoals++;
                        field[lastPosition[0], lastPosition[1]] = "*";
                        field[currentPosition[0], currentPosition[1]] = "s";
                        if (!CheckForCoals(field))
                        {
                            Console.WriteLine($"You collected all coals! ({currentPosition[0]}, {currentPosition[1]})");
                            isGameOver = true;
                            break;
                        }
                    }
                    else if(elementOfNewPosition == "*")
                    {
                        field[lastPosition[0], lastPosition[1]] = "*";
                        field[currentPosition[0], currentPosition[1]] = "s";
                    }
                        
                }

            }

            if (!isGameOver)
            {
                Console.WriteLine($"{GetCountOfCoals(field)} coals left. ({currentPosition[0]}, {currentPosition[1]})");
            }



          
        }

        static int GetCountOfCoals(string[,] field)
        {
            int result = 0;
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    if (field[row, col] == "c")
                    {
                        result++;
                    }
                }
            }
            return result;
        }
        static bool CheckForCoals(string[,] field)
        {
            bool result = false;
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    if(field[row,col] == "c")
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        static bool IsValidNextMove(string[,] field, string direction, int[] position)
        {
            bool result = true;
            int rowPosition = position[0];
            int colPosition = position[1];
            switch (direction)
            {
                case "up":
                    if(rowPosition - 1 < 0)
                    {
                        result = false;
                    }
                    break;
                case "down":
                    if(rowPosition + 1 == field.GetLength(0))
                    {
                        result = false;
                    }
                    break;
                case "left":
                    if(colPosition - 1 < 0)
                    {
                        result = false;
                    }
                    break;
                case "right":
                    if(colPosition + 1 == field.GetLength(1))
                    {
                        result = false;
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
        static void PrintField(string[,] field)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    Console.Write(field[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
