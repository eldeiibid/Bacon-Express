using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// IMPORTANTÍSIMO SCRIPPPTTT. Es literalmente crear en Unity unos objetos Items que te guardas como assets y llevan todos los atributos que van a tener nuestros items
// (acabo de descubrir que esto se puede hacer y me tiene dando volteretas)

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string id;

    public string itemName;
    public Sprite icon;

    public bool canUse;
    public bool canCombine;

    public int cost;

    [TextArea]
    public string description;

    public List<ItemCombination> combinations = new List<ItemCombination>();

}
