using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTapHandler : MonoBehaviour
{
    public GameObject item;
    public ShopList shopList;

    private void Awake()
    {
        shopList = ShopList.instance;
    }

    public void Tap()
    {
        shopList.BuyItem(item);
    }
}
