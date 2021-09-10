using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerMove : MonoBehaviour, ISingleFingerHandler, IPinchHandler
{
    public virtual void OnSingleFingerDown(Vector2 position) { }
    public virtual void OnSingleFingerUp(Vector2 position) { }
    public virtual void OnSingleFingerDrag(Vector2 delta)
    {
        _processSwipe(delta);
    }

    public virtual void OnPinchStart() { }
    public virtual void OnPinchEnd() { }
    public virtual void OnPinchZoom(float delta)
    {
        _processPinch(delta);
    }

    protected virtual void _processSwipe(Vector2 screenTravel)
    {
        //handle dragging
    }

    protected virtual void _processPinch(float delta)
    {
        //handle zooming
    }
}