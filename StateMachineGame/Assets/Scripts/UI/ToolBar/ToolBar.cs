using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform toolBar;

    public float ResolutionCheckRate = .2f;

    private void Start()
    {
        StartCoroutine(TranslateToolBar(ResolutionCheckRate));
    }

    System.Tuple<Vector2, Vector2> uiPosition
    {
        get
        {
            Vector2 canvasRect = GameManager.instance.canvas.pixelRect.size;
            float y = canvasRect.y - 80f;
            float newY = y / 600;
            float x = y / 3;
            float newX = x / 200;
            float toolbarWidth = toolBar.rect.width * toolBar.localScale.x;
            float partialToolbar = toolbarWidth / 7;
            Vector3 newScale = new Vector3(newX, newY, 1);
            Vector3 newPosition = new Vector2((canvasRect.x / 2) + (toolbarWidth / 2) - partialToolbar, 0);
            return new System.Tuple<Vector2, Vector2>(newPosition, newScale);
        }
    }

    // Update is called once per frame
    IEnumerator TranslateToolBar(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            var uiPos = uiPosition;
            toolBar.anchoredPosition = toolBar.anchoredPosition != uiPos.Item1 ? uiPos.Item1 : toolBar.anchoredPosition;
            toolBar.localScale = toolBar.localScale != (Vector3)uiPos.Item2 ? (Vector3)uiPos.Item2 : toolBar.localScale;
        }
    }
}
