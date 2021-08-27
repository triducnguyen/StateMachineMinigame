using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Seeker seeker //moves the ai around
    {
        get => _seeker;
        protected set => _seeker = value;
    } 
    Seeker _seeker;

    public CancellationTokenSource source = new CancellationTokenSource(); //What allows us to cancel a behaviour 

    public Behaviour currentBehaviour
    {
        get { return _currentBehaviour; }
        protected set { _currentBehaviour.OnExit(); _currentBehaviour = value; } //exits the current behaviour before it is changed
    } //Current behaviour tells ai how to act
    Behaviour _currentBehaviour;

    protected List<Behaviour> behaviours; //list of possible behaviours

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Behaviour next;
        //check enter conditions on all states
        foreach (var beh in behaviours)
        {
            if (Behaviour.CheckConditions(beh.enterConditions, out next))
            {
                //found a state to enter, set it as current state
                currentBehaviour = next;
                //enter the new state
                currentBehaviour.OnEnter();
                return; //dont allow a state to check conditions until next update
            }
        }

        //check exit conditions for current state
        
        if (Behaviour.CheckConditions(currentBehaviour.exitConditions, out next))
        {
            //swap to new state
            currentBehaviour = next;
            currentBehaviour.OnEnter();
        }
    }
}
