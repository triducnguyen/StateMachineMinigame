using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public Animator animator;
    bool shop = false;

    public void ToggleShop()
    {
        //set time scale & animation
        Time.timeScale = shop ? 0f : 1f;
        animator.Play($"{(shop ? "ShopUp" : "ShopDrop")}");

    }
}
