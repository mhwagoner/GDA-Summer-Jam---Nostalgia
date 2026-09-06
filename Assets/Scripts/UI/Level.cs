using UnityEngine;

public class Level : MonoBehaviour
{
    public float levelTime = 60f;
    public int score = 0;
    public float mult = 1f;
    public const float multPerScore = 0.1f;
    public bool isPaused = false;
    public bool levelActive = false;
    public bool isRainbowTime = false;
    public float timeToActivateRainbow;

    //UI Menus
    public HUDController HUDController;
    public HUDController optionsMenu;
    public HUDController winScreen;

    private AudioController audioController;
    public GameObject audioControllerPrototype;

    public Music music;

    private float _secondMilestone = 5.0f;

    public float consecutiveScoreTime;
    private ScoreCounter _scoreCounter;

    private void OnEnable()
    {
        //
    }

    private void OnDisable()
    {
        //
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject audioControllerObj = Instantiate(audioControllerPrototype);
        if (!audioControllerObj.TryGetComponent(out audioController)) Debug.Log("AudioController prototype does not have an AudioController component");

        _scoreCounter = new ScoreCounter(consecutiveScoreTime);

        EventBus.Instance.OnScoreEarned += ChangeScore;
        EventBus.Instance.OnTubeFilled += ApplyTubeBonus;
        StartLevel();
        EventBus.Instance.StartLevel();
    }

    private void OnDestroy()
    {
        EventBus.Instance.OnScoreEarned -= ChangeScore;
        EventBus.Instance.OnMultEarned -= ChangeMult;
        EventBus.Instance.OnTubeFilled -= ApplyTubeBonus;
    }

    public void StartLevel()
    {
        levelActive = true;
        Time.timeScale = 1f;
        isPaused = false;
        isRainbowTime = false;
        if (audioController != null) audioController.PlayMusic(music);
    }

    public void EndLevel()
    {
        levelActive = false;
        Time.timeScale = 0f;
        HUDController.clockSpriteAnimator.SetBool("timeLow", false);

        float finalScore = mult * score;
        winScreen.scoreLabel.text = $"Final Score: {(int)finalScore}";
        winScreen.ToggleOpen(true);
        EventBus.Instance.EndLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if(levelTime <= 0f && levelActive)
        {
            EndLevel();
        }
        else if (levelTime <= timeToActivateRainbow && !isRainbowTime)
        {
            ActivateRainbowTime();
        }

        levelTime -= Time.deltaTime;

        if(HUDController != null)
        {
            UpdateHUD();
        }

        _scoreCounter.Update(Time.deltaTime);

        if (levelTime <= _secondMilestone)
        {
            audioController.PlaySFX(SFX.CLOCK_TICK, 1.5f);
            _secondMilestone -= 1.0f;
        }
    }

    private void ActivateRainbowTime()
    {
        isRainbowTime = true;
        audioController.PlayMusic(Music.RAINBOW);
        EventBus.Instance.ActivateRainbowTime();
    }

    private void ApplyTubeBonus(int bonusPoints, float bonusMult)
    {
        score += bonusPoints;
        ChangeMult(bonusMult);
    }

    private void ChangeScore(int scoreToAdd)
    {
        if (!levelActive){return;}

        if (scoreToAdd > 0) {
            audioController.PlaySFX(SFX.SCORE_EARNED);
            ChangeMult(multPerScore);
            _scoreCounter.CountScore();
        }
        if (scoreToAdd < 0) {
            audioController.PlaySFX(SFX.SCORE_LOST);
            _scoreCounter.LoseScore();
        }
        
        score += scoreToAdd;
    }

    private void ChangeMult(float multToAdd)
    {
        float multAdd = isRainbowTime ? multToAdd * 3 : multToAdd;
        mult += multAdd;
        EventBus.Instance.MultEarned(multAdd);
    }

    private void UpdateHUD()
    {
        //timer
        HUDController.timeLabel.text = $"{Mathf.CeilToInt(levelTime)}";

        //score
        HUDController.scoreLabel.text = $"{score}";

        //mult
        HUDController.multLabel.text = string.Format("{0:0.0#}", mult);

        if (levelTime <= 10.0f)
        {
            HUDController.clockSpriteAnimator.SetBool("timeLow", true);
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        if (isPaused && !optionsMenu.isOpen)
        {
            optionsMenu.ToggleOpen();
        }
        if (!isPaused && optionsMenu.isOpen)
        {
            optionsMenu.ToggleOpen();
        }
    }

    public void PauseTime(bool toPause)
    {
        Time.timeScale = toPause ? 0f : 1f;
        Debug.Log("new timescale = " + Time.timeScale);
    }

    public void OnOpenStats()
    {
        HUDController.ToggleOpen();
    }
}
