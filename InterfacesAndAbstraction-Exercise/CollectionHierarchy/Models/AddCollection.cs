using CollectionHierarchy.Interfaces;

namespace CollectionHierarchy.Models;

public class AddCollection : StringCollection, IAdder
{

    public int Add(string item)
    {
        items.Add(item);
        return items.Count-1;
    }

    public string GetAdditionsString(string[] items)
    {
        int[] results = new int[items.Length];
        for(int i = 0; i < items.Length; i++)
        {
            results[i] = Add(items[i]);
        }
        return string.Join(" ", results);
    }
}
