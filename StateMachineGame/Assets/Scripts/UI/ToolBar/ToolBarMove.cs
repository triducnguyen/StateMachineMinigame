using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using TouchScript.Hit;
using TouchScript.Pointers;
using UnityEngine;

public class ToolBarMove : HitTest
{
    public Animator controller;
    public TapGesture tapGesture;
    public bool toggled
    {
        get { return controller.GetBool("toggled"); }
        protected set { controller.SetBool("toggled", value); }
    }

    bool notAnimating
    {
        get => (controller.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
    }

    private void OnEnable()
    {
        tapGesture = GetComponent<TapGesture>();
        tapGesture.Tapped += Tap;
    }

    private void OnDisable()
    {
        tapGesture.Tapped -= Tap;
    }

    public void Tap(object sender, System.EventArgs e)
    {
        Debug.Log("tapping");
        if (notAnimating)
        {
            Debug.Log("Not animating");
            if (toggled)
            {
                controller.Play("TrayClosed");
            }
            else
            {
                controller.Play("TrayOpen");
            }
            toggled = !toggled;
        }
    }
}
