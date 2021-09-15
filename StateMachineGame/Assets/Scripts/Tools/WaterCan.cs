using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : Tool
{
    public float dryInterval = 1f;

    public Dictionary<Vector3Int, float> wateredTiles = new Dictionary<Vector3Int, float>();

    public WaterCan(Sprite sprite):base("Water Can","WaterCan", "water", 2f, sprite)
    {
        offset = new Vector2(-10f, -30f);
    }

    public override void UseTool(ExtendedRuleTile tile, Vector3Int pos)
    {
        if (tile.tile.Contains("dry"))
        {
            string tiletype = tile.tile.Replace("dry","wet");
            SetTile(tiletype, pos);
            wateredTiles[pos] = level * 10;

            //start coroutine to dry off
            GameObject gobject;
            TileObject tobject = GameManager.Instance.GetTObject(pos, out gobject);
            tobject.StartCoroutine(Dry(dryInterval, pos));
        }else
        if (tile.tile.Contains("wet"))
        {
            if (!wateredTiles.ContainsKey(pos))
            {
                wateredTiles[pos] = level * 10;
                //start drying out the new tile
                GameObject gobject;
                TileObject tobject = GameManager.Instance.GetTObject(pos, out gobject);
                tobject.StartCoroutine(Dry(dryInterval, pos));
            }
            //update wetness
            wateredTiles[pos] = level * 10;
        }
    }

    IEnumerator Dry(float interval, Vector3Int pos)
    {
        
        while (wateredTiles[pos] > 0)
        {
            yield return new WaitForSeconds(interval);
            //dry out a little
            wateredTiles[pos] -= 3f + UnityEngine.Random.Range(-.5f,.5f);    
        }
        wateredTiles.Remove(pos);
        var tile = GameManager.Instance.GetTile(pos);
        if (tile.tile.Contains("wet"))
        {
            SetTile(tile.tile.Replace("wet", "dry"), pos);
            yield break;
        }
    }
}
