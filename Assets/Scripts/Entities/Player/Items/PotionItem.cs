using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PotionItem : PlayerItem
{
    public string type;
    [SerializeField]
    PlayerRecieveDamage playerDamage;
    [SerializeField]
    PlayerExtraStats extraStats;
    [SerializeField]
    PlayerMovement playerMovement;

    GameObject potionPrefab;

    [SerializeField]
    public Sprite[] potionSprites;
    bool speedPotionCooldown = false;


    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerDamage = player.GetComponent<PlayerRecieveDamage>();
        extraStats = player.GetComponent<PlayerExtraStats>();
        playerMovement = player.GetComponent<PlayerMovement>();

        switch (type)
        {
            case "health":
                renderer.sprite = potionSprites[0];
                break;
            case "defense":
                renderer.sprite = potionSprites[1];
                break;
            case "speed":
                renderer.sprite = potionSprites[2];
                break;
            default:
                Debug.LogError("Invalid Potion Type");
                break;
        }
    }
    public void Drink()
    {
        switch (type)
        {
            case "health":
                playerDamage.HealPlayer(20);
                break;
            case "defense":
                extraStats.defense += 2;
                if (extraStats.defense > 10)
                {
                    extraStats.defense = 10;
                }
                break;
            case "speed":
                if (!speedPotionCooldown)
                {
                    playerMovement.speed *= 2;
                    speedPotionCooldown = true;
                }
                break;
            default:
                Debug.LogError("Invalid Potion Type");
                break;
        }
    }

    override protected void PickupItem()
    {
        if (allowPickup)
        {
            if (!extraStats.hasLeftPotion)
            {
                Debug.Log("Filled Left");
                extraStats.leftPotion = this;
                extraStats.hasLeftPotion = true;
            }
            else if (!extraStats.hasRightPotion)
            {
                Debug.Log("Filled Right");
                extraStats.rightPotion = this;
                extraStats.hasRightPotion = true;
            }
            else
            {
                Debug.Log("Replaced Left");
                GameObject newPotion = Instantiate(Resources.Load("Prefabs/World/Potion Prefab") as GameObject, transform.position, Quaternion.identity);
                newPotion.GetComponent<Potion>().type = extraStats.leftPotion.type;
                extraStats.leftPotion = this;
                Debug.Log(extraStats.leftPotion);
            }
            extraStats.UpdatePotions();
            Destroy(gameObject);
        }
    }

    private IEnumerator SpeedPotionDuration()
    {
        yield return new WaitForSeconds(15);
        playerMovement.speed /= 2;
        speedPotionCooldown = false;
    }

}
