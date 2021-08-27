using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchBehaviour : Behaviour, IBehaviour
{

    public SearchBehaviour
        (
            Tuple<List<Condition>,List<Condition>> transitionConditions
            
        ) 
        :base //set the base constructor arguments
        (
            transitionConditions.Item1, transitionConditions.Item2
        )
    {}


}
