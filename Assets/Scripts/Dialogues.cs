using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


// Este script maneja todo el sistema de diálogos, es una movida y ha sido un lío horrible escribirlo.
// Sospecho que hay ahora mismo cosas que están duplicadas y que son redundantes, pero me da miedo ponerme a tocar porque no lo quiero romper.
// También hay algunas funcionalidades que debería haber puesto en un script aparte, pero todo eso ya lo haré. De momento con que funcione me siento satisfecha.
// Lo de los nodos es una movida que flipas, tanto aquí en el script como en el editor de Unity. Y aunque le he puesto cabeceras a lso apartados se ven cortadas, pero no sé como ponerlas bien.

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

<<<<<<< Updated upstream
=======

    public int escena;

>>>>>>> Stashed changes
    private int currentNodeIndex;
    private bool dialogueStarted;
    private bool isTyping;


    private HashSet<int> visitedNodes = new HashSet<int>();


    //Aquí inicia el diálogo, al pulsar el botón "Hablar". Se cierra el inventario y se habre una caja de texto.
    public void StartDialogue()
    {
        InventoryUI.Instance.CloseUI();

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
        visitedNodes.Add(currentNodeIndex);

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

    // si el nodo en el que estamos va seguido de opciones, las mostramos. Depende de la opción que elijamos pues compras un Item u otro (obviamente)
    private void ShowOptions()
    {
        DialogueOption[] options = nodes[currentNodeIndex].options;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < options.Length && IsOptionUnlocked(options[i]))
            {
                optionButtons[i].gameObject.SetActive(true);

                DialogueOption option = options[i];

                // ESTO ES PARA LOS ITEMS QUE SE PUEDEN COMPRAR
                if (option.itemToBuy != null)
                {
                    optionTexts[i].text = option.itemToBuy.itemName + " - " + option.itemToBuy.cost + "$";
                }
                else
                {
                    optionTexts[i].text = option.text;
                }

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

    //seleccionamos una opción y se muestra el siguiente texto, que se elije en el editor de Unity, no aquí, claro.
    private void SelectOption(int optionIndex)
    {
        DialogueOption selected = nodes[currentNodeIndex].options[optionIndex];

        // Si es opción de compra (porque hay una que no lo es, la de "No me interesa nada") pues hay que mirar las monedillas que tienes y ver si puedes.
        if (selected.itemToBuy != null)
        {
            int cost = selected.itemToBuy.cost;

            // 1. Inventario lleno
            if (!Inventory.Instance.CanAddItem())
            {
                Debug.Log("Inventario lleno");
                GoToNoSpaceNode(selected);
                return;
            }

            // 2. Dinero insuficiente
            if (!SistemaMonedas.Instance.SpendCoins(cost))
            {
                Debug.Log("No tienes suficiente dinero");
                GoToNoCoinsNode(selected);
                return;
            }

            // 3. Compra válida
            Inventory.Instance.AddItem(selected.itemToBuy);
            Debug.Log("Comprado: " + selected.itemToBuy.itemName);
        }

        // Fin de diálogo
        if (selected.endsDialogue)
        {
            EndDialogue();
            return;
        }

        // Avanzar diálogo
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

    // acaba el diálogo y se vuelven a mostrar los botones "Hablar" e "Irse", la caja de dialogo se cierra y vuelve a aparecer el inventario
    private void EndDialogue()
    {
        dialogueStarted = false;
        dialoguePanel.SetActive(false);
        actionBox.SetActive(true);
        dialogueText.text = "";
        HideOptions();
        InventoryUI.Instance.ShowUI();
    }

    private void GoToNoSpaceNode(DialogueOption option)
    {
        if (option.noSpaceIndex != -1)
        {
            currentNodeIndex = option.noSpaceIndex;
            HideOptions();
            StartCoroutine(ShowNode());
        }
    }
    private void GoToNoCoinsNode(DialogueOption option)
    {
        if (option.noCoindeIndex != -1)
        {
            currentNodeIndex = option.noCoindeIndex;
            HideOptions();
            StartCoroutine(ShowNode());
        }
    }

    // Con el update, al hacer click izquierdo se avanza el diálogo.
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

    // Esto checa si a ese nodo le siguen opciones, porque hay nodos que van seguidos de otros nodos, sin tener opciones que elegir ni nada.
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

    // Para cambiar de escena al pulsar "Irse", esto es una de las cosas que digo que pondré en otro script porque aquí no pinta nada.
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }

}


// Vale, esto son las calses Nodo y Opción de los diálogos. 
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

    [Header("Compra")]
    public ItemData itemToBuy;

    [Header("Nodo si no hay dinero")]
    public int noCoindeIndex = -1;

    [Header("Nodo si no hay espacio")]
    public int noSpaceIndex = -1;

}

// Esto es para lo de las ramificaciones cuando ya has pasado por nodos específicos,
public enum ConditionType
{
    None,
    SeenNode
}