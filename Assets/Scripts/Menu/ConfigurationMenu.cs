using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class configurationMenu : MonoBehaviour
{
    public GameObject panel;

    public Slider sliderGeneral;
    public Slider sliderMusica;
    public Slider sliderEfectos;

    public GameObject inventoryUI;

    void Start()
    {
        panel.SetActive(false);

        // Cargar valores guardados
        sliderGeneral.value = PlayerPrefs.GetFloat("volGeneral", 1f);
        sliderMusica.value = PlayerPrefs.GetFloat("volMusica", 1f);
        sliderEfectos.value = PlayerPrefs.GetFloat("volEfectos", 1f);

        // Listeners
        sliderGeneral.onValueChanged.AddListener(CambiarVolumenGeneral);
        sliderMusica.onValueChanged.AddListener(CambiarVolumenMusica);
        sliderEfectos.onValueChanged.AddListener(CambiarVolumenEfectos);
    }

    public void AbrirMenu()
    {
        panel.SetActive(true);
        inventoryUI.SetActive(false); // ocultamos el inventario
        Time.timeScale = 0; // Se supone que esto pausa el jeugo al abrir el menú
    }

    public void CerrarMenu()
    {
        panel.SetActive(false);
        inventoryUI.SetActive(true); // volvemos a mostrarlo
        Time.timeScale = 1; // despausamos el juego
    }

    public void IrAlInicio()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void CambiarVolumenGeneral(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("volGeneral", valor);
    }

    void CambiarVolumenMusica(float valor)
    {
        PlayerPrefs.SetFloat("volMusica", valor);

        // Aquí debes referenciar tu AudioSource de música
        // ejemplo:
        // musicaSource.volume = valor;
    }

    void CambiarVolumenEfectos(float valor)
    {
        PlayerPrefs.SetFloat("volEfectos", valor);

        // Igual que arriba, para efectos
        // efectosSource.volume = valor;
    }
}