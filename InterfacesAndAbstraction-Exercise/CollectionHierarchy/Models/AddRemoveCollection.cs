using CollectionHierarchy.Interfaces;

namespace CollectionHierarchy.Models;

public class AddRemoveCollection : StringCollection, IAdder, IRemover
{
    public int Add(string item)
    {
        items.Insert(0, item);
        return 0;
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
        for (int i = 0; i <removals; i++)
        {
            results[i] = Remove();
        }
        return string.Join(" ", results);
    }

    public string Remove()
    {
        string itemToRemove = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return itemToRemove;
        
    }
}
