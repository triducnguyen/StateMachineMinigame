using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BirdAi : AI
{
    protected override void Awake()
    {
        
        base.Awake();
        //var fly = new FlyBehaviour();
        var move = new WanderBehaviour(this, 3, 4f);
        //var hungry = new HungryBehaviour(this, 1f);

       // fly.enterConditions.Add(new Condition(() => energy <= 25));
        move.enterConditions.Add(new Condition(() => energy <= 25));
        move.exitConditions.Add(new Condition(() => energy > 25, move));

        //hungry.exitConditions.Add(new Condition(() => energy <= 25));

        behaviours.Add(move);
       // behaviours.Add(hungry);
       // behaviour.Add(fly);
    }

    protected override void Update()
    {
        base.Update();
    }
}
