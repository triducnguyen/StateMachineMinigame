using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;


public class ShopButton : TapGestureHandler
{
    public bool paused
    {
        get { return animator.GetBool("paused"); }
        protected set { animator.SetBool("paused", value); }
    }
    public override void TapEvent(object sender, System.EventArgs e)
    {
        base.TapEvent(sender, e);
        paused = toggled;
        Time.timeScale = paused ? 0 : 1f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

}
