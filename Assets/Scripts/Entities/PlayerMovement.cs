using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Animator animator;
    private SpriteRenderer renderer;
    public GameObject weaponObject;

    private void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        TakeInput();
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        SetAnimatorMovement(direction);
    }

    private async void TakeInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            if (!animator.GetBool("Flipped"))
            {
                renderer.flipX = false;
                animator.SetBool("Flipped", true);

            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            if (animator.GetBool("Flipped"))
            {
                renderer.flipX = false;
                animator.SetBool("Flipped", false);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!animator.GetBool("Attacking"))
            {
                animator.SetBool("Attacking", true);
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 myPos = transform.position;
                Vector2 direction = (mousePos - myPos).normalized;
                GameObject attackObject = Instantiate(weaponObject, transform.position, Quaternion.identity);
                attackObject.SetActive(true);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Debug.Log(angle);

                if (angle > 90 || angle < -90)
                {
                    animator.SetBool("Flipped", true);
                    renderer.flipX = true;
                    attackObject.GetComponent<SpriteRenderer>().flipY = true;
                    //attackObject.GetComponent<SpriteRenderer>().flipX = true;
                    attackObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                else
                {
                    renderer.flipX = false;
                    attackObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                }


            }
        }

    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetFloat("yDir", direction.y);
        animator.SetFloat("xDir", direction.x);
    }

    private void ResetAttack()
    {
        animator.SetBool("Attacking", false);

    }

}
