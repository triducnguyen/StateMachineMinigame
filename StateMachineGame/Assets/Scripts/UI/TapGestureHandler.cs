using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class TapGestureHandler : TouchScript.Hit.HitTest
{
    public TapGesture tapGesture;
    public Animator animator;
    public string[] animations;


    public bool toggled
    {
        get { return animator.GetBool("toggled"); }
        protected set { animator.SetBool("toggled", value); }
    }

    public bool notAnimating
    {
        get => (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
    }

    protected virtual void OnEnable()
    {
        tapGesture = GetComponent<TapGesture>();
        tapGesture.Tapped += TapEvent;
    }

    protected virtual void OnDisable()
    {
        tapGesture.Tapped -= TapEvent;
    }

    public virtual void Tap()
    {
        Debug.Log("tapping");
        if (notAnimating)
        {
            animator.Play(toggled ? animations[0] : animations[1]);
            toggled = !toggled;
        }
    }

    public virtual void TapEvent(object sender, System.EventArgs e)
    {
        Tap();
    }
}
