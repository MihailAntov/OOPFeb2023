using System;
using System.Linq;
int size = int.Parse(Console.ReadLine());
int[,] matrix = new int[size, size]; 
for(int row = 0; row < size; row++)
{
    int[] rowArgs = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();

    for(int col = 0; col < size; col++)
    {
        matrix[row, col] = rowArgs[col];
    }
}


int[,] newMatrix = new int[size, size];

for (int row = 0; row < matrix.GetLength(0); row++)
{
    for (int col = 0; col < matrix.GetLength(1); col++)
    {
        newMatrix[col, matrix.GetLength(0) - 1 - row] = matrix[row, col];
    }
}

for (int row = 0; row < size; row++)
{
    for (int col = 0; col < size; col++)
    {
        Console.Write($"{newMatrix[row,col]} ");
    }
    Console.WriteLine();
}