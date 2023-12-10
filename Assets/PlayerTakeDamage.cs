using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeDamage : MonoBehaviour
{
    public Animator animator;
    public Slider healthSlider; // Reference to the UI Slider

    private float maxHealth = 100;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        // Set the initial value of the health slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void OnTakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI(); // Update the health bar

        DeployAnimation();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void DeployAnimation()
    {
        animator.SetTrigger("Hurt");
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
    }

    void UpdateHealthUI()
    {
        // Update the health bar value
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;

            // Check if health is zero, and make the fill invisible
            if (currentHealth <= 0)
            {
                Image fillImage = healthSlider.fillRect.GetComponent<Image>();
                if (fillImage != null)
                {
                    fillImage.color = new Color(0, 0, 0, 0); // Set alpha to 0 (completely transparent)
                }
            }
        }
    }
}
