using System;
using System.Linq;

int[] nums = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

Console.WriteLine(String.Join(" ",MergeSort(nums)));

int[] MergeSort(int[] nums)
{
    if (nums.Length == 1)
    {
        return nums;
    }

    var middleIndex = nums.Length / 2;
    var leftHalf = nums.Take(middleIndex).ToArray();
    var rightHalf = nums.Skip(middleIndex).ToArray();

    return MergeArrays(MergeSort(leftHalf), MergeSort(rightHalf));
}

int[] MergeArrays(int[] leftHalf, int[] rightHalf)
{
    int[] result = new int[leftHalf.Length + rightHalf.Length];
    int sortedIndex = 0;
    int leftIndex = 0;
    int rightIndex = 0;

    while(leftIndex < leftHalf.Length && rightIndex <  rightHalf.Length)
    {
        if(leftHalf[leftIndex] < rightHalf[rightIndex])
        {
            result[sortedIndex++] = leftHalf[leftIndex++];
        }
        else
        {
            result[sortedIndex++] = rightHalf[rightIndex++];
        }
    }

    while(leftIndex < leftHalf.Length)
    {
        result[sortedIndex++] = leftHalf[leftIndex++];
    }

    while (rightIndex < rightHalf.Length)
    {
        result[sortedIndex++] = rightHalf[rightIndex++];
    }

    return result;
}