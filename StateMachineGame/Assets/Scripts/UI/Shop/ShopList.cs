using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopList : Singleton<ShopList>
{
    public RectTransform thisRect;

    public GameObject listItemPrefab;

    public ItemList itemList;

    //list of current shop items
    public List<GameObject> shopList = new List<GameObject>();
    public List<ShopItem> listed
    {
        get
        {
            List<ShopItem> tmp = new List<ShopItem>();
            foreach (var listItem in shopList)
            {
                var listItemScript = GetComponent<ShopItem>();
                tmp.Add(listItemScript);
            }
            return tmp;
        }
    }

    public void Awake()
    {
        base.Awake();

        AddShopItem(ItemDictionary.instance.items["WaterCan2"]);
        AddShopItem(ItemDictionary.instance.items["Harvester2"]);
    }

    public void AddShopItem(Item item)
    {
        //create listitem from item
        var shopItem = Instantiate(listItemPrefab);
        shopItem.transform.parent = transform;
        var rect = shopItem.GetComponent<RectTransform>();
        rect.parent = thisRect;
        rect.localScale = Vector3.one;
        var itemScript = shopItem.GetComponent<ShopItem>();
        itemScript.SetItem(item);
    }

    public void BuyItem(GameObject item)
    {
        var itemScript = item.GetComponent<ShopItem>();
        if (Money.instance.Withdrawl(itemScript.item.basePrice))
        {
            //update player's items
            List<Item> newItems;
            if (itemScript.replaces != null)
            {
                itemList.ReplaceItem(itemScript.item, itemScript.replaces);
            }
            if (ShopProgression.instance.progression.TryGetValue(itemScript.item, out newItems))
            {
                foreach (var newItem in newItems)
                {
                    AddShopItem(newItem);
                }
            }
        }
    }
}
