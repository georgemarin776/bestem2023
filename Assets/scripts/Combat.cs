using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;
    public bool enter = false;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public Animator hitIndicatorAnimator;

    float nextAttackTime = 0f;
    float nextDefenseTime = 0f;
    float nextAfterDefense = 0f;

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
            for(int i = 0; i<4; i++)
            {
                GetComponent<Movement>().isGuarding[i] = false; 
            }

            GetComponent<Movement>().isInDefense = false;

            if (GetComponent<Movement>().isGuardingInDirection)
            {
                Debug.Log(GetComponent<Movement>().isGuardingInDirection);
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Debug.Log("asdfadsfa");
                    Defense();
                    GetComponent<Movement>().isGuarding[0] = true;
                    nextDefenseTime = Time.time + 0.875f;
                    GetComponent<Movement>().isGuardingInDirection = false;
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    Defense();
                    GetComponent<Movement>().isGuarding[1] = true;
                    nextDefenseTime = Time.time + 0.875f;
                    GetComponent<Movement>().isGuardingInDirection = false;
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    Defense();
                    GetComponent<Movement>().isGuarding[2] = true;
                    nextDefenseTime = Time.time + 0.875f;
                    GetComponent<Movement>().isGuardingInDirection = false;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    Defense();
                    GetComponent<Movement>().isGuarding[3] = true;
                    nextDefenseTime = Time.time + 0.875f;
                    GetComponent<Movement>().isGuardingInDirection = false;
                }
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

        StartCoroutine(CheckForEnemiesInRange(0.6f));

    }

    IEnumerator CheckForEnemiesInRange(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

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
