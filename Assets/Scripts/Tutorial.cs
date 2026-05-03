using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject[] textObjects;
    public GameObject nextButton;
    public GameObject prevButton;

    private int currentIndex = 0;

    void Start()
    {
        UpdateUI();
    }

    public void NextText()
    {
        if (currentIndex < textObjects.Length - 1)
        {
            currentIndex++;
            UpdateUI();
        }
    }

    public void PreviousText()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateUI();
        }
    }

    public void FinishTutorial()
    {
        SceneManager.LoadScene(1);
    }

    void UpdateUI()
    {
        for (int i = 0; i < textObjects.Length; i++)
        {
            textObjects[i].SetActive(i == currentIndex);
        }

        prevButton.SetActive(currentIndex > 0);
        nextButton.SetActive(currentIndex < textObjects.Length - 1);
    }
}