using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Gestures.TransformGestures.Base;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera camera;
    //public Transform world;

    public void TransformChanges(Gesture gesture)
    {
        //check changes
        var tgesture = (TransformGesture)gesture;
        if (tgesture is object)
        {
            camera.transform.position -= tgesture.DeltaPosition;
            camera.orthographicSize *= 1/tgesture.DeltaScale;
        }
    }
}
