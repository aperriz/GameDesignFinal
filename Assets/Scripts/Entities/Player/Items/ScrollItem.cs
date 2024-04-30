using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScrollItem : PlayerItem
{
    public string type;

    PlayerRecieveDamage playerDamage;

    PlayerExtraStats extraStats;

    AgentMover playerMovement;

    GameObject scrollPrefab;

    [SerializeField]
    public Sprite[] scrollSprites;

    public ScrollItem(ScrollItem other)
    {
        type = other.type;
        playerDamage = other.playerDamage;
        extraStats = other.extraStats;
        playerMovement = other.playerMovement;
        scrollPrefab = other.scrollPrefab;
        scrollSprites = other.scrollSprites;

    }

    new private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            allowPickup = true;
            if (popup != null)
            {
                Destroy(popup);
            }
            popup = Instantiate(popupPrefab, new Vector3(transform.position.x, transform.position.y + 2.5f, -12), Quaternion.identity);
            popup.SetActive(true);
            TextMeshProUGUI nameText = popup.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descText = popup.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            Debug.Log(nameText);
            nameText.text = "Scroll of " + Char.ToUpper(type[0]).ToString();

            for (int i = 1; i < type.Length; i++)
            {
                nameText.text = nameText.text + type[i].ToString();
            }

            switch (type)
            {

                case "Fire Storm":
                    descText.text = "Summons 3 small zones of flame on the cursor dealing 5 damage to enemies within them";
                    break;
                case "Shield":
                    descText.text = "Makes the player invincible for 3 seconds";
                    break;
                case "Holy Radiance":
                    descText.text = "Summons a circle of holy flame on the cursor, dealing 10 damage to enemies in the area";
                    break;
                default:
                    descText.text = "Invalid scroll type!";
                    break;
            }
        }

    }

    private void Start()
    {
        if (playerDamage == null)
        {
            GameObject player = GameObject.Find("Player");
            playerDamage = player.GetComponent<PlayerRecieveDamage>();
            extraStats = player.GetComponent<PlayerExtraStats>();
            playerMovement = player.GetComponent<AgentMover>();
        }

        switch (type)
        {
            case "Fire Storm":
                renderer.sprite = scrollSprites[0];
                break;
            case "Shield":
                renderer.sprite = scrollSprites[1];
                break;
            case "Holy Radiance":
                renderer.sprite = scrollSprites[2];
                break;
            default:
                Debug.LogError("Invalid Scroll Type");
                break;
        }
    }

    override protected void PickupItem()
    {

        if (allowPickup)
        {
            if (!extraStats.hasLeftScroll)
            {
                Debug.Log("Filled Left Scroll");
                extraStats.leftScroll = ScriptableObject.CreateInstance<Scroll>() as Scroll;
                extraStats.leftScroll.type = type;
                extraStats.leftScroll.sprite = renderer.sprite;
                extraStats.hasLeftScroll = true;


            }
            else if (!extraStats.hasRightScroll)
            {
                Debug.Log("Filled Right Scroll");
                extraStats.rightScroll = ScriptableObject.CreateInstance<Scroll>() as Scroll;
                extraStats.rightScroll.type = type;
                extraStats.rightScroll.sprite = renderer.sprite;
                extraStats.hasRightScroll = true;

            }
            else
            {
                Debug.Log("Replaced Left Scroll");
                GameObject newScroll = Instantiate(Resources.Load("Prefabs/World/Scroll Prefab") as GameObject, transform.position, Quaternion.identity);
                newScroll.GetComponent<ScrollItem>().type = extraStats.leftScroll.type;
                extraStats.leftScroll = ScriptableObject.CreateInstance<Scroll>() as Scroll;
                extraStats.leftScroll.type = type;
                extraStats.leftScroll.sprite = renderer.sprite;

            }
            extraStats.UpdateScrolls();
            Destroy(gameObject);
        }
    }

}
