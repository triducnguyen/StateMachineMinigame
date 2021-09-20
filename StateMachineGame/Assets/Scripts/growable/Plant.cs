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

    public bool healing = false;
    public float checkWaterInterval = .5f;
    public bool hydrated
    {
        get { return _hydrated; }
        protected set
        {
            //growthCycleModifier = value? hydratedGrowthRate : baseGrowthCycleModifier;
            _hydrated = value;
        }
    }
    [SerializeField]
    bool _hydrated;
    public float hydrationThreshold = 4f;
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
            /*
            _hydration = value > hydrationCap ? hydrationCap : _hydration;
            _hydration = value <= 0 ? 0 : _hydration;
            */
            _hydration = value > hydrationCap ? hydrationCap : (value < 0 ? 0 : value);
            hydrated = _hydration >= hydrationThreshold ? true : false;
            healing = _hydration > hydrationHealThreshold ? true : false;
        }
    }

    [SerializeField]
    float _hydration = 10f;

    public float dehydrationInterval = 10f;
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
            var cropYield = Mathf.RoundToInt(UnityEngine.Random.Range(minCrops, maxCrops));
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

    public virtual void Harvest(Tool harvester)
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
        //uh oh, Goodbye plant.
        for (var i = growingCrops.Count - 1; i >=0; i--)
        {
            var crop = growingCrops[i];
            Destroy(crop);
        }
        base.OnGrowableDestroyed();
    }

    protected override void Subscribe()
    {
        StartRoutine("checkwater", CheckWater(checkWaterInterval, TileManager.instance.GetTilePos(transform.position)));
        StartRoutine("dehydrate", Dehydrate());
        base.Subscribe();
    }

    protected virtual Color SetSaturation(float val)
    {
        var currentColor = spriteRenderer.color;
        float h;
        float s;
        float v;
        Color.RGBToHSV(currentColor, out h, out s, out v);
        s = val;
        return Color.HSVToRGB(h, s, v);
    }

    public virtual IEnumerator CheckWater(float interval, Vector3Int tilePos)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            ExtendedRuleTile tile = TileManager.instance.GetTile(tilePos);
            if (tile.tile.Contains("wet") && TileManager.instance.wateredTiles.ContainsKey(tilePos))
            {
                //plant is watered, slowly use it
                TileManager.instance.wateredTiles[tilePos] -= hydrationRate;
                hydration += hydrationRate;
            }
            
        }
    }

    public virtual IEnumerator Dehydrate()
    {
        while (true)
        {
            yield return new WaitForSeconds(dehydrationInterval);
            hydration -= dehydrationRate;
            spriteRenderer.color = SetSaturation(hydration.Map(0f,hydrationThreshold,0f,1f));
            health += healing ? 1 : (hydration <= 0 ? -1 : health);
        }
    }
}
