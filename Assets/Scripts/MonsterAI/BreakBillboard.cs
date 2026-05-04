using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBillboard : MonoBehaviour
{
    private float speed = 0;

    [Header("Referencias Externas")]
    [SerializeField] TrackAnim trackAnim;
    [SerializeField] Lights lights;

    public void SetSpeed(float speed2set)
    {
        speed = speed2set;
    }

    // Update is called once per frame
    void Update()
    {
        speed = trackAnim.GetSpeed();

        //Actualizar posición en función de la velocidad.
        transform.position -= new Vector3(0, 0, speed*10 * Time.deltaTime);
    }

    // Cuando entra en el Trigger vuelve a su posición inicial.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Break"))
        {
            if (lights.lightsOn)
            {
                Debug.Log($"[BreakBillboard] Las luces estaban encendidas, cambiar a estado IDLE");
            }
            else
            {
                Debug.Log($"[BreakBillboard] Las luces estaban apagadas, cambiar a estado IDLE y llamar a funcion de JUMPSCARE.");
            }

            transform.position += new Vector3(0, 0, 100);
        }
    }
}
