using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatContainer : MonoBehaviour
{
    int min = 1;
    int max = 5;
    

    private void Awake()
    {
        //determine how many wheat plants to create
        int count = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f) * max);
        count = count < min ? min : count;
        count = count > max ? max : count;
        for (var i = 0; i < count; i++)
        {

        }
    }

    Vector3 GetRandomPos()
    {
        //choose row
        int row = UnityEngine.Random.Range(0, 4);
        //0 is left side, 1 is right side
        float randY = UnityEngine.Random.Range(0f, 1f);

        return Vector3.zero;
    }
}
