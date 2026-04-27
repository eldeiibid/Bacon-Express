using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItems : MonoBehaviour
{
    public void BuyItem(ItemData item)
    {
        if (SistemaMonedas.Instance.SpendCoins(item.cost))
        {
            Debug.Log("Has comprado: " + item.itemName);

             Inventory.Instance.AddItem(item);
        }
        else
        {
            Debug.Log("No puedes comprar este item");


        }
    }
}

