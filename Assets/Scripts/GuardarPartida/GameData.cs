using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public int health;
    public int coins;
    public bool difficulty;

    public string sceneName;

    public Inventory inventory;

    public GameData(HealthSystem sistemaVida, SistemaMonedas sistemaMonedas, ConfigurationMenu menuConfig, Scene escena, Inventory inventario)
    {
        health = sistemaVida.health;
        coins = sistemaMonedas.coins;
        difficulty = menuConfig.diffOn;
        sceneName = escena.name;
        inventory = inventario;
    }
   
}
