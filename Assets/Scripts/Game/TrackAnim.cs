using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//controla la animación del raíl.
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
        //La textura se va desplazando en Y en función de la velocidad y deltaTime.
        Yoffset = (Yoffset + (speed * Time.deltaTime*2)) % 10;
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
