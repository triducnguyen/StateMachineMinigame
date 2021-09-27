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
    public Dictionary<Item, List<SPD>> progression = new Dictionary<Item, List<SPD>>();

    private void Awake()
    {
        base.Awake();

        progression[items["WaterCan1"]] = new List<SPD>() { new SPD(items["WaterCan2"],items["WaterCan3"]) };
        progression[items["WaterCan2"]] = new List<SPD>() { new SPD(items["WaterCan3"],items["WaterCan4"]) };
        progression[items["WaterCan3"]] = new List<SPD>() { new SPD(items["WaterCan4"],items["WaterCan5"]) };
        progression[items["WaterCan4"]] = new List<SPD>() { new SPD(items["WaterCan5"],items["WaterCan6"]) };
        progression[items["WaterCan5"]] = new List<SPD>() { new SPD(items["WaterCan6"]) };


        progression[items["Harvester1"]] = new List<SPD>() { new SPD(items["Harvester2"],items["Harvester3"]) };
        progression[items["Harvester2"]] = new List<SPD>() { new SPD(items["Harvester3"],items["Harvester4"]) };
        progression[items["Harvester3"]] = new List<SPD>() { new SPD(items["Harvester4"],items["Harvester5"]) };
        progression[items["Harvester4"]] = new List<SPD>() { new SPD(items["Harvester5"],items["Harvester6"]) };
        progression[items["Harvester5"]] = new List<SPD>() { new SPD(items["Harvester6"]) };


    }
}
public class SPD
{
    public Item nextItem;
    public Item toReplace;
    

    public SPD(Item nextItem, Item toReplace = null)
    {
        this.toReplace = toReplace;
        this.nextItem = nextItem;
    }
}
