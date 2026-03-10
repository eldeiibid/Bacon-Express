using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackAnim : MonoBehaviour
{

    public Material trackMat;
    public float speed = 0;
    public float Yoffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        trackMat.SetTextureOffset("_MainTex", new Vector2(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Yoffset = (Yoffset + (speed * Time.deltaTime)) % 10;
        trackMat.SetTextureOffset("_MainTex", new Vector2(Yoffset, 0));
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float speed2set)
    {
        speed = speed2set;
    }
}
