using System;
using UnityEngine;

public class ScoreCollider : MonoBehaviour
{
    public enum Type
    {
        ENTRY,
        EXIT,
        UNPAIRED,
    }

    public Type type;

    public Goal goal;

    public event Action<Object> OnObjectEnter;
    public event Action<int> OnScoreEarned;

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

        if (obj.hasScored || !obj.canScore) return;

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
        //Unpaired ones should just give points
        else if (type == Type.UNPAIRED)
        {
            obj.scoreToAdd = goal.points;
            obj.hasEnteredBottomScorebox = true;
            obj.hasEnteredTopScorebox = true;
        }

        // If both hitboxes were hit, earn points
        if (obj.hasEnteredTopScorebox && obj.hasEnteredBottomScorebox)
        {
            OnScoreEarned?.Invoke(obj.scoreToAdd);
            obj.EarnPoints();
        }

        OnObjectEnter?.Invoke(obj);
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
