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
    public List<Vector2> aoe;

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
        tool = GameManager.Instance.tool;
        List<Vector2> baseLattice;
        if (TileDictionary.Instance.radius2lattice.TryGetValue(tool.toolRadius, out baseLattice))
        {
            //lattice was already calculated, use it.
            aoe = baseLattice;
        }
        else
        {
            TileDictionary.Instance.CalculateLattices(tool.toolRadius);
            aoe = TileDictionary.Instance.radius2lattice[tool.toolRadius];
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
        foreach (var lattice in aoe)
        {
            //use tool on tiles
            Vector2 localPos = tilemap.transform.worldToLocalMatrix * cam.ScreenToWorldPoint(tGesture.ScreenPosition) * 2;
            var tilePos = Vector3Int.FloorToInt((Vector2)localPos + lattice);
            var tile = (ExtendedRuleTile)GameManager.Instance.tilemap.GetTile(tilePos);
            if (tile is object)
            {
                tool.UseTool(tile, tilePos);
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
