using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
  
    public float moveSpeed = 3f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public int attackDamage = 10;


    public float speedMultiplier = 1.7f;      
    public float cooldownMultiplier = 0.6f;    

    private Transform player;
    private PlayerHealth playerHealth;
    private float lastAttackTime;
    private SpriteRenderer sr;
    private Color baseCOlor;

    public int health = 3;
    int currentHealth;
    private bool overloaded;
    void Start()
    {
        currentHealth = health;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj)
        {
            player = playerObj.transform;
            playerHealth = playerObj.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("Player GameObject not found. Check tag.");
        }

        sr = GetComponent<SpriteRenderer>();
        if (sr)
            baseCOlor = sr.color;

     }

    public void InitializeEnemy(string spawnPointName)
    {
         switch (spawnPointName)
        {
            case "Spawn_Back":
                transform.localScale = new Vector3(-4, 4, 1);
                break;

            case "Spawn_Right":
                 transform.localScale = new Vector3(4, 4, 1);

                break;

            case "Spawn_Middle":
                 transform.localScale = new Vector3(4, 4, 1);

                break;

            default:
                Debug.LogWarning("Unrecognized spawn point name: " + spawnPointName);
                break;
        }
    }

    void Update()
    {
        if (!player) return;

        Vector2 direction = (player.position - transform.position).normalized;

        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            playerHealth.TakeDamage(attackDamage);
            lastAttackTime = Time.time;
            Debug.Log("Enemy attacked player!");
        }

        if (player.position.x > transform.position.x)
            sr.flipX = true;
        else
            sr.flipX = false;

    }

    public void EnterOverloadMode()
    {
        if (overloaded) return;
        overloaded = true;

        moveSpeed *= speedMultiplier;
        attackCooldown *= cooldownMultiplier;    // smaller cooldown = faster attacks

        if (sr) sr.color = Color.red;
       
    }


    
    public void ExitOverloadMode()
    {
        if (!overloaded) return;
        overloaded = false;

        moveSpeed /= speedMultiplier;
        attackCooldown /= cooldownMultiplier;

        if (sr) sr.color = baseCOlor;
    }

    public event System.Action OnDied;

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
            Die();
        EnemyFlash.instance.Flash();
    }

    void Die()                          
    {
    
        Debug.Log("[EnemyAI] Die() called");
        LevelManager.Instance.RegisterKill();
        Destroy(gameObject);
    }
    public void SetStats(float speed, int hp)
    {
        moveSpeed = speed;
        health = hp;
        currentHealth = hp;
    }

}


