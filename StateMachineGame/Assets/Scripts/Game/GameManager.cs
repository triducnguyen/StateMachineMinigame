using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum ControlScheme
    {
        MouseKeyboard,
        Touch
    }

    ControlScheme controls = ControlScheme.MouseKeyboard;

    private void Awake()
    {
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

        if (controls == ControlScheme.MouseKeyboard)
        {
            //set mouse cursor
            
        }
    }

}
