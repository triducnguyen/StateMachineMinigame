using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : Tool
{
    public Harvester(Sprite sprite) : base("Hand Clippers", "HandClippers", "harvester", 1f, sprite)
    {

    }

    public override void UseTool(ExtendedRuleTile tile, Vector3Int pos)
    {
        TileObject tobject = GameManager.Instance.tilemap.GetInstantiatedObject(pos).GetComponent<TileObject>();
        if (tobject.occupier != null)
        {
            var plants = tobject.occupier.GetComponentsInChildren<Plant>();
            if (plants.Length > 0)
            {
                for (var i = plants.Length-1; i>=0; i--)
                {
                    var plant = plants[i];
                    plant.Harvest();
                }
            }
        }
    }
}
