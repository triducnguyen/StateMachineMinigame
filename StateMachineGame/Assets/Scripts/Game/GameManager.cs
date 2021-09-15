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

    public Vector3Int GetTilePos(Vector3 worldpos)
    {
        var tmp = tilemap.transform.worldToLocalMatrix * worldpos * 2f;
        tmp.z = 0;
        return Vector3Int.FloorToInt(tmp);
    }


    public ExtendedRuleTile GetTile(Vector3 worldpos, out Vector3Int tilePos)
    {
        tilePos = GetTilePos(worldpos);
        return (ExtendedRuleTile)tilemap.GetTile(tilePos);
    }
    public ExtendedRuleTile GetTile(Vector3Int tilePos)
    {
        return (ExtendedRuleTile)tilemap.GetTile(tilePos);
    }

    public GameObject GetGameObject(Vector3Int tilePos)
    {
        return tilemap.GetInstantiatedObject(tilePos);
    }

    public TileObject GetTObject(Vector3 worldpos, out Vector3Int tilePos, out GameObject gameObject)
    {
        tilePos = GetTilePos(worldpos);
        gameObject = GetGameObject(tilePos);
        return gameObject.GetComponent<TileObject>();
    }

    public TileObject GetTObject(Vector3Int tilePos, out GameObject gameObject)
    {
        gameObject = GetGameObject(tilePos);
        return gameObject.GetComponent<TileObject>();
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
