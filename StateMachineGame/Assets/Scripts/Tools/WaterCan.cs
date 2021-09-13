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
        string tiletype;
        if (ToolUseDictionary.Instance.ToolUse.TryGetValue(new System.Tuple<string, string>(name, tile.tile), out tiletype))
        {
            SetTile(tiletype, pos);
        }
    }
}
