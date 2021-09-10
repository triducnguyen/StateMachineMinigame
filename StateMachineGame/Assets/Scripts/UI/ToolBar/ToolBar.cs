using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get res
        Vector2 canvasRect = canvas.pixelRect.size;
        rectTransform.anchoredPosition = new Vector2((canvasRect.x/2)+(rectTransform.rect.width/2)-40, 0);
    }
}
