using System;

public class EventBus
{
    private static EventBus _instance;

    public static EventBus Instance
    {
        get
        {
            if (_instance == null) _instance = new();
            return _instance;
        }
    }

    private EventBus()
    {

    }

    public event Action<int> OnScoreEarned;

    public void ScoreEarned(int points)
    {
        OnScoreEarned?.Invoke(points);
        UnityEngine.Debug.Log($"Earned {points} points");
    }

}