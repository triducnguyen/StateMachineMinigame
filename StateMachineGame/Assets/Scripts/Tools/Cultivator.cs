using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultivator : Tool
{

    public Cultivator(Sprite sprite) : base("Hand Cultivator", "tiller", 1f, sprite)
    {
    }

    public override void UseTool(ExtendedRuleTile tile, Vector3Int pos)
    {
        switch (tile.tile)
        {
            case "grass":
                SetTile("tilleddry", pos);
                break;
            case "dirtdry":
                SetTile("tilleddry", pos);
                break;
            case "dirtwet":
                SetTile("tilledwet", pos);
                break;
        }
    }
}
