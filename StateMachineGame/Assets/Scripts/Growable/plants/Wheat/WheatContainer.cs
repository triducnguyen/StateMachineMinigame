using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using random = UnityEngine.Random;

public class WheatContainer : MonoBehaviour
{
    Coroutine checkWheat;

    public GameObject wheatPrefab;
    public Vector3Int tilePos;
    public TileObject tileObject;

    List<Vector3> points = new List<Vector3>();
    int total = 0;
    public int min = 1;
    public int max = 5;
    

    private void Awake()
    {
        var testTilePos = Vector3Int.FloorToInt(transform.localPosition * 2f);

        var gobject = GameManager.Instance.tilemap.GetInstantiatedObject(testTilePos);
        TileObject tobject;
        if (gobject.TryGetComponent<TileObject>(out tobject)){
            tileObject = tobject;
        }
        //determine how many wheat plants to create
        if (wheatPrefab is null)
        {
            wheatPrefab = PrefabDictionary.Instance.prefabList["wheatplant"];
        }
        total = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f) * max);
        total = total < min ? min : total;
        total = total > max ? max : total;
        for (var i = 0; i < total; i++)
        {
            var pos = GetRandomPos();
            //Debug.Log("randomPos: "+pos);
            points.Add(pos);
            var wheat = Instantiate(wheatPrefab, transform);
            Wheat wheat1 = wheat.GetComponent<Wheat>();
            wheat1.OnDestroyed += WheatDestroyed;
            wheat.transform.localPosition = pos;

        }
    }

    Vector3 GetRandomPos()
    {
        //choose row
        int row = UnityEngine.Random.Range(0, 4);
        if (row == 4)
        {
            row = 0;
        }
        float px2units = 1f/32f;

        Vector3 local = Vector3.zero;
        local.x = random.Range(-6f, 7f);
        float start = 0;
        float end = 0;
        switch (row)
        {
            case 0:
                //top
                start = 6f;
                end = 7f;
                break;
            case 1:
                //second
                start = 2f;
                end = 4f;
                break;
            case 2:
                //third
                start = 0f;
                end = -2f;
                break;
            case 3:
                //bottom
                start = -4f;
                end = -6f;
                break;
        }
        local.y = Random.Range(start, end);
        local *= px2units;
        foreach (var p in points)
        {
            if (Vector3.Distance(local,p) <= 0.13f)
            {
                return GetRandomPos();
            }
        }
        
        return local;
    }

    void WheatDestroyed(System.EventArgs args)
    {
        if (transform.childCount < 2)
        {
            tileObject.DestroyOccupier();
        }
    }
}
