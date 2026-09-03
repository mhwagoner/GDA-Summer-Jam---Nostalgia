using UnityEngine;
using UnityEngine.InputSystem;

public class Machine : MonoBehaviour
{
    public Vector2 size;
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
        size = GetComponent<SpriteRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rotateL != null)
        {
            if (_rotateL.IsPressed())
            {
                transform.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
            }
        } 
        if (_rotateR != null)
        {
            if (_rotateR.IsPressed())
            {
                transform.Rotate(0.0f, 0.0f, -rotationSpeed * Time.deltaTime);
            }
        }
    }
}
