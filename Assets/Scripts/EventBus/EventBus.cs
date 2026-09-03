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

    public event Action<int> onScoreEarned;

    public void OnScoreEarned(int points)
    {
        onScoreEarned?.Invoke(points);
        UnityEngine.Debug.Log($"Earned {points} points");
    }
}