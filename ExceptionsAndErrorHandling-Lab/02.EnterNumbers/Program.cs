using System;
using System.Collections.Generic;


List<int> results = new List<int>();


while (results.Count < 10)
{
    int lastNumber = results.Count > 0 ? results[results.Count - 1] : 1;
    try
    {
        int nextNumber = ReadNumber(lastNumber, 100);

        results.Add(nextNumber);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch(FormatException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

Console.WriteLine(String.Join(", ",results));

int ReadNumber(int start, int end)
{

    int n = 0;
    try
    {
        n = int.Parse(Console.ReadLine());
    }
    catch(FormatException)
    {
        throw new FormatException("Invalid Number!");
    }
    

    if (n <= start || n >= end)
    {
        throw new ArgumentException($"Your number is not in range {start} - {end}!");
    }




    return n;
}