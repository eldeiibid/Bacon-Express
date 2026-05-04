using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Se encarga de la animación del árbol "infinito"
public class TreeScript : MonoBehaviour
{
    [SerializeField] TrackAnim trackAnim;
    private float speed = 0;

    public void SetSpeed(float speed2set)
    {
        speed = speed2set;
    }

    void Update()
    {
        //recibe la velocidad de las vías.
        speed = trackAnim.GetSpeed();

        //Actualizar posición en función de la velocidad.
        transform.position -= new Vector3(0, 0, speed*10 * Time.deltaTime);
    }

// Cuando entra en el Trigger vuelve a su posición inicial.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            transform.position += new Vector3(0, 0, 100);
        }
    }
}
