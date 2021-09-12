using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : Tool
{
    public WaterCan(Sprite sprite):base("Water Can", "water", 2f, sprite)
    {
        offset = new Vector2(-10f, -30f);
    }

    public override void UseTool(ExtendedRuleTile tile, Vector3Int pos)
    {
        switch (tile.tile)
        {
            case "dirtdry":
                //wet the dirt
                SetTile("dirtwet", pos);
                break;
            case "tilleddry":
                SetTile("tilledwet", pos);
                break;
        }
    }
}
