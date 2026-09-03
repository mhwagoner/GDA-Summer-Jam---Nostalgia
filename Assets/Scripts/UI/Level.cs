using UnityEngine;

public class Level : MonoBehaviour
{
    public float levelTime = 60f;
    public int score = 0;
    public float mult = 1f;
    public const float multToAdd = 0.1f;
    public bool isPaused = false;
    public bool levelActive = false;
    public bool isRainbowTime = false;
    [SerializeField] private AudioClip scoreSFX;
    [SerializeField] private AudioClip loseScoreSFX;

    //UI Menus
    public HUDController HUDController;
    public HUDController optionsMenu;
    public HUDController winScreen;

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
        EventBus.Instance.OnScoreEarned += ChangeScore;
        StartLevel();
    }

    public void StartLevel()
    {
        levelActive = true;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void EndLevel()
    {
        levelActive = false;
        Time.timeScale = 0f;

        float finalScore = mult * score;
        winScreen.scoreLabel.text = "Final Score: " + finalScore;
        winScreen.ToggleOpen(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(levelTime <= 0f)
        {
            EndLevel();
        } else {
            levelTime -= Time.deltaTime;

            if(HUDController != null){UpdateHUD();}
        }
    }

    private void ChangeScore(int scoreToAdd)
    {
        if(!levelActive){return;}

        if(scoreToAdd > 0){GetComponent<AudioSource>().PlayOneShot(scoreSFX, 0.7f);}
        if(scoreToAdd < 0){GetComponent<AudioSource>().PlayOneShot(loseScoreSFX, 0.7f);}
        
        score += scoreToAdd;
        
        if(isRainbowTime){mult += multToAdd * 3;}
        else{mult += multToAdd;}
    }

    private void UpdateHUD()
    {
        //timer
        HUDController.timeLabel.text = "" + (int)levelTime;

        //score
        HUDController.scoreLabel.text = "Score: " + score;

        //mult
        HUDController.multLabel.text = "Multiplier: " + mult;
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
