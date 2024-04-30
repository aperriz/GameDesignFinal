using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : PlayerItem
{
    public string type;

    public PlayerRecieveDamage playerDamage;

    public PlayerExtraStats extraStats;

    public AgentMover playerMovement;

    public int bonusDamage = 0;

    [SerializeField]
    public Sprite[] weaponSprites;

    public Vector3 locationToSpawn;
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
            //Debug.Log(nameText);
            nameText.text = type;

            switch (type)
            {
                case "Sword":
                    descText.text = (5 + bonusDamage).ToString() + " Damage\nModerate Attack Speed";
                    break;
                case "Bow":
                    descText.text = (3 + bonusDamage).ToString() + " Damage\nFast Attack Speed";
                    break;
                case "Staff":
                    descText.text = (8 + bonusDamage).ToString() + " Damage\nSlow Attack Speed";
                    break;
            }

            if (bonusDamage > 0)
            {
                nameText.text = "+" + bonusDamage + " " + type;
            }

        }

    }

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (playerDamage == null)
        {
            playerDamage = player.GetComponent<PlayerRecieveDamage>();
        }
        if (extraStats == null)
        {
            extraStats = player.GetComponent<PlayerExtraStats>();
        }
        if (playerMovement == null)
        {
            playerMovement = player.GetComponent<AgentMover>();
        }

        switch (type)
        {
            case "Sword":
                renderer.sprite = weaponSprites[0];
                break;
            case "Bow":
                renderer.sprite = weaponSprites[1];
                break;
            case "Staff":
                renderer.sprite = weaponSprites[2];
                break;
            default:
                Debug.LogError("Invalid Weapon Type");
                break;
        }
    }

    override protected void PickupItem()
    {
        if(allowPickup)
        {
            locationToSpawn = new Vector3(transform.position.x, transform.position.y, -1);

            GameObject newWeapon = Instantiate(Resources.Load("Prefabs/World/WeaponItem Prefab") as GameObject, transform.position, Quaternion.identity);
            newWeapon.transform.position = locationToSpawn;

            WeaponItem newItemComp = newWeapon.GetComponent<WeaponItem>();
            newItemComp.type = extraStats.weaponType;
            newItemComp.bonusDamage = extraStats.getBonusDamage();
            newItemComp.weaponSprites = weaponSprites;

            extraStats.weaponType = type;

            switch (type)
            {
                case "Sword":
                    extraStats.setDamage(5);
                    break;
                case "Bow":
                    extraStats.setDamage(3);
                    break;
                case "Staff":
                    extraStats.setDamage(8);
                    break;
                default:
                    Debug.LogError("Invalid Weapon Type");
                    break;
            }

            extraStats.setBonusDamage(bonusDamage);

            GameObject.Find("Weapon Image").GetComponent<Image>().sprite = renderer.sprite;

            GameObject test = Resources.Load("Prefabs/World/WeaponItem Prefab") as GameObject;

            Destroy(gameObject);
        }
    }
}
