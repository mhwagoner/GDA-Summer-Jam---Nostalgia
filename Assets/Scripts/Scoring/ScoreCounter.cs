using UnityEngine;
using UnityEngine.UI;
public class ScoreCounter
{
    private float _consecutiveScoreTime = float.MaxValue;
    private float _timeSinceLastScore = 0.0f;
    private int _consecutiveScores = 0;

    public Animator dolphinAnimator;
    public Animator fingerAnimator;
    public Image dolphinText;

    public Sprite wowSprite;
    public Sprite goodJobSprite;
    public Sprite holyFuckSprite;

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
        if (_consecutiveScores < 0)
        {
            Game.Instance.audioController.PlaySFX(SFX.UH_UH_UH);
            dolphinAnimator.SetTrigger("talk");
            fingerAnimator.SetTrigger("finger_wag");
            dolphinText.color = Color.clear;
        }
        else if (_consecutiveScores > 1)
        {
            int random = Random.Range(0, 3);
            switch(random)
            {
                case 2:
                    Game.Instance.audioController.PlaySFX(SFX.HOLY_FUCK);
                    dolphinAnimator.SetTrigger("talk");
                    dolphinText.sprite = holyFuckSprite;
                    dolphinText.color = Color.white;
                    break;
                case 1:
                    Game.Instance.audioController.PlaySFX(SFX.WOW);
                    dolphinAnimator.SetTrigger("talk");
                    dolphinText.sprite = wowSprite;
                    dolphinText.color = Color.white;
                    break;
                case 0:
                    Game.Instance.audioController.PlaySFX(SFX.GOOD_JOB);
                    dolphinAnimator.SetTrigger("talk");
                    dolphinText.sprite = goodJobSprite;
                    dolphinText.color = Color.white;
                    break;
            }
        }
    }
}