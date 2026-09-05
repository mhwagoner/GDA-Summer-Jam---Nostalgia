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
    public bool canScore = true;

    public bool hasEnteredTopScorebox = false;
    public bool hasEnteredBottomScorebox = false;

    public bool hasEnteredOneway = false;

    public int scoreToAdd = 0;

    private Vector3 _initialPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        _initialPosition = transform.localPosition;
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

    public void ResetObject()
    {
        _timeSinceLastScore = 0.0f;
        hasScored = false;
        canScore = true;
        transform.localPosition = _initialPosition;
        hasEnteredBottomScorebox = false;
        hasEnteredTopScorebox = false;
        hasEnteredOneway = false;
        scoreToAdd = 0;
    }

    public void EarnPoints()
    {
        EventBus.Instance.ScoreEarned(scoreToAdd);

        scoreToAdd = 0;
        hasEnteredBottomScorebox = false;
        hasEnteredTopScorebox = false;
        hasScored = true;
    }
}
