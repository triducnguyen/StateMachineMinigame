using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyOutBehaviour : AIBehaviour
{
    public FlyOutBehaviour(AI ai)
    {
        behaviourAction = new Action(() => { BirdPosFlyOffScreen(); });
        this.ai = ai;
        name = "flyOut";
    }


    void BirdPosFlyOffScreen()
    {
        ai.aStar.destination = new Vector2(5, -4);

    }
}