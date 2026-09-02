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

    private event Action<int> _onScoreEarned;

    public void OnScoreEarned(int points)
    {
        _onScoreEarned?.Invoke(points);
        UnityEngine.Debug.Log($"Earned {points} points");
    }
}