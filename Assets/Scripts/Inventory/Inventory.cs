using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Localization.Settings;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<ItemData> items = new List<ItemData>();

    public List<string> itemIDs = new List<string>();

    public event Action OnInventoryChanged;

    public int maxItems = 4;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NotifyInventoryChanged()
    {
        OnInventoryChanged?.Invoke();
    }
    public bool CanAddItem()
    {
        return items.Count < maxItems;
    }

    public bool AddItem(ItemData item)
    {
        if (items.Count >= maxItems)
        {
            //empezamos a usar valores de la tabla csv
            FeedbackUI.Instance.Show(LocalizationSettings.StringDatabase.GetLocalizedString("FEEDBACK.FULL_INVENTORY"));

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
