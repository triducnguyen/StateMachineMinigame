using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BirdAi : AI
{
    protected override void Awake()
    {
        
        base.Awake();
        var fly = new FlyBehaviour(this);
        var flyon = new Fly2Behaviour(this);

        fly.enterConditions.Add(new Condition(() => CheckForFood() == null));
        fly.exitConditions.Add(new Condition(() => CheckForFood() != null));

        flyon.enterConditions.Add(new Condition(()=> CheckForFood() != null));
        flyon.exitConditions.Add(new Condition(() => hunger <= 20));

       


        behaviours.Add(fly);
        behaviours.Add(flyon);
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
