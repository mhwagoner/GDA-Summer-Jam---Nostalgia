using UnityEngine;
using UnityEngine.InputSystem;

public class Machine : MonoBehaviour
{
    public float rotationSpeed;

    public InputActionAsset actionMap;
    private InputAction _rotateL;
    private InputAction _rotateR;

    private float _timeSinceLastSplash = 0.0f;
    public float timeBetweenSplashes;
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

        // Little hack to make the splash play immediately after input
        _timeSinceLastSplash = timeBetweenSplashes;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rotateL != null)
        {
            CheckInput(_rotateL, 1);
        } 
        if (_rotateR != null)
        {
            CheckInput(_rotateR, -1);
        }
    }

    void CheckInput(InputAction action, int factor)
    {
        if (action.IsPressed())
        {
            float rotateAmount = rotationSpeed * Time.deltaTime * factor;
            transform.Rotate(0.0f, 0.0f, rotateAmount);

            _timeSinceLastSplash += Time.deltaTime;
            if (_timeSinceLastSplash >= timeBetweenSplashes)
            {
                Game.Instance.audioController.PlaySFX(SFX.SPLASH);
                _timeSinceLastSplash = 0.0f;
            }
        }
        /*
        if (action.WasPressedThisFrame())
        {
            Game.Instance.audioController.PlaySFX(SFX.SPLASH);
            _timeSinceLastSplash = 0.0f;
        }
        */
    }
}
