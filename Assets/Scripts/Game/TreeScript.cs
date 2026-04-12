using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    private float speed = 0;

    public void SetSpeed(float speed2set)
    {
        speed = speed2set;
    }

    void Update()
    {
        transform.position -= new Vector3(0, 0, speed*10 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            transform.position += new Vector3(0, 0, 100);
        }
    }
}
