using UnityEngine;

// Vale, este script define las 3 acciones que puedes hacer con un itme, para poder definirlas como acción actual (según lo que el jugador elija en los botones)

public enum ActionType
{
    None,
    Use,
    Combine,
    Inspect,
    Throw
}

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    public ActionType currentAction = ActionType.None;
    public ItemData selectedItem;

    void Awake()
    {
        Instance = this;
    }

    public void SetUse(ItemData item)
    {
        currentAction = ActionType.Use;
        selectedItem = item;
    }

    public void SetCombine(ItemData item)
    {
        currentAction = ActionType.Combine;
        selectedItem = item;
    }

    public void SetInspect(ItemData item)
    {
        currentAction = ActionType.Inspect;
        selectedItem = item;
    }

    public void SetThrow(ItemData item)
    {
        currentAction = ActionType.Throw;
        selectedItem = item;
    }
    public void ClearAction()
    {
        currentAction = ActionType.None;
        selectedItem = null;
    }
}