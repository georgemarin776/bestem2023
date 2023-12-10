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
    float nextDefenseTime = 0f;

    public Animator hitIndicatorAnimator;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            GetComponent<EnemyMovement>().isInAttack = false;

            if (GetComponent<EnemyMovement>().isAttakingInDirection)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Attack(0);
                    nextAttackTime = Time.time + 0.875f;
                    GetComponent<EnemyMovement>().isAttakingInDirection = false;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Attack(1);
                    nextAttackTime = Time.time + 0.875f;
                    GetComponent<EnemyMovement>().isAttakingInDirection = false;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Attack(2);
                    nextAttackTime = Time.time + 0.875f;
                    GetComponent<EnemyMovement>().isAttakingInDirection = false;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Attack(3);
                    nextAttackTime = Time.time + 0.875f;
                    GetComponent<EnemyMovement>().isAttakingInDirection = false;
                }
            }
        }

        if (Time.time >= nextDefenseTime)
        {
            GetComponent<EnemyMovement>().isInDefense = false;

            if (Input.GetKeyDown(KeyCode.O))
            {
                Defense();

                nextDefenseTime = Time.time + 1f;
            }
        }
    }

    void Defense()
    {
        animator.SetTrigger("Block");

        GetComponent<EnemyMovement>().isInDefense = true;
    }


    void Attack(int direction)
    {
        switch (direction)
        {
            case 0:
                hitIndicatorAnimator.SetTrigger("rightHit");
                break;
            case 1:
                hitIndicatorAnimator.SetTrigger("rightHit");
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

        GetComponent<EnemyMovement>().isInAttack = true;


        StartCoroutine(CheckForPlayersInRange(0.6f, direction));

    }
    IEnumerator CheckForPlayersInRange(float waitTime, int direction)
    {
        yield return new WaitForSeconds(waitTime);

        // Detect players in range of attack
        Collider2D[] hittedPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hittedPlayers)
        {
            player.GetComponent<PlayerTakeDamage>().OnTakeDamage(40, direction);
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
