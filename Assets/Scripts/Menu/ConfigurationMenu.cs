using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

<<<<<<< Updated upstream
public class configurationMenu : MonoBehaviour
=======
[System.Serializable]
public class ConfigurationMenu : MonoBehaviour
>>>>>>> Stashed changes
{
    public GameObject panel;

    public Slider sliderGeneral;
    public Slider sliderMusica;
    public Slider sliderEfectos;

    public GameObject inventoryUI;
<<<<<<< Updated upstream
=======
    public const int HardModeBonus = 2;
    public bool diffOn = false;

    // Datos a guardar
    public HealthSystem healthSystem;
    public SistemaMonedas sistemaMonedas;
    public ConfigurationMenu configMenu;
    public Inventory inventory;
>>>>>>> Stashed changes

    void Start()
    {
        panel.SetActive(false);

        // Cargamos valores guardados
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
        Time.timeScale = 0; // Se supone que esto pausa el jeugo al abrir el men�
    }

    public void CerrarMenu()
    {
        panel.SetActive(false);
        inventoryUI.SetActive(true); // volvemos a mostrarlo
        Time.timeScale = 1; // despausamos el juego
    }

    public void IrAlInicio()
    {
        Time.timeScale = 1;
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

<<<<<<< Updated upstream
        // Aqu� debes referenciar tu AudioSource de m�sica
        // ejemplo:
=======
        // Aquí habrá que referenciar el AudioSource de música caundo lo tengamos
        // así:
>>>>>>> Stashed changes
        // musicaSource.volume = valor;
    }

    void CambiarVolumenEfectos(float valor)
    {
        PlayerPrefs.SetFloat("volEfectos", valor);

        // Igual que arriba, para efectos
        // efectosSource.volume = valor;
    }
<<<<<<< Updated upstream
=======
    private void OnHardModeToggleChanged(bool isOn)
    {
        if (isOn)
        {
            diffOn = true;
            multiplyAI.aiValue = Mathf.Min(multiplyAI.aiValue + HardModeBonus, 10);
            Debug.Log($"[HardMode] Modo difícil activado — aiValue: {multiplyAI.aiValue}");
        }
        else
        {
            diffOn = false;
            multiplyAI.aiValue = Mathf.Max(multiplyAI.aiValue - HardModeBonus, 1);
            Debug.Log($"[HardMode] Modo difícil desactivado — aiValue: {multiplyAI.aiValue}");
        }
    }
    public void SaveGame()
    {
        SaveGameData.SGameData(
            healthSystem,
            sistemaMonedas,
            configMenu,
            SceneManager.GetActiveScene(),
            inventory
        );

        Debug.Log("Partida guardada");
    }
>>>>>>> Stashed changes
}