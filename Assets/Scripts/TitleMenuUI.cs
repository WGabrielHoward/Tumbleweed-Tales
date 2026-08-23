
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuUI : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI TopScore;
    [SerializeField] private TMPro.TMP_InputField playerName;
    [SerializeField] private GameObject hiddenObj;
    private bool hidden;
    PersistentData pData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerName.characterLimit = 12;
        pData = PersistentData.Instance;
        if (pData != null)
        {
            pData.TopScoreUpdate();
            TopScoreUpdate();
            ClearTotalScore();
        }
        hiddenObj.SetActive(false);
        hidden = true;
        
    }

    public void StartPlay()
    {
        if (pData != null)
        {
            pData.playerName = playerName.text.ToString();
        }
        Launcher.Instance.LevelManager.LoadLevel(Launcher.Instance.LevelManager.firstLevel);
    }

    public void TopScoreUpdate()
    {
        //pMan.Dump();
        TopScore.text = $"Top Score: {pData.GetTopName()} {pData.GetTopPoints()}";
    }

    public void ClearMemory()
    {
        pData.ClearTopScore();
        TopScoreUpdate();
    }

    public void ClearTotalScore()
    {
        pData.ClearTotalScore();
    }

    public void HideOrReveal()
    {
        if (hiddenObj)
        {
            if (hidden)
            {
                hiddenObj.SetActive(hidden);
                hidden = false;
            }
            else
            {
                hiddenObj.SetActive(hidden);
                hidden = true;
            }

        }
    }

   

    public void Exit()
    {
        pData.SaveTopScore();     // enable this to autoSave any changes, else it will go with whatever was last saved
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
