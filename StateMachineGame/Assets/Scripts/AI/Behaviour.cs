using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Behaviour : IBehaviour
{
    public AI ai 
    {
        get => _ai;
        set => _ai = value;
    }
    AI _ai;

    CancellationToken token; //for cancelling a behaviour

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

    public Behaviour() { }

    public Behaviour(AI ai, List<Condition> enter, List<Condition> exit, Action action)
    {
        this.ai = ai;
        token = ai.source.Token;
        enterConditions = enter;
        exitConditions = exit;
        behaviourAction = action;
    }

    public virtual void OnEnter() //Command to be called when behaviour is entered
    {
        try
        {
            Task t = Task.Factory.StartNew(behaviourAction, token);
        }
        catch (AggregateException ae)
        {
            foreach (Exception e in ae.InnerExceptions)
            {
                if (e is TaskCanceledException)
                {
                    Console.WriteLine("Behaviour cancelled...", ((TaskCanceledException)e).Message);
                }
                else
                {
                    Console.WriteLine("Exception: " + e.GetType().Name);
                }
            }
        }
    }

    public virtual void OnExit() //Command to be called when behaviour is exited
    {
        token.ThrowIfCancellationRequested(); //cancel current behaviour
    }

    public static bool CheckConditions(List<Condition> list, out Behaviour newState)
    {
        foreach (var con in list)
        {
            if (con.newBehaviour is object)
            {
                newState = con.newBehaviour;
            }
        }
        newState = null;
        return false;
    }
}
