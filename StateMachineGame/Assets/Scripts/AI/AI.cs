using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Seeker seeker;

    public Vector2 position2D
    {
        get => new Vector2(aiTransform.position.x, aiTransform.position.y);
    }
    public Transform aiTransform;

    public CancellationTokenSource source = new CancellationTokenSource(); //What allows us to cancel a behaviour 

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

    protected List<AIBehaviour> behaviours; //list of possible behaviours

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
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
}
