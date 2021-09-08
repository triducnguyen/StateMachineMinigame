using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SleepBehaviour : AIBehaviour
{
    
    public float sleepInterval;
    public float energyGain;
    public SleepBehaviour(AI ai, float interval, float gain, Action behaviour = null)
    {
        name = "Tired";
        this.ai = ai;
        sleepInterval = interval;
        energyGain = gain;
        behaviourAction = behaviour is null ? () => Sleep() : behaviour;
    }

    void Sleep()
    {
        StartCoroutine(RepeatedAction(sleepInterval, () => ai.energy += energyGain));
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
