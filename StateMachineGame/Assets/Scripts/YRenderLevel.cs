using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YRenderLevel : MonoBehaviour
{
    public Vector3 lastPos;
    public int LayerIndex
    {
        get
        {
            if (target is object && spriteRenderer is object) //check that target & renderer are not null
            {
                return Mathf.RoundToInt((target.position.y*-100)) ;
            }
            else//target is null, send to back
            {
                return 999999999;
            }
        }
    }
    int _layerIndex;


    public Transform target;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer.sortingOrder = LayerIndex;
        lastPos = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        //update renderer sorting layer
        if (lastPos != target.position)
        {
            spriteRenderer.sortingOrder = LayerIndex;
        }
    }
}
