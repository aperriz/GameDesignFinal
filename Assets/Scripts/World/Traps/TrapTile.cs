using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTile : Trap
{
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void DestroySelf()
    {
        if(Application.isPlaying)
        {
            Destroy(this);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (!activated && collision.TryGetComponent<PlayerRecieveDamage>(out PlayerRecieveDamage player))
        {
            activated = true;
            animator.SetBool("Activated", true);
            player.DealDamage(damage);
        }
        else if (!activated && collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            activated = true;
            animator.SetBool("Activated", true);
            enemy.DealDamage(damage);
        }
    }

    public void TrapReset()
    {
        animator.SetBool("Activated", false);
        StartCoroutine(TrapDelay());
    }

    private IEnumerator TrapDelay()
    {
        yield return new WaitForSeconds(5);
        activated = false;
    }
}
