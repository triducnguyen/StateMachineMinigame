using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : Growable
{
    //if the crop is finished growing
    public bool harvestable
    {
        get => _harvestable;
        set => _harvestable = value;
    }
    [SerializeField]
    bool _harvestable;

    public virtual void Harvest()
    {
        if (harvestable)
        {
            //add items to storage

        }
    }

    protected override void OnGrowableDestroyed()
    {
        Destroy(this);
    }
}
