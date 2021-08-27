using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Pathfinding;
public class WanderBehaviour : Behaviour, IBehaviour
{
    public WanderBehaviour() { }
    public WanderBehaviour(AI ai, Tuple<List<Condition>,List<Condition>> conditions, Action action ) : base(ai, conditions.Item1,conditions.Item2, action )
    {}

    /// <summary>
    /// Method <c>Explore</c> generates a path of length <paramref name="pathLength"/>
    /// that does not exceed <paramref name="max"/> distance from <paramref name="origin"/>.
    /// The max distance between nodes on a path is determined by <paramref name="maxNodeDistance"/>.
    /// <paramref name="onNodeComplete"/> will be executed every time a node in the path has completed.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="max"></param>
    /// <param name="pathLength"></param>
    /// <param name="maxNodeDistance"></param>
    /// <param name="onNodeComplete"></param>
    async void Explore(Vector2 origin, float max, int pathLength, float maxNodeDistance, Action onNodeComplete)
    {
        if (pathLength < 1)
        {
            Console.WriteLine("Invalid path length.");
            return;
        }
        //generate path length
        var length = UnityEngine.Random.Range(0.1f, pathLength);
        
        


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


}
