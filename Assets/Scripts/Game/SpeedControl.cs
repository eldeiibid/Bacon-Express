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
    [SerializeField] TreeScript treeScript;
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
        if (trackAnim.GetSpeed() < 3) trackAnim.SetSpeed(trackAnim.GetSpeed()+1);
        UpdateSpeedometer((int)trackAnim.GetSpeed());
        treeScript.SetSpeed(trackAnim.GetSpeed());
    }

    void DecreaseSpeed()
    {
        if (trackAnim.GetSpeed() > 0) trackAnim.SetSpeed(trackAnim.GetSpeed()-1);
        UpdateSpeedometer((int)trackAnim.GetSpeed());
        treeScript.SetSpeed(trackAnim.GetSpeed());
    }

    void UpdateSpeedometer(int newSpeed)
    {
        speedometer.text = "Speed: " + newSpeed + "/3";
    }

}
