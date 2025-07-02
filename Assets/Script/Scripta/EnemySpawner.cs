using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner Instance;
    public Transform[] spawnPoints;
    public GameObject[] enemySpawn;
    public int enemiesPerStage = 8;
    public float spawnInterval = 3f; 
    public float initialSpawnDelay = 2f;
    Coroutine spawnLoop;
    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
       
        spawnLoop = StartCoroutine(SpawnLoop());
    }
    IEnumerator SpawnLoop()
    {
      
        yield return new WaitForSeconds(initialSpawnDelay);

        while (true)
        {
            SpawnEnemy();
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void StopSpawning()
    {
     
        if (spawnLoop != null)
            StopCoroutine(spawnLoop);
    }

    void SpawnEnemy()
    {
        Transform pt = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject pf = enemySpawn[Random.Range(0, enemySpawn.Length)];
        var go = Instantiate(pf, pt.position, Quaternion.identity);

        var ai = go.GetComponent<EnemyAI>();
        

        float enemySpeed = DiffcultController.Instance.GetEnemySpeed(LevelManager.Instance.stageIndex);
        int hp = DiffcultController.Instance.GetEnemyHp(LevelManager.Instance.stageIndex);
        ai.InitializeEnemy(pt.name);
        ai.moveSpeed *= enemySpeed;
        ai.SetStats(enemySpeed, hp);
       
        ai.OnDied += HandleEnemyDeath;
    }

    void HandleEnemyDeath()
    {
        Debug.Log("Debug.Log([EnemyAI] Die() called);");
        // notify LevelManager of a kill
    }
}





