using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// MUY IMPORTANTE ESTE SCRIPT para no perder en inventario entre una escena y otra. Cr�ditos a mi chimichurri Yi.
public class KeepInventory : MonoBehaviour
{
    public static KeepInventory instance;

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
