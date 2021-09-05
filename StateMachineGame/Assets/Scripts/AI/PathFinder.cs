using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public delegate void TargetReachedHandler();
public class PathFinder : AIPath
{
    public event TargetReachedHandler DestinationReached;

    public override void OnTargetReached()
    {
        base.OnTargetReached();
        if (DestinationReached is object)
        {
            DestinationReached();
        }
        
    }

}
