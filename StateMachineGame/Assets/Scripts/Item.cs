using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Sprite sprite;
    //display name of the item. User will see this name in UI.
    public string displayName
    {
        get;
        protected set;
    }
    //the name of this specific item (unique)
    public string itemName
    {
        get;
        protected set;
    }
    //item type (harvester, water, seed, hand, item, etc)
    public string type
    {
        get;
        protected set;
    }
    //item description
    public string itemDescription
    {
        get;
        protected set;
    }
    //base cost of the item
    public float basePrice
    {
        get;
        protected set;
    }
}
