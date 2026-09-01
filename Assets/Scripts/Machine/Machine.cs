using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField]
    private Object _protoObject;

    public Vector2 size;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        size = GetComponent<SpriteRenderer>().bounds.size;
        const int iterations = 6;
        if (_protoObject != null)
        {
            for (int i = 0; i < iterations; i++)
            {
                Object obj = Instantiate(_protoObject);
                obj.machine = this;
                float position = size.x * i / iterations - size.x / 2 + obj.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                obj.transform.localPosition = new Vector2(position, 0);

                obj.Body.linearVelocity = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)).normalized * Random.Range(-1.0f, 1.0f);
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
