using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopList : Singleton<ShopList>
{
    public RectTransform thisRect;

    public GameObject listItemPrefab;

    public ItemList itemList;

    public Dictionary<string, Item> items
    {
        get => ItemDictionary.instance.items;
    }

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

    public bool GetShopItem(Item item, out ShopItem shopItem)
    {
        shopItem = null;
        foreach (var i in listed)
        {
            if (i.item == item)
            {
                shopItem = i;
                return true;
            }
        }
        return false;
    }

    public void Awake()
    {
        base.Awake();

        AddShopItem(items["WaterCan2"], items["WaterCan1"]);
        AddShopItem(items["Harvester2"], items["Harvester1"]);
    }

    public void AddShopItem(Item item, Item toReplace = null)
    {
        //create listitem from item
        var shopItem = Instantiate(listItemPrefab);
        shopItem.transform.parent = transform;
        var rect = shopItem.GetComponent<RectTransform>();
        rect.parent = thisRect;
        rect.localScale = Vector3.one;
        var itemScript = shopItem.GetComponent<ShopItem>();
        itemScript.SetItem(item, toReplace);
    }

    public void DelShopItem(Item item)
    {
        ShopItem shopItem;
        if (GetShopItem(item, out shopItem))
        {
            shopList.Remove(shopItem.gameObject);
            Destroy(shopItem.gameObject);
        }
    }
    public void DelShopItem(GameObject item)
    {
        shopList.Remove(item);
        Destroy(item);
    }

    public void BuyItem(GameObject item)
    {
        var itemScript = item.GetComponent<ShopItem>();
        if (Money.instance.Withdrawl(itemScript.item.basePrice))
        {
            //update player's items
            if (itemScript.replaces != null)
            {
                itemList.ReplaceItem(itemScript.item, itemScript.replaces);
            }

            List<SPD> newItems;
            if (ShopProgression.instance.progression.TryGetValue(itemScript.item, out newItems))
            {
                foreach (var newItem in newItems)
                {
                    AddShopItem(newItem.nextItem, newItem.toReplace);
                }
            }
            //remove listing from shop
            DelShopItem(item);
        }
    }
}
