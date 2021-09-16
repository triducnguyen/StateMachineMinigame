using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillCanvas : MonoBehaviour
{
    Canvas canvas;
    public RectTransform layer;
    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameManager.instance.canvas;
    }

    // Update is called once per frame
    void Update()
    {
        layer.sizeDelta = new Vector2(canvas.pixelRect.width, canvas.pixelRect.height);
    }
}
