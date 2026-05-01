using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void jugar()
    {
        SceneManager.LoadScene("MainScene");
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
}
