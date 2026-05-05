using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


//Controla la distancia necesaria para llegar al siguiente nivel. Además dispone en pantalla la restante para llegar al cambio de pantalla.
public class DistanceControl : MonoBehaviour
{
    [Header("Assets Requeridos")]
    [SerializeField] TrackAnim trackAnim;
    [SerializeField] TextMeshProUGUI distanceLeftDisplay;
    [SerializeField] SistemaMonedas sistemaMonedas;
    public GameObject InventoryUI;
    [Header("Variables")]
    //escena a la que cambiar al terminar de recorrer la distancia.
    public int escena;
    //LA DISTANCIA A RECORRER PARA PASAR A LA SIGUIENTE ESCENA EN METROS.
    public int INITIAL_DISTANCE = 500; //Valor recomendado: 1000m, tambien depende del nivel.
    float distance_done;

    void Start()
    {
        distance_done = 0;
        distanceLeftDisplay.text = INITIAL_DISTANCE + " m";
    }

    void Update()
    {
        distance_done += trackAnim.GetSpeed() * Time.deltaTime;
        int distance_left = INITIAL_DISTANCE - (int)distance_done;

        CheckDistanceLeft(distance_left);
        UpdateDistanceLeftDisplay(distance_left);
    }

    void UpdateDistanceLeftDisplay(int value)
    {
        distanceLeftDisplay.text = value + " m";
    }

    void CheckDistanceLeft(int value)
    {   
        
        if (value <= 0)
        {
            //Añade mondedas al terminar el nivel, la paga depende de la distancia recorrida.
            sistemaMonedas.AddCoins(INITIAL_DISTANCE / 10);
            //Cambiar por el valor de la escena de la estación
            SceneManager.LoadScene(escena);
            //InventoryUI.SetActive(false);
        }

    }

    public int getDistanceDone()
    {
        return (int) distance_done;
    }
}