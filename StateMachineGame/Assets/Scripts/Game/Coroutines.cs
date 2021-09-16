using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutines : Singleton<Coroutines>
{
    public Dictionary<string, Coroutine> coroutines = new Dictionary<string, Coroutine>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddCoroutine(string name, IEnumerator routine)
    {
        coroutines.Add(name, StartCoroutine(routine));
    }

    public void DelCoroutine(string name)
    {
        StopCoroutine(coroutines[name]);
        coroutines.Remove(name);
    }
}
