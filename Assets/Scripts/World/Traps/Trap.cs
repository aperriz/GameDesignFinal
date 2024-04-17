using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField]
    protected int damage = 5;

    protected Animator animator;
    protected bool activated;

    protected void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void DestroySelf()
    {
        if (Application.isPlaying)
        {
            Destroy(this);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
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

    protected IEnumerator TrapDelay()
    {
        yield return new WaitForSeconds(5);
        activated = false;
    }
}
