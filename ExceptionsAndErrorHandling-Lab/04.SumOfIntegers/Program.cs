using System;

string[] elements = Console.ReadLine()
    .Split(" ");
int sum = 0;
foreach(string element in elements)
{
    try
    {
        int number = int.Parse(element);
        sum += number;
    }
    catch (OverflowException)
    {
        Console.WriteLine($"The element '{element}' is out of range!");

    }
    catch (FormatException)
    {
        Console.WriteLine($"The element '{element}' is in wrong format!");
    }
    
    finally
    {
        Console.WriteLine($"Element '{element}' processed - current sum: {sum}");
    }
}

Console.WriteLine($"The total sum of all integers is: {sum}");