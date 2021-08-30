using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : Growable
{
    //what growth stage the plant is at.
    int stage; 
    //how many stages of growth
    int stages;

    float startGrowth; //how much the plant has to grow on the first stage
    float growthModifier; //a modifier that changes growth amount per stage

    float timeGrowthModifier; //modifies how much plant grows over time

    float stageGrowthSize //determines how much more the plant will have to grow each consecutive stage
    {
        get
        {
            return startGrowth+(stage*growthModifier);
        }
    }

    //if the crop is finished growing
    public bool harvestable
    {
        get => _harvestable;
        set => _harvestable = value;
    }
    bool _harvestable;

    // Start is called before the first frame update
    void Start()
    {
        //start coroutine to grow

    }

    // Update is called once per frame
    void Update()
    {
        Grow();
    }
}
