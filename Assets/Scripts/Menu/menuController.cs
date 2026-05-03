using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void jugar()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // Update is called once per frame
    public void salir()
    {
        Application.Quit();
    }

    public Image buttonImage;
    public Sprite newSprite;

    public void ChangeButtonImage()
    {
        if(buttonImage != null && newSprite != null)
        {
            buttonImage.sprite = newSprite;
        }
    }

    public void ContinuarPartida()
    {
        GameData data = SaveGameData.LoadGameData();

        if (data != null)
        {
            // Cargar escena guardada
            SceneManager.LoadScene(data.sceneName);

            // Guardamos los datos en algún sitio temporal
            GameManager.Instance.loadedData = data;
        }
        else
        {
            Debug.Log("No hay partida guardada");
        }
    }

    public void NuevaPartida()
    {
        string dataPath = Path.Combine(Application.persistentDataPath, "gameData.save");

        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
        }

        SceneManager.LoadScene("Tutorial");
    }
}
