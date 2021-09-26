using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly2Behaviour : AIBehaviour
{
    public Fly2Behaviour(AI ai)
    {
        behaviourAction = new Action(() => { BirdPosOnScreen(); });
        this.ai = ai;
        name = "fly";
    }


    void BirdPosOnScreen()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        ai.transform.position = pos;

    }
}
