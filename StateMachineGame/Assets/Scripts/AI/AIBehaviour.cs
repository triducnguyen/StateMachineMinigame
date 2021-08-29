using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AIBehaviour : IBehaviour
{
    public AI ai 
    {
        get => _ai;
        set => _ai = value;
    }
    AI _ai;

    public List<Condition> enterConditions
    {
        get;
        set;
    }
    public List<Condition> exitConditions
    {
        get;
        set;
    }

    protected Action behaviourAction; //what the behaviour actually does. Called only when behaviour is entered.

    public AIBehaviour() { }

    public AIBehaviour(AI ai, List<Condition> enter, List<Condition> exit, Action action)
    {
        this.ai = ai;
        enterConditions = enter;
        exitConditions = exit;
        behaviourAction = action;
    }

    public virtual void OnEnter() //Command to be called when behaviour is entered
    {
        ai.StartCoroutine(Behaviour());
    }

    public virtual void OnExit() //Command to be called when behaviour is exited
    {
        ai.StopCoroutine(Behaviour()); //cancel current behaviour
        ai.StopCoroutine(DelayedBehaviour());
    }

    public static bool CheckConditions(List<Condition> list, out AIBehaviour newState)
    {
        newState = null;
        foreach (var con in list)
        {
            if (con.check.Invoke())
            {
                newState = con.newBehaviour;
                return true;
            }
        }
        return false;
    }

    public virtual void OnTargetReached()
    {
        //start the behaviour again
        ai.canSearch = false;
        ai.StartCoroutine(DelayedBehaviour());
    }

    public IEnumerator DelayedBehaviour()
    {
        yield return new WaitForSeconds(2);
        behaviourAction.Invoke();
    }

    public IEnumerator Behaviour()
    {
        behaviourAction.Invoke();
        yield return null;
    }
}
