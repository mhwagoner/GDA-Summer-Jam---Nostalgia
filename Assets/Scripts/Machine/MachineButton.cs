using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class MachineButton : MonoBehaviour
{
    public event Action<bool> onChange;
    public bool isHeld = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            DoButtonAction();
        }
    }

    public void OnPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isHeld = true;
            DoButtonAction();
            onChange?.Invoke(true);
        }
        else if (context.canceled)
        {
            isHeld = false;
            onChange?.Invoke(false);
        }
    }

    virtual public void DoButtonAction()
    {

    }
}
