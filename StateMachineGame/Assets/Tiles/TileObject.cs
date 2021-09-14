using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof())]
public class TileObject : MonoBehaviour
{
    Coroutine occupiedCheck;

    public delegate void TileChanged(object sender, System.EventArgs args);
    public event TileChanged Destroyed;
    public GameObject occupier
    {
        get
        {
            return _occupier;
        }
        set
        {
            _occupier = value;
        }
    }
    GameObject _occupier = null;

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
        if (tile == null)
        {
            tile = (ExtendedRuleTile)GameManager.Instance.tilemap.GetTile(Vector3Int.FloorToInt(tilePos));
            type = tile.tile;
        }
        //check occupied
        CheckOccupied();
    }

    protected virtual void CheckOccupied()
    {
        Vector3 worldPos = tilePos * 2;
        worldPos.z = -3;
        if (occupier != null)
        {
            occupied = true;
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(GameManager.Instance.cam.ScreenPointToRay(worldPos), out hit, 4f, LayerMask.NameToLayer("Occupied")) )
        {
            //position is occupied
            occupied = true;
            occupier = hit.collider.gameObject;
            return;
        }
        occupied = false;
    }

    public void SetOccupier(GameObject occupier)
    {
        this.occupier = occupier;
        CheckOccupied();
    }

    public virtual void OnOccupierDestroyed(System.EventArgs args)
    {
        occupiedCheck = StartCoroutine(CheckOccupier());
    }

    IEnumerator CheckOccupier()
    {
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(occupiedCheck);
        CheckOccupied();
    }

    public virtual void DestroyOccupier()
    {
        Destroy(occupier);
        occupier = null;
        CheckOccupied();
    }

    private void OnDestroy()
    {
        if (Destroyed is object)
        {
            Destroyed(this, new System.EventArgs());
        }
    }
}
