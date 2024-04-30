using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerExtraStats : MonoBehaviour
{
    public int gold = 0;
    public int defense = 0;

    [SerializeField]
    public Sprite emptySprite;
    [SerializeField]
    public TextMeshProUGUI goldText;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    public bool hasLeftPotion = false, hasRightPotion = false, hasLeftScroll = false, hasRightScroll = false;
    public Potion leftPotion, rightPotion;
    public Scroll leftScroll, rightScroll;
    public string weaponType = "sword";

    [SerializeField]
    private InputActionReference useLeftPotion, useRightPotion, swapPotions, useLeftScroll, useRightScroll, swapScrolls;

    [SerializeField]
    Image leftPotionImage, rightPotionImage, leftScrollImage, rightScrollImage;

    public bool speedPotionCooldown = false, shieldScrollCooldown = false;
    public bool immune = false;

    GameObject shield,player;

    PlayerMovement playerMovement;

    private void Start()
    {
        UpdateScrolls();
        hasLeftPotion = false;
        hasRightPotion = false;
        hasLeftScroll = false;
        hasRightScroll = false;
        player = gameObject;
        playerMovement = player.GetComponent<PlayerMovement>();
        //Debug.Log(hasLeftPotion);
        //Debug.Log(hasRightPotion);
        //UpdatePotions();
    }

    public void SetPaused(bool paused)
    {
        playerMovement.paused = paused;
    }

    private void FixedUpdate()
    {
        if(playerMovement == null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
        //Debug.Log(hasLeftPotion);
        //Debug.Log(hasRightPotion);
    }

    public void UpdateGold(int change)
    {
        gold += change;
        goldText.text = gold.ToString();

    }

    public void setDamage(int dmg)
    {
        playerMovement.baseDamage = dmg;
        GameObject.Find("AtkText").GetComponent<TextMeshProUGUI>().text = (playerMovement.baseDamage + dmg).ToString();
    }
    public void setBonusDamage(int dmg)
    {
        playerMovement.extraDamage = dmg;
        GameObject.Find("AtkText").GetComponent<TextMeshProUGUI>().text = (playerMovement.baseDamage + dmg).ToString();
    }


    public int getBonusDamage()
    {
        return playerMovement.extraDamage;
    }
    public int getBaseDamage()
    {
        return playerMovement.baseDamage;
    }

    public void UpdateDefense(int change)
    {
        defense += change;
        if(defense > 10)
        {
            defense = 10;
        }

        if(defense < 0)
        {
            defense = 0;
        }
        TextMeshProUGUI defenseText = GameObject.Find("Defense Text").GetComponent<TextMeshProUGUI>() as TextMeshProUGUI;
        defenseText.text = defense.ToString();
    }

    private void Update()
    {
        PotionInput();
        ScrollInput();
    }

    public void ShieldCoroutine()
    {
        if(animator.GetBool("Invincible") == false)
        {
            shield = Instantiate(Resources.Load("Prefabs/Shield Spell") as GameObject, new Vector3(player.transform.position.x, player.transform.position.y, -10), Quaternion.identity, player.transform);
            animator.SetBool("Invincible", true);
            StartCoroutine(ShieldDuration());
        }
    }

    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("Invincible", false);
        Destroy(shield);
    }

    private void PotionInput()
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

    private void ScrollInput()
    {
        if (useLeftScroll.action.triggered && hasLeftScroll && !(leftScroll.type == "shield" && animator.GetBool("Invincible")))
        {
            leftScroll.Cast();
            if (hasRightScroll)
            {
                leftScroll = rightScroll.Clone();
            }
            else
            {
                leftScroll = null;
            }
            rightScroll = null;
            if (leftScroll == null)
            {
                hasLeftScroll = false;
            }
            hasRightScroll = false;
            UpdateScrolls();
        }

        if (useRightScroll.action.triggered && hasRightScroll && !(rightScroll.type == "shield" && animator.GetBool("Invincible")))
        {
            rightScroll.Cast();
            rightScroll = null;
            hasRightScroll = false;

            UpdateScrolls();
        }

        if (swapScrolls.action.triggered && hasLeftScroll && hasRightScroll)
        {
            Scroll temp = leftScroll.Clone();
            leftScroll = rightScroll.Clone();
            rightScroll = temp.Clone();
            Destroy(temp);
            UpdateScrolls();
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

        if (hasRightScroll)
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
