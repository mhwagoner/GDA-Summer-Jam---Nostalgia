using UnityEngine;
using UnityEngine.InputSystem;

public class MachineButton : MonoBehaviour
{
    private bool _isHeld = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isHeld)
        {
            DoButtonAction();
        }
    }

    public void OnPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isHeld = true;
            DoButtonAction();
        }
        else if (context.canceled)
        {
            _isHeld = false;
        }
    }

    virtual public void DoButtonAction()
    {
        Debug.Log("Hi!");
    }
}
