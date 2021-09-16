using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : Singleton<TileManager>
{
    public Tilemap tilemap;

    public Dictionary<Vector3Int, float> wateredTiles = new Dictionary<Vector3Int, float>();


    protected override void Awake()
    {
        base.Awake();
    }

    public Vector3Int GetTilePos(Vector3 worldpos)
    {
        var tmp = tilemap.transform.worldToLocalMatrix * worldpos * 2f;
        tmp.z = 0;
        return Vector3Int.FloorToInt(tmp);
    }


    public ExtendedRuleTile GetTile(Vector3 worldpos, out Vector3Int tilePos)
    {
        tilePos = GetTilePos(worldpos);
        return (ExtendedRuleTile)tilemap.GetTile(tilePos);
    }
    public ExtendedRuleTile GetTile(Vector3Int tilePos)
    {
        return (ExtendedRuleTile)tilemap.GetTile(tilePos);
    }

    public GameObject GetGameObject(Vector3Int tilePos)
    {
        return tilemap.GetInstantiatedObject(tilePos);
    }

    public TileObject GetTObject(Vector3 worldpos, out Vector3Int tilePos, out GameObject gameObject)
    {
        tilePos = GetTilePos(worldpos);
        gameObject = GetGameObject(tilePos);
        return gameObject.GetComponent<TileObject>();
    }

    public TileObject GetTObject(Vector3Int tilePos, out GameObject gameObject)
    {
        gameObject = GetGameObject(tilePos);
        return gameObject.GetComponent<TileObject>();
    }
}
