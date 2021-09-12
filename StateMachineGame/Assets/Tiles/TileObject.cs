using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{

    public Vector2 tilePos
    {
        get => transform.position;
    }
    public ExtendedRuleTile tile;

    public string type
    {
        get => _type;
        protected set => _type = value;
    }
    string _type = "Default";

    private void Awake()
    {
        //initialize tile object
        if ( this is object && tile is null)
        {
            tile = (ExtendedRuleTile)GameManager.Instance.tilemap.GetTile(Vector3Int.FloorToInt(tilePos));
            type = tile.tile;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //
    }
    
    private void OnDestroy()
    {
        //what to do when tile is destroyed/changed
    }
}
