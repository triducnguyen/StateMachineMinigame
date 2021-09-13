using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabDictionary : MonoBehaviour
{
    public static PrefabDictionary Instance;

    public List<NamedPrefab> prefabs;
    public Dictionary<string, GameObject> prefabList 
    {
        get => prefabs.ToDictionary((p) => p.name, (p)=> p.prefab);
    }
    public List<RuntimeAnimatorController> animationControllers = new List<RuntimeAnimatorController>();

    private void Awake()
    {
        CheckSingleton();


    }

    void CheckSingleton()
    {
        if (Instance is object)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}

[System.Serializable]
public struct NamedPrefab
{
    public string name;
    public GameObject prefab;
}
