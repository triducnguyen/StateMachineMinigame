using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopProgression : Singleton<ShopProgression>
{
    public Dictionary<string, Item> items
    {
        get
        {
            return ItemDictionary.instance.items;
        }
    }
    public Dictionary<Item, List<Item>> progression = new Dictionary<Item, List<Item>>();

    private void Awake()
    {
        base.Awake();

        progression[items["WaterCan1"]] = new List<Item>() { items["WaterCan2"] };
        progression[items["WaterCan2"]] = new List<Item>() { items["WaterCan3"] };
        progression[items["WaterCan3"]] = new List<Item>() { items["WaterCan4"] };
        progression[items["WaterCan4"]] = new List<Item>() { items["WaterCan5"] };
        progression[items["WaterCan5"]] = new List<Item>() { items["WaterCan6"] };


        progression[items["Harvester1"]] = new List<Item>() { items["Harvester2"] };
        progression[items["Harvester2"]] = new List<Item>() { items["Harvester3"] };
        progression[items["Harvester3"]] = new List<Item>() { items["Harvester4"] };
        progression[items["Harvester4"]] = new List<Item>() { items["Harvester5"] };
        progression[items["Harvester5"]] = new List<Item>() { items["Harvester6"] };


    }
}
