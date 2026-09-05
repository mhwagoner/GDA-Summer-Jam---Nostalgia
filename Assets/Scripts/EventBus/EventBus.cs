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
    }

    public event Action OnRainbowTimeActivated;

    public void ActivateRainbowTime()
    {
        OnRainbowTimeActivated?.Invoke();
    }

    public event Action OnLevelStart;

    public void StartLevel()
    {
        OnLevelStart?.Invoke();
    }

    // note: UNUSED
    public event Action<float> OnMultEarned;

    public void MultEarned(float mult)
    {
        OnMultEarned?.Invoke(mult);
    }

    public event Action<int, float> OnTubeFilled;

    public void TubeFilled(int bonusScore, float bonusMult)
    {
        OnTubeFilled?.Invoke(bonusScore, bonusMult);
    }
}