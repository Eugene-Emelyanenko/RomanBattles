using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayer;

    public int damage = 20;
    public float speed = 8f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;

    private bool isAttacking = false;
    private Animator animator;
    private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight = false;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(AttackPlayer());
    }

    void Update()
    {
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
        
        Flip();
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            horizontal = direction.x;
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    IEnumerator AttackPlayer()
    {
        while (true)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
            if (playerCollider != null)
            {
                isAttacking = true;
                animator.SetTrigger("Attack");
                playerCollider.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
                yield return new WaitForSeconds(attackCooldown);
                isAttacking = false;
            }
            yield return null;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
