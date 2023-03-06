using System;
using System.Linq;

int[] nums = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();


int targetNumber = int.Parse(Console.ReadLine());

Console.WriteLine(Find(nums, targetNumber));

int Find(int[] nums, int target)
{
    return FindInRange(nums, 0, nums.Length - 1, target);
}

int FindInRange(int[] nums, int startIndex, int endIndex, int target)
{
    int middle = (startIndex + endIndex) / 2;

    if(startIndex == endIndex)
    {
        if (nums[startIndex] == target)
        {
            return startIndex;
        }
        else
        {
            return -1;
        }
    }

    if (nums[middle] > target)
    {
        return FindInRange(nums, startIndex, middle-1, target);
    }
    else if (nums[middle] < target)
    {
        return FindInRange(nums, middle+1, endIndex, target);
    }
    else
    {
        return middle;
    }

    
}