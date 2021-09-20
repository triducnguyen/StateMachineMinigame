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

    public float baseValue = 0.25f; // 1/4th of a dollar

    public virtual void Harvest(Tool harvester)
    {
        if (harvestable && harvester.toolType == "harvester")
        {
            float value = baseValue * harvester.level;
            Money.instance.Deposit(value);
        }
    }

    protected override void OnGrowableDestroyed()
    {
        Destroy(gameObject);
    }
}
