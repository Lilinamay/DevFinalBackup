using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class peopleDialogue : MonoBehaviour
{
    public dialogueClass[] mydialogues;

    public dialogueClass dialogue;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] Image textboxSprite;

    //private bool onFloor = false;
    private bool haveTriggered = false;
    private bool triggered = false;
    private bool first = false;
    public bool dialogue2Complete = false;
    [SerializeField] GameObject player;


    [SerializeField] Queue<string> sentences;
    [SerializeField] Queue<string> names; //a list of strings
    //public Image endCG;

    //GameObject player;

    void Start()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();
        textboxSprite.enabled = false; //disable without dialogue
    }

    private void triggerConversation()
    {
        if (haveTriggered == false && !dialogue2Complete && GetComponent<NPCTrigger>().convTriggered)
        {
            Debug.Log("trigger conversation");
            triggered = true;
            haveTriggered = true;
            sentences.Clear();
            names.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            textboxSprite.enabled = true;

            foreach (string name in dialogue.names)
            {
                names.Enqueue(name);
            }

        }
    }



    void EndDialogue()
    {
        Debug.Log("End conversation ");// + names.Peek());

        player.GetComponent<PlayerMove>().disableMove = false;
        player.GetComponent<PlayerMove>().convMove = false;
        //avatarSprite.enabled = false;
        textboxSprite.enabled = false;
        nameText.text = "";
        dialogueText.text = "";
        names.Clear();
        sentences.Clear();
        haveTriggered = false;
        //dialogue2Complete = true;
        first = false;
        triggered = false;
        GetComponent<NPCTrigger>().listened = false;
        GetComponent<NPCTrigger>().convTriggered = false;
    }


    void Update()
    {
        if (mydialogues.Length <= plotNumber.Globals.maxPlotNum)
        {
            dialogue = mydialogues[plotNumber.Globals.plotNum];
        }
        triggerConversation();
        if (triggered)
        {
            if (first == false)
            {
                string name = names.Dequeue();
                string sentence = sentences.Dequeue();
                textboxSprite.enabled = true;   //show image
                                                //avatarSprite.gameObject.SetActive(true);
                                                //textboxSprite.gameObject.SetActive(true);
                nameText.text = name;
                dialogueText.text = sentence;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
                Debug.Log(name);
                Debug.Log(sentence);
                first = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (sentences.Count == 0)   //if queue empty, end dialogue
                {
                    StopAllCoroutines();
                    EndDialogue();
                    return;
                }

                string name2 = names.Dequeue();
                string sentence2 = sentences.Dequeue();
                dialogueText.text = sentence2;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence2));
                textboxSprite.enabled = true;   //show image
                nameText.text = name2;
                Debug.Log(sentence2);
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
