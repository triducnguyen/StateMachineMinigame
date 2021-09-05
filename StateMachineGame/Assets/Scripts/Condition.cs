using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    public Func<bool> check;
    [SerializeField]
    string checkExpression;
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
    [SerializeField]
    AIBehaviour _newBehaviour;

    public Condition(Func<bool> condition, AIBehaviour newBehaviour = null)
    {
        check = condition;
        checkExpression = check.ToString();
        this.newBehaviour = newBehaviour;
    }
}
