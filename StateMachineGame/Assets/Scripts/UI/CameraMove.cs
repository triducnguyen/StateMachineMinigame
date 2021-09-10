using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : FingerMove
{
    protected override void _processSwipe(Vector2 screenTravel)
    {
        //move the game world
        transform.position += new Vector3(screenTravel.x, screenTravel.y, transform.position.z);
    }

    protected override void _processPinch(float delta)
    {
        //zoom
        base._processPinch(delta);
    }
}
