using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ToolBarMove : FingerMove
{
    public Animator controller;

    public bool toggled
    {
        get
        {
            return controller.GetBool("toggled");
        }
        protected set
        {
            controller.SetBool("toggled", value);
        }
    }

    public override void OnSingleFingerUp(Vector2 position)
    {
        Debug.Log("Clicked");
        //toggle toolbar
        if (controller.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !controller.IsInTransition(0))
        {
            if (toggled)
            {
                controller.Play("TrayClosed", 0, 0);
            }
            else
            {
                controller.Play("TrayOpen", 0, 0);
            }
            toggled = !toggled;
        }
    }
}
