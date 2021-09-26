using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : Tool
{
    public Harvester(string displayName, string itemName, float radius, float cost):
        base(displayName, itemName, "harvester", 1f, ItemDictionary.instance.sprites[itemName])
    { basePrice = cost; }

    public override void UseTool(ExtendedRuleTile tile, Vector3Int pos)
    {
        TileObject tobject = TileManager.instance.tilemap.GetInstantiatedObject(pos).GetComponent<TileObject>();
        if (tobject.occupier != null)
        {
            var plants = tobject.occupier.GetComponentsInChildren<Plant>();
            if (plants.Length > 0)
            {
                for (var i = plants.Length-1; i>=0; i--)
                {
                    var plant = plants[i];
                    plant.Harvest(this);
                }
            }
        }
    }
}
