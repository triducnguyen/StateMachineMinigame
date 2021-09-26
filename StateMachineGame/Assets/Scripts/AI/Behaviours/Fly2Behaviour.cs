using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly2Behaviour : AIBehaviour
{
    Growable growable;
    public Fly2Behaviour(AI ai)
    {
        behaviourAction = new Action(() => { FlyOnScreen(); });
        this.ai = ai;
        name = "flyon";
    }

    void FlyOnScreen()
    {
        BirdPosOnScreen();
        DoBirdMoveToCrop();
       
    }
    void BirdPosOnScreen()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(8, 8, 5));
        ai.transform.position = pos;

    }
    void DoBirdMoveToCrop()
    {
        growable = ((BirdAi)ai).CheckForFood();
        ai.aStar.destination = growable.transform.position;
        ai.aStar.canSearch = true;
        ai.aStar.SearchPath();
        
    }
    /*public  IEnumerator BirdMoveToCrop(float delayTime)
    {
        while (Vector3.Distance(ai.transform.position, growable.transform.position)>= 1) 
        {
            yield return new WaitForSeconds(delayTime);
            ai.transform.position = Vector3.Lerp(ai.transform.position, growable.transform.position, (.1f));
        } 
        
    }
    */
    
}
