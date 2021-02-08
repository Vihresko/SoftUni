using System;
using System.Collections.Generic;
using System.Linq;
namespace _07.KnightGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            char[,] table = new char[n, n];
            List<int[]> knightsPositions = new List<int[]>();
            int countOfMoves = 0;

            // Input TAble - DONE
            for (int row = 0; row < n; row++)
            {
                string input = Console.ReadLine();
                for (int col = 0; col < n; col++)
                {
                    table[row, col] = input[col];
                }
            }
            // Find Knights - DONE
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    if(table[row,col] == 'K')
                    {
                        knightsPositions.Add(new int[] { row, col });
                    }
                   
                }
            }
            //Find knigths in biggest danger and remove it - DONE
            int firstRemoveRow = -1;
            int firstRemoveCol = -1;
            int circles = knightsPositions.Count;
            for (int circle = 0; circle < circles; circle++)
            {
                
                bool isFoundEnemy = false;
                int maxEnemyCount = 0;
                
                for (int position = 0; position < knightsPositions.Count; position++)
                {
                    int positionRow = knightsPositions[position][0];
                    int positionCol = knightsPositions[position][1];
                    int currentKnightDanger = CheckKnightDanger(positionRow, positionCol, knightsPositions);
                    if(currentKnightDanger > maxEnemyCount)
                    {
                        maxEnemyCount = currentKnightDanger;
                        firstRemoveRow = positionRow;
                        firstRemoveCol = positionCol;
                        isFoundEnemy = true;
                    }
                    currentKnightDanger = 0;
                }

                if (isFoundEnemy)
                {
                    int[] removeThis = new int[] 
                    {
                        firstRemoveRow, firstRemoveCol
                    };

                    for (int i = 0; i < knightsPositions.Count; i++)
                    {
                        if(removeThis[0] == knightsPositions[i][0] && removeThis[1] == knightsPositions[i][1])
                        {
                            knightsPositions.RemoveAt(i);
                            break;
                        }
                    }
                    countOfMoves++;
                }
                else
                {
                    Console.WriteLine(countOfMoves);
                    break;
                }
                
            }
            
        }
        
        private static int CheckKnightDanger(int row, int col, List<int[]> knightPositions)
        {
            int countOfPotentionalAtack = 0;
            foreach (var knigth in knightPositions)
            {
                int knightRow = knigth[0];
                int knightCol = knigth[1];

                if(   row + 2 == knightRow && (col + 1 == knightCol|| col -1 == knightCol)  ||
                      row - 2 == knightRow && (col + 1 == knightCol|| col - 1 == knightCol) ||
                      col + 2 == knightCol && (row + 1 == knightRow|| row - 1 == knightRow) ||
                      col - 2 == knightCol && (row + 1 == knightRow|| row - 1 == knightRow)   )
                {
                    countOfPotentionalAtack++;
                }
            }
            return countOfPotentionalAtack;
        }
    }
}
