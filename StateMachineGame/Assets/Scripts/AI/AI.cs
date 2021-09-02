using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
public class AI : MonoBehaviour
{
    //position
    Vector2 position2D
    {
        get => transform.position;
    }

    //behaviour

    public AIBehaviour currentBehaviour
    {
        get { return _currentBehaviour; }
        protected set 
        {
            if (currentBehaviour is object)
            {
                _currentBehaviour.OnExit();
            }
             _currentBehaviour = value;
        } //exits the current behaviour before it is changed
    } //Current behaviour tells ai how to act
    AIBehaviour _currentBehaviour;

    protected List<AIBehaviour> behaviours = new List<AIBehaviour>(); //list of possible behaviours

    //a*
    public PathFinder aStar;

    //AI Agro Collider
    public CircleCollider2D agro;

    protected virtual void Awake()
    {
        if (aStar is null)
        {
            aStar = GetComponent<PathFinder>();
        }
        aStar.DestinationReached += OnTargetReached;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
         
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        AIBehaviour next;
        //check enter conditions on all states
        foreach (var beh in behaviours)
        {
            if (beh != currentBehaviour && AIBehaviour.CheckConditions(beh.enterConditions, out next))
            {
                //found a state to enter, set it as current state
                currentBehaviour = beh;
                //enter the new state
                currentBehaviour.OnEnter();
                return; //dont allow a state to check conditions until next update
            }
        }

        //check exit conditions for current state
        if (currentBehaviour is object)
        {
            if (AIBehaviour.CheckConditions(currentBehaviour.exitConditions, out next))
            {
                //swap to new state
                currentBehaviour = next;
                currentBehaviour.OnEnter();
            }
        }
        
    }

    public void OnTargetReached()
    {
        //check if first time getting to target

        if (currentBehaviour is object)
        {
            currentBehaviour.OnTargetReached();
        }
    }
}
