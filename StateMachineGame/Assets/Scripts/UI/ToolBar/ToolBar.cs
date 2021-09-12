using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform toolBar;

    private void Awake()
    {
        StartCoroutine(TranslateToolBar());
    }

    // Update is called once per frame
    IEnumerator TranslateToolBar()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            Vector2 canvasRect = canvas.pixelRect.size;
            float y = canvasRect.y - 80f;
            float newY = y / 600;
            float x = y / 3;
            float newX = x / 200;

            float toolbarWidth = toolBar.rect.width * toolBar.localScale.x;
            float partialToolbar = toolbarWidth / 7;
            toolBar.localScale = new Vector3(newX, newY, 1);
            toolBar.anchoredPosition = new Vector2((canvasRect.x / 2) + (toolbarWidth/2) - partialToolbar, 0);
        }
    }
}
