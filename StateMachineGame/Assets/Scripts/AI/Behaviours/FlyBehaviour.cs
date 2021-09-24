using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehaviour : AIBehaviour
{
     public float Speed = 2f;   
     public GameObject Crow;
     private Vector2 position1 = new Vector3(-0.14f, -0.775f);

    void Start()
    {
        //to fly:
        Crow.transform.position = Vector2.MoveTowards(Crow.transform.position, position1, Speed);
    }
}
