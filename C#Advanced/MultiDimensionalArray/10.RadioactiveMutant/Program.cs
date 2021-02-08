using System;
using System.Collections.Generic;
using System.Linq;

namespace _10.RadioactiveMutant
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] fieldParameters = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            int n = fieldParameters[0];
            int m = fieldParameters[1];
            string[,] field = new string[n, m];
            int[] currentPlayerPosition = new int[2];
            for (int row = 0; row < n; row++)
            {
                char[] inputLineOfField = Console.ReadLine().ToCharArray();
                for (int col = 0; col < m; col++)
                {
                    string element = inputLineOfField[col].ToString();
                    field[row, col] = element;
                    if(element == "P")
                    {
                        currentPlayerPosition[0] = row;
                        currentPlayerPosition[1] = col;
                    }
                }
            }

            char[] directionsForMove = Console.ReadLine().ToCharArray();
            for (int move = 0; move < directionsForMove.Length; move++)
            {
                int[] lastPosition = currentPlayerPosition;
                int[] futureDestination = currentPlayerPosition;
                string elementOfNewPosition = string.Empty;
                int currentRow = currentPlayerPosition[0];
                int currentCol = currentPlayerPosition[1];
                if(IsEscapeFromField(field, directionsForMove[move].ToString(), lastPosition))
                {
                    PrintField(field);
                    Console.WriteLine($"won: {currentRow} {currentCol}");
                    break;
                }
                switch (directionsForMove[move].ToString())
                {
                    case "U":
                        elementOfNewPosition = field[currentRow - 1, currentCol];

                        currentPlayerPosition[0] = currentRow - 1;
                        currentPlayerPosition[1] = currentCol;
                        break;
                    case "D":
                        elementOfNewPosition = field[currentRow + 1, currentCol];
                        currentPlayerPosition[0] = currentRow + 1;
                        currentPlayerPosition[1] = currentCol;
                        break;
                    case "L":
                        elementOfNewPosition = field[currentRow, currentCol - 1];
                        currentPlayerPosition[0] = currentRow;
                        currentPlayerPosition[1] = currentCol - 1;
                        break;
                    case "R":
                        elementOfNewPosition = field[currentRow, currentCol + 1];
                        currentPlayerPosition[0] = currentRow;
                        currentPlayerPosition[1] = currentCol + 1;
                        break;
                    default:
                        break;
                }
                if(elementOfNewPosition != "B")
                {
                    field[lastPosition[0], lastPosition[1]] = ".";
                    field[currentPlayerPosition[0], currentPlayerPosition[1]] = "P";
                    
                }
                else
                {
                    field[lastPosition[0], lastPosition[1]] = ".";
                    BunnySpread(field, currentPlayerPosition);
                    PrintField(field);
                    Console.WriteLine($"dead: {currentPlayerPosition[0]} {currentPlayerPosition[1]}");
                    break;
                }
                if (BunnySpread(field, currentPlayerPosition))
                {
                    PrintField(field);
                    Console.WriteLine($"dead: {currentPlayerPosition[0]} {currentPlayerPosition[1]}");
                    break;
                }
            }
        }

        static void PrintField(string[,] field)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    Console.Write(field[row, col]);
                }
                Console.WriteLine();
            }
        }
        static bool BunnySpread(string[,] field, int[] playerPosition)
        {
            bool isLostGame = false;
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    if(field[row,col] == "B")
                    {
                        if(IsBunnyActionKillPlayer(field, playerPosition))
                        {
                            isLostGame = true;
                        }
                    }
                }
            }
            return isLostGame;
        }

        static bool IsBunnyActionKillPlayer(string[,] fieldInput, int[] position)
        {
            bool result = false;
            int currentRow = position[0];
            int currentCol = position[1];
            List<string> valueOfBunnyNewDestination = new List<string>();
            if(currentRow - 1 >= 0)
            {
                valueOfBunnyNewDestination.Add(fieldInput[currentRow - 1, currentCol]);
                fieldInput[currentRow - 1, currentCol] = "B";
            }
            if (currentRow + 1 < fieldInput.GetLength(0))
            {
                valueOfBunnyNewDestination.Add(fieldInput[currentRow + 1, currentCol]);
                fieldInput[currentRow + 1, currentCol] = "B";
            }
            if (currentCol - 1 >= 0)
            {
                valueOfBunnyNewDestination.Add(fieldInput[currentRow, currentCol - 1]);
                fieldInput[currentRow, currentCol - 1] = "B";
            }
            if (currentRow + 1 < fieldInput.GetLength(1))
            {
                valueOfBunnyNewDestination.Add(fieldInput[currentRow, currentCol + 1]);
                fieldInput[currentRow, currentCol + 1] = "B";
            }

            if (valueOfBunnyNewDestination.Contains("P"))
            {
                result = true;
            }
            return result;
        }
        static bool IsEscapeFromField(string[,] field, string direction, int[] position)
        {
            bool result = false;
            int rowPosition = position[0];
            int colPosition = position[1];
            switch (direction)
            {
                case "U":
                    if (rowPosition - 1 < 0)
                    {
                        result = true;
                    }
                    break;
                case "D":
                    if (rowPosition + 1 == field.GetLength(0))
                    {
                        result = true;
                    }
                    break;
                case "L":
                    if (colPosition - 1 < 0)
                    {
                        result = true;
                    }
                    break;
                case "R":
                    if (colPosition + 1 == field.GetLength(1))
                    {
                        result = true;
                    }
                    break;
                
            }

            return result;
        }
    }
}
