using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

// MUY IMPORTANTE ESTE SCRIPT para no perder en inventario entre una escena y otra. Créditos a mi chimichurri Yi.
public class KeepInventory : MonoBehaviour
{
    public static KeepInventory instance;
    public ItemData miItem1;
    public ItemData miItem2;
    public ItemData miItem3;
    public ItemData miItem4;
    public ItemData miItem5;
    void Start()
    {
        Inventory.Instance.AddItem(miItem1);
        Inventory.Instance.AddItem(miItem2);
        Inventory.Instance.AddItem(miItem3);
        Inventory.Instance.AddItem(miItem4);
        Inventory.Instance.AddItem(miItem5);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
