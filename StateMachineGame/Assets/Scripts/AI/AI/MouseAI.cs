using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MouseAI : AI
{
    Vector2 home;

    protected override void Awake()
    {
        base.Awake();
        var wander = new WanderBehaviour(this, 3, 4f);

        wander.enterConditions.Add(new Condition(() => hunger <= 50 && energy >= 45) );

        behaviours.Add(wander);
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        //base.Start();
        //check if mouse doesn't have a home
            //make a home
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
