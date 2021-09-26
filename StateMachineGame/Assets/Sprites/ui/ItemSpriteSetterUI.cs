using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ItemSpriteSetterUI : MonoBehaviour
{
    public Image image;
    public Item item
    {
        get { return _item; }
        set { _item = value; itemSprite = item.sprite; }
    }
    Item _item;
    public Sprite itemSprite
    {
        get => image.sprite;
        set => image.sprite = value;
    }
}
