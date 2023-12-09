using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            GetComponent<EnemyMovement>().isInAttack = false;

            if (Input.GetKeyDown(KeyCode.P))
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

        GetComponent<EnemyMovement>().isInAttack = true;

        // Detect players in range of attack
        Collider2D[] hittedPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        foreach (Collider2D enemy in hittedPlayers)
        {
            enemy.GetComponent<PlayerTakeDamage>().OnTakeDamage(40);
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
