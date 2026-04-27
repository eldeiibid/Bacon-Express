using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{
    public Image buttonImage;
    public Sprite newSprite;

    string savePath;

    void Awake()
    {
<<<<<<< Updated upstream
        SceneManager.LoadScene("MainScene");
=======
        savePath = Path.Combine(Application.persistentDataPath, "gameData.save");
    }

    // NUEVA PARTIDA
    public void NuevaPartida()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save eliminado");
        }

        SceneManager.LoadScene("Level_1");
>>>>>>> Stashed changes
    }

    // CONTINUAR PARTIDA
    public void ContinuarPartida()
    {
        GameData data = SaveGameData.LoadGameData();

        if (data != null)
        {
            // Cargar escena guardada
            SceneManager.LoadScene(data.sceneName);

        }
        else
        {
            Debug.LogWarning("No hay datos guardados");
        }
    }

    public void salir()
    {
        Application.Quit();
    }

    public void ChangeButtonImage()
    {
        if (buttonImage != null && newSprite != null)
        {
            buttonImage.sprite = newSprite;
        }
    }
}