using System;
using System.Linq;
using System.Collections.Generic;


int n = int.Parse(Console.ReadLine());

List<int> people = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToList();

List<int> firstTeam = new List<int>();
List<int> secondTeam = new List<int>();
int counter = 0;

List<int> biggestFirst = FindBiggestFirst();
List<int> biggestSecond = FindBiggestSecond();

if(biggestSecond.Count() > biggestFirst.Count())
{
    //second leads

    for(int i = 0; i < people.Count; i++)
    {
        if (biggestSecond.Contains(people[i]))
        {
            secondTeam.Add(people[i]);
        }
        else if (biggestFirst.Contains(people[i]))
        {
            firstTeam.Add(people[i]);
        }
        else
        {
            counter++;
        }

    }

}
else
{
    for (int i = 0; i < people.Count; i++)
    {
        
        if (biggestFirst.Contains(people[i]))
        {
            firstTeam.Add(people[i]);
        }
        else if (biggestSecond.Contains(people[i]))
        {
            secondTeam.Add(people[i]);
        }
        else
        {
            counter++;
        }

    }
}
Console.WriteLine(counter);






List<int> FindBiggestFirst()
{
    int maxCount = 0;
    int maxStartIndex = 0;
    for(int i = 0; i < people.Count; i++)
    {
        int num = people[i];
        int currentMaxCount = 0;
        for(int j = i+1; j < people.Count; j++)
        {
            if (people[j] > num)
            {
                currentMaxCount++;
            }
        }
        if(currentMaxCount > maxCount)
        {
            maxCount = currentMaxCount;
            maxStartIndex = i;
        }
    }

    List<int> result = new List<int> { people[maxStartIndex] };
    for(int i = 0; i < people.Count; i++)
    {
        if (people[i] > result[result.Count-1])
        {
            result.Add(people[i]);
        }
    }
    return result;
}

List<int> FindBiggestSecond()
{
    
   
}




