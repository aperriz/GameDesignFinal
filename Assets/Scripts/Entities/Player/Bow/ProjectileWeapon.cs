using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    int speed = 10;
    private Vector2 direction;
    private Animator animator;
    private SpriteRenderer renderer;
    [SerializeField]
    float attackCooldown = 0.5f;
    private void Update()
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

    private void Start()
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

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        GameObject.Find("Player").GetComponent<Animator>().SetBool("Attacking", false);
        Destroy(this.gameObject);
    }
}
