using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Controla el sistema de salud del juego

[System.Serializable]
public class HealthSystem : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] GameObject Heart1; 
    [SerializeField] GameObject Heart2; 
    [SerializeField] GameObject Heart3;
    [SerializeField] GameObject glass;
    [SerializeField] GameObject shattered_glass;
    [SerializeField] GameObject very_shattered_glass;

    [Header("Health")]
    [Range(0, 3)]
    public int health = 3;

    void Update()
    {

        if (health > 2)
        {
            Heart1.SetActive(true);
            Heart2.SetActive(true);
            Heart3.SetActive(true);

            glass.SetActive(true);
            shattered_glass.SetActive(false);
            very_shattered_glass.SetActive(false);
        }
        else if (health > 1)
        {
            Heart1.SetActive(true);
            Heart2.SetActive(true);
            Heart3.SetActive(false);

            glass.SetActive(false);
            shattered_glass.SetActive(true);
            very_shattered_glass.SetActive(false);
        }
        else if (health > 0)
        {
            Heart1.SetActive(true);
            Heart2.SetActive(false);
            Heart3.SetActive(false);

            glass.SetActive(false);
            shattered_glass.SetActive(false);
            very_shattered_glass.SetActive(true);
        }
        else
        {
            Heart1.SetActive(false);
            Heart2.SetActive(false);
            Heart3.SetActive(false);

            glass.SetActive(false);
            shattered_glass.SetActive(false);
            very_shattered_glass.SetActive(false);
        }

    }

    public void decreaseHealth()
    {
        health--;
    }

    public void increaseHealth()
    {
        health++;
    }
}