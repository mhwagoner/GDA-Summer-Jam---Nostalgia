using UnityEngine;
public class ScoreCounter
{
    private float _consecutiveScoreTime = float.MaxValue;
    private float _timeSinceLastScore = 0.0f;
    private int _consecutiveScores = 0;

    public Animator dolphinAnimator;
    public Animator fingerAnimator;

    public ScoreCounter(float consecutiveScoreTime)
    {
        _consecutiveScoreTime = consecutiveScoreTime;
    }

    public void Update(float deltaTime)
    {
        _timeSinceLastScore += deltaTime;
        if (_timeSinceLastScore > _consecutiveScoreTime)
        {
            _consecutiveScores = 0;
        }
    }

    public void CountScore()
    {
        _consecutiveScores += 1;
        ShowFun();
        _timeSinceLastScore = 0.0f;
        if (_consecutiveScores >= 4)
        {
            _consecutiveScores = 0;
        }
    }

    public void LoseScore()
    {
        _consecutiveScores = -1;
        ShowFun();
        _consecutiveScores = 0;
    }

    public void ShowFun()
    {
        switch(_consecutiveScores)
        {
            case 4:
                Game.Instance.audioController.PlaySFX(SFX.HOLY_FUCK);
                dolphinAnimator.SetTrigger("talk");
                break;
            case 3:
                Game.Instance.audioController.PlaySFX(SFX.WOW);
                dolphinAnimator.SetTrigger("talk");
                break;
            case 2:
                Game.Instance.audioController.PlaySFX(SFX.GOOD_JOB);
                dolphinAnimator.SetTrigger("talk");
                break;
            case -1:
                // TODO: uh uh uh!
                Game.Instance.audioController.PlaySFX(SFX.UH_UH_UH);
                dolphinAnimator.SetTrigger("talk");
                fingerAnimator.SetTrigger("finger_wag");
                break;
        }
    }
}