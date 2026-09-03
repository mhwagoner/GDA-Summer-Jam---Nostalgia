using UnityEngine;

public class ScoreCollider : MonoBehaviour
{
    public enum Type
    {
        ENTRY,
        EXIT
    }

    public Type type;

    public Goal goal;

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

        if (obj.hasScored) return;

        if (type == Type.ENTRY)
        {
            obj.hasEnteredTopScorebox = true;
            // If we hit the bottom first, subtract points
            if (obj.hasEnteredBottomScorebox)
            {
                obj.scoreToAdd = -goal.points;
            }
            // Otherwise, add points
            else obj.scoreToAdd = goal.points;
        }
        else if (type == Type.EXIT)
        {
            obj.hasEnteredBottomScorebox = true;
        }

        // If both hitboxes were hit, earn points
        if (obj.hasEnteredTopScorebox && obj.hasEnteredBottomScorebox)
        {
            obj.EarnPoints();
        }
    }

    // Clear whichever collider the object hit
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Object obj)) return;

        if (type == Type.ENTRY)
        {
            obj.hasEnteredTopScorebox = false;
        }
        else if (type == Type.EXIT)
        {
            obj.hasEnteredBottomScorebox = false;
        }
    }
}
