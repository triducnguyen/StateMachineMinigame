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
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(-3, Screen.height, 0));
        ai.transform.position = pos;

    }
}