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
        canvas = GameManager.Instance.canvas;
        cam = GameManager.Instance.cam;
        tool = GameManager.Instance.tool;
        toolImage.sprite = tool.sprite;
        var color = toolImage.color;
        if (tool.type != "hand")
        {
            toolImage.color = new Color(color.r, color.g, color.b, 1f);
        }
        toolTransform = toolImage.GetComponent<RectTransform>();
        baseTransform = GetComponent<RectTransform>();
        toolTransform.anchoredPosition = tool.offset;
        StartCoroutine(UpdateHand());
    }

    private void Start()
    {
        handAnimator.Play("HandClose");
    }

    

    IEnumerator UpdateHand()
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

    private void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    TileObject tile;
    //    if (other.gameObject.CompareTag("tile") &&
    //        other.TryGetComponent<TileObject>(out tile) &&
    //        !tiles.Contains(tile))
    //    {
    //        tiles.Add(tile);
    //    }

    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    TileObject tile;
    //    if (other.gameObject.CompareTag("tile") &&
    //        TryGetComponent<TileObject>(out tile) &&
    //        tiles.Contains(tile))
    //    {
    //        tiles.Remove(tile);
    //    }
    //}

    private void OnDestroy()
    {
        handAnimator.Play("HandOpen");
    }
}
