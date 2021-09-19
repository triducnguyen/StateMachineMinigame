using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    public delegate void CanvasChanged(object sender, System.EventArgs args);

    public event CanvasChanged CanvasSizeChanged;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRectTransformDimensionsChange()
    {
        if (CanvasSizeChanged != null)
        {
            CanvasSizeChanged(this, new System.EventArgs());
        }
    }
}
