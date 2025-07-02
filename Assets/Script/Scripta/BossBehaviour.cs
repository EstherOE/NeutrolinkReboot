using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    public float bossSpeed = 6;
    public float bossHp = 150;
    public int bossAttack = 15;
    internal int currentHealth;

    public static BossBehaviour instance;
    public float bossAttackRange = 2;
    public int bossAttackCoolDown = 2;

   // public Slider healthSlider;

    Transform player;
    float lastAttack;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = (int)bossHp;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;


        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * bossSpeed * Time.deltaTime;
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= bossAttackRange && Time.time > lastAttack + bossAttackCoolDown)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(bossAttack);
            lastAttack = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (LevelManager.Instance.bossHealthBar != null)
            LevelManager.Instance.bossHealthBar.value = currentHealth;
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        if (LevelManager.Instance.bossHealthBar != null)
            LevelManager.Instance.bossHealthBar.gameObject.SetActive(false);

        LevelManager.Instance.BossDefeated();
        Destroy(gameObject, 0.2f);
    }
}
