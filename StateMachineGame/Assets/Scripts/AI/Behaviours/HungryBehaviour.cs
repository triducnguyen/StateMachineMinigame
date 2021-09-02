using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryBehaviour : AIBehaviour
{
    //find food!

    public HungryBehaviour(PathFinder aStar, List<Condition> enter, List<Condition> exit, Action action)
        : base(aStar, enter, exit, action)
    {
        
    }
}
