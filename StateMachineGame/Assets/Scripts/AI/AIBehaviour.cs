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

    List<Coroutine> coroutines = new List<Coroutine>();

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
        coroutines.Add(ai.StartCoroutine(Behaviour()));
    }

    public virtual void OnExit() //Command to be called when behaviour is exited
    {
        ai.StopCoroutine(Behaviour()); //cancel current behaviour
        foreach (var routine in coroutines)
        {
            ai.StopCoroutine(routine);
        }
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
}
