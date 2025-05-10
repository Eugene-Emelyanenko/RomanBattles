using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;

    private Animator animator;
    private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isDead;
    
    private void Start()
    {
        isDead = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isDead)
            return;
        
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
        
        Flip();
    }

    private void FixedUpdate()
    {
        if(isDead)
            return;
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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

    public void MoveRight()
    {
        horizontal = 1;
    }
    public void MoveLeft()
    {
        horizontal = -1;
    }
    public void StopMoving()
    {
        horizontal = 0;
    }

    public void GameOver()
    {
        rb.velocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
        isDead = true;
    }
}
