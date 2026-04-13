using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Controla la distancia necesaria para llegar al siguiente nivel. Además dispone en pantalla la restante para llegar al cambio de pantalla.
public class DistanceControl : MonoBehaviour
{
    [SerializeField] TrackAnim trackAnim;
    [SerializeField] TextMeshProUGUI distanceLeftDisplay;
    public GameObject InventoryUI;

    public const int INITIAL_DISTANCE = 1000;
    float distance_done;

    void Start()
    {
        distance_done = 0;
        distanceLeftDisplay.text = INITIAL_DISTANCE + " m";
    }

    void Update()
    {
        distance_done += trackAnim.GetSpeed() * Time.deltaTime;
        int distance_left = INITIAL_DISTANCE - (int)distance_done;

        CheckDistanceLeft(distance_left);
        UpdateDistanceLeftDisplay(distance_left);
    }

    void UpdateDistanceLeftDisplay(int value)
    {
        distanceLeftDisplay.text = value + " m";
    }

    void CheckDistanceLeft(int value)
    {   
        //Cambiar por el valor de la escena de la estación
        if (value <= 0)
        {
            SceneManager.LoadScene("MainMenu");
            InventoryUI.SetActive(false);
        }

    }
}