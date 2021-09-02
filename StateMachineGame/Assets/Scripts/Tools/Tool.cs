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
            return _level;
        }
        protected set
        {
            _level = value;
        }
    }
    int _level = 1;
    public float experience
    {
        get
        {
            return _experience;
        }
        protected set
        {
            _experience = value;
        }
    }
    float _experience = 0;


    //tool area of effect
    float toolRadius;

    

    protected virtual void UseTool(Tile tile)
    {

    }

    protected virtual void UseTool(Growable growable)
    {
    }
}
