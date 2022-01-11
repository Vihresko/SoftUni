using System;
using System.Collections.Generic;

namespace Armory
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            char[,] matrix = new char[n, n];

            int[] officerPosition = new int[2];
            List<int[]> mirrorsPositions = new List<int[]>();
            FullfillMatrix(n, matrix, officerPosition, mirrorsPositions);
            PrintMatrix(matrix);

            int target = 65;


        }

        static void FullfillMatrix(int n, char[,] matrix, int[] officerPosition, List<int[]> mirrorsPositions)
        {
            for (int i = 0; i < n; i++)
            {
                char[] inputValues = Console.ReadLine().ToCharArray();
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == 'A')
                    {
                        officerPosition[0] = i;
                        officerPosition[1] = j;
                    }

                    if (matrix[i, j] == 'M')
                    {
                        mirrorsPositions.Add(new int[] { i, j });
                    }

                    matrix[i, j] = inputValues[j];
                }
            }
        }

        static void PrintMatrix(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j]);
                }
                Console.WriteLine();
            }
        }

        static Dictionary<char, int[]> GetElementOnNextStep(string moveTo,int[] currentPostion, char[,] matrix)
        {
            int[] positionForCheck = currentPostion;
            if(moveTo == "left")
            {
                positionForCheck[0] -= 1;
            }
            else if(moveTo == "right")
            {
                positionForCheck[0] += 1;
            }
            else if(moveTo == "up")
            {
                positionForCheck[1] -= 1;
            }
            else if(moveTo == "down")
            {
                positionForCheck[1] += 1;
            }

            return new Dictionary<char, int[]> { { matrix[positionForCheck[0], positionForCheck[1]], positionForCheck } };
        }

        static bool isNextPositionInRange(int matrixSize , int[] coordinates)
        {
            if(coordinates[0] < 0 || coordinates[0] >= matrixSize || coordinates[1] < 0 || coordinates[1] >= matrixSize)
            {
                return false;
            }
            return true;
        }
    }
}
