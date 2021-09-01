using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tool : MonoBehaviour
{
    public int level
    {
        get
        {

        }
        set
        {

        }
    }
    int _level = 1;
    public float experience
    {

    }


    //tool area of effect
    float toolRadius;

    

    protected virtual void UseTool(Tile tile)
    {

    }

    protected virtual void UseTool(Growable growable)
    {
    }
}
