using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainlessBillboard : MonoBehaviour
{
    private float speed = 0;

    [Header("Debug")]
    public bool hasAttacked = false;

    [Header("Referencias Externas")]
    [SerializeField] TrackAnim trackAnim;
    [SerializeField] Lights lights;
    [SerializeField] PainlessAI painlessAI;

    public void SetSpeed(float speed2set)
    {
        speed = speed2set;
    }

    // Update is called once per frame
    void Update()
    {
        speed = trackAnim.GetSpeed();

        //Actualizar posición en función de la velocidad.
        transform.position -= new Vector3(0, 0, speed*20 * Time.deltaTime);
    }

    // Cuando entra en el Trigger vuelve a su posición inicial.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Break"))
        {
            //comprueba si la luz de la cabina esta apagada.
            if (!lights.lightsOn)
            {
                Debug.Log($"[BreakBillboard] Las luces estaban apagadas, cambiar a estado IDLE");
                painlessAI.ChangeState(PainlessAI.EnemyState.Idle);
                transform.position += new Vector3(0, 0, 150);        

            }
            else
            {
                if (!hasAttacked)
                {
                    hasAttacked = true;
                    Debug.Log($"[BreakBillboard] Las luces estaban encendidas, llamando a funcion de JUMPSCARE.");
                    painlessAI.ChangeState(PainlessAI.EnemyState.Idle);
                    painlessAI.Jumpscare();
                    transform.position += new Vector3(0, 0, 150);
                
                }

            }

        }
    }
}
