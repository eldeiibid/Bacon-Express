using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        if (trackAnim.GetSpeed() < 3) trackAnim.SetSpeed(trackAnim.GetSpeed()+1);
        UpdateSpeedometer((int)trackAnim.GetSpeed());
    }

    void DecreaseSpeed()
    {
        if (trackAnim.GetSpeed() > 0) trackAnim.SetSpeed(trackAnim.GetSpeed()-1);
        UpdateSpeedometer((int)trackAnim.GetSpeed());
    }

    void UpdateSpeedometer(int newSpeed)
    {
        speedometer.text = "Speed: " + newSpeed + "/3";
    }

}
