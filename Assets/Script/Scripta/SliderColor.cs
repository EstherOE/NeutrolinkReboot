using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class SliderColor : MonoBehaviour
{

    public static SliderColor instance;

    [Header("UI Sliders")]
    public Slider slashSlider;
    public Slider dashSlider;
    public Slider idleSlider;

    [Header("Tuning")]
    [Range(0f, 1f)] public float addStep = 0.25f;
    [Range(0f, 2f)] public float decayRate = 0.35f;

    void Awake() => instance = this;

    void Update()
    {
        if (LoopTracker.instance == null) return;

        slashSlider.value = LoopTracker.instance.slashCount;
        dashSlider.value = LoopTracker.instance.dashCountl;
        idleSlider.value = LoopTracker.instance.idleloop;

        // OPTIONAL: colour-tint each fill if you want thresholds
        //   (assumes the Fill image is child 0)
        UpdateFillColour(slashSlider);
        UpdateFillColour(dashSlider);
        UpdateFillColour(idleSlider);
    }

    void UpdateFillColour(Slider s)
    {
        Image fill = s.fillRect.GetComponent<Image>();
        float v = s.value;

        if (v < 0.4f) fill.color = Color.green;
        else if (v < 0.7f) fill.color = Color.yellow;
        else fill.color = Color.red;
    }
    public void OnSlash()
    {
        if (LoopTracker.instance != null)
            LoopTracker.instance.RegisterSlash();
    }

    public void OnDash()
    {
        if (LoopTracker.instance != null)
            LoopTracker.instance.RegisterDash();
    }

    public void OnIdle()   // keep only if you really need manual idle bumps
    {
        if (LoopTracker.instance != null)
            LoopTracker.instance.idleloop =
                Mathf.Clamp01(LoopTracker.instance.idleloop + 0.05f);
    }


}