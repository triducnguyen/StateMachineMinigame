using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAnimationHandler : TapGestureHandler
{
    public Animator animator;
    public string[] animations;

    public override bool toggled { get => animator.GetBool("toggled"); protected set => animator.SetBool("toggled",value); }

    public bool notAnimating
    {
        get => (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
    }

    public override void Tap()
    {
        Debug.Log("tapping");
        if (notAnimating)
        {
            Debug.Log("Not animating");
            animator.Play(toggled ? animations[0] : animations[1]);
            toggled = !toggled;
        }
    }
}
