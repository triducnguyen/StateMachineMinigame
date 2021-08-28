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
    float maxDistance = 10;

    int pauseTime = 5;

    public WanderBehaviour() { }
    public WanderBehaviour(AI ai, Tuple<List<Condition>,List<Condition>> conditions, Action action ) : base(ai, conditions.Item1,conditions.Item2, action )
    {}

    /// <summary>
    /// Method <c>Explore</c> generates a path 
    /// that does not exceed <paramref name="max"/> distance from <paramref name="origin"/>.
    /// <paramref name="onPathComplete"/> will be executed every time a node in the path has completed.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="max"></param>
    /// <param name="onPathComplete"></param>
    void Explore(Vector2 origin, float max, OnPathDelegate onPathComplete)
    {
        //get direction
        var direction = URandom.insideUnitCircle * URandom.Range(0, max); //get a random point within circle
        StartPath(origin, direction, max, onPathComplete);
    }

    void StartPath(Vector2 origin, Vector2 direction, float max, OnPathDelegate onPathComplete)
    {

        //A little bit confusing but I'll break it down

        //this function starts a new path for the seeker to follow.
        ai.seeker.StartPath(
            origin,     //Where the AI currently is
            direction,  //Where we want to go
            //This is a function thats executed when the seeker completes a path
            onPathComplete
        );
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

    void OnPathComplete(Path path)
    {
        Task.Factory.StartNew //creating a new task on a new thread
                (
                    () =>
                    {
                        Thread.Sleep(pauseTime * 1000);                         //wait
                        Explore(ai.position2D, maxDistance, OnPathComplete);    //Explore again
                    },
                    token //token for cancelling behaviour
                );
    }

    public override void OnEnter()
    {
        Explore(
            ai.position2D,
            maxDistance,
            OnPathComplete
            );
                
    }

}
