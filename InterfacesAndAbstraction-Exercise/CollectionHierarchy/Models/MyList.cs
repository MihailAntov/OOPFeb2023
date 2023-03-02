using CollectionHierarchy.Interfaces;

namespace CollectionHierarchy.Models;

public class MyList : StringCollection, IAdder, IRemover
{
    public int Add(string item)
    {
        items.Insert(0, item);
        return 0;
    }

    public string Remove()
    {
        string itemToRemove = items[0];
        items.RemoveAt(0);
        return itemToRemove;
    }

    public string GetAdditionsString(string[] items)
    {
        int[] results = new int[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            results[i] = Add(items[i]);
        }
        return string.Join(" ", results);
    }

    public string GetRemovalStrings(int removals)
    {
        string[] results = new string[removals];
        for (int i = 0; i < removals; i++)
        {
            results[i] = Remove();
        }
        return string.Join(" ", results);
    }

    public int Used { get { return items.Count; } }
}
