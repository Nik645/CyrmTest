using System.Collections.Generic;

public static class Extensions
{
    public static ItemType GetNextItem(this List<ItemType> list, ItemType currentItem)
    {
        var index = list.IndexOf(currentItem);
        if (index < 0 || index + 1 >= list.Count)
        {
            return list[0];
        }

        return list[index + 1];
    }
}