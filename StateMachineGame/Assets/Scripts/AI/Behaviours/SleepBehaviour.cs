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

    void RegainEnergy()
    {
        ai.energy += energyGain;
    }

    void Sleep()
    {
        StartCoroutine(RepeatedAction(sleepInterval, () => RegainEnergy()));
    }

    public override void OnEnter()
    {
        Sleep();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
