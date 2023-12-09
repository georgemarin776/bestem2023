using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public Animator animator;

    private float maxHealth = 100;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnTakeDamage(float damage)
    {
        currentHealth -= damage;

        Invoke("deployAnimation", 0.6f);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void deployAnimation()
    {
        animator.SetTrigger("Hurt");
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
    }
}
