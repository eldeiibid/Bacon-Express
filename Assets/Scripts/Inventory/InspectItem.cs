using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;


// Gestinamos el tema de inspeccionar los items, sacando un panelillo con el nombre, la foto y una descripci�n que me pienso inventar en todos y cada uno de ellos :3

public class InspectItem : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private Image itemImage;

    public Button closeButton;

    public static InspectItem Instance;

    private void Start()
    {
        panel.SetActive(false);
    }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Show(ItemData item)
    {
        panel.SetActive(true);

        //No preguntes, sólo gózalo.
        //Por cierto, con este sistema ya es inutil poner la descripcion en el propio objeto, ya que está en el CSV
        itemName.text = LocalizationSettings.StringDatabase.GetLocalizedString($"ITEM.{item.itemName}.NAME");
        itemDescription.text = LocalizationSettings.StringDatabase.GetLocalizedString($"ITEM.{item.itemName}.DESC");
        itemImage.sprite = item.icon;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}