using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
    
public class PlayerExtraStats : MonoBehaviour
{
    int gold = 0;
    public int defense = 0;

    [SerializeField]
    public Sprite emptySprite;
    [SerializeField]
    public TextMeshProUGUI goldText;

    [SerializeField]
    public bool hasLeftPotion = false, hasRightPotion = false, hasLeftScroll = false, hasRightScroll = false;
    public PotionItem leftPotion, rightPotion;
    public Scroll leftScroll, rightScroll;
    public string weaponType = "sword";

    [SerializeField]
    Image leftPotionImage, rightPotionImage, leftScrollImage, rightScrollImage;

    private void Start()
    {
        UpdatePotions();
    }

    public void UpdateGold(int change)
    {
        gold += change;
        goldText.text = gold.ToString();

    }

    public void UpdatePotions()
    {
        if(hasLeftPotion)
        {
            switch (leftPotion.type)
            {
                case "health":
                    leftPotionImage.sprite = leftPotion.potionSprites[0];
                    break;
                case "defense":
                    leftPotionImage.sprite = leftPotion.potionSprites[1];
                    break;
                case "speed":
                    leftPotionImage.sprite = leftPotion.potionSprites[2];
                    break;
                default:
                    Debug.LogError("Invalid Potion Type");
                    break;
            }
        }
        else
        {
            leftPotionImage.sprite = emptySprite;
        }

        if(hasRightPotion)
        {
            switch (rightPotion.type)
            {
                case "health":
                    rightPotionImage.sprite = rightPotion.potionSprites[0];
                    break;
                case "defense":
                    rightPotionImage.sprite = rightPotion.potionSprites[1];
                    break;
                case "speed":
                    rightPotionImage.sprite = rightPotion.potionSprites[2];
                    break;
                default:
                    Debug.LogError("Invalid Potion Type");
                    break;
            }
        }
        else
        {
            rightPotionImage.sprite = emptySprite;
        }
    }
}
