using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform attackPoint;
    
    public float attackRange = 0.5f;
    public int damage = 30;
    public float attackRate = 2f;
    
    private float nextAttackTime = 0f;
    private bool canAttack;
    [NonSerialized] public bool isBlocking;
    private bool isDead;

    private Animator animator;
    void Start()
    {
        isDead = false;
        isBlocking = false;
        canAttack = true;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isDead)
            return;
        
        if (Time.time >= nextAttackTime)
        {
            canAttack = true;
        }
        
        animator.SetBool("IsBlocking", isBlocking);
    }

    public void Attack()
    {
        if (canAttack && !isDead)
        {
            animator.SetTrigger("Attack");

            Collider2D enemyCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);
            if (enemyCollider != null)
            {
                AudioManager.instance.Play("AttackHit");
                Debug.Log("Hit" + enemyCollider.gameObject.name);
                enemyCollider.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            }
            else
                AudioManager.instance.Play("Attack");
            canAttack = false;  
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void BlockDown()
    {
        isBlocking = true;
    }
    public void BlockUp()
    {
        isBlocking = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    public void GameOver()
    {
        isDead = true;
    }
}
