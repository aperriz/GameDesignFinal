using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField]
    public bool hasLeftPotion = false, hasRightPotion = false, hasLeftScroll = false, hasRightScroll = false;
    public Potion leftPotion, rightPotion;
    public Scroll leftScroll, rightScroll;
    public string weaponType = "sword";

    [SerializeField]
    private InputActionReference useLeftPotion, useRightPotion, swapPotions, useLeftScroll, useRightScroll, swapScrolls;

    [SerializeField]
    Image leftPotionImage, rightPotionImage, leftScrollImage, rightScrollImage;

    public bool speedPotionCooldown = false;
    public bool immune = false;



    private void Start()
    {
        hasLeftPotion = false;
        hasRightPotion = false;
        Debug.Log(hasLeftPotion);
        Debug.Log(hasRightPotion);
        //UpdatePotions();
    }

    private void FixedUpdate()
    {
        //Debug.Log(hasLeftPotion);
        //Debug.Log(hasRightPotion);
    }

    public void UpdateGold(int change)
    {
        gold += change;
        goldText.text = gold.ToString();

    }

    private void Update()
    {
        if (useLeftPotion.action.triggered && hasLeftPotion && !(leftPotion.type == "speed" && speedPotionCooldown))
        {
            leftPotion.Drink();
            if (hasRightPotion)
            {
                leftPotion = rightPotion.Clone();
            }
            else
            {
                leftPotion = null;
            }
            rightPotion = null;
            if (leftPotion == null)
            {
                hasLeftPotion = false;
            }
            hasRightPotion = false;
            UpdatePotions();
        }

        if (useRightPotion.action.triggered && hasRightPotion && !(rightPotion.type == "speed" && speedPotionCooldown))
        {
            rightPotion.Drink();
            rightPotion = null;
            hasRightPotion = false;

            UpdatePotions();
        }

        if (swapPotions.action.triggered && hasLeftPotion && hasRightPotion)
        {
            Potion temp = leftPotion.Clone();
            leftPotion = rightPotion.Clone();
            rightPotion = temp.Clone();
            Destroy(temp);
            UpdatePotions();
        }
    }

    public void UpdatePotions()
    {

        if (hasLeftPotion)
        {
            leftPotionImage.sprite = leftPotion.sprite;
        }
        else
        {
            leftPotionImage.sprite = emptySprite;
        }

        if (hasRightPotion)
        {
            rightPotionImage.sprite = rightPotion.sprite;
        }
        else
        {
            rightPotionImage.sprite = emptySprite;
        }

        //Debug.Log("Left Potion: " + leftPotion);
        //Debug.Log("Right Potion: " + rightPotion);
    }

    public void UpdateScrolls()
    {

        if (hasLeftScroll)
        {
            leftScrollImage.sprite = leftScroll.sprite;
        }
        else
        {
            leftScrollImage.sprite = emptySprite;
        }

        if (hasRightPotion)
        {
            rightScrollImage.sprite = rightScroll.sprite;
        }
        else
        {
            rightScrollImage.sprite = emptySprite;
        }

        //Debug.Log("Left Potion: " + leftPotion);
        //Debug.Log("Right Potion: " + rightPotion);
    }
}
