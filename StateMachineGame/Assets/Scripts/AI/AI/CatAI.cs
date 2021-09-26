using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAI : AI
{
    private Animator anim;
    private Transform cat;

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();
        cat = GetComponent<Transform>();

        var tired = new SleepBehaviour(this, 1f, 1f);
        var explore = new WanderBehaviour(this, 3, 4f);

        tired.enterConditions.Add(new Condition(() => energy <= 25));
        tired.exitConditions.Add(new Condition(() => energy > 25, explore));

        explore.exitConditions.Add(new Condition(() => energy <= 25));

        behaviours.Add(explore);
        behaviours.Add(tired);
    }

    protected override void Update()
    {
        float directionX = cat.position.x - activeBehaviours[0].target.x;
        float directionY = cat.position.y - activeBehaviours[0].target.y;

        if (Mathf.Abs(directionX) > Mathf.Abs(directionY))
        {
            if (directionX < 0)
            {
                anim.SetBool("Cat_WalkSideR", true);
                anim.SetBool("Cat_WalkSideL", false);
                anim.SetBool("Cat_WalkUp", false);
                anim.SetBool("Cat_WalkDown", false);

                anim.SetBool("Cat_WalkSideRSTOP", false);
                anim.SetBool("Cat_WalkSideLSTOP", false);
                anim.SetBool("Cat_WalkUpSTOP", false);
                anim.SetBool("Cat_WalkDownSTOP", false);

            }
            else if (directionX > 0)
            {
                anim.SetBool("Cat_WalkSideR", false);
                anim.SetBool("Cat_WalkSideL", true);
                anim.SetBool("Cat_WalkUp", false);
                anim.SetBool("Cat_WalkDown", false);

                anim.SetBool("Cat_WalkSideRSTOP", false);
                anim.SetBool("Cat_WalkSideLSTOP", false);
                anim.SetBool("Cat_WalkUpSTOP", false);
                anim.SetBool("Cat_WalkDownSTOP", false);
            }
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (directionY > 0)
            {
                anim.SetBool("Cat_WalkSideR", false);
                anim.SetBool("Cat_WalkSideL", false);
                anim.SetBool("Cat_WalkUp", false);
                anim.SetBool("Cat_WalkDown", true);

                anim.SetBool("Cat_WalkSideRSTOP", false);
                anim.SetBool("Cat_WalkSideLSTOP", false);
                anim.SetBool("Cat_WalkUpSTOP", false);
                anim.SetBool("Cat_WalkDownSTOP", false);
            }
            else if (directionY < 0)
            {
                anim.SetBool("Cat_WalkSideR", false);
                anim.SetBool("Cat_WalkSideL", false);
                anim.SetBool("Cat_WalkUp", true);
                anim.SetBool("Cat_WalkDown", false);

                anim.SetBool("Cat_WalkSideRSTOP", false);
                anim.SetBool("Cat_WalkSideLSTOP", false);
                anim.SetBool("Cat_WalkUpSTOP", false);
                anim.SetBool("Cat_WalkDownSTOP", false);
            }
        }

        if (aStar.canSearch == false)
        {
            anim.SetBool("Moving", false);

            if (Mathf.Abs(directionX) > Mathf.Abs(directionY))
            {
                if (directionX < 0)
                {
                    anim.SetBool("Cat_WalkSideRSTOP", true);
                    anim.SetBool("Cat_WalkSideLSTOP", false);
                    anim.SetBool("Cat_WalkUpSTOP", false);
                    anim.SetBool("Cat_WalkDownSTOP", false);

                    anim.SetBool("Cat_WalkSideR", false);
                    anim.SetBool("Cat_WalkSideL", false);
                    anim.SetBool("Cat_WalkUp", false);
                    anim.SetBool("Cat_WalkDown", false);
                } else if (directionX > 0)
                {
                    anim.SetBool("Cat_WalkSideRSTOP", false);
                    anim.SetBool("Cat_WalkSideLSTOP", true);
                    anim.SetBool("Cat_WalkUpSTOP", false);
                    anim.SetBool("Cat_WalkDownSTOP", false);

                    anim.SetBool("Cat_WalkSideR", false);
                    anim.SetBool("Cat_WalkSideL", false);
                    anim.SetBool("Cat_WalkUp", false);
                    anim.SetBool("Cat_WalkDown", false);
                }
            } else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
            {
                if (directionY < 0)
                {
                    anim.SetBool("Cat_WalkSideRSTOP", false);
                    anim.SetBool("Cat_WalkSideLSTOP", false);
                    anim.SetBool("Cat_WalkUpSTOP", true);
                    anim.SetBool("Cat_WalkDownSTOP", false);

                    anim.SetBool("Cat_WalkSideR", false);
                    anim.SetBool("Cat_WalkSideL", false);
                    anim.SetBool("Cat_WalkUp", false);
                    anim.SetBool("Cat_WalkDown", false);
                }
                else if (directionY > 0)
                {
                    anim.SetBool("Cat_WalkSideRSTOP", false);
                    anim.SetBool("Cat_WalkSideLSTOP", false);
                    anim.SetBool("Cat_WalkUpSTOP", false);
                    anim.SetBool("Cat_WalkDownSTOP", true);

                    anim.SetBool("Cat_WalkSideR", false);
                    anim.SetBool("Cat_WalkSideL", false);
                    anim.SetBool("Cat_WalkUp", false);
                    anim.SetBool("Cat_WalkDown", false);
                }
            }
        }
        else
        {
            anim.SetBool("Moving", true);
        }
        //base.Update();
    }
}
