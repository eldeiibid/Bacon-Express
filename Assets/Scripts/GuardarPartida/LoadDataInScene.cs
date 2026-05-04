using UnityEngine;

public class LoadDataInScene : MonoBehaviour
{
    public HealthSystem sistemaVida;
    public SistemaMonedas sistemaMonedas;
    public ConfigurationMenu menuConfig;

    void Start()
    {
        GameData data = GameManager.Instance.loadedData;

        if (data != null)
        {
            
            sistemaVida.health = data.health;
            sistemaMonedas.coins = data.coins;
            menuConfig.diffOn = data.difficulty;

            
            Inventory inv = Inventory.Instance;

            inv.items.Clear();

            foreach (string id in data.inventory.itemIDs)
            {
                ItemData item = ItemDatabase.Instance.GetItemByID(id);

                if (item != null)
                    inv.items.Add(item);
            }

            inv.NotifyInventoryChanged();

            
            GameManager.Instance.loadedData = null;
        }
    }
}