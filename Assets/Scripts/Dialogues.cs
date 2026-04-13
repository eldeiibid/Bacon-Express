using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


// Este script maneja todo el sistema de diálogos

public class DialogueSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject actionBox;

    [Header("Opciones")]
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private TMP_Text[] optionTexts;

    [Header("Configuración")]
    [SerializeField] private float typingTime = 0.05f;

    [Header("Diálogo")]
    [SerializeField] private DialogueNode[] nodes;

    private int currentNodeIndex;
    private bool dialogueStarted;
    private bool isTyping;

    private HashSet<int> visitedNodes = new HashSet<int>();

    //Aquí inicia el diálogo, al pulsar el botón "Hablar"
    public void StartDialogue()
    {
        dialogueStarted = true;
        dialoguePanel.SetActive(true);
        actionBox.SetActive(false);

        currentNodeIndex = 0;
        visitedNodes.Clear();

        HideOptions();
        StartCoroutine(ShowNode());
    }

    // Esto hace que el texto se vaya escribiendo caracter a caracter
    private IEnumerator ShowNode()
    {
        isTyping = true;
        dialogueText.text = "";

        string line = nodes[currentNodeIndex].dialogueText;

        foreach (char ch in line)
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }

        isTyping = false;

        if (HasAvailableOptions())
        {
            ShowOptions();
        }
    }

    // si el nodo en el que estamos va seguido de opciones, las mostramos
    private void ShowOptions()
    {
        DialogueOption[] options = nodes[currentNodeIndex].options;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < options.Length && IsOptionUnlocked(options[i]))
            {
                optionButtons[i].gameObject.SetActive(true);
                optionTexts[i].text = options[i].text;

                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectOption(index));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

   
    private void HideOptions()
    {
        foreach (Button btn in optionButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    //seleccionamos una opción y se muestra el siguiente texto
    private void SelectOption(int optionIndex)
    {
        DialogueOption selected = nodes[currentNodeIndex].options[optionIndex];

        if (selected.endsDialogue)
        {
            EndDialogue();
            return;
        }

        currentNodeIndex = selected.nextNodeIndex;

        HideOptions();
        StartCoroutine(ShowNode());
    }

    // esto es para hacer ramificaciones en los diálogos SI Y SOLO SI ya se han visto nodos específicos.
    private bool IsOptionUnlocked(DialogueOption option)
    {
        switch (option.condition)
        {
            case ConditionType.None:
                return true;

            case ConditionType.SeenNode:
                return visitedNodes.Contains(option.requiredNode);

            default:
                return true;
        }
    }

    // acaba el diálogo y se vuelven a mostrar los botones "Hablar" e "Irse" 
    private void EndDialogue()
    {
        dialogueStarted = false;
        dialoguePanel.SetActive(false);
        actionBox.SetActive(true);
        dialogueText.text = "";
        HideOptions();
    }

    // Con el update, al hacer click se avanza
    private void Update()
    {
        if (!dialogueStarted) return;

        if (Input.GetButtonDown("Fire1"))
        {
            // Si está escribiendo -> completa el texto
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = nodes[currentNodeIndex].dialogueText;
                isTyping = false;

                if (HasAvailableOptions())
                    ShowOptions();

                return;
            }

            // Si NO hay opciones y el texto ya está completo -> avanza
            if (!HasAvailableOptions())
            {
                int next = nodes[currentNodeIndex].nextNodeIndex;

                if (next != -1)
                {
                    currentNodeIndex = next;
                    StartCoroutine(ShowNode());
                }
                else
                {
                    EndDialogue();
                }
            }

        }


       
    }

    // Esto checa si a ese nodo le siguen opciones 
    private bool HasAvailableOptions()
    {
        DialogueOption[] options = nodes[currentNodeIndex].options;

        if (options == null || options.Length == 0)
            return false;

        foreach (var option in options)
        {
            if (IsOptionUnlocked(option))
                return true;
        }

        return false;
    }

    // Este método estaba para pasar automáticamente al nodo siguiente, pero al final no se usa. Lo dejo por si queremos cambiarlo o por si luego resulta útil para otra cosa.
    private void GoToNextNode()
    {
        int next = nodes[currentNodeIndex].nextNodeIndex;

        if (next != -1)
        {
            currentNodeIndex = next;
            StartCoroutine(ShowNode());
        }
        else
        {
            EndDialogue();
        }
    }

    // Para cambiar de escena al pulsar "Irse"
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }

}


// Vale, esto son las calses Nodo y Opción de los diálogos
[System.Serializable]
public class DialogueNode
{
    [TextArea(3, 5)]
    public string dialogueText;

    public DialogueOption[] options;

    [Header("Siguiente nodo automático")]
    public int nextNodeIndex = -1;
}

[System.Serializable]
public class DialogueOption
{
    public string text;
    public int nextNodeIndex;

    
    public ConditionType condition;
    public int requiredNode;

    [Header("Finalizar diálogo")]
    public bool endsDialogue;


}

// Esto es para lo de las ramificaciones cuando ya has pasado por nodos específicos
public enum ConditionType
{
    None,
    SeenNode
}