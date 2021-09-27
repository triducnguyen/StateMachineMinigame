using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BirdAi : AI
{
    public bool ReachedDest = false;
    public bool ReachedDest1 = false;

    protected override void Awake()
    {
        
        base.Awake();
        //var fly = new FlyBehaviour(this);
        var flyOn = new Fly2Behaviour(this);
        var flyOut = new FlyOutBehaviour(this);

        //fly.enterConditions.Add(new Condition(() => CheckForFood() == null));
        //fly.exitConditions.Add(new Condition(() => CheckForFood() != null));

        flyOn.enterConditions.Add(new Condition(() => GameManager.instance.growables.Count > 10 && CheckForFood() != null));
        //fly.exitConditions.Add(new Condition(() => ReachedDest));

        flyOut.enterConditions.Add(new Condition(() => ReachedDest));
        //flyout.exitConditions.Add(new Condition(() =>ReachedDest1));
        
       

        //behaviours.Add(fly);
        behaviours.Add(flyOn);
        behaviours.Add(flyOut);
    }
    public Growable CheckForFood()
    {
        if (GameManager.instance.growables.Count > 0)
        {

            return GameManager.instance.growables[UnityEngine.Random.Range(0, GameManager.instance.growables.Count - 1)];
        }
        else
        {
            return null;
        }
    }
    public override void EnterBehaviour(AIBehaviour behaviour)
    {
        base.EnterBehaviour(behaviour);
    }
    public override void CheckNewBehviours()
    {
        base.CheckNewBehviours();
    }
   
    protected override void Update()
    {
        base.Update();
    }
}
