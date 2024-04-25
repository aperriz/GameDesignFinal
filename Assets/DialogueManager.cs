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
    string[] sentences;
    Queue<string> dialogueQueue = new Queue<string>();
    [SerializeField]
    UnityEvent dialogueEnd;
    bool invoked = false;

    private void Start()
    {
        
    }

    public void StartDialogue()
    {
        foreach (string st in sentences)
        {
            dialogueQueue.Enqueue(st);
        }

        invoked = true;
        dialogueBox.SetActive(true);
        GameObject.Find("Player").transform.GetChild(1).gameObject.SetActive(false);

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {

        if (dialogueQueue.Count == 0)
        {
            dialogueBox.SetActive(false);
            dialogueEnd?.Invoke();
        }
        else
        {
            string sentence = dialogueQueue.Dequeue();

            StartCoroutine(TypeSentence(sentence));
        }


    }


    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return null;
        }
    }
}
