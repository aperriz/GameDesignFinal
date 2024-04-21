using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Potion : ScriptableObject
{
    public string type;
    public Sprite sprite;
    private PlayerExtraStats extraStats;
    private PlayerRecieveDamage playerDamage;
    private AgentMover playerMovement;

    public void Awake()
    {
        GameObject player = GameObject.Find("Player");
        extraStats = player.GetComponent<PlayerExtraStats>();
        playerMovement = player.GetComponent<AgentMover>();
        playerDamage = player.GetComponent<PlayerRecieveDamage>();
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
                if (!extraStats.speedPotionCooldown)
                {
                    Debug.Log("Gotta go fast");
                    playerMovement.maxSpeed *= 1.5f;
                    extraStats.speedPotionCooldown = true;
                    playerMovement.SpeedPotionCoroutine();
                }
                break;
            default:
                Debug.LogError("Invalid Potion Type");
                break;
        }
        extraStats.UpdatePotions();

    }

    public Potion Clone()
    {
        Potion clone = ScriptableObject.CreateInstance<Potion>() as Potion;
        clone.type = type;
        clone.sprite = sprite;
        return clone;
    }

    public Potion(string type, Sprite sprite)
    {
        this.type = type;
        this.sprite = sprite;
    }

}
