using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehaviour : AIBehaviour
{
    public FlyBehaviour(AI ai)
    {
        behaviourAction = new Action(() => { BirdPosOffScreen(); }) ;
        this.ai = ai;
        name = "fly";
    }


    void BirdPosOffScreen()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(-3, Screen.height, 0));
        ai.transform.position = pos;
        
    }
}
