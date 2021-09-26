using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public Sprite sprite;
    public Sprite sprite2;
    List<Item> itemList = new List<Item>();
    public List<Item> items
    {
        get
        {
            return new List<Item>(itemList);
        }
    }
    //the items shown are the first three

    public List<Image> images = new List<Image>();

    private void Awake()
    {
        AddItem(ToolDictionary.instance.tools["WaterCan"]) ;
        AddItem(ToolDictionary.instance.tools["HandCultivator"]) ;
        AddItem(ToolDictionary.instance.tools["HandClippers"]) ;
        AddItem(ToolDictionary.instance.tools["WheatSeed"]) ;
        AddItem(ToolDictionary.instance.tools["Fertilizer"]) ;
    }

    

    public void Up()
    {
        //rotate items down
        Item last = itemList[itemList.Count-1];
        itemList.RemoveAt(itemList.Count-1);
        itemList.Insert(0, last);
        UpdateImages();
    }
    public void Down()
    {
        //rotate items up
        Item first = itemList[0];
        itemList.RemoveAt(0);
        itemList.Add(first);
        UpdateImages();
    }

    public void Select1()
    {
        if (items.Count > 0)
        {
            if (items[0].type == "tool")
            {
                GameManager.instance.tool = (Tool)items[0];

            }
        }
    }

    public void Select2()
    {
        if(items.Count > 1){
            if (items[0].type == "tool")
            {
                GameManager.instance.tool = (Tool)items[1];
            }
        }
    }

    public void Select3()
    {
        if (items.Count > 2)
        {
            if (items[0].type == "tool")
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

    void AddItem(Item item)
    {
        itemList.Add(item);
        UpdateImages();
    }

    void RemoveTool(Item item)
    {
        itemList.Remove(item);
        UpdateImages();
    }
}
