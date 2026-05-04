using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Configuración")]
    public ItemData requiredItem;
    public bool consumesItem = false;

    [Header("Eventos")]
    public UnityEvent onSuccess;
    public UnityEvent onFail;

    public void OnClick()
    {
        var action = ActionManager.Instance;

        // Si no hay acción activa -> interacción básica
        if (action.currentAction == ActionType.None)
        {
            DefaultInteraction();
            return;
        }

        if (action.currentAction == ActionType.Use)
        {
            HandleUse(action);
        }
    }

    void HandleUse(ActionManager action)
    {
        if (requiredItem == null)
        {
            Debug.Log("Este objeto no requiere item");
            onFail?.Invoke();
            return;
        }

        if (action.selectedItem == requiredItem)
        {
            Debug.Log("Acción correcta!");

            // AQU� est� la clave
            onSuccess?.Invoke();

            if (consumesItem)
            {
                Inventory.Instance.RemoveItem(requiredItem);
            }

            action.ClearAction();
        }
        else
        {
            Debug.Log("No funciona aquí");
            onFail?.Invoke();
        }
    }

    void DefaultInteraction()
    {
        Debug.Log("Interacción básica con " + gameObject.name);
    }
}