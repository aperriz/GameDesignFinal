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
    protected GameObject popupPrefab;
    protected GameObject popup;
    [SerializeField]
    protected InputActionReference pickup;
    [SerializeField]
    private float speedOfHeightChange = 0.01f;

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
        popupPrefab = Resources.Load("Prefabs/World/Popup Prefab") as GameObject;
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
        if(collision.name == "Player")
        {
            allowPickup = false;
            Destroy(popup);
        }
    }

    protected virtual void PickupItem()
    {
        if (allowPickup)
        {
            Destroy(gameObject);
            Debug.Log("Picked up!");
            Destroy(pickup);
        }
    }

    private void Update()
    {
        if (pickup.action.triggered)
        {
            PickupItem();
        }
    }
}