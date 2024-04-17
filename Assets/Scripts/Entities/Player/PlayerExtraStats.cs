using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerExtraStats : MonoBehaviour
{
    int gold = 0;
    public TextMeshProUGUI goldText;

    public void UpdateGold(int change)
    {
        gold += change;
        goldText.text = gold.ToString();

    }
}
