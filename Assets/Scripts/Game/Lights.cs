using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lights : MonoBehaviour
{
    [SerializeField] Button botonLuces;
    [SerializeField] Image imagenLuces;
    public bool lightsOn = false;

    void Start()
    {
        botonLuces.onClick.AddListener(ActualizarLuces);

        imagenLuces.enabled = false;

    }

    void ActualizarLuces()
    {
        lightsOn = !lightsOn;
        imagenLuces.enabled = lightsOn;
    }
}