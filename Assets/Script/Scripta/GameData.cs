using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "NeuroLink/Game_Data")]

public class GameData  : ScriptableObject
{
    [Header("Player")]
    public int playerMaxHP = 100;
    public int playerBaseDMG = 25;
    [Range(0, 1)] public float critChance = .15f;

    [Header("Enemy")]
    public int enemyHP = 3;
    public int enemyDamage = 10;
    public float enemySpeed = 3f;
    public float attackCooldown = 1.2f;

    [Header("Spawning")]
    public float spawnInterval = 3f;   
    public float firstDelay = 2f;

    
}


