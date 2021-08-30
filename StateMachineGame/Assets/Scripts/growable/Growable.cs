using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : MonoBehaviour
{
    //plants starting health
    float startHealth = 10;

    //plant's current health
    float health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value <= 0)
            {
                //growable has died
                OnGrowableDestroyed();
            }
            else
            {
                _health = value;
            }
        }
    }
    float _health;

    //max health of plant. Based on starting health, growth stage, and health modifier.
    float maxHealth
    {
        get
        {
            return startHealth + (stage * healthModifier);
        }
    }
    float healthModifier;

    //current stage of growth. Doesn't necesarilly mean it is ready to be harvested
    int stage;
    //how many stages of growth there are.
    int stages;
    //how much the plant has to grow on the first stage
    float startGrowth;
    //a modifier that changes growth amount per stage
    float growthModifier;

    float stageGrowthSize //determines how much more the plant will have to grow each consecutive stage
    {
        get
        {
            return startGrowth + (stage * growthModifier);
        }
    }

    //whether can grow or not
    bool canGrow = true;

    //how far plant is from finishing current stage of growth
    float growth
    {
        get
        {
            return _growth;
        }
        set
        {
            if (value <= 0 )
            {
                _growth = 0;
            }
            if (value >= stageGrowthSize) //next stage
            {
                _growth = 0;
                stage++;
            }
            else
            {
                _growth = value;
            }
        }
    }
    float _growth;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Grow()
    {
        
    }

    protected virtual void OnGrowableDestroyed()
    {

    }
    
}
