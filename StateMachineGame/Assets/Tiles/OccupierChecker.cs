using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TileObject;

public class OccupierChecker : MonoBehaviour
{
    public delegate void OccupierAltered(object sender, System.EventArgs args);

    public event OccupierAltered OccupierChanged;

    public void Changed()
    {
        OccupierChanged(this, new System.EventArgs());
    }

}
