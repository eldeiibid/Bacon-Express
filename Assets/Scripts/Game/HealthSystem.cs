using System;
using UnityEngine;
using UnityEngine.SceneManagement;


//Controla el sistema de salud del juego
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
    private float time = 0.5f;

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

            GameOver();
        }

    }

    private void GameOver()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            SceneManager.LoadScene("EndScene");
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