using UnityEngine;

public class WaterJet : MonoBehaviour
{
    public Vector2 force;

    [SerializeField]
    private float _torqueDampener = 0.3f;

    public MachineButton button;

    public bool colliderActive = false;

    public bool doAccurateJet = false;

    public ParticleSystem particles;

    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (button != null)
        {
            button.OnChange += SetJetStatus;
        }

        SetJetStatus(colliderActive);

        if (audioSource == null) Debug.Log("Water jet not provided an AudioSource");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetJetStatus(bool status)
    {
        colliderActive = status;
        if (colliderActive)
        {
            if (particles != null) particles.Play();
            if (audioSource != null) Game.Instance.audioController.PlaySFX(SFX.BUBBLES, audioSource);
        }
        else
        {
            if (particles != null) particles.Stop();
            if (audioSource != null) audioSource.Stop();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!colliderActive) return;
        if (!collision.TryGetComponent(out Object obj)) return;
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        if (doAccurateJet)
        {
            DoAccurateJet(collision, obj, box);
        }
        else
        {
            DoJet(obj);
        }
    }

    private void DoJet(Object obj)
    {
        Vector2 _force = Quaternion.Euler(transform.eulerAngles) * force;
        obj.body.AddForce(_force);
    }

    private void DoAccurateJet(Collider2D collision, Object obj, BoxCollider2D box)
    {
        float leftBound = box.bounds.min.x;
        float rightBound = box.bounds.max.x;

        if (collision.GetType() == typeof(CircleCollider2D))
        {
            CircleCollider2D circle = collision as CircleCollider2D;
            float radius = circle.radius * circle.transform.localScale.x;
            float xMin = (leftBound - circle.bounds.center.x) / radius;
            float xMax = (rightBound - circle.bounds.center.x) / radius;

            GetAngleBounds(xMin, xMax, out float lowAngle, out float highAngle);

            obj.body.AddForce(GetForceVector(lowAngle, highAngle));

            obj.body.AddTorque(GetTorque(radius, lowAngle, highAngle));
        }

    }

    private void GetAngleBounds(float xMin, float xMax, out float lowAngle, out float highAngle)
    {
        if (Mathf.Abs(xMin) < 1.0f)
        {
            lowAngle = Mathf.Asin(xMin);
        }
        else
        {
            lowAngle = -Mathf.Deg2Rad * 90.0f;
        }

        if (Mathf.Abs(xMax) < 1.0f)
        {
            highAngle = Mathf.Asin(xMax);
        }
        else
        {
            highAngle = Mathf.Deg2Rad * 90.0f;
        }
    }

    private Vector2 GetForceVector(float lowAngle, float highAngle)
    {
        float ResX(float angle)
        {
            // You have to negate this field for some reason, I don't know why
            return -(force.x * 0.5f * (angle - Mathf.Sin(angle) * Mathf.Cos(angle))) - (force.y * 0.5f * Mathf.Sin(angle) * Mathf.Sin(angle));
        }
        float ResY(float angle)
        {
            return (force.x * 0.5f * Mathf.Sin(angle) * Mathf.Sin(angle)) + (force.y * 0.5f * (angle + Mathf.Sin(angle) * Mathf.Cos(angle)));
        }
        return new Vector2(ResX(highAngle) - ResX(lowAngle), ResY(highAngle) - ResY(lowAngle));
    }

    private float GetTorque(float radius, float lowAngle, float highAngle)
    {
        float Result(float angle)
        {
            return -force.y * Mathf.Cos(angle) - force.x * Mathf.Sin(angle);
        }

        return _torqueDampener * radius * (Result(highAngle) - Result(lowAngle));
    }
}
