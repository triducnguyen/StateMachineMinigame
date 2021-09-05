using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HungryBehaviour : AIBehaviour
{
    //find food!

    List<Growable> edibles = new List<Growable>();
    
    public HungryBehaviour(PathFinder aStar, Action action)
    {
        name = "Hungry";    
    }
}
