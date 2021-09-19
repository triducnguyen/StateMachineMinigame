using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BirdAi : AI
{
    protected override void Awake()
    {
        base.Awake();
        var fly = new WanderBehaviour(this, 3, 4f);
        //var hungry = new HungryBehaviour(this, 1f, 1f);
        

        fly.enterConditions.Add(new Condition(() => energy <= 25));
        fly.exitConditions.Add(new Condition(() => energy > 25, fly));

        //hungry.exitConditions.Add(new Condition(() => energy <= 25));

        behaviours.Add(fly);
        //behaviours.Add(hungry);
    }

    protected override void Update()
    {
        base.Update();
    }
}
