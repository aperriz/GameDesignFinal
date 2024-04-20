using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D collider;
    [SerializeField]
    Animator animator;
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioClip audioClip;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            animator.enabled = true;
            audio.clip = audioClip;
            audio.Play();
            Destroy(collider);
            SpawnItems();
        }
    }


    private void SpawnItems()
    {
        new Potion(new Vector2(transform.position.x + Random.Range(-2, 2), transform.position.y), "defense");
    }

}
