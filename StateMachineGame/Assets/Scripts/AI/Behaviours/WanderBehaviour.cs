using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using URandom = UnityEngine.Random;
using Pathfinding;

[System.Serializable]
public class WanderBehaviour : AIBehaviour, IBehaviour
{
    
    public float maxDistance = 10;

    public int pauseTime = 5;

    public WanderBehaviour(AI ai, int pauseTime, float maxDist, Action action = null)
    {
        name = "Wander";
        this.ai = ai;
        behaviourAction = action is null ? () => Explore(maxDistance) : action;
        this.pauseTime = pauseTime;
        maxDistance = maxDist;
    }

    /// <summary>
    /// Method <c>Explore</c> generates a path 
    /// that does not exceed <paramref name="max"/> distance from <paramref name="origin"/>.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="max"></param>
    void Explore(float max)
    {
        //get direction
        var direction = URandom.insideUnitCircle * URandom.Range(0, max); //get a random point within circle
        target = direction;
        aStar.destination = target;
        aStar.canSearch = true;
        aStar.SearchPath();
        aStar.DestinationReached += OnTargetReached;
    }

    /// <summary>
    ///  Method <c>GetRandomPos</c> gets a random point within a circle with its middle at <paramref name="center"/> and a radius of <paramref name="r"/>.
    /// </summary>
    /// <param name="center"></param>
    /// <param name="r"></param>
    /// <returns></returns>

    public Vector2 GetRandomPos(Vector2 center, float r)
    {
        var normalizedPoint = UnityEngine.Random.insideUnitCircle;
        var newPoint = normalizedPoint * r;
        return newPoint;
    }

    /// <summary>
    /// Method <c>GetRandomPos</c> gets a random point at least <paramref name="r1"/> far and at most <paramref name="r2"/> far from <paramref name="center"/>.
    /// <paramref name="r1"/> and <paramref name="r2"/> will switch if <paramref name="r1"/> is larger than <paramref name="r2"/>.
    /// </summary>
    /// <param name="center"></param>
    /// <param name="r1"></param>
    /// <param name="r2"></param>
    /// <returns></returns>

    public Vector2 GetRandomPos(Vector2 center, float r1, float r2)
    {
        float upperBound = r2;
        float lowerBound = r1;
        if (r1 > r2)
        {
            upperBound = r1;
            lowerBound = r2;
        }
        var normalizedPoint = UnityEngine.Random.insideUnitCircle;
        var newPoint = normalizedPoint * UnityEngine.Random.Range(lowerBound, upperBound);
        return newPoint;
    }

    public override void OnTargetReached()
    {
        //check if it was the destination we wanted
        if ((Vector2)(aStar.destination) == target)
        {
            //start the behaviour again
            aStar.DestinationReached -= OnTargetReached;
            aStar.canSearch = false;
            StartCoroutine(DelayedBehaviour(pauseTime));
        }
    }

    public override void OnExit()
    {
        aStar.DestinationReached -= OnTargetReached;
        base.OnExit();
    }
}
