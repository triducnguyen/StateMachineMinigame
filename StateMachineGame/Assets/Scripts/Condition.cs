using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    public Func<bool> check;
    public AIBehaviour newBehaviour
    {
        get
        {
            if (check.Invoke())
            {
                return _newBehaviour;
            }
            return null;
        }
        set
        {
            _newBehaviour = value;
        }
    }
    AIBehaviour _newBehaviour;

    public Condition(Func<bool> condition, AIBehaviour newBehaviour = null)
    {
        check = condition;
        this.newBehaviour = newBehaviour;
    }
}
