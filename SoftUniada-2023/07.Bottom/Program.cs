using System;
using System.Linq;
using System.Collections.Generic;

int pointsNumber = int.Parse(Console.ReadLine());
int ribs = int.Parse(Console.ReadLine());

List<Point> points = new List<Point>();
for(int i = 0; i < pointsNumber; i++)
{
    points.Add(new Point());
}

for(int i = 0; i < ribs; i++)
{
    int[] inputArgs = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();

    int start = inputArgs[0];
    int end = inputArgs[1];
    int weight = inputArgs[2];

    Rib nextRib = new Rib(points[start], points[end], weight);
}

int lowest = points
    .Select(p => p.LowestRib).Max();
Console.WriteLine(lowest + 1);


class Point
{
    public Point()
    {
        Ribs = new List<Rib>();
    }
    public List<Rib> Ribs { get; set; }
    public int LowestRib { get { return Ribs.MinBy(n => n.Weight).Weight; } }
}


class Rib
{
    public Rib(Point a, Point b, int weight)
    {
        a.Ribs.Add(this);
        b.Ribs.Add(this);
        Weight = weight;
    }
    public int Weight { get; set; }

}
