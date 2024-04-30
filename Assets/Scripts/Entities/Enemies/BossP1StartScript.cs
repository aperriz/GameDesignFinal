using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossP1StartScript : MonoBehaviour
{
    [SerializeField]
    GameObject bossHealthBar;
    GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        player.transform.position = new Vector3(0, -30, -10);

        GameObject boss = GameObject.Find("Phase 1").transform.GetChild(1).gameObject;
        boss.GetComponentInChildren<Phase1>().enabled = false;
        boss.GetComponentInChildren<PolygonCollider2D>().enabled = false;
        GameObject loadingScreen = GameObject.Find("Loading Screen");
        for (int i = 0; i < loadingScreen.transform.childCount; i++)
        {
            loadingScreen.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
