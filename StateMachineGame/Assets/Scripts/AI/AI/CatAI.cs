using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : AI
{

    protected override void Awake()
    {
        base.Awake();
        var tired = new SleepBehaviour(this, 1f, 1f);
        var explore = new WanderBehaviour(this, 3, 4f);

        tired.enterConditions.Add(new Condition(() => energy <= 25));
        tired.exitConditions.Add(new Condition(() => energy > 25, explore));

        explore.exitConditions.Add(new Condition(() => energy <= 45, tired));

        behaviours.Add(explore);
        behaviours.Add(tired);
    }

    protected override void Update()
    {
        base.Update();
    }
}
