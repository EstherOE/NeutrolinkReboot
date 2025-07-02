using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuManaager : MonoBehaviour
{
    public string gameScene = "GameScene";
    public TextMeshProUGUI bestLevelText;
    private void Start()
    {
        int bestScore = PlayerPrefs.GetInt("BestLevel",0);
        bestLevelText.text = $"BEST LEVEl: {bestScore}"; 
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame()
    {

        Application.Quit();
    }

    public void ResetProgess()
    {
        PlayerPrefs.DeleteKey("BestLevel");
        bestLevelText.text = "BestLevel:  0";
    }
}
