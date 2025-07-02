using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    

    public int stageIndex = 1;  
    public int enemiesPerStage = 8;   

    private int killCount = 0;

    [Header("UI & References")]
    public EnemySpawner spawner;      

    public GameObject winPanel;
    public GameObject finalPanel;
    public TextMeshProUGUI levelText;    
    public TextMeshProUGUI progressText;
    public Button panicButton;

     public Slider bossHealthBar;    
    public GameObject pauseButton;     
  
   
    public int maxLevel = 6;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        if (spawner != null) spawner.StartSpawning();
       
        if (winPanel != null) winPanel.SetActive(false);
        if (bossHealthBar != null) bossHealthBar.gameObject.SetActive(false);
        UpdateProgressText();
        UpdateUI();
    }

    bool stageComplete = false;
    public void RegisterKill()
    {
        if (stageComplete) return;
        Debug.Log("hdd");
        killCount++;
        UpdateProgressText();

        if (killCount >= enemiesPerStage)
        {
           if(!spawner)
            spawner.StopSpawning();
            PlayerPrefs.SetInt("BestLevel", Mathf.Max(stageIndex, PlayerPrefs.GetInt("BestLevel", 0)));
            PlayerPrefs.Save();
            if ( stageIndex  >= maxLevel) 
                {
                 
                    if (finalPanel != null) finalPanel.SetActive(true);
                    Time.timeScale = 0f;
                    return; 
                }
            

            if (winPanel != null) winPanel.SetActive(true);
            panicButton.interactable = true;
            Time.timeScale = 0f;
        }

        UpdateUI();
    }
    void UpdateProgressText()
    {
        if (progressText != null)
            progressText.text = $"{killCount} / {enemiesPerStage} kills";

    }
    void UpdateUI()
    {
        if (levelText != null) levelText.text = $"LEVEL {stageIndex}";
     }
    public void RetryButton()
    {
        Time.timeScale = 1f; 
                             
        killCount = 0;
        if (winPanel != null) winPanel.SetActive(false);
        if (finalPanel != null) finalPanel.SetActive(false);

       
        foreach (var e in FindObjectsOfType<EnemyAI>())
            Destroy(e.gameObject);

       
        if (spawner != null) spawner.StartSpawning();
        UpdateProgressText();
        UpdateUI();
    }
    public void NextStageButton()
    {
        stageComplete = false;
        foreach (var e in FindObjectsOfType<EnemyAI>())
            Destroy(e.gameObject);
        if (stageIndex  >= maxLevel)
        {
            
            return;
        }

        stageIndex++;
        enemiesPerStage++;
        killCount = 0;
        UpdateUI();
        UpdateProgressText();


        if (winPanel != null) winPanel.SetActive(false);

        Time.timeScale = 1f;
        
        if (spawner != null) spawner.StartSpawning();
    }  
   

    public void QuitToMenu(string menuSceneName = "MainMenu")
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(menuSceneName);
    }

    public void BossHealth()
    {
        if(bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(true);
            bossHealthBar.maxValue = BossBehaviour.instance.bossHp;
            bossHealthBar.value = BossBehaviour.instance.currentHealth;
        }
    }
    internal void BossDefeated()
    {
        if (bossHealthBar != null)
            bossHealthBar.gameObject.SetActive(true);

        if (pauseButton != null)
            pauseButton.SetActive(false);
        if (finalPanel != null)
            finalPanel.SetActive(true);
        if (panicButton != null)
            panicButton.gameObject.SetActive(false);
    }
}
