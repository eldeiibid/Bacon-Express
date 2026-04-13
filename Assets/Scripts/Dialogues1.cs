using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;



//Créditos a: @indierama en Youtube - "Sistema de DIÁLOGO BÁSICO en Unity"



public class Dialogues1 : MonoBehaviour
{

    //PARA LA INTERFAZ
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject actionBox;

    //PARA EL TEXTO Y EL NOMBRE DEL PERSONAJE
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(5, 7)] private string[] dialogueLines;

    private bool dialogueStarted;
    public bool dialogoTerminado = false;
    private int lineIndex;
    private float typingTime = 0.05f;

    public void StartDialogue()
    {
        dialogueStarted = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
        actionBox.SetActive(false);
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());

        }
        else
        {
            dialogueStarted = false;
            dialogoTerminado = true;
            dialoguePanel.SetActive(false);
            dialogueText.text = string.Empty;
        }
    }


    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;


        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!dialogueStarted)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
                actionBox.SetActive(true);
              
            }
        }
    }
}
