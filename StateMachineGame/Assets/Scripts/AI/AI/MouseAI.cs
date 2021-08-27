using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAI : AI
{
    Vector2 home;

    Behaviour currentBehaviour;

    int hunger = 0;
    int energy = 100;

    private void Awake()
    {
        behaviours = new List<Behaviour> //base class variable
        {
            new WanderBehaviour()
            {
                ai = this,
                enterConditions = new List<Condition>()
                {
                    //hunger is below 60 & energy is > 50
                    new Condition()
                    {
                        check = () => hunger < 60 && energy > 50
                    }
                },
                exitConditions = new List<Condition>() 
                {

                },
            },
            
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        //check if mouse doesn't have a home
            //make a home
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
