using UnityEngine;
using UnityEngine.UI;

public class CamaraMovement : MonoBehaviour
{
    // Botones
    [SerializeField] Button botonFrenteIzquierda;
    [SerializeField] Button botonFrenteDerecha;

    [SerializeField] Button botonDerechaFrente;
    [SerializeField] Button botonDerechaAtras;

    [SerializeField] Button botonIzquierdaFrente;
    [SerializeField] Button botonIzquierdaAtras;

    [SerializeField] Button botonAtrasIzquierda;
    [SerializeField] Button botonAtrasDerecha;

    // Canvas
    [SerializeField] GameObject canvasFrontal;
    [SerializeField] GameObject canvasIzquierda;
    [SerializeField] GameObject canvasDerecha;
    [SerializeField] GameObject canvasTrasero;

    // 0 = izquierda, 1 = frontal, 2 = derecha, 3 = atrás
    int ladoActual = 1;

    void Start()
    {
        botonFrenteIzquierda.onClick.AddListener(GirarIzquierda);
        botonFrenteDerecha.onClick.AddListener(GirarDerecha);

        botonDerechaFrente.onClick.AddListener(GirarIzquierda);
        botonDerechaAtras.onClick.AddListener(GirarDerecha);

        botonIzquierdaFrente.onClick.AddListener(GirarDerecha);
        botonIzquierdaAtras.onClick.AddListener(GirarIzquierda);

        botonAtrasIzquierda.onClick.AddListener(GirarDerecha);
        botonAtrasDerecha.onClick.AddListener(GirarIzquierda);

        MostrarCanvas();
    }

    void GirarIzquierda()
    {
        transform.Rotate(0, -90, 0);

        ladoActual--;
        if (ladoActual < 0) ladoActual = 3;

        MostrarCanvas();
    }

    void GirarDerecha()
    {
        transform.Rotate(0, 90, 0);

        ladoActual++;
        if (ladoActual > 3) ladoActual = 0;

        MostrarCanvas();
    }

    void MostrarCanvas()
    {
        canvasIzquierda.SetActive(ladoActual == 0);
        canvasFrontal.SetActive(ladoActual == 1);
        canvasDerecha.SetActive(ladoActual == 2);
        canvasTrasero.SetActive(ladoActual == 3);
    }
}