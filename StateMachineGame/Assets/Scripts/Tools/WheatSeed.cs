using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatSeed : Tool
{
    GameObject wheatContainerPrefab;

    public WheatSeed(Sprite sprite) : base("Wheat Seeds", "WheatSeed", "seed", 1f, sprite)
    {
        wheatContainerPrefab = PrefabDictionary.Instance.prefabList["wheatplant"];
    }

    public override void UseTool(ExtendedRuleTile tile, Vector3Int pos)
    {
        //plant a wheat plant at this tile
        GameObject gobject = GameManager.Instance.tilemap.GetInstantiatedObject(pos);
        TileObject tobject;
        if(gobject.TryGetComponent<TileObject>(out tobject))
        {
            if (!tobject.occupied && tile.tile.Contains("tilled"))
            {
                //add wheat object to this pos
                tobject.SetOccupier(GameObject.Instantiate(wheatContainerPrefab, GameManager.Instance.worldObjects.transform));
                var wheatContainer = tobject.occupier.GetComponent<WheatContainer>();
                Vector3 localPos = (Vector3)pos / 2f;
                Vector3 worldPos = GameManager.Instance.tilemap.transform.localToWorldMatrix * localPos;
                tobject.occupier.transform.position = worldPos + new Vector3(.25f,.25f);
                //Debug.Log(tobject.transform.position);
                tobject.occupied = true;
            }
        }

    }
}
