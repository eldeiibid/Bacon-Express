using TMPro;
using UnityEngine;
using System.Collections;


// Este script es para que si intentas hacer una opción con un objeto y no se puede  (por ejemplo intentar usar la pistola vacía)
// pues  te salte un panelito diciendo "no puedes payaseteee que te has colao"

public class FeedbackUI : MonoBehaviour
{
    public static FeedbackUI Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text messageText;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(message));
    }

    IEnumerator ShowRoutine(string message)
    {
        panel.SetActive(true);
        messageText.text = message;

        yield return new WaitForSeconds(2f);

        panel.SetActive(false);
    }
}