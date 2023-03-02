using System;

using CollectionHierarchy.Models;
using CollectionHierarchy;

namespace CollectionHierarchy;

internal class Program
{
    static void Main(string[] args)
    {
        AddCollection addCollection = new AddCollection();
        AddRemoveCollection addRemoveCollection = new AddRemoveCollection();
        MyList myList = new MyList();

        string[] elements = Console.ReadLine()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);

        int removals = int.Parse(Console.ReadLine());

        Console.WriteLine(addCollection.GetAdditionsString(elements));
        Console.WriteLine(addRemoveCollection.GetAdditionsString(elements));
        Console.WriteLine(myList.GetAdditionsString(elements));
        Console.WriteLine(addRemoveCollection.GetRemovalStrings(removals));
        Console.WriteLine(myList.GetRemovalStrings(removals));
   
    }
}