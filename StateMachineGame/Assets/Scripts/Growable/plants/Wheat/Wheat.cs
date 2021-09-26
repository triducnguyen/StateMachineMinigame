using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : Plant
{
    protected override void Awake()
    {
        GameManager.instance.growables.Add(this);
        //growableCrops.Add(PrefabDictionary.Instance.prefabList["wheatplant"]);
        Subscribe();
        animator.speed = 0;
        animator.Play("wheat", 0, 0);
        stages = 12;
        stage = 0;
        maxCrops = 12;
        minCrops = 1;
        hydrationHealThreshold = 2;
        //introduce some randomness
        baseCycleGrowth += UnityEngine.Random.Range(0f, 1f)/2;
        yield = UnityEngine.Random.Range(minCrops, maxCrops);
    }

    public override void Harvest(Tool harvester)
    {
        for (var i = growingCrops.Count-1; i>=0; i--)
        {
            var gobject = growingCrops[i];
            Crop crop;
            if (gobject.TryGetComponent<Crop>(out crop) && crop.harvestable)
            {
                crop.Harvest(harvester);
                growingCrops.RemoveAt(i);
            }
        }
        if (stage == stages && growingCrops.Count == 0)
        {
            Destroy(gameObject);
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
            //Debug.Log("growingcrops: "+growingCrops.Count);
        }
    }

    protected override void OnHealthChanged()
    {
        //indicate that the plant's health has altered
    }
}
