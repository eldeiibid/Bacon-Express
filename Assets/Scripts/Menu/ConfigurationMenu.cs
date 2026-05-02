using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigurationMenu : MonoBehaviour
{
    public GameObject panel;

    public Slider sliderGeneral;
    public Slider sliderMusica;
    public Slider sliderEfectos;
    [SerializeField] Toggle hardModeToggle;
    [SerializeField] MultiplyAI multiplyAI;

    public GameObject inventoryUI;
    public const int HardModeBonus = 2;

    public bool diffOn = false;


    // DATOS PARA GAURDAR PARTIDA
    public HealthSystem sistemaVida;
    public SistemaMonedas sistemaMonedas;
    public ConfigurationMenu menuConfig;
    public Inventory inventario;


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
        hardModeToggle.onValueChanged.AddListener(OnHardModeToggleChanged);
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
        Time.timeScale = 1; // despausamos el juego
        SceneManager.LoadScene("MainMenu");
    }
    public void Guardar()
    {
        Scene escenaActual = SceneManager.GetActiveScene();

        SaveGameData.SGameData(
            sistemaVida,
            sistemaMonedas,
            menuConfig,
            escenaActual,
            inventario
        );

        Debug.Log("Partida guardada");
    }

    void CambiarVolumenGeneral(float valor)
    {
        AudioListener.volume = valor * 0.1f;
        PlayerPrefs.SetFloat("volGeneral", valor);
    }

    void CambiarVolumenMusica(float valor)
    {
        PlayerPrefs.SetFloat("volMusica", valor);

        // Aqu� debes referenciar tu AudioSource de m�sica
        // ejemplo:
        // musicaSource.volume = valor;
    }

    void CambiarVolumenEfectos(float valor)
    {
        PlayerPrefs.SetFloat("volEfectos", valor);

        // Igual que arriba, para efectos
        // efectosSource.volume = valor;
    }
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
}