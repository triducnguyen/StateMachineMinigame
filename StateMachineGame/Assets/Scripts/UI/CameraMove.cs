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
    public RectTransform rTransform;
    public Canvas canvas;
    public Camera camera;
    public TransformGesture tGesture;
    //public Transform world;

    private void OnEnable()
    {
        tGesture.Transformed += TransformChanges;
    }

    private void OnDisable()
    {
        tGesture.Transformed -= TransformChanges;
    }

    public void TransformChanges(object sender, System.EventArgs e)
    {
        camera.transform.position -= tGesture.DeltaPosition;
        camera.orthographicSize *= 1 / tGesture.DeltaScale;
    }

    private void Update()
    {
        //update size of touch layer
        rTransform.sizeDelta = new Vector2(canvas.pixelRect.width, canvas.pixelRect.height);
    }
}
