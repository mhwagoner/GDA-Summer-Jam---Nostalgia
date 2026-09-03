using UnityEngine;

public class Goal : MonoBehaviour
{
    public int points;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Object obj)) return;
        if (!obj.hasScored)
        {
            EventBus.Instance.ScoreEarned(points);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Object obj)) return;
        // Only start the scoring cooldown once leaving the collider
        if (!obj.hasScored)
        {
            obj.hasScored = true;
        }
    }
}
