using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatSeed : Tool
{
    GameObject wheatContainerPrefab;
    float cost = 0.25f;
    public WheatSeed(Sprite sprite) : base("Wheat Seeds", "WheatSeed", "seed", 1f, sprite)
    {
        wheatContainerPrefab = PrefabDictionary.Instance.prefabList["wheatplant"];
    }

    public override void UseTool(ExtendedRuleTile tile, Vector3Int pos)
    {
        //plant a wheat plant at this tile
        GameObject gobject = TileManager.instance.tilemap.GetInstantiatedObject(pos);
        TileObject tobject;
        if(gobject.TryGetComponent<TileObject>(out tobject))
        {
            //charge user for seeds
            if (!tobject.occupied && tile.tile.Contains("tilled") && Money.instance.Withdrawl(cost))
            {
                //add wheat object to this pos
                Vector3 localPos = (Vector3)pos / 2f;
                Vector3 worldPos = TileManager.instance.tilemap.transform.localToWorldMatrix * localPos;
                tobject.SetOccupier(GameObject.Instantiate(wheatContainerPrefab, worldPos + new Vector3(.25f, .25f), Quaternion.identity, GameManager.instance.worldObjects.transform));
                var wheatContainer = tobject.occupier.GetComponent<WheatContainer>();
            }
        }

    }
}
