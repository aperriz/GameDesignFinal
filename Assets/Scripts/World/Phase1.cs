using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Phase1 : MonoBehaviour
{
    int loops = 0;
    [SerializeField]
    GameObject paperProjectile;
    GameObject player;
    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject batPrefab;

    [SerializeField]
    public float atk1cd, atk2cd, initcd;

    private void OnEnable()
    {
        animator.SetInteger("Attack", 0);
        player = GameObject.Find("Player");
        StartCoroutine(FirstAtkCD());
    }

    IEnumerator FirstAtkCD()
    {
        yield return new WaitForSeconds(initcd);

        StartCoroutine(Attack1CD());

    }

    private IEnumerator Attack1CD()
    {
        yield return new WaitForSeconds(atk1cd);
        loops++;
        if (loops < 3)
        {
            //set it so attack happens on animation finish with cd
            animator.SetInteger("Attack", 1);
        }
        else
        {
            StartCoroutine(Attack2CD());
            loops = 0;
        }
    }
    private IEnumerator Attack2CD()
    {
        yield return new WaitForSeconds(atk2cd);
        animator.SetInteger("Attack", 2);
    }

    private void Attack1()
    {
        Debug.Log("Attack 1");
        Vector2 dir = (player.transform.position - transform.position);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Debug.Log(angle);

        int roll = Random.Range(1, 4);
        int angleChange = 45 / roll;
        Debug.Log(roll * 2 + 1);
        for(int i = 0; i < roll*2+1; i++)
        {
            //Debug.Log("paper");
            Instantiate(paperProjectile, new Vector3(transform.position.x, transform.position.y , -9), Quaternion.Euler(0, 0, angle - 90 + (angleChange * i)), transform);
        }
    }

    private void Attack2()
    {
        //12, 7
        Debug.Log("Attack 2");
        Instantiate(batPrefab, new Vector3(12, 7, -6), Quaternion.identity, transform);
        Instantiate(batPrefab, new Vector3(-12, 7, -6), Quaternion.identity, transform);
        Instantiate(batPrefab, new Vector3(12, -7, -6), Quaternion.identity, transform);
        Instantiate(batPrefab, new Vector3(-12, -7, -6), Quaternion.identity, transform);
    }

    private void ResetAttack()
    {
        StartCoroutine(AttackRestCD());
    }

    IEnumerator AttackRestCD()
    {
        yield return new WaitForSeconds(1);
        animator.SetInteger("Attack", 0);//starts new coroutine/cooldown
        StartCoroutine(Attack1CD());
    }

}
