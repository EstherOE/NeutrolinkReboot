using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffcultController : MonoBehaviour
{
    public static DiffcultController Instance;

   
    [Header("Enemies info")]

    public float enemySpeed = 2f;
    public int enemyHp = 3;
    public float spawnRate = 3;

    [Header("Difficult Scaling")]
    public float speedIncrease = .3f;
    public int increaseHp = 1;
    public float spawnRateDecrease = .2f;

    public float endlessMultiplier = 2f;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public float GetEnemySpeed(int level)
    {
        float speed = enemySpeed + (speedIncrease * level);
        speed *= endlessMultiplier;
        return speed;
    }


    public int GetEnemyHp(int level)
    {
        int hp = enemyHp + (increaseHp * level);
        hp *= Mathf.RoundToInt(endlessMultiplier);
        return hp;
    }

    public float GetEnemyRate(int level)
    {
        float rate = spawnRate + (spawnRateDecrease * level);
        rate *= endlessMultiplier;
        return rate;
    }

}
