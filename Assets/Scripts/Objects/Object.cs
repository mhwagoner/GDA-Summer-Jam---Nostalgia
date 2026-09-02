using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Object : MonoBehaviour
{
    [SerializeField]
    private float _terminalVelocity;

    [HideInInspector]
    public Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        body.AddForce(-_terminalVelocity * body.linearVelocity.magnitude * body.linearVelocity);
    }
}
