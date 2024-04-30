using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossP2Start : MonoBehaviour
{
    [HideInInspector]
    public GameObject player, bossHealthBar;
    PlayerMovement pInput;
    [SerializeField]
    GameObject dialogue, boss;

    // Start is called before the first frame update
    void Start()
    {
        pInput = GameObject.Find("Player").GetComponent<PlayerMovement>();
        pInput.paused = true;
        boss = GameObject.Find("Boss");

        player = GameObject.Find("Player");
        player.transform.position = new Vector3(8, -41, -10);

        bossHealthBar = player.transform.GetChild(1).GetChild(1).gameObject;
        bossHealthBar.SetActive(false);
        bossHealthBar.transform.GetChild(2).GetComponent<Image>().sprite = GameObject.Find("Boss").transform.GetComponentInChildren<SpriteRenderer>().sprite;
        bossHealthBar.GetComponent<Slider>().value = 1;

        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueManager>().StartDialogue();
        dialogue.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("Boss").GetComponentInChildren<SpriteRenderer>().sprite;

        GameObject.Find("Player").transform.GetChild(1).gameObject.SetActive(false);
        Debug.Log("Starting Fight");

        boss.GetComponentInChildren<Phase2>().gameObject.SetActive(false);
    }

    public void StartFight()
    {
        Time.timeScale = 1;
        bossHealthBar.SetActive(true);
        dialogue.SetActive(false);
        pInput.paused=false;
        boss.transform.GetChild(0).GetComponent<Phase2>().gameObject.SetActive(true);
        player.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void GameEnd()
    {
        Camera.main.transform.position = Vector3.zero;
        Camera.main.orthographicSize = 20;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Destroy(boss.GetComponent<Phase2>());
    }
}
