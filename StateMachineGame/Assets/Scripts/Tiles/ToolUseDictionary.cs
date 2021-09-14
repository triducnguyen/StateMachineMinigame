using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolUseDictionary : MonoBehaviour
{
    public static ToolUseDictionary Instance;

    public Dictionary<System.Tuple<string, string>, string> ToolUse = new Dictionary<System.Tuple<string, string>, string>();
    
    TileDictionary tiles = TileDictionary.Instance;
    ToolDictionary tools = ToolDictionary.Instance;


    private void Awake()
    {
        CheckSingleton();

        //create all tool + tile interactions

        //water + dirtdry = dirtwet
        //water + tilleddry = tilledwet
        
        //cultivator + dirtdry = tilleddry
        //cultivator + dirtwet = tilledwet
        //cultivator + grass = tilleddry

        //ToolUse.Add(new System.Tuple<string, string>("WateringCan","dirtdry"), "dirtwet");
        //ToolUse.Add(new System.Tuple<string, string>("WateringCan","tilleddry"), "tilledwet");
        //ToolUse.Add(new System.Tuple<string, string>("HandCultivator", "dirtdry"), "tilleddry");
        //ToolUse.Add(new System.Tuple<string, string>("HandCultivator", "dirtwet"), "tilledwet");
        //ToolUse.Add(new System.Tuple<string, string>("HandCultivator", "grass"), "tilleddry");
    }

    void CheckSingleton()
    {
        if (Instance is object)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
