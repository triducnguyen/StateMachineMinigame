using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Canvas canvas;
    public Camera cam;

    public GameObject world;
    public GameObject worldObjects;

    

    public List<AI> ai
    {
        get
        {
            List<AI> tmp = new List<AI>();
            foreach (Transform t in worldObjects.transform)
            {
                AI current;
                GameObject gobject = t.gameObject;
                if (gobject.TryGetComponent(out current))
                {
                    tmp.Add(current);
                }
            }
            return tmp;
        }
    }

    
    
    public List<Growable> growables
    {
        get
        {
            return _growables;
        }
        set
        {
            _growables = value;
        }
    }
    List<Growable> _growables = new List<Growable>();


    public Tool tool;

    public enum ControlScheme
    {
        MouseKeyboard,
        Touch
    }

    public ControlScheme controls
    {
        get => _controls;
        set => _controls = value;
    }
    ControlScheme _controls = ControlScheme.MouseKeyboard;

    private void Awake()
    {
        base.Awake();
        //game setup.
        DontDestroyOnLoad(gameObject);
        //check platform
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
                break;
            case RuntimePlatform.WindowsEditor:
                break;
            case RuntimePlatform.OSXPlayer:
                break;
            case RuntimePlatform.OSXEditor:
                break;
            case RuntimePlatform.LinuxPlayer:
                break;
            case RuntimePlatform.LinuxEditor:
                break;
            case RuntimePlatform.Android:
                controls = ControlScheme.Touch;
                break;
        }
        
        worldObjects = worldObjects == null ? new GameObject() : worldObjects;
        worldObjects.transform.parent = world.transform;
        worldObjects.transform.position = Vector3.zero;
        tool = (Tool)ItemDictionary.instance.items["Hand"];
    }

    
}
