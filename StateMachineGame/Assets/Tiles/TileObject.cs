using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof())]
public class TileObject : MonoBehaviour
{
    public delegate void TileChanged(object sender, System.EventArgs args);

    public event TileChanged Destroyed;

    public bool occupied = false;
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
        //check occupied
        CheckOccupied();
    }

    private void CheckOccupied()
    {
        Camera cam = GameManager.Instance.cam;
        Vector3 worldPos = tilePos * 2;
        worldPos.z = -3;
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(worldPos), out hit, 4f, LayerMask.NameToLayer("occupied")))
        {
            //position is occupied
            occupied = true;
        }
    }

    private void OnDestroy()
    {
        if (Destroyed is object)
        {
            Destroyed(this, new System.EventArgs());
        }
    }
}
