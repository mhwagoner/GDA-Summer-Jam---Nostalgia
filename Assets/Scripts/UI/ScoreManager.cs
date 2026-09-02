using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Clear duplicates
            return;
        }

        Instance = this;
    }

    public int levelIndex = 0;

    public float levelTime = 0f;
    public float score = 0f;
    public float mult = 1f;
    public bool isPaused = false;
    [SerializeField] private AudioClip collectSFX;

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
        StartGame();
    }

    public void StartGame()
    {
        levelTime = 0f;
        score = 0f;
        mult = 1f;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        levelTime += Time.deltaTime;
        if(HUDController != null){UpdateHUD();}
    }

    public void AddScore(float scoreToAdd)
    {
        GetComponent<AudioSource>().PlayOneShot(collectSFX, 0.7f);
        score += scoreToAdd;
    }

    public HUDController HUDController;
    public HUDController optionsMenu;

    private void UpdateHUD()
    {
        //timer
        int seconds = (int)levelTime % 60;
        int minutes = (int)levelTime / 60;
        HUDController.timeLabel.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

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
