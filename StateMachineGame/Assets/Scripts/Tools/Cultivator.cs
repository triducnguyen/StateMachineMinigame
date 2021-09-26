using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultivator : Tool
{

    public Cultivator(Sprite sprite) : base("Hand Cultivator", "HandCultivator", "cultivator", 1f, sprite)
    {
    }

    public override void UseTool(ExtendedRuleTile tile, Vector3Int pos)
    {
        string newTile;
        if (tile.tile.Contains("dirt"))
        {
            newTile = tile.tile.Replace("dirt", "tilled");
            SetTile(newTile, pos);
        }
        else if (tile.tile == "grass")
        {
            SetTile("tilleddry", pos);
        }
    }
}
