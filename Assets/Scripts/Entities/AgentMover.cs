using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AgentMover : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField]
    public float maxSpeed = 2, acceleration = 50, decelleration = 100;
    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }
    PlayerMovement playerMovement;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (playerMovement.paused)
        {
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            if (MovementInput.magnitude > 0 && currentSpeed >= 0)
            {
                oldMovementInput = MovementInput;
                currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed -= decelleration * maxSpeed * Time.deltaTime;
            }

            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            rb2d.velocity = oldMovementInput * currentSpeed;
        }
    }


    public void SpeedPotionCoroutine()
    {
        StartCoroutine(SpeedPotion());
    }

    private IEnumerator SpeedPotion()
    {
        yield return new WaitForSeconds(30);
        maxSpeed /= 2;
    }
}
