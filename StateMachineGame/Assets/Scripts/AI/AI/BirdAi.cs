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
        var fly = new FlyBehaviour(this);
        var fly2 = new Fly2Behaviour(this);
        //var move = new WanderBehaviour(this, 3, 4f);
        //var hungry = new HungryBehaviour(this, 1f);

        fly.enterConditions.Add(new Condition(() => energy >= 24)) ;
       // fly2.enterConditions.Add(new Condition(()=> energy >= 24 && CheckForFood() != null));
       /// fly.exitConditions.Add(new Condition(() => CheckForFood() != null));

        //hungry.exitConditions.Add(new Condition(() => energy <= 25));

       // behaviours.Add(move);
       // behaviours.Add(hungry);
       behaviours.Add(fly);
        behaviours.Add(fly2);
    }

    protected override void Update()
    {
        base.Update();
    }
}
