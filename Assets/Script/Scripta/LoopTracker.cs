using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoopTracker : MonoBehaviour
{
    public float slashCount;
    public float dashCountl;
    public float idleloop;

    public float decaySpeed = 0.2f;

    private float idleTimer;
    private float moveInterval = .5f;
    private float moveTimer;
    private Vector2 lastPt;
    [Header("Reboot")]
    public bool rebootReady;     
    public float rebootTimer;      
    public float rebootCooldown;   
    public float rebootHpCostPercent = 0.25f;

    [Header("Panic")]
    public bool panicReady=false;
    public float painictHpCostPercent = .50f;

    [Header("Overload")]
    public float overloadThreshold = 1f;  
    public bool isOverloading { get; private set; }
    public float overloadCooldown = 5f;   
    float overloadTimer;

    public static LoopTracker instance;
    private void Awake()
    {

        if (instance == null)
            instance = this;

        else
        {
            Destroy(gameObject);
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        float dt = Time.unscaledDeltaTime;
        if (!rebootReady)
        {
            rebootTimer += Time.unscaledDeltaTime;
            if (rebootTimer >= rebootCooldown)
            {
                rebootTimer = 0f;
                rebootReady = true;          
            }
        }
        DecayLoopValue(dt);
        trackeIdle(dt);
        HandleOverload(dt);
        HandleRebootCooldown(dt);
       
    }

   

    private void HandleRebootCooldown(float dt)
    {
        if (rebootReady) return;

        rebootTimer += dt;
        if (rebootTimer >= rebootCooldown)
        {
            rebootTimer = 0f;
            rebootReady = true;
        }
    }

    private void DecayLoopValue(float dt)
    {
        slashCount = Mathf.MoveTowards(slashCount, 0, decaySpeed * dt);
        dashCountl = Mathf.MoveTowards(dashCountl, 0, decaySpeed * dt);
      idleloop = Mathf.MoveTowards(idleloop, 0, decaySpeed * dt);

    }

    private void trackeIdle( float dt)
    {
        moveTimer += dt;
        if(moveTimer >=moveInterval)
        {
            if(Vector3.Distance(transform.position, lastPt)<0.1f)
            {

                idleloop += .05f;
                idleloop = Mathf.Clamp01(idleloop);

                
            }
            lastPt = transform.position;
            moveTimer = 0f;

        }

    }


    public void RebootLoop(float amount)
    {
        if (!rebootReady) return;
        ReduceHp(amount);
        slashCount = dashCountl = idleloop = 0f;
        rebootReady = false;
        rebootTimer = 0f;
       
    }
    public void PanicLoop()
    {
       
        ReduceHp(painictHpCostPercent);
       
       // panicReady = false;
    }

    void ReduceHp( float cost)
    {
        var hp = PlayerHealth.instance;
        if (hp != null)
        {
            int dmg = Mathf.RoundToInt(hp.maxHealth *cost);
            if (hp.currentHealth <= dmg) return;
            hp.TakeDamage(dmg);
        }

    }

    void HandleOverload(float dt)
    {
        float maxBar = Mathf.Max(slashCount, dashCountl, idleloop);

        if (!isOverloading && maxBar >= overloadThreshold)
            StartCoroutine(TriggerOverload());

        if (isOverloading)
        {
            overloadTimer += dt;
            if (overloadTimer >= overloadCooldown)
            {
                isOverloading = false;
                overloadTimer = 0f;
                ClearAllLoops();
                NotifyEnemiesExitOverload();
            }
        }
    }

    private void ClearAllLoops()
    {
        slashCount = dashCountl = idleloop = 0f;
    }

    System.Collections.IEnumerator TriggerOverload()
    {
        isOverloading = true;

     //   GlitchFX.Instance?.PlayGlitch();      // optional overlay

        NotifyEnemiesEnterOverload();         // speed / attack boosts
        yield return null;
    }

    void NotifyEnemiesEnterOverload()
    {
        foreach (EnemyAI e in FindObjectsOfType<EnemyAI>())
            e.EnterOverloadMode();
    }

    void NotifyEnemiesExitOverload()
    {
        foreach (EnemyAI e in FindObjectsOfType<EnemyAI>())
            e.ExitOverloadMode();
    }

    public void RegisterDash()
    {
        dashCountl += .1f;
        dashCountl = Mathf.Clamp01(dashCountl);
    }

    public void RegisterSlash()
    {
        slashCount += .1f;
        slashCount = Mathf.Clamp01(slashCount);

    }
}
