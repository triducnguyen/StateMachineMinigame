using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour : IBehaviour
{
    public List<Func<bool>> EnterConditions
    {
        get;
        protected set;
    }
    public List<Func<bool>> ExitConditions
    {
        get;
        protected set;
    }

    public Behaviour(List<Func<bool>> enter, List<Func<bool>> exit)
    {
        EnterConditions = enter;
        ExitConditions = exit;
    }

    public virtual void OnEnter() //Command to be called when behaviour is entered
    {
    }

    public virtual void OnExit() //Command to be called when behavior is exited
    {
    }

    public virtual bool CheckConditions(List<Func<bool>> list)
    {
        foreach (var con in list)
        {
            if (con.Invoke())
            {
                return true;
            }
        }
        return false;
    }
}
