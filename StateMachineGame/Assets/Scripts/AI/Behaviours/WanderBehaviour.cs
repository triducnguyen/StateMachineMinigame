using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using URandom = UnityEngine.Random;
using Pathfinding;
public class WanderBehaviour : AIBehaviour, IBehaviour
{

    public float maxDistance = 10;

    public int pauseTime = 5;

    public WanderBehaviour(AI ai, List<Condition> enter, List<Condition> exit, int pauseTime, float maxDist, Action action = null)
    {
        this.ai = ai;
        enterConditions = enter;
        exitConditions = exit;
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
        ai.destination = direction;
        ai.canSearch = true;
        ai.SearchPath();
    }

    Vector2 GetPointInDirection(Vector2 origin, Vector2 direction, float maxNodeDistance)
    {
        
        //generate distance of new point
        var dist = URandom.Range(0.2f, maxNodeDistance);
        //get angle from direction
        var angle = Vector2.Angle(Vector2.zero, direction);
        //create some variance to the angle
        var variance = URandom.Range(angle - 20, angle + 20);
        //convert angle back to normalized vector
        var newDirection = DegreeToVector2(variance);
        //return scaled vector
        return newDirection * dist;
    }

    public Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
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
        //start the behaviour again
        ai.canSearch = false;
        ai.StartCoroutine(DelayedBehaviour(pauseTime));
    }
}
