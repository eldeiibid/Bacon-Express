using UnityEngine;
using UnityEngine.UI;

//Esto maneja los slots (donde dentro van los items). Los slots son prefabs, y cuando adquieres uno en el juego, se genera un slot dentro del panel "contenedor"
// y mete el item dentro. El item NO es un botón, pero el slot SÍ lo es, así q cuando estas ""cickando un item" en realidad estás clickando el slot que lo contiene.
// Esta no la pensó ni Megamind

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;

    private ItemData item;
    private InventoryUI inventoryUI;

    public void Setup(ItemData newItem, InventoryUI ui)
    {
        item = newItem;
        inventoryUI = ui;

        icon.sprite = item.icon;
        icon.enabled = true;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        inventoryUI.OnItemClicked(item);
    }
}