
using Scripts.Systems;
using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;


[DefaultExecutionOrder(-100)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public static PersistentData pData;

    private int buildIndex;
    private int nextSceneIndex;
    private int titleScreenIndex;

    public int firstLevel { get; private set; }
    public int currentLevel { get; private set; }

    public event Action OnLevelChange;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        buildIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = buildIndex + 1;
        titleScreenIndex = 0; // SceneManager.GetSceneByName("TitleScreen").buildIndex;   // should be 0 aka main menu
        firstLevel = 1;
        pData = PersistentData.Instance;

        ScoreSystem.Instance.ResetLevelScore();


        if (GameStateSystem.Instance != null)
        {
            GameStateSystem.Instance.TriggerPlay();
            GameStateSystem.Instance.OnStateChanged += OnGameStateChanged;
        }
    }

    private void Update()
    {
        HandleStateInput(GameStateSystem.Instance.CurrentState);

    }

    private void OnEnable()
    {
        if (GameStateSystem.Instance != null)
        {
            GameStateSystem.Instance.OnStateChanged += OnGameStateChanged;
        }
    }

    private void OnDisable()
    {        
        if (GameStateSystem.Instance != null)
        {
            GameStateSystem.Instance.OnStateChanged -= OnGameStateChanged;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        OnLevelChange?.Invoke();
        ScoreSystem.Instance.ResetLevelScore();
        SceneManager.LoadScene(levelIndex);     // 0 = main menu, buildIndex = this level, nextSceneIndex = next level
        GameStateSystem.Instance.TriggerPlay();
        buildIndex = levelIndex;
        currentLevel = levelIndex;
        nextSceneIndex = levelIndex + 1;
    }


    public void NextLevel()
    {
        // adding currentLevelScore to total
        pData.AddToTotalScore(ScoreSystem.Instance.GetCurrentLevelScore());
        
        if(nextSceneIndex>= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = titleScreenIndex;
        }

        LoadLevel(nextSceneIndex);

    }

  
    void OnGameStateChanged(GameState from, GameState to)
    {
        Debug.Log($"OnGameStateChanged, from({from}) to({to})");
        switch (to)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.Pause:
                Debug.Log("Paused");
                Time.timeScale = 0f;
                break;

            case GameState.Defeat:
                Time.timeScale = 0f;
                pData.SaveTopScore();
                break;

            case GameState.Victory:
                Time.timeScale = 0f;
                pData.SaveTopScore();
                break;
        }
    }
  

    void HandleStateInput(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                if (Input.GetKeyDown(KeyCode.P))
                {
                    Debug.Log("Pause Triggered");
                    GameStateSystem.Instance.TriggerPause();
                }
                break;         
            case GameState.Pause:
                HandlePauseInput();
                break;
            case GameState.Defeat:
                HandleGameOverInput();                
                break;
            case GameState.Victory:
                HandleVictoryInput();
                break;
        }
    }

    void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameStateSystem.Instance.TriggerPlay(); // play
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel(buildIndex);                  // restart level
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadLevel(titleScreenIndex);            // go to main menu
        }
    }

    void HandleGameOverInput()
    {        
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel(buildIndex);                  // restart level
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadLevel(titleScreenIndex);            // go to main menu
        }
    }

    void HandleVictoryInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel(buildIndex);                  // restart level
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadLevel(titleScreenIndex);            // go to main menu
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();                            // go to next level
        }
    }


}
