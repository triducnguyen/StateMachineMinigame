using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Image itemImage;
    public Item item;
    public Item replaces;
    public Text description;
    public Text cost;

    public void SetItem(Item item, Item replaces = null)
    {
        this.item = item;
        if (replaces != null)
        {
            this.replaces = replaces;
        }
        itemImage.sprite = item.sprite;
        description.text = item.itemDescription;
        cost.text = item.basePrice.ToString("$0.00");
    }
}
