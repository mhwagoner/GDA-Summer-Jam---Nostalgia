using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField]
    private float _terminalVelocity;

    public Rigidbody2D Body
    {
        get
        {
            return GetComponent<Rigidbody2D>();
        }
    }
    public CircleCollider2D Circle
    {
        get
        {
            return GetComponent<CircleCollider2D>();
        }
    }

    public Machine machine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void FixedUpdate()
    {
        Body.AddForce(-_terminalVelocity * Body.linearVelocity.magnitude * Body.linearVelocity);
    }
}
