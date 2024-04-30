using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PotionItem : PlayerItem
{
    public string type;
    
    PlayerRecieveDamage playerDamage;
    
    PlayerExtraStats extraStats;
   
    AgentMover playerMovement;

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

    new private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "Player")
        {
            allowPickup = true;
            if (popup != null)
            {
                Destroy(popup);
            }
            popup = Instantiate(popupPrefab, new Vector3(transform.position.x, transform.position.y + 2.5f, -2), Quaternion.identity);
            popup.SetActive(true);
            TextMeshProUGUI nameText = popup.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descText = popup.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            //Debug.Log(nameText);
            nameText.text = Char.ToUpper(type[0]).ToString();
            
            for(int i = 1; i < type.Length; i++)
            {
                nameText.text = nameText.text + type[i].ToString();
            }

            nameText.text = nameText.text + " Potion";

            switch (type)
            {
                case "health":
                    descText.text = "Restores 20 health";
                    break;
                case "defense":
                    descText.text = "Increases defense by 2 (Max 10)";
                    break;
                case "speed":
                    descText.text = "Increases speed by 50% for 30 seconds";
                    break;
                default:
                    descText.text = "Invalid potion type!";
                    break;
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

    override protected void PickupItem()
    {
        
        if (allowPickup)
        {
            //Debug.Log(this);
            //Debug.Log(extraStats.hasLeftPotion);
            //Debug.Log(extraStats.hasRightPotion);

            if (!extraStats.hasLeftPotion)
            {
                Debug.Log("Filled Left");
                extraStats.leftPotion = ScriptableObject.CreateInstance<Potion>() as Potion;
                extraStats.leftPotion.type = type;
                extraStats.leftPotion.sprite = renderer.sprite;
                extraStats.hasLeftPotion = true;


            }
            else if (!extraStats.hasRightPotion)
            {
                Debug.Log("Filled Right");
                extraStats.rightPotion = ScriptableObject.CreateInstance<Potion>() as Potion;
                extraStats.rightPotion.type = type;
                extraStats.rightPotion.sprite = renderer.sprite;
                extraStats.hasRightPotion = true;
                
            }
            else
            {
                Debug.Log("Replaced Left");
                GameObject newPotion = Instantiate(Resources.Load("Prefabs/World/Potion Prefab") as GameObject, transform.position, Quaternion.identity);
                newPotion.GetComponent<PotionItem>().type = extraStats.leftPotion.type;
                
                extraStats.leftPotion = ScriptableObject.CreateInstance<Potion>() as Potion;
                extraStats.leftPotion.type = type;
                extraStats.leftPotion.sprite = renderer.sprite;
                
            }
            extraStats.UpdatePotions();
            Destroy(gameObject);
        }
    }

    private void SpeedPotionDuration()
    {
        StartCoroutine(SpeedPot());
    }

    public IEnumerator SpeedPot()
    {
        yield return new WaitForSeconds(15);
        playerMovement.maxSpeed /= 1.5f;
        extraStats.speedPotionCooldown = false;

    }

}
