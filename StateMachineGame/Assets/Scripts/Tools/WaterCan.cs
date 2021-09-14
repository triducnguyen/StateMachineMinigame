using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : Tool
{
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
        }
    }
}
