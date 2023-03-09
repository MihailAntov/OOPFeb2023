using System;
using System.Linq;

int[] nums = Console.ReadLine()
    .Split(" ")
    .Select(int.Parse)
    .ToArray();

int exceptionCount = 0;

while(exceptionCount < 3)
{
    string[] commandArgs = Console.ReadLine()
        .Split(" ");

    string command = commandArgs[0];
    try
    {
        if (command == "Replace")
        {
            int index = int.Parse(commandArgs[1]);
            int element = int.Parse(commandArgs[2]);
            nums[index] = element;
        }
        else if (command == "Print")
        {
            int startIndex = int.Parse(commandArgs[1]);
            int endIndex = int.Parse(commandArgs[2]);
            Console.WriteLine(String.Join(", ", nums[startIndex..(endIndex+1)]));
        }
        else if (command == "Show")
        {
            int index = int.Parse(commandArgs[1]);
            Console.WriteLine(nums[index]);
        }
    }
    catch(FormatException)
    {
        Console.WriteLine("The variable is not in the correct format!");
        exceptionCount++;
    }
    catch(IndexOutOfRangeException)
    {
        Console.WriteLine("The index does not exist!");
        exceptionCount++;

    }
    catch(ArgumentOutOfRangeException)
    {
        Console.WriteLine("The index does not exist!");
        exceptionCount++;
    }


}

Console.WriteLine(String.Join(", ",nums));