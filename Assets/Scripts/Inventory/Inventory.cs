using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<ItemData> items = new List<ItemData>();

    public event Action OnInventoryChanged;

    public int maxItems = 4;

    void Awake()
    {
        Instance = this;
    }

    public bool AddItem(ItemData item)
    {
        if (items.Count >= maxItems)
        {
            FeedbackUI.Instance.Show("Inventario lleno");

            return false;
        }

        items.Add(item);
        OnInventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItem(ItemData item)
    {
        items.Remove(item);
        OnInventoryChanged?.Invoke();
    }
}
