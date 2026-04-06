using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<ItemData> items = new List<ItemData>();

    public event Action OnInventoryChanged;

    void Awake()
    {
        Instance = this;
    }

    public void AddItem(ItemData item)
    {
        items.Add(item);
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemData item)
    {
        items.Remove(item);
        OnInventoryChanged?.Invoke();
    }
}
