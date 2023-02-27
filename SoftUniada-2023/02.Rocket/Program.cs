using System;
using System.Text;
using System.Collections.Generic;

int n = int.Parse(Console.ReadLine());

int width = n + 5;

int height = 2 * n + n / 2 + 3;
string savedRow = string.Empty;
string firstRow = $"{new string('_', width / 2)}^{new string('_', width / 2)}";
Console.WriteLine(firstRow);
for(int i = 1; i <= 2+n/2; i++)
{
    
    string currentRow = string.Empty;
   
    
    if(i == 1)
    {
        currentRow = "/|\\";
    }
    else if (i == 2)
    {
        currentRow = "/|||\\";
    }
    else
    {
        string dots = new string('.', i - 2);
        currentRow = $"/{dots}|||{dots}\\";
    }

    while (currentRow.Length < width)
    {
        currentRow = $"_{currentRow}_";
    }


    Console.WriteLine(currentRow);
    
    if(i == (2+n/2)-1)
    {
        savedRow = currentRow;
    }
}
Console.WriteLine(savedRow);

for(int i = 0; i <= n; i++)
{
    string firstPart = new string('_', (width - 3) / 2);
    if(i != n)
    {
        Console.WriteLine($"{firstPart}|||{firstPart}");
    }
    else
    {
        Console.WriteLine($"{firstPart}~~~{firstPart}");
    }
}

for (int i = 0; i < n/2; i++)
{
    List<char> row = new List<char>() { '!'};
    for(int j = 0; j < i ; j++)
    {
        row.Add('.');
        row.Insert(0, '.');
    }

    row.Add('\\');
    row.Add('\\');
    row.Insert(0, '/');
    row.Insert(0, '/');

    while (row.Count < width)
    {
        row.Add('_');
        row.Insert(0, '_');
    }

    Console.WriteLine(String.Join("",row));

}
