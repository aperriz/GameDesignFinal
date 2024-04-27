using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePillar : MonoBehaviour
{
    PolygonCollider2D col;
    Animator animator;
    [SerializeField]
    int damage;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            collision.GetComponent<PlayerRecieveDamage>().DealDamage(damage);
            col.enabled = false;
        }
    }

    public void DealDamage()
    {
        col.enabled = true;
    }

    public void AnimationEnd()
    {
        Destroy(gameObject);
    }
}
