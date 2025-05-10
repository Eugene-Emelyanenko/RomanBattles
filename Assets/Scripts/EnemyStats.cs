using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 100;
    
    private EnemyStats enemyStats;
    private int currentHealth;
    private Animator animator;
    private Vector2 spawnPoint;
    private Score score;
    void Start()
    {
        score = FindObjectOfType<Score>().GetComponent<Score>();
        spawnPoint = transform.position;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        animator.SetTrigger("Hurt");
        
        score.IncreaseScore(1);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioManager.instance.Play("Win");
        Debug.Log(gameObject.name + " Died");
        score.IncreaseScore(20);
        transform.position = spawnPoint;
        currentHealth = maxHealth;
    }
    
    
}
