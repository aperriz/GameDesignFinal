using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    BoxCollider2D collider;
    Animator animator;
    AudioSource audio;
    [SerializeField]
    AudioClip audioClip;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() != null)
        {
            animator.enabled = true;
            audio.clip = audioClip;
            audio.Play();
        }
    }
}
