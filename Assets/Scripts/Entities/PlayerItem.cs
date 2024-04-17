using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class PlayerItem : MonoBehaviour
{
    protected BoxCollider2D collider;

    private void Awake()
    {
        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.enabled = true;
        collider.isTrigger = true;
        collider.size = new Vector2(transform.localScale.x, transform.localScale.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }



}