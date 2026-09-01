using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField]
    private float _terminalVelocity;

    private Rigidbody2D _body;
    private CircleCollider2D _circle;

    public Machine machine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _circle = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        _body.AddForce(-_body.linearVelocity.normalized / _terminalVelocity * _body.linearVelocity.magnitude * _terminalVelocity);
    }
}
