using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Tool : Item
{
    public Vector2 offset = Vector2.zero;

    public string toolType
    {
        get;
        protected set;
    }
    public int level
    {
        get
        {
            return _level;
        }
        protected set
        {
            _level = value;
        }
    }
    int _level = 1;

    public float experience
    {
        get
        {
            return _experience;
        }
        protected set
        {
            _experience = value;
        }
    }
    float _experience = 0;


    //tool area of effect
    public float toolRadius;

    //tool
    public Tool(string displayName, string name, string toolType, float radius, Sprite sprite) { this.displayName = displayName; itemName = name; this.type = "tool"; this.toolType = toolType; toolRadius = radius; this.sprite = sprite; }

    public virtual void UseTool(ExtendedRuleTile tile, Vector3Int pos)
    {

    }

    protected void SetTile(string tile, Vector3Int pos)
    {
        TileManager.instance.tilemap.SetTile(
                    pos,
                    TileDictionary.instance.tiles[tile]
                );
    }
}
