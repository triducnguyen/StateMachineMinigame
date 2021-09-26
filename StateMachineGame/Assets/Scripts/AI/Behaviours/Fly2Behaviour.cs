using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly2Behaviour : AIBehaviour
{
    public Fly2Behaviour(AI ai)
    {
        behaviourAction = new Action(() => { BirdPosOnScreen(); DoBirdMoveToCrop(10f); });
        this.ai = ai;
        name = "flyon";
    }


    void BirdPosOnScreen()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(8, 8, 5));
        ai.transform.position = pos;

    }
    void DoBirdMoveToCrop(float delayTime)
    {
        Vector3 pos = Growable.position;
        ai.transform.position = pos;
    }
    /*public  IEnumerator BirdMoveToCrop(float 10f);
    {
    yield return new WaitForSeconds(delayTime)
    }
    */
}
