using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PositionUI : MonoBehaviour
{
    public Canvas canvas
    {
        get => GameManager.instance.canvas;
    }
    public CanvasTrigger canvasTrigger;

    public Rect pixelRect
    {
        get => canvas.pixelRect;
    }

    public static Vector2 TopLeft = new Vector2(-1,0);
    public static Vector2 TopMiddle = new Vector2(0,1);
    public static Vector2 TopRight = new Vector2(1,1);
    public static Vector2 MiddleLeft = new Vector2(-1,0);
    public static Vector2 Center = Vector2.zero;
    public static Vector2 MiddleRight = new Vector2(1, 0);
    public static Vector2 BottomLeft = new Vector2(-1, -1);
    public static Vector2 BottomMiddle = new Vector2(0, -1);
    public static Vector2 BottomRight = new Vector2(1, -1);

    //desired position is proportional, -1 being left and 1 being right
    //same as up and down.

    public RectTransform rectTransform;
    public float positionInterval = .2f;

    public Vector2 nativePos;
    public Vector2 nativeSize;
    public Vector2 nativeResolution;
    public float aspectRatio;
    public Vector2 nativeScale;
    public Vector2 desiredUiPos = Vector2.zero;

    public virtual System.Tuple<Vector2, Vector3> newTransforms
    {
        get
        {
            Debug.Log("Getting newTransforms");
            return new System.Tuple<Vector2, Vector3>(GetNewPos(), GetNewScale());
        }
    }

    private void Awake()
    {
        aspectRatio = nativeSize.x / nativeSize.y;
        if (canvasTrigger == null)
        {
            canvasTrigger = canvas.GetComponent<CanvasTrigger>();
        }
        canvasTrigger.CanvasSizeChanged += CanvasChanged;
    }

    public virtual Vector2 GetNewPos()
    {
        
        return GetDesiredScreenPos();
    }

    public virtual Vector3 GetNewScale()
    {
        //Debug.Log("PixelRect.width: "+pixelRect.width);
        //Debug.Log("PixelRect.height: "+pixelRect.height);
        float x = (pixelRect.width/nativeResolution.x);
        float y = (pixelRect.height/nativeResolution.y);
        var scale = new Vector3(x, y, 1);
        Debug.Log("scale: "+scale);
        return scale;
    }
    
    public virtual Vector2 GetDesiredScreenPos()
    {
        //convert desired pos to screen pos
        Vector2 canvasSize = canvas.pixelRect.size;
        Vector2 desiredScreenPos = new Vector2(0,0);
        desiredScreenPos.x = desiredUiPos.x.Map(-1f,1f,-canvasSize.x/2,canvasSize.x/2);
        desiredScreenPos.y = desiredUiPos.y.Map(-1f,1f,-canvasSize.y/2,canvasSize.y/2);
        Debug.Log("DesiredScreenPos: " + desiredScreenPos);
        return desiredScreenPos;
    }

    public virtual void CanvasChanged(object sender, System.EventArgs args)
    {
        var transforms = newTransforms;
        if (rectTransform.anchoredPosition != transforms.Item1 || rectTransform.localScale != transforms.Item2)
        {
            rectTransform.anchoredPosition = transforms.Item1;
            rectTransform.localScale = transforms.Item2;
        }
    }

    private void OnDestroy()
    {
        canvasTrigger.CanvasSizeChanged -= CanvasChanged;
    }
}
