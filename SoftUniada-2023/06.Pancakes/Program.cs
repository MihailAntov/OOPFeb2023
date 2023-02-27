using System;
using System.Linq;

int[] pancake = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int maxStartIndex = -1;
int maxEndIndex = -1;
long maxSum = int.MinValue;
int maxLength = 0;

for (int startIndex = 0; startIndex < pancake.Length; startIndex++)
{
    if (pancake[startIndex] < 0)
    {
        continue;
    }


    int length = 1;
    int biggestSumFromThisIndex = pancake[startIndex];
    while (startIndex+length < pancake.Length-1 )
    {
        int nextNum = pancake[startIndex + length];

        if(nextNum + biggestSumFromThisIndex < 0 )
        {
            break;
        }
        biggestSumFromThisIndex += pancake[startIndex + length];
        length++;

        if (biggestSumFromThisIndex > maxSum)
        {
            maxSum = biggestSumFromThisIndex;
            maxStartIndex = startIndex;
            maxLength = length;
            maxEndIndex = startIndex + length - 1;
        }
        else if (biggestSumFromThisIndex == maxSum)
        {
            if (length > maxLength)
            {
                maxSum = biggestSumFromThisIndex;
                maxStartIndex = startIndex;
                maxLength = length;
                maxEndIndex = startIndex + length - 1;
            }
        }
    }


}

Console.WriteLine($"{maxSum} {maxStartIndex} {maxEndIndex}");


