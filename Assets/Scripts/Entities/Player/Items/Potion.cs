using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Potion : PlayerItem
{
    string type;
    char storage;
    [SerializeField]
    PlayerRecieveDamage playerDamage;
    [SerializeField]
    PlayerExtraStats extraStats;
    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    Sprite[] potionSprites;
    bool speedPotionCooldown = false;

    Potion(string type, char storage)
    {
        this.type = type;
        this.storage = storage;
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
                if(extraStats.defense > 10)
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
        }
    }

    private IEnumerator SpeedPotionDuration()
    {
        yield return new WaitForSeconds(15);
        playerMovement.speed /= 2;
        speedPotionCooldown = false;
    }
}
