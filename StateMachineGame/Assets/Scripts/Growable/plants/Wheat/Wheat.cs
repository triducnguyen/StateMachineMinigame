using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : Plant
{


    protected override void Awake()
    {
        growableCrops.Add(PrefabDictionary.Instance.prefabList["wheatplant"]);
        Subscribe();
        animator.speed = 0;
        animator.Play("wheat", 0, 0);
        stages = 12;
        stage = 0;
        maxCrops = 3;
        yield = randomYield;
        StartGrow();
    }

    protected override void Harvest()
    {
        foreach (var gobject in growingCrops)
        {
            Crop crop;
            if (gobject.TryGetComponent<Crop>(out crop))
            {
                crop.Harvest();
            }
        }
    }

    protected override void StageUp(object sender, EventArgs e)
    {
        //progress animator to sprite stage
        float frame = (float)stage / (float)stages;
        animator.Play("wheat", 0, frame);
    }

    protected override void FinalStage(object sender, EventArgs e)
    {
        //grain is finished growing
        StopGrow();
        for (var i = 0; i < yield; i++)
        {
            AddCrop(growableCrops[0]);
        }
    }

    protected override void OnGrowableDestroyed()
    {
        Destroy(gameObject);
    }
}
