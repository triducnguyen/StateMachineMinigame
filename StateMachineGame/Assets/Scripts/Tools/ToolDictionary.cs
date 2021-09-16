using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolDictionary : Singleton<ToolDictionary>
{
    public List<Sprite> sprites = new List<Sprite>();

    public Dictionary<string, Tool> tools = new Dictionary<string, Tool>();

    protected override void Awake()
    {
        base.Awake();
        //add all tools to dictionary
        tools["WaterCan"] = new WaterCan(sprites[0]);
        tools["HandCultivator"] = new Cultivator(sprites[1]);
        tools["HandClippers"] = new Harvester(sprites[2]);
        tools["WheatSeed"] = new WheatSeed(sprites[3]);
        tools["Fertilizer"] = new Fertilizer(sprites[4]);
        tools["hand"] = new HandTool(sprites[5]);
    }
}
