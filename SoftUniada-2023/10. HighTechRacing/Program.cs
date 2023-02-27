using System;
using System.Linq;
using System.Collections.Generic;

int townCount = int.Parse(Console.ReadLine());
List<Town> towns = new List<Town>();
int roadCount = int.Parse(Console.ReadLine());

for(int i = 0; i < townCount; i ++)
{
    towns.Add(new Town());
}

for(int i = 0; i < roadCount; i++)
{
    int[] roadArgs = Console.ReadLine()
        .Split(" ")
        .Select(int.Parse)
        .ToArray();

    int startIndex = roadArgs[0];
    int endIndex = roadArgs[1];
    int length = roadArgs[0];

    Town startTown = towns[startIndex];
    Town endTown = towns[endIndex];

    Road road = new Road(startIndex, endIndex, length, towns);
    road.Start = startTown;
    road.End = endTown;
    startTown.Roads.Add(road);
    endTown.Roads.Add(road);
}




class Town
{
    public Town()
    {
        Roads = new List<Road>();
    }
    public List<Road> Roads { get; set; }
}

class Road
{
    public Road(int start, int end, int length, List<Town> towns)
    {
        Length = length;
        Start = towns[start];
        End = towns[end];

    }
    public Town Start { get; set; }
    public Town End { get; set; }
    public int Length { get; set; }
}