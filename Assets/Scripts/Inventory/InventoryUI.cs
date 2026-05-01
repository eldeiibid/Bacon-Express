using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    // Elementos de la interfaz
    public Transform container;          
    public GameObject slotPrefab;
    public GameObject UI;

    // Elementos del menú de opciones
    public GameObject contextMenu;
    public Button useButton;
    public Button combineButton;
    public Button inspectButton;

    // El item que el jugador tiene seleccionado
    private ItemData selectedItem;


    public static InventoryUI Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        Inventory.Instance.OnInventoryChanged += RefreshUI;
        RefreshUI(); //esta es importante, sin esta linea no aparecía el inventario después de comprar. Puede parecer redundante pero si no no funciona.
    }

    void OnDisable()
    {
        Inventory.Instance.OnInventoryChanged -= RefreshUI;
    }

    void Start()
    {
        RefreshUI();
        contextMenu.SetActive(false);
    }

    void Update()
    {
        if (contextMenu.activeSelf && Input.GetMouseButtonDown(0))
        {
            // Si el click NO está sobre el menú lo cerramos, porque si no no hay otra forma de quitarlo y se queda en pantalla
            if (!IsPointerOverUI(contextMenu))
            {
                CloseContextMenu();
            }
        }
    }

    public void RefreshUI()
    {
        // Limpiamos los slots actuales
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        // Creamos nuevos slots
        foreach (var item in Inventory.Instance.items)
        {
            GameObject slotGO = Instantiate(slotPrefab, container);
            InventorySlotUI slot = slotGO.GetComponent<InventorySlotUI>();
            slot.Setup(item, this);
        }
    }

    // Click en un item
    public void OnItemClicked(ItemData item)
    {
        var action = ActionManager.Instance;

        // Si queremos combinar:
        if (action.currentAction == ActionType.Combine)
        {
            // Aquí evitamos combinar el mismo objeto
            if (action.selectedItem != item)
            {
                bool success = TryCombine(action.selectedItem, item);
            }
            action.ClearAction();
            return;
        }
        selectedItem = item;
        ShowContextMenu(item);
    }

    //  Mostramos el menú de opciones
    void ShowContextMenu(ItemData item)
    {
        contextMenu.SetActive(true);

        useButton.gameObject.SetActive(true);
        combineButton.gameObject.SetActive(true);
        inspectButton.gameObject.SetActive(true);

        // Limpiar listeners previos para que no se ralle
        useButton.onClick.RemoveAllListeners();
        combineButton.onClick.RemoveAllListeners();
        inspectButton.onClick.RemoveAllListeners();

        // Al pulsar cada botón hace una acción (ovbiamente)
        useButton.onClick.AddListener(OnUse);
        combineButton.onClick.AddListener(OnCombine);
        inspectButton.onClick.AddListener(OnInspect);
    }

    //  Acción: Usar
    void OnUse()
    {
        if (!selectedItem.canUse)
        {
            FeedbackUI.Instance.Show("No se puede usar");
            CloseContextMenu();
            return;
        }

        ActionManager.Instance.SetUse(selectedItem);
        contextMenu.SetActive(false);
    }

    //  Acción: Combinar
    void OnCombine()
    {
        if (!selectedItem.canCombine)
        {
            FeedbackUI.Instance.Show("No se puede combinar");
            CloseContextMenu();
            return;
        }
        ActionManager.Instance.SetCombine(selectedItem);
        contextMenu.SetActive(false);
    }

    //  Acción: Inspeccionar
    void OnInspect()
    {
        InspectItem.Instance.Show(selectedItem);
        contextMenu.SetActive(false);
    }

    // Cerrar menú 
    public void CloseContextMenu()
    {
        contextMenu.SetActive(false);
        selectedItem = null;
    }
    public void ShowUI()
    {
        UI.SetActive(true);
    }
    public void CloseUI()
    {
        UI.SetActive(false);
    }

    // Buscamos si los objetos están uno en la lista de combinables del otro
    public bool TryCombine(ItemData itemA, ItemData itemB)
    {
        // Buscar combinación en A
        foreach (var combo in itemA.combinations)
        {
            if (combo.otherItem == itemB)
            {
                CombineItems(itemA, itemB, combo.result);
                return true;
            }
        }

        // Buscar combinación en B 
        foreach (var combo in itemB.combinations)
        {
            if (combo.otherItem == itemA)
            {
                CombineItems(itemA, itemB, combo.result);
                return true;
            }
        }

        return false;
    }
    // Si lo eestán, quitamos los dos items que hemos combinado y ponemos el nuevo
    void CombineItems(ItemData itemA, ItemData itemB, ItemData result)
    {
        Inventory.Instance.RemoveItem(itemA);
        Inventory.Instance.RemoveItem(itemB);

        Inventory.Instance.AddItem(result);

    }

    // Estoy hay que hacerlo para que, si ya has pinchado en un item y te ha salido el menú de 3 opciones, al pinchar fuera se quite.
    // POrque lo dicho, sin esto, se queda en pantalla hasta que hagas alguna de las 3 opciones, pero hay veces que puede que no quieras hacer ninguna 
    // o que no te de tiempo porque te viene un bicho.
    bool IsPointerOverUI(GameObject target)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var r in results)
        {
            if (r.gameObject == target || r.gameObject.transform.IsChildOf(target.transform))
            {
                return true;
            }
        }

        return false;
    }
}
// Y ya. He llorado sangre haciendo este script ngl.