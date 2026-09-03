using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Object : MonoBehaviour
{
    [SerializeField]
    private float _terminalVelocity;

    [HideInInspector]
    public Rigidbody2D body;

    private float _timeSinceLastScore = 0.0f;
    public float scoreCooldown;
    public bool hasScored = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (body.linearVelocity.magnitude > _terminalVelocity)
        {
            body.linearVelocity = body.linearVelocity.normalized * _terminalVelocity;
        }

        if (hasScored)
        {
            UpdateScoreCooldown();
        }
    }

    private void UpdateScoreCooldown()
    {
        _timeSinceLastScore += Time.fixedDeltaTime;
        if (_timeSinceLastScore >= scoreCooldown)
        {
            hasScored = false;
            _timeSinceLastScore = 0.0f;
        }
    }
}
