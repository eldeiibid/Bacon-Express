using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


// Gestinamos el tema de inspeccionar los items, sacando un panelillo con el nombre, la foto y una descripción que me pienso inventar en todos y cada uno de ellos :3

public class InspectItem : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private Image itemImage;

    public Button closeButton;

    public static InspectItem Instance;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(ItemData item)
    {
        panel.SetActive(true);

        itemName.text = item.itemName;
        itemDescription.text = item.description;
        itemImage.sprite = item.icon;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}