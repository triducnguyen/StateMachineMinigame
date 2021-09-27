using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

public class ToolUse : MonoBehaviour
{
    public GameObject UI;
    public Camera cam;
    public Canvas canvas;
    public GameObject tilemap;
    public TransformGesture tGesture;

    public GameObject handPrefab;
    GameObject hand;
    RectTransform handTransform;

    bool slideStarted;

    public Tool tool;
    public List<TileObject> tiles = new List<TileObject>();
    public List<Vector2Int> aoe;

    Vector2 lastPos = Vector2.zero;

    private void Awake()
    {
        Configure();
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        tGesture.Transformed += OnSlide;
        tGesture.TransformCompleted += OnStop;
    }

    private void OnDisable()
    {
        tGesture.Transformed -= OnSlide;
        tGesture.TransformCompleted -= OnStop;
    }

    void Configure()
    {
        tool = GameManager.instance.tool;
        List<Vector2Int> baseLattice;
        if (TileDictionary.instance.radius2lattice.TryGetValue(tool.toolRadius, out baseLattice))
        {
            //lattice was already calculated, use it.
            aoe = baseLattice;
        }
        else
        {
            TileDictionary.instance.CalculateLattices(tool.toolRadius);
            aoe = TileDictionary.instance.radius2lattice[tool.toolRadius];
        }
    }

    public void OnSlide(object sender, System.EventArgs args)
    {
        if (!slideStarted)
        {
            //create visual hand
            hand = Instantiate(handPrefab, UI.transform);
            handTransform = hand.GetComponent<RectTransform>();
            Configure();
            slideStarted = true;
        }
        //move visual hand
        handTransform.anchoredPosition = tGesture.ScreenPosition - (canvas.pixelRect.size / 2);

        //update affected lattices
        var worldPos = cam.ScreenToWorldPoint(tGesture.ScreenPosition);
        Vector3Int pos = TileManager.instance.GetTilePos(worldPos);
        foreach (var lattice in aoe)
        {
            var latticePos = pos + (Vector3Int)lattice;
            var tile = TileManager.instance.GetTile(pos);
            if (tile != null)
            {
                tool.UseTool(tile, pos);
            }
        }
    }
    



    public void OnStop(object sender, System.EventArgs args)
    {
        //destroy tool
        slideStarted = false;
        Destroy(hand);
        hand = null;
    }
}
