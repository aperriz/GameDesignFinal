using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItem : MonoBehaviour
{
    [SerializeField]
    protected BoxCollider2D collider;
    [SerializeField]
    GameObject popupPrefab;
    protected GameObject popup;
    [SerializeField]
    protected InputActionReference pickup;
    [SerializeField]
    private float speedOfHeightChange = 0.0025f;

    [SerializeField]
    public SpriteRenderer renderer;

    protected float startHeight;
    protected float maxHeight;
    protected bool movingUp = true;
    protected bool allowPickup = false;

    private void Awake()
    {
        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.enabled = true;
        collider.isTrigger = true;
        collider.size = new Vector2(transform.localScale.x, transform.localScale.y);
        startHeight = transform.position.y;
        maxHeight = transform.position.y + .5f;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            allowPickup = true;
            if (popup != null)
            {
                Destroy(popup);
            }
            popup = Instantiate(popupPrefab, new Vector3(transform.position.x, transform.position.y + 2.5f, -2), Quaternion.identity);
            popup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        allowPickup = false;
        Destroy(popup);
    }

    protected void PickupItem()
    {
        if (allowPickup)
        {
            Destroy(gameObject);
            Debug.Log("Picked up!");
            Destroy(pickup);
        }
    }

    private void FixedUpdate()
    {
        SpinItemOnGround();

        if (pickup.action.IsPressed())
        {
            PickupItem();
        }
    }

    protected void SpinItemOnGround()
    {
        if(transform.position.y <= maxHeight && movingUp)
        {
            //Debug.Log("Moving up");
            transform.position += new Vector3(0, speedOfHeightChange, 0);
        }
        else if(transform.position.y >= maxHeight && movingUp)
        {
            movingUp = false;
        }
        else if (!movingUp)
        {
            //Debug.Log("Moving down");
            transform.position -= new Vector3(0, speedOfHeightChange, 0);
            if (transform.position.y <= startHeight)
            {
                movingUp = true;
            }
        }

    }
}