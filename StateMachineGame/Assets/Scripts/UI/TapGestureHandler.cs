using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class TapGestureHandler : TouchScript.Hit.HitTest
{
    public TapGesture tapGesture;
    
    public virtual bool toggled
    {
        get => _toggled;
        protected set => _toggled = value;
    }
    bool _toggled = false;

    

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
        toggled = !toggled;
    }

    public virtual void TapEvent(object sender, System.EventArgs e)
    {
        Tap();
    }
}
