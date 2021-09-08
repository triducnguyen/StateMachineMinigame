using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using URandom = UnityEngine.Random;
using Pathfinding;

[System.Serializable]
public class ScaredBehaviour : AIBehaviour, IBehaviour
{
    public float scareDistance;
    private Vector3 target;
    public Transform mouse;
    public Transform cat;

   public ScaredBehaviour(AI ai, float scareDistance,Transform mouse,Transform cat, Action action = null)
    {
        name = "Scare";
        this.ai = ai;
        this.scareDistance = scareDistance;
        this.mouse = mouse;
        this.cat = cat; 
        behaviourAction = action is null ? () => Scared(scareDistance) : action;
    }

    public void Scared(float max)
    {
        Vector3 direction = new Vector3(cat.position.x, cat.position.y, cat.position.z);
        Vector3 oppisiteDirection = -direction;
        target = oppisiteDirection;
        aStar.destination = target;
        aStar.canSearch = true;
        aStar.SearchPath();
        aStar.DestinationReached += OnTargetReached;
    }

    public override void OnTargetReached()
    {
        //check if it was the destination we wanted
        if (aStar.destination == target)
        {
            //start the behaviour again
            aStar.canSearch = false;
        }
    }
    public override void OnExit()
    {
        aStar.DestinationReached -= OnTargetReached;
        base.OnExit();
    }
}
