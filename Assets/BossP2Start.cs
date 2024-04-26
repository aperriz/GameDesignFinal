using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossP2Start : MonoBehaviour
{
    [HideInInspector]
    public GameObject player, bossHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.transform.position = new Vector3(8, -41, -10);
        bossHealthBar = player.transform.GetChild(1).GetChild(1).gameObject;
        bossHealthBar.SetActive(false);
        bossHealthBar.transform.GetChild(2).GetComponent<Image>().sprite = GameObject.Find("Boss").transform.GetComponentInChildren<SpriteRenderer>().sprite;
        bossHealthBar.GetComponent<Slider>().value = 1;
    }
}
