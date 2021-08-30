using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryBehaviour : WanderBehaviour
{
    //find food!

    public HungryBehaviour(AI ai, List<Condition> enter, List<Condition> exit, Action action) : base(ai, enter, exit, action)
    {
        
    }
}
