using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    public Func<bool> check;
    public Behaviour newBehaviour
    {
        get
        {
            if (check.Invoke())
            {
                return _newBehaviour;
            }
            return null;
        }
    }
    Behaviour _newBehaviour;
}
