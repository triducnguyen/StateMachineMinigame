using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTool : Tool
{
    public HandTool():base("Hand", "Hand", "hand", 1f, ItemDictionary.instance.sprites["Hand"])
    {}
}
