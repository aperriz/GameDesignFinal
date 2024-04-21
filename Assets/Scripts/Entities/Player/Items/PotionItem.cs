using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class PotionItem : PlayerItem
{
    public string type;
    
    PlayerRecieveDamage playerDamage;
    
    PlayerExtraStats extraStats;
   
    PlayerMovement playerMovement;

    GameObject potionPrefab;

    [SerializeField]
    public Sprite[] potionSprites;

    public PotionItem(PotionItem other)
    {
        type = other.type;
        playerDamage = other.playerDamage;
        extraStats = other.extraStats;
        playerMovement = other.playerMovement;
        potionPrefab = other.potionPrefab;
        potionSprites = other.potionSprites;

    }

    private void Start()
    {
        if(playerDamage == null)
        {
            GameObject player = GameObject.Find("Player");
            playerDamage = player.GetComponent<PlayerRecieveDamage>();
            extraStats = player.GetComponent<PlayerExtraStats>();
            playerMovement = player.GetComponent<PlayerMovement>();
        }

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
    /*public void Drink()
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
                if (!extraStats.speedPotionCooldown)
                {
                    playerMovement.speed *= 2;
                    extraStats.speedPotionCooldown = true;
                }
                break;
            default:
                Debug.LogError("Invalid Potion Type");
                break;
        }
        extraStats.UpdatePotions();

    }*/

    override protected void PickupItem()
    {
        
        if (allowPickup)
        {
            //Debug.Log(this);
            Debug.Log(extraStats.hasLeftPotion);
            Debug.Log(extraStats.hasRightPotion);

            if (!extraStats.hasLeftPotion)
            {
                Debug.Log("Filled Left");
                //extraStats.leftPotion = this;
                extraStats.hasLeftPotion = true;


            }
            else if (!extraStats.hasRightPotion)
            {
                Debug.Log("Filled Right");
                //extraStats.rightPotion = this;
                extraStats.hasRightPotion = true;
                
            }
            else
            {
                /*Debug.Log("Replaced Left");
                GameObject newPotion = Instantiate(Resources.Load("Prefabs/World/Potion Prefab") as GameObject, transform.position, Quaternion.identity);
                newPotion.GetComponent<PotionItem>().type = extraStats.leftPotion.type;
                extraStats.leftPotion = this;
                */
            }
            //extraStats.UpdatePotions();
            Destroy(renderer);
            Destroy(collider);
        }
    }

    private IEnumerator SpeedPotionDuration()
    {
        yield return new WaitForSeconds(15);
        playerMovement.speed /= 2;
        extraStats.speedPotionCooldown = false;

        Destroy(gameObject);
    }

}
