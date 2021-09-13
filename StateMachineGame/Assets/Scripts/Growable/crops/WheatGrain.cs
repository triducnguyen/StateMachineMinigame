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

    protected override void StageUp(object sender, EventArgs e)
    {
        //immediately ready to harvest
        harvestable = true;
    }
}
