using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatGrain : Crop
{
    protected override void Awake()
    {
        harvestable = true;
    }
}
