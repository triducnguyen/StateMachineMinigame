using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatSpike : Crop
{
    int grains = 0;

    protected override void Awake()
    {
        grains = UnityEngine.Random.Range(4, 65);
        baseValue = 0.001f * grains;
        harvestable = true;
    }
}
