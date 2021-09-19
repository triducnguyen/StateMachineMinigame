using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Sprite sprite;
    public System.Action action;
    public string displayName
    {
        get;
        protected set;
    }
    public string itemName
    {
        get;
        protected set;
    }
    public string type
    {
        get;
        protected set;
    }
}
