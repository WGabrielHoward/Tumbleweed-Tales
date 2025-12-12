using Scripts.NPC;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//[DefaultExecutionOrder(500)]
public class LevelCanvas : MonoBehaviour
{

    public TMPro.TextMeshProUGUI ScoreText;
    public TMPro.TextMeshProUGUI TopScore;
    public TMPro.TextMeshProUGUI HealthText;
    public GameObject GameOverText;

    private MainManager pMan;
    //Start is called before the first frame update
    void Start()
    {
        GameOverText.SetActive(false);
        pMan = MainManager.ManInstance;
    }

    // change the game over update to a listerner or event (check that it is actually better)
    void LateUpdate()
    {
        PlayState tmpState = pMan.GetState();
        switch (tmpState)
        {
            case PlayState.gameOver:
                GameOverText.SetActive(true);
                break;
            case PlayState.playing:     
                GameOverText.SetActive(false);  // I'm not a fan of calling this so often.
                break;
            // Room for other cases
        }
    }

    public void ScoreUpdate()
    {
        ScoreText.text = $"Score : {pMan.GetScore()}";
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
