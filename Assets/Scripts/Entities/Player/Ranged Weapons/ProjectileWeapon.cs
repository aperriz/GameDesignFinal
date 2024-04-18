using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    int speed = 10;
    protected Vector2 direction;
    protected Animator animator;
    protected SpriteRenderer renderer;
    [SerializeField]
    float attackCooldown = 0.5f;
    protected void Update()
    {
        if (enabled)
        {
            transform.position = GameObject.Find("Player").transform.position;
        }

    }

    public void ResetAttack()
    {
        StartCoroutine(AttackCooldown());
    }

    protected void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Shoot()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = ((Vector2)transform.position - mousePos);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Instantiate(projectile, transform.position, transform.rotation);
    }

    protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        GameObject.Find("Player").GetComponent<Animator>().SetBool("Attacking", false);
        Destroy(this.gameObject);
    }
}
