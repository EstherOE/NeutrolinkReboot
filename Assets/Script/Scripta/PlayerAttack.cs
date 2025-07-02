using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 1;
    public LayerMask enemyLayer;
    public LayerMask bossLayer;
    public Transform attackPt;
    public float attackRange = .5f;
    Animator anime;

    public int attackDamage = 10;
 
    private void Start()
    {
        anime = GetComponent<Animator>();
        
    }

    private void Update()
    {
        
    }
    public void PerformedAttack()
    {
        // the boss

        Collider2D[] bosses = Physics2D.OverlapCircleAll(attackPt.position, attackRange, bossLayer);
        foreach (Collider2D boss in bosses)
        {
            var bossScript = boss.GetComponent<BossBehaviour>(); // or BossAI
            if (bossScript != null)
            {
                bossScript.TakeDamage(attackDamage);
            }
        }

        //hits the enemies
         Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayer); 

        foreach(Collider2D enemy in colliders)
        {
            var enemyScript = enemy.GetComponent<EnemyAI>();
            int finalDaange = 2;

            if(enemyScript != null)
            {

                Debug.Log("sdhdhfdvhfvdhfvd");

                enemyScript.TakeDamage(finalDaange);
            }
        }
    }

    


    
    void OnDrawGizomosSelected()
{
        if (attackPt == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPt.position, attackRange);
}
}