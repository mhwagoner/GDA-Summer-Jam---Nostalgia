using UnityEngine;

public class WaterJet : MonoBehaviour
{
    public Vector2 force;

    [SerializeField]
    private float _torqueDampener = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(typeof(Object), out Component comp)) return;

        Object obj = comp as Object;
        float leftBound = GetComponent<BoxCollider2D>().bounds.min.x;
        float rightBound = GetComponent<BoxCollider2D>().bounds.max.x;
        // The object's collider is always a circle collider
        float radius = (collision as CircleCollider2D).radius;
        float xMin = (leftBound - collision.bounds.center.x) / radius;
        float xMax = (rightBound - collision.bounds.center.x) / radius;

        GetAngleBounds(xMin, xMax, out float lowAngle, out float highAngle);

        obj.body.AddForce(GetForceVector(lowAngle, highAngle));

        obj.body.AddTorque(GetTorque(radius, lowAngle, highAngle));
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
