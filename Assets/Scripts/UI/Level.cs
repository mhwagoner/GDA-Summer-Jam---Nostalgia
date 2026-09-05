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

        EventBus.Instance.OnScoreEarned += ChangeScore;
        EventBus.Instance.OnMultEarned += ChangeMult;
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

        float finalScore = mult * score;
        winScreen.scoreLabel.text = $"Final Score: {(int)finalScore}";
        winScreen.ToggleOpen(true);
        EventBus.Instance.EndLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if(levelTime <= 0f)
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
    }

    private void ActivateRainbowTime()
    {
        isRainbowTime = true;
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
        }
        if (scoreToAdd < 0) {
            audioController.PlaySFX(SFX.SCORE_LOST);
        }
        
        score += scoreToAdd;
    }

    private void ChangeMult(float multToAdd)
    {

        if(isRainbowTime){mult += multToAdd * 3;}
        else{mult += multToAdd;}
    }

    private void UpdateHUD()
    {
        //timer
        HUDController.timeLabel.text = $"{(int)levelTime}";

        //score
        HUDController.scoreLabel.text = $"{score}";

        //mult
        HUDController.multLabel.text = string.Format("{0:0.0#}", mult);
    }

    public void OnPause()
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

    public void OnOpenStats()
    {
        HUDController.ToggleOpen();
    }
}
