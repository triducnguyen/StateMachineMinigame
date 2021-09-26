using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDictionary : Singleton<ItemDictionary>
{
    public List<NamedSprite> keyVals;
    public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    public Dictionary<string, Item> items = new Dictionary<string, Item>();
    public List<Item> itemList = new List<Item>();

    List<System.Tuple<string, Type>> baseItems = new List<System.Tuple<string, Type>>();

    protected override void Awake()
    {
        base.Awake();
        //create item -> sprite dictionary
        foreach (var item in keyVals)
        {
            sprites[item.name] = item.sprite;
        }

        //set base items
        baseItems.Add(new Tuple<string, Type>("WaterCan1",typeof(WaterCan1)));
        baseItems.Add(new Tuple<string, Type>("WaterCan2",typeof(WaterCan2)));
        baseItems.Add(new Tuple<string, Type>("WaterCan3",typeof(WaterCan3)));
        baseItems.Add(new Tuple<string, Type>("WaterCan4",typeof(WaterCan4)));
        baseItems.Add(new Tuple<string, Type>("WaterCan5",typeof(WaterCan5)));
        baseItems.Add(new Tuple<string, Type>("WaterCan6",typeof(WaterCan6)));

        baseItems.Add(new Tuple<string, Type>("Harvester1",typeof(Harvester1)));
        baseItems.Add(new Tuple<string, Type>("Harvester2",typeof(Harvester2)));
        baseItems.Add(new Tuple<string, Type>("Harvester3",typeof(Harvester3)));
        baseItems.Add(new Tuple<string, Type>("Harvester4",typeof(Harvester4)));
        baseItems.Add(new Tuple<string, Type>("Harvester5",typeof(Harvester5)));
        baseItems.Add(new Tuple<string, Type>("Harvester6",typeof(Harvester6)));
        
        baseItems.Add(new Tuple<string, Type>("Cultivator",typeof(Cultivator)));
        baseItems.Add(new Tuple<string, Type>("WheatSeeds",typeof(WheatSeed)));
        baseItems.Add(new Tuple<string, Type>("Fertilizer",typeof(Fertilizer)));
        baseItems.Add(new Tuple<string, Type>("Hand",typeof(HandTool)));

        //instantiate all items
        foreach (var item in baseItems)
        {
            dynamic instance = Convert.ChangeType(Activator.CreateInstance(item.Item2), item.Item2);
            items[item.Item1] = instance;
        }
        itemList = items.ToList().Select((x)=> x.Value).ToList();
    }
}
[System.Serializable]
public class NamedSprite
{
    public string name;
    public Sprite sprite;
}
