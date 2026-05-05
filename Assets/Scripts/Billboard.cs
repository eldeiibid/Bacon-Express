using UnityEngine;

// Funccion que hace que los planos 3D (billoboards) miren siempre a la camara.
public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
