using UnityEngine;
using UnityEngine.InputSystem;

public class Machine : MonoBehaviour
{
    public float rotationSpeed;

    public InputActionAsset actionMap;
    private InputAction _rotateL;
    private InputAction _rotateR;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rotateL = actionMap.FindAction("RotateL");
        if (_rotateL == null)
        {
            Debug.Log("Could not find action \"RotateL\"");
        }
        else
        {
            _rotateL.Enable();
        }

        _rotateR = actionMap.FindAction("RotateR");
        if (_rotateR == null)
        {
            Debug.Log("Could not find action \"RotateR\"");
        }
        else
        {
            _rotateR.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_rotateL != null)
        {
            if (_rotateL.IsPressed())
            {
                float rotateAmount = rotationSpeed * Time.deltaTime;
                transform.Rotate(0.0f, 0.0f, rotateAmount);
            }
        } 
        if (_rotateR != null)
        {
            if (_rotateR.IsPressed())
            {
                float rotateAmount = -rotationSpeed * Time.deltaTime;
                transform.Rotate(0.0f, 0.0f, rotateAmount);
            }
        }
    }
}
