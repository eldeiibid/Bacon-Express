using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    public List<ItemData> allItems;

    private Dictionary<string, ItemData> itemDictionary;

    private void Awake()
    {
        Instance = this;

        itemDictionary = new Dictionary<string, ItemData>();

        foreach (var item in allItems)
        {
            itemDictionary[item.id] = item;
        }
    }

    public ItemData GetItemByID(string id)
    {
        if (itemDictionary.ContainsKey(id))
            return itemDictionary[id];

        Debug.LogError("No existe item con ID: " + id);
        return null;
    }
}