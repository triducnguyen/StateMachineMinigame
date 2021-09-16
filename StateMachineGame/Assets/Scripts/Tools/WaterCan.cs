using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : Tool
{
    public float wetnessCap = 15f;
    public float hydration = 3f;
    
    public float dryInterval = .5f;


    public WaterCan(Sprite sprite):base("Water Can","WaterCan", "water", 2f, sprite)
    {
        offset = new Vector2(-10f, -30f);
    }

    public override void UseTool(ExtendedRuleTile tile, Vector3Int pos)
    {
        if (TileManager.instance.wateredTiles.ContainsKey(pos))
        {
            //returning customer, I see
            TileManager.instance.wateredTiles[pos] += hydration * level;

        }
        else
        if (tile.tile.Contains("dry"))
        {
            //turn the tile wet
            string tiletype = tile.tile.Replace("dry", "wet");
            SetTile(tiletype, pos);
        }
        else
        {
            //store how wet this position is
            TileManager.instance.wateredTiles[pos] = level * 10;
            //start coroutine to dry the tile
            Coroutines.instance.AddCoroutine("dry" + pos, Dry(dryInterval, pos));
        }
    }

    public IEnumerator Dry(float interval, Vector3Int pos)
    {
        
        while (TileManager.instance.wateredTiles[pos] > 0)
        {
            yield return new WaitForSeconds(interval);
            //dry out a little
            TileManager.instance.wateredTiles[pos] -= 3f + UnityEngine.Random.Range(-.5f,.5f);    
        }
        TileManager.instance.wateredTiles.Remove(pos);
        var tile = TileManager.instance.GetTile(pos);
        if (tile.tile.Contains("wet"))
        {
            SetTile(tile.tile.Replace("wet", "dry"), pos);
        }
        Coroutines.instance.DelCoroutine("dry" + pos);
        yield break;
    }
}
