using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

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
        DontDestroyOnLoad(this);

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
