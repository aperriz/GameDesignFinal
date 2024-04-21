using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
    
public class PlayerExtraStats : MonoBehaviour
{
    int gold = 0;
    public int defense = 0;

    [SerializeField]
    public Sprite emptySprite;
    [SerializeField]
    public TextMeshProUGUI goldText;

    //[SerializeField]
    public bool hasLeftPotion = false, hasRightPotion = false, hasLeftScroll = false, hasRightScroll = false;
    public Potion leftPotion, rightPotion;
    public Scroll leftScroll, rightScroll;
    public string weaponType = "sword";

    [SerializeField]
    private InputActionReference useLeftPotion, useRightPotion, swapPotions;

    [SerializeField]
    Image leftPotionImage, rightPotionImage, leftScrollImage, rightScrollImage;

    public bool speedPotionCooldown = false;



    /*private void Start()
    {
        hasLeftPotion = false;
        hasRightPotion = false;
        Debug.Log(hasLeftPotion);
        Debug.Log(hasRightPotion);
        //UpdatePotions();
    }

    private void FixedUpdate()
    {    
        *//*Debug.Log(hasLeftPotion);
        Debug.Log(hasRightPotion);*//*
    }

    public void UpdateGold(int change)
    {
        gold += change;
        goldText.text = gold.ToString();

    }

    private void Update()
    {
        if (useLeftPotion.action.IsPressed() && hasLeftPotion)
        {
            leftPotion.Drink();
            leftPotion = new PotionItem(rightPotion);
            rightPotion = null;
            if(leftPotion == null)
            {
                hasLeftPotion = false;
            }
            hasRightPotion = false;
            UpdatePotions();
        }

        if (useRightPotion.action.IsPressed() && hasRightPotion)
        {
            rightPotion.Drink();
            rightPotion = null;
            hasRightPotion = false;

            UpdatePotions();
        }

        if(swapPotions.action.IsPressed() && hasLeftPotion && hasRightPotion)
        {
            PotionItem temp = leftPotion;
            leftPotion = rightPotion;
            rightPotion = temp;
            Destroy(temp);
            UpdatePotions();
        }
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
                    hasLeftPotion = false;
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
                    hasRightPotion = false;
                    break;
            }
        }
        else
        {
            rightPotionImage.sprite = emptySprite;
        }

        //Debug.Log("Left Potion: " + leftPotion);
        //Debug.Log("Right Potion: " + rightPotion);
    }*/
}
