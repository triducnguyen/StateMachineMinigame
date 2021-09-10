using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MouseAI : AI
{
    Vector2 home;
    private float dist;
    public Transform cat;
    private Transform mouse;
    private ScaredBehaviour scare;
    private bool scared;

    protected override void Awake()
    {
        base.Awake();
        mouse = GetComponent<Transform>();

        var wander = new WanderBehaviour(this, 3, 4f);
        var scare = new ScaredBehaviour(this, 3f, mouse, cat);
        this.scare = scare;

        wander.enterConditions.Add(new Condition(() => hunger <= 50 && energy >= 45 && !scared));
        wander.exitConditions.Add(new Condition(() => scared, scare));

        scare.exitConditions.Add(new Condition(() => scare.reachedDestination, wander));

        behaviours.Add(wander);
        behaviours.Add(scare);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        //base.Start();
        //check if mouse doesn't have a home
            //make a home
    }


    IEnumerator CheckScare()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            //get distance from closest cat
            var cats = manager.ai.Find(match => match.GetType() == typeof(CatAI));
            float dist = 0f;
            //find closest cat
            //check distance
            if (dist < 3f)
            {
                scared = true;
            }
            else
            {
                scared = false;
            }
            
        }
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        //dist = Vector3.Distance(mouse.position, cat.position);
        //Debug.Log(dist);
        //if(Vector3.Distance(mouse.position, cat.position) < 2f)
        //{
        //    scare.cat = cat;
        //    enterScare = true;
        //}
        //else
        //{
        //    enterScare = false;
        //    Debug.Log("EnterScare " + enterScare);
        //}

        base.Update();
    }
}
