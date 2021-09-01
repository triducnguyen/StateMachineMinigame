using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class AI : AIPath, IAstarAI
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

    //AI Agro Collider
    public CircleCollider2D agro;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();   
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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

    public override void OnTargetReached()
    {
        //check if first time getting to target

        if (currentBehaviour is object)
        {
            currentBehaviour.OnTargetReached();
        }
    }
}
