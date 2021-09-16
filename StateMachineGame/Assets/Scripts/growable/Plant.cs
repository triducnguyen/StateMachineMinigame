using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Growable
{
    //max possible crops to grow
    [SerializeField]
    protected int maxCrops = 5;
    //min possible crops to grow
    [SerializeField]
    protected int minCrops = 1;

    public bool hydrated = false;
    public float hydrationThreshold = 10f;
    public float hydrationHealThreshold = 10f;

    public float hydrationCap = 15f;
    public float baseHydration = 5f;
    public float hydrationModifier = 1f;
    public float hydrationRate { get => baseHydration * hydrationModifier; }
    public float hydratedGrowthRate = 3f;
    protected float hydration
    {
        get
        {
            return _hydration;
        }
        set
        {

            if (value > hydrationCap)
            {
                _hydration = hydrationCap;
            }else
            if (value <= 0)
            {
                _hydration = 0;
            }
            else
            {
                _hydration = value;
            }
        }
    }
    [SerializeField]
    float _hydration = 10f;

    public float dehydrationInterval = 1f;
    public float baseDehydration = 1f;
    public float dehydrationModifier = 0f;
    public float dehydrationRate
    {
        get => baseDehydration +  dehydrationModifier;
    }
    protected int randomYield
    {
        get
        {
            var cropYield = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f) * maxCrops);
            cropYield = cropYield < minCrops ? minCrops : cropYield;
            cropYield = cropYield > maxCrops ? maxCrops : cropYield;
            return cropYield;
        }
    }

    public int yield = 1;

    //list of crops plant can produce
    public List<GameObject> growableCrops = new List<GameObject>();

    //list of currently growing crops
    public List<GameObject> growingCrops = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
    }

    public virtual void Harvest()
    {
        
    }

    public virtual void AddCrop(GameObject crop)
    {
        growingCrops.Add(Instantiate(crop, transform));
    }
    
    public virtual void RemoveCrop(GameObject crop)
    {
        growingCrops.Remove(crop);
        Destroy(crop);
    }

    protected override void OnGrowableDestroyed()
    {
        //uh oh. Goodbye crops.
        for (var i = growingCrops.Count - 1; i >=0; i--)
        {
            var crop = growingCrops[i];
            Destroy(crop);
        }
    }

    protected override void Subscribe()
    {
        StartRoutine("checkwater", CheckWater(dehydrationInterval, TileManager.instance.GetTilePos(transform.position)));
        base.Subscribe();
    }

    public IEnumerator CheckWater(float interval, Vector3Int tilePos)
    {
        yield return new WaitForSeconds(interval);
        
        //hydrate the plant before killing it
        ExtendedRuleTile tile = TileManager.instance.GetTile(tilePos);
        if (tile.tile.Contains("wet") && TileManager.instance.wateredTiles.ContainsKey(tilePos))
        {
            //plant is watered, slowly use it
            TileManager.instance.wateredTiles[tilePos] -= hydrationRate;
            hydration += hydrationRate;
        }
        else
        {
            //not hydrated
            hydration -= dehydrationRate;
        }
        if (hydration >= hydrationThreshold)
        {
            //increase growth rate
            growthCycleModifier = hydratedGrowthRate;
        }
        else
        {
            growthCycleModifier = baseGrowthCycleModifier;
        }
        if (hydration > hydrationHealThreshold)
        {
            //heal plant if hydrated enough
            health++;
        }
        if (hydration == 0)
        {
            //hurt the plant, it is very thirsty!
            Debug.Log("Hurting plant @ "+transform.position);
            health--;
        }
        
    }
}
