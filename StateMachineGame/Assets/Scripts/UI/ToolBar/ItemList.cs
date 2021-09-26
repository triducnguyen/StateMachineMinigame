using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : Singleton<ItemList>
{
    List<Item> items = new List<Item>();
    //the items shown are the first three

    public List<Image> images = new List<Image>();

    private void Awake()
    {
        base.Awake();
        AddItem(ItemDictionary.instance.items["WaterCan1"]) ;
        AddItem(ItemDictionary.instance.items["Cultivator"]) ;
        AddItem(ItemDictionary.instance.items["Harvester1"]) ;
        AddItem(ItemDictionary.instance.items["WheatSeeds"]) ;
        AddItem(ItemDictionary.instance.items["Fertilizer"]) ;
    }

    

    public void Up()
    {
        //rotate items down
        Item last = items[items.Count-1];
        items.RemoveAt(items.Count-1);
        items.Insert(0, last);
        UpdateImages();
    }
    public void Down()
    {
        //rotate items up
        Item first = items[0];
        items.RemoveAt(0);
        items.Add(first);
        UpdateImages();
    }

    public void Select1()
    {
        if (items.Count > 0)
        {
            if (items[0].type != "item")
            {
                GameManager.instance.tool = (Tool)items[0];

            }
        }
    }

    public void Select2()
    {
        if(items.Count > 1){
            if (items[0].type != "item")
            {
                GameManager.instance.tool = (Tool)items[1];
            }
        }
    }

    public void Select3()
    {
        if (items.Count > 2)
        {
            if (items[0].type != "item")
            {
                GameManager.instance.tool = (Tool)items[2];
            }
        }
    }

    void UpdateImages()
    {
        var count = 0;
        foreach (Item item in items)
        {
            if(count > 2)
            {
                return;
            }
            images[count].sprite = item.sprite;
            var color = images[count].color;
            images[count].color = new Color(color.r, color.g, color.b, 1f);
            count++;
        }
        if (count < 2)
        {
            for (int i = items.Count; i < 3; i++)
            {
                var color = images[i].color;
                images[i].color = new Color(color.r, color.g, color.b, 0f);
            }
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        UpdateImages();
    }

    public void RemoveItem(Item item)
    {
        if (item == GameManager.instance.tool)
        {
            GameManager.instance.tool = null;
        }
        items.Remove(item);
        UpdateImages();
    }

    public void ReplaceItem(Item newItem, Item currentItem)
    {
        items.Insert(items.IndexOf(currentItem), newItem);
        items.Remove(currentItem);
        if (newItem.GetType() == typeof(Tool) && newItem == GameManager.instance.tool)
        {
            GameManager.instance.tool = null;
            GameManager.instance.tool = (Tool)newItem;
        }
        UpdateImages();
    }

    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }
}