using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatSeed : Tool
{
    GameObject wheatPrefab;

    public WheatSeed(Sprite sprite) : base("Wheat Seeds", "WheatSeed", "seed", 1f, sprite)
    {
        wheatPrefab = PrefabDictionary.Instance.prefabList["wheatplant"];
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
                GameObject.Instantiate(wheatPrefab);
                Vector3 worldPos = (pos / 2)+new Vector3(.5f,.5f,0f);
                wheatPrefab.transform.position = worldPos;
                tobject.occupied = true;
            }
        }

    }
}
