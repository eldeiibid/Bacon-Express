using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGameData 
{
    public static void SGameData(HealthSystem sistemaVida, SistemaMonedas sistemaMonedas, ConfigurationMenu menuConfig, Scene escena, Inventory inventario)
    {
        string dataPath = Path.Combine(Application.persistentDataPath, "gameData.save");

        try
        {
            GameData gameData = new GameData(sistemaVida, sistemaMonedas, menuConfig, escena, inventario);

            using (FileStream fileStream = new FileStream(dataPath, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fileStream, gameData);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());
        }


        Debug.Log("GUARDANDO EN: " + dataPath);
        Debug.Log("Antes de guardar existe: " + File.Exists(dataPath));
    }

    public static GameData LoadGameData()
    {
        string dataPath = Path.Combine(Application.persistentDataPath, "gameData.save");

        if (File.Exists(dataPath))
        {
            try
            {
                using (FileStream fileStream = new FileStream(dataPath, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    GameData gameData = (GameData)bf.Deserialize(fileStream);
                    return gameData;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error al cargar la partida: " + e.Message);
                return null;
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de guardado.");
            return null;
        }
    }
}
