using UnityEngine;

public class LoadDataInScene : MonoBehaviour
{
    public HealthSystem sistemaVida;
    public SistemaMonedas sistemaMonedas;
    public ConfigurationMenu menuConfig;
    public Inventory inventario;

    void Start()
    {
        GameData data = GameManager.Instance.loadedData;

        if (data != null)
        {
            sistemaVida.health = data.health;
            sistemaMonedas.coins = data.coins;
            menuConfig.diffOn = data.difficulty;
            inventario = data.inventory;

            GameManager.Instance.loadedData = null;
        }
    }
}