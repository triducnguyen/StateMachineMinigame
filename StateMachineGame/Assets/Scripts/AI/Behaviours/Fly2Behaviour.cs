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
        name = "flyOn";
    }

    void FlyOnScreen()
    {
        ai.aStar.DestinationReached += CropReached;
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
    void CropReached()
    {
        aStar.DestinationReached -= CropReached;
        ((BirdAi)ai).ReachedDest = true;
        ai.ExitBehaviour(this);  
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
