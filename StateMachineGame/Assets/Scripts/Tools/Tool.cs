using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Tool
{
    public Sprite sprite;
    public Vector2 offset = Vector2.zero;

    public string displayName
    {
        get;
        protected set;
    }
    public string name
    {
        get;
        protected set;
    }
    public string type
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
    public Tool(string displayName, string name, string type, float radius, Sprite sprite) { this.displayName = displayName; this.name = name; this.type = type; toolRadius = radius; this.sprite = sprite; }

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
