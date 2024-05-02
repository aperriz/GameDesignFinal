using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueBox;
    [SerializeField]
    TextMeshProUGUI dialogueText;
    [SerializeField]
    string[] startSentences, deathSentences;
    Queue<string> dialogueQueue = new Queue<string>();
    [SerializeField]
    UnityEvent dialogueEnd, fightEnd;
    bool invoked = false, dead = false;
    Button contButton;
    [SerializeField]
    Sprite deathSprite;

    private void Start()
    {
        
    }

    public void StartDialogue()
    {
        contButton = GetComponentInChildren<Button>();
        foreach (string st in startSentences)
        {
            dialogueQueue.Enqueue(st);
        }

        invoked = true;
        dialogueBox.SetActive(true);
        GameObject.Find("Player").transform.GetChild(1).gameObject.SetActive(false);

        DisplayNextSentence();

    }

    public void DeathDialogue()
    {
        contButton = GetComponentInChildren<Button>();
        dialogueQueue.Clear();
        foreach(string st in deathSentences)
        {
            dialogueQueue.Enqueue(st);
        }

        dialogueBox.SetActive(true);

        GameObject.Find("Player").transform.GetChild(1).gameObject.SetActive(false);
        dialogueBox.transform.GetChild(2).GetComponent<Image>().sprite = deathSprite;
        dead = true;
        DisplayNextSentence();
        
        foreach(var enemy in FindObjectsOfType<BatScript>())
        {
            enemy.DealDamage(1000);
        }
    }

    public void DisplayNextSentence()
    {
        contButton.enabled = false;
        if (dialogueQueue.Count == 0)
        {
            dialogueBox.SetActive(false);
            if (!dead)
            {
                dialogueEnd?.Invoke();
            }
            else
            {
                fightEnd?.Invoke();
                GameObject.Find("Player").transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else
        {
            string sentence = dialogueQueue.Dequeue();
            Debug.Log(sentence);
            StartCoroutine(TypeSentence(sentence));
        }


    }


    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char c in sentence)
        {
            dialogueText.text += c;

            if(dialogueText.text == sentence)
            {
                contButton.enabled = true;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
