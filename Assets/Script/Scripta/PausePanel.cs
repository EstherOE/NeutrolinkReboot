using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;

    public Button rebootButton;
    public Button panicButton;
    public bool isRebootGlitch = false;

    void Update()
    {
        

         if (pausePanel.activeSelf)
        {
            bool ready = LoopTracker.instance.rebootReady;
            rebootButton.interactable = ready;

            
            float pct = LoopTracker.instance.rebootTimer /
                        LoopTracker.instance.rebootCooldown;
          
        }
    }

    public void TogglePause()
    {
        bool isPaused = Time.timeScale == 0;
        Time.timeScale = isPaused ? 1f : 0f;
      
    }

    public void ToogleResume()
    {
        Time.timeScale = 1f;
    }

    public void PanicPressed()
    {
     
        panicButton.interactable = false;
    }


    
    public void RebootPressed(float amount)
    {
        
        if (!LoopTracker.instance.rebootReady) return;
     
        LoopTracker.instance.RebootLoop(amount);
       
        Time.timeScale = 1;
    }
}


