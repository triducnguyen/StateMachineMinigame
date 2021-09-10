using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class Condition
{
    public System.Linq.Expressions.Expression<Func<bool>> check;
    public Func<bool> checkFunction;
    [SerializeField]
    string expression;
    public bool givesNewBehaviour = true;

    public AIBehaviour newBehaviour
    {
        get
        {
            if (checkFunction.Invoke())
            {
                return _newBehaviour;
            }
            return null;
        }
        set
        {
            givesNewBehaviour = value is null ? false : true;
            _newBehaviour = value;
        }
    }
    [NonSerialized]
    AIBehaviour _newBehaviour;

    public Condition(System.Linq.Expressions.Expression<Func<bool>> condition, AIBehaviour newBehaviour = null)
    {
        check = condition;
        checkFunction = condition.Compile();
        expression = condition.ToString();
        this.newBehaviour = newBehaviour;
    }
}
