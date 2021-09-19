using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWater : MonoBehaviour
{
    public float wetness = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int tilePos = TileManager.instance.GetTilePos(transform.position);
        if(TileManager.instance.wateredTiles.TryGetValue(tilePos, out wetness)){

        }
    }
}
