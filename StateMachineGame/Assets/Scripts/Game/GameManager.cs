using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    public Canvas canvas;
    public Camera cam;

    public Tilemap tilemap;
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
            List<Growable> tmp = new List<Growable>();
            foreach (Transform t in worldObjects.transform)
            {
                Growable current;
                GameObject gobject = t.gameObject;
                if (gobject.TryGetComponent(out current))
                {
                    tmp.Add(current);
                }
            }
            return tmp;
        }
    }


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
        CheckSingleton();
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
        tool = ToolDictionary.Instance.tools["hand"];
    }

    void CheckSingleton()
    {
        if (_instance is object && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
}
