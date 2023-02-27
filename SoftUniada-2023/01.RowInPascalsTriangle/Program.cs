
using System;
using System.Collections.Generic;

int n = int.Parse(Console.ReadLine());
int[] row = new int[] { 1 };
for (int i = 0; i < n; i++)
{
    int[] nextRow = new int[row.Length + 1];
    nextRow[0] = 1;
    nextRow[nextRow.Length-1] = 1;
    for (int j = 1; j < nextRow.Length - 1; j++)
    {
        nextRow[j] = row[j-1] + row[j];
    }

    row = nextRow;
}

Console.WriteLine(String.Join(" ",row));

