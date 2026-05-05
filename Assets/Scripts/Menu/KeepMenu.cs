using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// MUY IMPORTANTE ESTE SCRIPT para no perder en inventario entre una escena y otra. Créditos a mi chimichurri Yi.
public class KeepMenu : MonoBehaviour
{
    public static KeepMenu instance;

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
