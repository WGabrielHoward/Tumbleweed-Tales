
using System.Net.NetworkInformation;
using UnityEngine;

//[DefaultExecutionOrder(500)]
public class LevelCanvas : MonoBehaviour
{

    public TMPro.TextMeshProUGUI ScoreText;
    public TMPro.TextMeshProUGUI HealthText;

    public TMPro.TextMeshProUGUI TotalScoreText;
    public TMPro.TextMeshProUGUI TopScore;
    
    public GameObject GameOverScreen;
    public GameObject VictoryScreen;
    public GameObject PauseScreen;

    private LevelManager pMan;
    private PlayState lastState;
    //Start is called before the first frame update
    void Start()
    {
        pMan = LevelManager.ManInstance;
        PlayingSetup();
    }

    // change the game over update to a listerner or event (check that it is actually better)
    void Update()
    {
        PlayState tmpState = pMan.GetState();
        if (tmpState != lastState)
        {
            ChangePlayState(tmpState);
            lastState = tmpState;
        }
    }

    // Now we only call the screen setActives when state is changed
    private void ChangePlayState(PlayState newState)
    {
        switch (newState)
        {
            case PlayState.gameOver:
                GameOverScreen.SetActive(true);
                TotalScoreText.gameObject.SetActive(true);
                break;
            case PlayState.playing:
                PlayingSetup();
                break;
            case PlayState.victory:
                VictoryScreen.SetActive(true);
                NotPlayingSetup();
                break;
            case PlayState.paused:
                PauseScreen.SetActive(true);
                NotPlayingSetup();
                break;
        }
    }

    private void PlayingSetup()
    {
        GameOverScreen.SetActive(false);
        VictoryScreen.SetActive(false);
        PauseScreen.SetActive(false);
        TotalScoreText.gameObject.SetActive(false);
        TopScore.gameObject.SetActive(false);

        ScoreText.gameObject.SetActive(true);
        HealthText.gameObject.SetActive(true);
    }

    // Screen actives called outside of this
    // only used for paused and victory right now
    private void NotPlayingSetup()
    {
        TotalScoreText.gameObject.SetActive(true);
        TopScore.gameObject.SetActive(true);
    }

    public void ScoreUpdate()
    {
        ScoreText.text = $"Score : {pMan.GetScore()}";
    }

    public void TotalScoreUpdate()
    {
        // pMan.GetTotalScore() returns sum of all prior and current level points
        TotalScoreText.text = $"Total Score: {pMan.GetTotalScore()}";
    }

    public void TopScoreUpdate()
    {
        //pMan.Dump();
        TopScore.text = pMan.GetTopScoreText();
    }

    public void HealthUpdate(int health)
    {
        HealthText.text = $"Health: {health}";
    }
}
