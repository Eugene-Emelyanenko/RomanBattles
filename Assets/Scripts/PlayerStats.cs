using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Image[] playerLives;
    [SerializeField] private Sprite activeLive;
    [SerializeField] private Sprite nonActiveLive;
    [SerializeField] private GameObject gameOverMenu;
    
    public int maxHealth = 100;

    private PlayerController playerController;
    private PlayerCombat playerCombat;
    private int currentHealth;
    private Animator animator;
    private bool isDead;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerCombat = GetComponent<PlayerCombat>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateLives();
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        if(isDead)
            return;
        
        if (playerCombat.isBlocking)
        {
            AudioManager.instance.Play("Block");
            Debug.Log("Blocked");
            return;
        }

        AudioManager.instance.Play("Hurt");

        currentHealth -= damage;
        UpdateLives();
        
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateLives()
    {
        playerLives[0].sprite = currentHealth > 0 ? activeLive : nonActiveLive;
        playerLives[1].sprite = currentHealth > 50 ? activeLive : nonActiveLive;
        playerLives[2].sprite = currentHealth > 75 ? activeLive : nonActiveLive;
    }

    private void Die()
    {
        AudioManager.instance.Play("Lose");
        Debug.Log(gameObject.name + " Died");
        gameOverMenu.SetActive(true);
        isDead = true;
        playerCombat.GameOver();
        playerController.GameOver();
    }
}
