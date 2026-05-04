using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Controla la velocidad del tren (las vías y arbol en realidad) a través de los botones y actualiza el valor del velocímetro
public class SpeedControl : MonoBehaviour
{
    [SerializeField] Button speedUpButton;
    [SerializeField] Button speedDownButton;
    [SerializeField] TrackAnim trackAnim;
    [SerializeField] TextMeshProUGUI speedometer;

    // Start is called before the first frame update
    void Start()
    {
        speedUpButton.onClick.AddListener(AddSpeed);
        speedDownButton.onClick.AddListener(DecreaseSpeed);
        speedometer.text = "Speed: 0/3";
    }

    void AddSpeed()
    {
        float trackSpeed = trackAnim.GetSpeed();

        if (trackSpeed < 3) 
        {
            //subir velocidad de las vias
            trackSpeed +=1;
            trackAnim.SetSpeed(trackSpeed);

            //Actualizar velocimetro
            UpdateSpeedometer((int)trackSpeed);
        }
        
    }

    void DecreaseSpeed()
    {
        float trackSpeed = trackAnim.GetSpeed();

        if (trackSpeed > 0)
        {   
            //reducir velocidad de las vias
            trackSpeed -= 1;
            trackAnim.SetSpeed(trackSpeed);

            //Actualizar velocimetro
            UpdateSpeedometer((int)trackSpeed);
        } 

    }

    void UpdateSpeedometer(int newSpeed)
    {
        speedometer.text = "Speed: " + newSpeed + "/3";
    }

}
