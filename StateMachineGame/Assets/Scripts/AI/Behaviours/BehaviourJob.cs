using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public struct BehaviourJob : IJob
{
    public Action behaviourAction;

    public void Execute()
    {
        behaviourAction.Invoke();
    }
}
