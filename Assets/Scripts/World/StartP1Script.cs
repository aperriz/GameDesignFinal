using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartP1Script : MonoBehaviour
{
    Camera camera;
    GameObject mainCamera;
    [SerializeField]
    string nameText;
    [SerializeField]
    UnityEvent onTriggerEnter;
    PlayerMovement pInput;

    bool invokedDialog = false;
    bool triggered = false;

    private void Start()
    {
        pInput = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && !triggered)
        {
            triggered = true;

            pInput.paused = true;

            Destroy(GetComponent<BoxCollider2D>());
            GameObject.Find("Player").transform.GetChild(1).gameObject.SetActive(false);
            Debug.Log("Starting Fight");
            //Time.timeScale = 0;
            mainCamera = GameObject.Find("Main Camera");
            camera = mainCamera.GetComponent<Camera>();

            StartCoroutine(CameraChange());

            onTriggerEnter?.Invoke();
        }
    }

    IEnumerator CameraChange()
    {
        StartCoroutine(CameraMovement());
        StartCoroutine(CameraZoom());
        yield return null;

    }

    IEnumerator CameraMovement()
    {
        while (camera.orthographicSize < 20)
        {
            camera.orthographicSize += .02f;
            yield return null;
        }

    }
    IEnumerator CameraZoom()
    {
        while (mainCamera.transform.position.y < 0)
        {
            mainCamera.transform.position = new Vector3(0, mainCamera.transform.position.y + 0.1f, -15);
            yield return null;
            
        }
    }

    public void StartFight()
    {
        GameObject ui = GameObject.Find("Player").transform.GetChild(1).gameObject;
        ui.SetActive(true);
        GameObject healthBar = ui.transform.GetChild(1).gameObject;
        healthBar.SetActive(true);
        healthBar.GetComponentInChildren<TextMeshProUGUI>().text = nameText;
        healthBar.GetComponentInChildren<Slider>().value = 1;

        Debug.Log("Starting Fight");
        GameObject boss = GameObject.Find("Phase 1").transform.GetChild(1).gameObject;
        boss.GetComponentInChildren<Phase1>().enabled = true;
        boss.GetComponentInChildren<PolygonCollider2D>().enabled = true;
        //boss.GetComponent<Phase1>().enabled = true;
        Time.timeScale = 1.0f;
        pInput.paused = false;
        camera.orthographicSize = 13;
        camera.transform.localPosition = new Vector3(0, 0, -10);
        Destroy(gameObject);
    }
}
