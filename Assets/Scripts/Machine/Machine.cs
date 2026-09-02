using UnityEngine;
using UnityEngine.InputSystem;

public class Machine : MonoBehaviour
{
    [SerializeField]
    private Object _protoObject;

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

        //SpawnInitialObjects();
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

    private void SpawnInitialObjects()
    {
        const int iterations = 6;
        if (_protoObject != null)
        {
            for (int i = 0; i < iterations; i++)
            {
                Object obj = Instantiate(_protoObject);
                float position = size.x * i / iterations - size.x / 2 + obj.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                obj.transform.localPosition = new Vector2(position, 0);

                obj.body.linearVelocity = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)).normalized * Random.Range(-1.0f, 1.0f);
            } 
        }
    }
}
