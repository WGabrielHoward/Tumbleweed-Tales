using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playerName;

   public void PlayPressed()
    {
        SceneManager.LoadScene("Scene_0");
    }
}
