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

    IEnumerator CheckWater()
    {
        yield return null;
    }
}
