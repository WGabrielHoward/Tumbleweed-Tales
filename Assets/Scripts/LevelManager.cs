
using Scripts.Systems;
using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager 
{
    public static PersistentData pData;

    private int buildIndex;
    private int nextSceneIndex;
    private int titleScreenIndex;

    public int firstLevel { get; private set; }
    public int currentLevel { get; private set; }

    public event Action OnLevelChange;


    // Start is called before the first frame update
    public LevelManager()
    {

        buildIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = buildIndex + 1;
        titleScreenIndex = 0; // SceneManager.GetSceneByName("TitleScreen").buildIndex;   // should be 0 aka main menu
        firstLevel = 1;
        pData = Launcher.Instance.Persistent;

        Launcher.Instance.ScoreSystem.ResetLevelScore();

        Launcher.Instance.GameStateSystem.TriggerPlay();
        Launcher.Instance.GameStateSystem.OnStateChanged += OnGameStateChanged;
        
    }

    public void Update()
    {
        HandleStateInput(Launcher.Instance.GameStateSystem.CurrentState);

    }

    

    public void LoadLevel(int levelIndex)
    {
        OnLevelChange?.Invoke();
        Launcher.Instance.ScoreSystem.ResetLevelScore();
        SceneManager.LoadScene(levelIndex);     // 0 = main menu, buildIndex = this level, nextSceneIndex = next level
        Launcher.Instance.GameStateSystem.TriggerPlay();
        buildIndex = levelIndex;
        nextSceneIndex = levelIndex + 1;
        if (levelIndex != 0)
        {
            currentLevel = levelIndex;
        }
    }


    public void NextLevel()
    {
        // adding currentLevelScore to total
        pData.AddToTotalScore(Launcher.Instance.ScoreSystem.GetCurrentLevelScore());
        
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
                    Launcher.Instance.GameStateSystem.TriggerPause();
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
            Launcher.Instance.GameStateSystem.TriggerPlay(); // play
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
