using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Vector2 direction;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected SpriteRenderer renderer;
    [SerializeField]
    protected float attackCooldown = 0.5f;

    private void Update()
    {
        transform.position = transform.parent.position;
    }

    public void ResetAttack()
    {
        StartCoroutine(AttackCooldown());
    }

    protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        GameObject.Find("Player").GetComponent<Animator>().SetBool("Attacking", false);
        Destroy(this.gameObject);
    }
}
