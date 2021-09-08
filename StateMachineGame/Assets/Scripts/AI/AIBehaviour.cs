using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class AIBehaviour : IBehaviour
{
    //weight will determine how important this behaviour is.
    //When multiple behaviours are active, higher weights are prioritized
    float weight = 0;

    public string name = "Default";
    public bool enabled
    {
        get;
        protected set;
    }
    bool _enabled = false;

    public AI ai
    {
        get => _ai;
        set {
            _ai = value;
            aStar = value.aStar;
        }
    }
    [SerializeField]
    AI _ai;

    public PathFinder aStar 
    {
        get => _aStar;
        set => _aStar = value;
    }
    [SerializeField]
    PathFinder _aStar;

    //location that his behaviour wants to go to
    public Vector2 target
    {
        get => _target;
        protected set => _target = value;
    }
    [SerializeField]
    Vector2 _target;

    public List<Condition> enterConditions = new List<Condition>();
    public List<Condition> exitConditions = new List<Condition>();

    public List<Coroutine> coroutines = new List<Coroutine>();
    
    [SerializeField]
    protected Action behaviourAction; //what the behaviour actually does. Called only when behaviour is entered.

    public AIBehaviour() { }

    public AIBehaviour(AI ai, Action action)
    {
        this.ai = ai;
        behaviourAction = action;
    }

    public virtual void OnEnter() //Command to be called when behaviour is entered
    {
        enabled = true;
        aStar.canSearch = true;
        aStar.canMove = true;
        StartCoroutine(Behaviour());
    }

    public virtual void OnExit() //Command to be called when behaviour is exited
    {
        enabled = false;
        //clear pathfinding
        aStar.destination = ai.position2D;
        aStar.canSearch = false;
        aStar.canMove = false;
        StopCoroutines();
    }

    protected void StartCoroutine(IEnumerator routine)
    {
        if (enabled)
        {
            coroutines.Add(ai.StartCoroutine(routine));
        }
        
    }

    protected void StopCoroutines()
    {
        for ( int i = coroutines.Count-1; i>=0; i--)
        {
            var routine = coroutines[i];
            ai.StopCoroutine(routine);
            coroutines.RemoveAt(i);
        }
    }

    public bool CheckConditions(List<Condition> list, out AIBehaviour newState)
    {
        newState = null;
        foreach (var con in list)
        {
            if (con.givesNewBehaviour && con.newBehaviour is object)
            {
                newState = con.newBehaviour;
                return true;
            }
            else if (!con.givesNewBehaviour && con.checkFunction.Invoke())
            {
                return true;
            }
        }
        return false;
    }

    //when AI has reached a location. May not be the location
    //desired by this behaviour
    public virtual void OnTargetReached()
    {
        
    }

    public IEnumerator DelayedBehaviour(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        behaviourAction.Invoke();
    }

    public IEnumerator Behaviour()
    {
        behaviourAction.Invoke();
        yield return null;
    }

    public IEnumerator RepeatedAction(float delay, Action action)
    {
        while (true)
        {
            action.Invoke();
            yield return new WaitForSeconds(delay);
        }
    }
}
