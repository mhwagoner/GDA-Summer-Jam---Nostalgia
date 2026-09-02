using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MachineButton : MonoBehaviour
{
    public event Action<bool> OnChange;
    [HideInInspector]
    public bool isHeld = false;
    public InputActionAsset actionMap;
    public string actionName;

    private InputAction _action;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (actionMap == null)
        {
            throw new Exception("MachineButton was not provided an actionMap");
        }

        _action = actionMap.FindAction(actionName);
        if (_action == null)
        {
            Debug.Log($"Action \"{actionName}\" not found!");
            gameObject.SetActive(false);
        }
        else
        {
            _action.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_action.IsPressed())
        {
            if (!isHeld)
            {
                isHeld = true;
                OnChange?.Invoke(true);
            }
        }
        else
        {
            if (isHeld)
            {
                isHeld = false;
                OnChange?.Invoke(false);
            }
        }
    }
}
