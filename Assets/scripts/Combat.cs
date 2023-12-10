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

    public Animator hitIndicatorAnimator;

    float nextAttackTime = 0f;
    float nextDefenseTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            GetComponent<Movement>().isInAttack = false;
            if (GetComponent<Movement>().isAttakingInDirection)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Attack(0);
                    nextAttackTime = Time.time + 0.875f;
                    GetComponent<Movement>().isAttakingInDirection = false;
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    Attack(1);
                    nextAttackTime = Time.time + 0.875f;
                    GetComponent<Movement>().isAttakingInDirection = false;
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    Attack(2);
                    nextAttackTime = Time.time + 0.875f;
                    GetComponent<Movement>().isAttakingInDirection = false;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    Attack(3);
                    nextAttackTime = Time.time + 0.875f;
                    GetComponent<Movement>().isAttakingInDirection = false;
                }
            }
        }

        if (Time.time >= nextDefenseTime)
        {
            GetComponent<Movement>().isInDefense = false;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Defense();

                nextDefenseTime = Time.time + 1f;
            }
        }
    }

    void Defense()
    {
        animator.SetTrigger("Block");

        GetComponent<Movement>().isInDefense = true;
    }

    void Attack(int direction)
    {
        switch (direction)
        {
            case 0:
                hitIndicatorAnimator.SetTrigger("leftHit");
                break;
            case 1:
                hitIndicatorAnimator.SetTrigger("leftHit");
                break;
            case 2:
                hitIndicatorAnimator.SetTrigger("topHit");
                break;
            case 3:
                hitIndicatorAnimator.SetTrigger("bottomHit");
                break;

            default: break;
        }

        // play the attack animator
        animator.SetTrigger("Attack");

        GetComponent<Movement>().isInAttack = true;

        // Detect enemies in range of attack

        StartCoroutine(CheckForEnemiesInRange(0.6f));
    }

    IEnumerator CheckForEnemiesInRange(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hittedEnemies)
        {
            // TODO: asteapta 0.6 secunde si dupa verifica daca e in range

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
