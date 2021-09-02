using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAI : AI
{
    Vector2 home;

    int hunger = 0;
    int energy = 100;

    protected override void Awake()
    {
        base.Awake();
        behaviours = new List<AIBehaviour>
        {
            new WanderBehaviour(        //Wander/Exploring behaviour
                aStar,                   //pass reference of pathfinder to behaviour
                new List<Condition>()   //Enter conditions of behaviour
                {
                    new Condition(() => hunger <= 50 && energy >= 45) 
                },
                new List<Condition>()   //Exit conditions of behaviour
                {
                    //new Condition(() => hunger > 50 && energy < 45, )
                },
                3, //pause time
                4f,//max distance from location
                null //use default action (explore)
            ),
            
        };
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //check if mouse doesn't have a home
            //make a home
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
