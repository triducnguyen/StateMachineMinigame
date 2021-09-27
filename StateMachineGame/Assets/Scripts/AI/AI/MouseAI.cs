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
    private Animator anim;
    protected override void Awake()
    {
        base.Awake();
        mouse = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        var wander = new WanderBehaviour(this, 3, 4f);
        var scare = new ScaredBehaviour(this, 3f, mouse, cat);
        this.scare = scare;

        wander.enterConditions.Add(new Condition(() => hunger <= 50 && energy >= 45 && !scared && activeBehaviours.Count == 0));
        wander.exitConditions.Add(new Condition(() => scared, scare));

        scare.enterConditions.Add(new Condition(() => scared && activeBehaviours.Count == 0));
        scare.exitConditions.Add(new Condition(() => scare.reachedDestination && !scared, wander));

        behaviours.Add(wander);
        behaviours.Add(scare);
        StartCoroutine(CheckScare());
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
            //List<AI> cats = manager.ai.FindAll(match => match.GetType() == typeof(CatAI));
            //float dist = 0f;
            //foreach (AI cat in cats)
            //{
            //    scare.cat = cat.transform;
            //    dist = Vector3.Distance(this.transform.position, cat.transform.position);
            //    //check distance
            //    if (dist < 3f)
            //    {
            //        scared = true;
            //    }
            //    else
            //    {
            //        scared = false;
            //    }
            //}
            //Debug.Log(cats.Count);
            scare.cat = cat;
            dist = Vector3.Distance(mouse.position, cat.position);
            //check distance
            if (dist < 2f)
            {
                scared = true;
                scare.scared = true;
            }
            else
            {
                scared = false;
                scare.scared = false;

            }
            Debug.Log("Scare? " + scared);
            //Debug.Log(cat.position);
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

        float directionX = mouse.position.x - activeBehaviours[0].target.x;
        float directionY = mouse.position.y - activeBehaviours[0].target.y;
        if (Mathf.Abs(directionX) > Mathf.Abs(directionY))
        {
            if (directionX < 0)
            {
                anim.SetBool("Right", true);
                anim.SetBool("Left", false);
                anim.SetBool("Up", false);
                anim.SetBool("Down", false);

            }
            else if (directionX > 0)
            {
                anim.SetBool("Left", true);
                anim.SetBool("Right", false);
                anim.SetBool("Up", false);
                anim.SetBool("Down", false);
            }
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (directionY > 0)
            {
                anim.SetBool("Up", false);
                anim.SetBool("Down", true);
                anim.SetBool("Right", false);
                anim.SetBool("Left", false);
            }
            else if (directionY < 0)
            {
                anim.SetBool("Up", true);
                anim.SetBool("Down", false);
                anim.SetBool("Right", false);
                anim.SetBool("Left", false);
            }
        }

        if (aStar.canSearch == false)
        {
            anim.SetBool("Moving", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
        }
        else
        {
            anim.SetBool("Moving", true);
        }
        //base.Update();
    }
}
