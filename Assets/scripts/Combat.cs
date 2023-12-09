using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            GetComponent<Movement>().isInAttack = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 0.875f;
            }
        }
    }

    void Attack()
    {
        // play the attack animator
        animator.SetTrigger("Attack");

        GetComponent<Movement>().isInAttack = true;

        // Detect enemies in range of attack
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hittedEnemies)
        {
            enemy.GetComponent<TakeDamage>().OnTakeDamage(40);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }


        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
