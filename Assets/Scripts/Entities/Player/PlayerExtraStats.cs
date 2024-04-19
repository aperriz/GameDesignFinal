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
    public TextMeshProUGUI goldText;

    Potion leftPotion, rightPotion;
    Scroll leftScroll, rightScroll;
    public string weaponType = "sword";

    [SerializeField]
    Image leftPotionImage, righPotionImage, leftScrollImage, rightScrollImage;

    public void UpdateGold(int change)
    {
        gold += change;
        goldText.text = gold.ToString();

    }
}
