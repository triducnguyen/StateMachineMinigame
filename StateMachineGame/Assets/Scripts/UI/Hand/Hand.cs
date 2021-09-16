using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public Tool tool;

    public Canvas canvas;
    public Camera cam;
    public RectTransform baseTransform;
    public RectTransform handTransform;
    public RectTransform toolTransform;
    
    public Animator handAnimator;
    public Image toolImage;

    private void Awake()
    {
        canvas = GameManager.instance.canvas;
        cam = GameManager.instance.cam;
        tool = GameManager.instance.tool;
        toolImage.sprite = tool.sprite;
        var color = toolImage.color;
        if (tool.type != "hand")
        {
            toolImage.color = new Color(color.r, color.g, color.b, 1f);
        }
        toolTransform = toolImage.GetComponent<RectTransform>();
        baseTransform = GetComponent<RectTransform>();
        toolTransform.anchoredPosition = tool.offset;
        Coroutines.instance.AddCoroutine("updatehand",UpdateHand());
    }

    private void Start()
    {
        handAnimator.Play("HandClose");
    }

    

    public IEnumerator UpdateHand()
    {
        while (true)
        {
            float height = (canvas.pixelRect.height / 12) / 56.25f;
            float width = height * (68.75f / 56.25f);
            handTransform.localScale = new Vector3(width, height, 1);
            toolTransform.localScale = new Vector3(2f, 2f, 1);
            yield return new WaitForSeconds(.05f);
        }
    }

    private void OnDestroy()
    {
        Coroutines.instance.DelCoroutine("updatehand");
    }
}
