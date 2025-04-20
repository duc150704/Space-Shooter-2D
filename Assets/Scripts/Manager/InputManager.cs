using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
        Cursor.visible = false;
    }

    public bool IsShootButtonPressed()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    public bool IsLaunchingRocket()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }
}
