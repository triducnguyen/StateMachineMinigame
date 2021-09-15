using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof())]
public class TileObject : MonoBehaviour
{

    public delegate void TileChanged(object sender, System.EventArgs args);
    public event TileChanged Destroyed;
    public GameObject occupier;

    public bool occupied = false;

    private void Awake()
    {
        //check occupied
        CheckOccupied();
    }

    protected virtual void CheckOccupied()
    {
        if (occupier != null)
        {
            occupied = true;
        }
        else
        {
            occupied = false;
        }
        //RaycastHit hit;
        //if (Physics.Raycast(GameManager.Instance.cam.ScreenPointToRay(worldPos), out hit, 4f, LayerMask.NameToLayer("Occupied")) )
        //{
        //    //position is occupied
        //    occupied = true;
        //    occupier = hit.collider.gameObject;
        //    return;
        //}
    }

    public void SetOccupier(GameObject occupier)
    {
        this.occupier = occupier;
        occupied = true;
    }

    public virtual void DestroyOccupier()
    {
        if (occupier != null)
        {
            Destroy(occupier);
            occupier = null;
            occupied = false;
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
