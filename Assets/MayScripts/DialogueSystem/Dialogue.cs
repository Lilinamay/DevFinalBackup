using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    //[SerializeField] Dialogue dialogue;
    [TextArea(3, 10)]
    [SerializeField] string[] Text;
    //[SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;
    //[SerializeField] Image avatarSprite;
    [SerializeField] Image textboxSprite;

    //private bool onFloor = false;
    private bool haveTriggered = false;
    private bool triggered = false;
    private bool first = false;
    public bool dialogue2Complete = false;
    [SerializeField] GameObject player;

  
    [SerializeField] Queue<string> sentences;
    //[SerializeField] Queue<string> names; //a list of strings
    //[SerializeField] Queue<Sprite> avatars;
    //[SerializeField] Queue<Sprite> textboxs;

    //GameObject player;

    void Start()
    {
        //names = new Queue<string>();
        sentences = new Queue<string>();
        //avatars = new Queue<Sprite>();
        //textboxs = new Queue<Sprite>();

        //avatarSprite.enabled = false;
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
            //names.Clear();

            foreach (string sentence in Text)
            {
                sentences.Enqueue(sentence);
            }

            textboxSprite.enabled = true;

            /*foreach (string name in dialogue.names)
            {
                names.Enqueue(name);
            }

            foreach (Sprite avatar in dialogue.avatars)
            {
                avatars.Enqueue(avatar);
            }

            foreach (Sprite textbox in dialogue.textboxs)
            {
                textboxs.Enqueue(textbox);
            }
            
            Debug.Log("Trigger conversation " + names.Peek());
            */

        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            triggered = false;
            EndDialogue();
        }
    }*/

    void EndDialogue()
    {
        Debug.Log("End conversation ");// + names.Peek());

        player.GetComponent<PlayerMove>().disableMove = false;
        player.GetComponent<PlayerMove>().convMove = false;
        //avatarSprite.enabled = false;
        textboxSprite.enabled = false;
        //nameText.text = "";
        dialogueText.text = "";
        //avatarSprite.GetComponent<Image>().sprite = null;
        //textboxSprite.GetComponent<Image>().sprite = null;  //clear dialogue stuff after dialogue
        //names.Clear();
        sentences.Clear();
        //avatars.Clear();
        //textboxs.Clear();

        //FindObjectOfType<playerMove>().speed = 7;
        //FindObjectOfType<playerMove>().jumpHeight = FindObjectOfType<playerMove>().jumpheightInput;     //unfreeze player
        //FindObjectOfType<playerMove>().canFlip = true;
        haveTriggered = false;
        //dialogue2Complete = true;
        first = false;
        triggered = false;
        GetComponent<NPCTrigger>().listened = false;
        GetComponent<NPCTrigger>().convTriggered = false;

    }


    void Update()
    {

        triggerConversation();
        if (triggered)
        {
            if (first == false)
            {
                 //freeze player during dialogue


                //string name = names.Dequeue();
                string sentence = sentences.Dequeue();
                //Sprite avatar = avatars.Dequeue();
                //Sprite textbox = textboxs.Dequeue();    //go down list and put into a sprite/string
                //avatarSprite.enabled = true;
                textboxSprite.enabled = true;   //show image
                                                //avatarSprite.gameObject.SetActive(true);
                                                //textboxSprite.gameObject.SetActive(true);
                //nameText.text = name;
                dialogueText.text = sentence;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
                //avatarSprite.GetComponent<Image>().sprite = avatar;
                //textboxSprite.GetComponent<Image>().sprite = textbox;   //input sprite/string onto placeholders in canvas
                Debug.Log(name);
                Debug.Log(sentence);
                first = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                //FindObjectOfType<playerMove>().speed = 0;    //freeze player during dialogue
                //FindObjectOfType<playerMove>().jumpHeight = 0;
                if (sentences.Count == 0)   //if queue empty, end dialogue
                {
                    StopAllCoroutines();
                    EndDialogue();
                    return;
                }
               
                //string name2 = names.Dequeue();
                string sentence2 = sentences.Dequeue();
                dialogueText.text = sentence2;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence2));
                //Sprite avatar2 = avatars.Dequeue();
                //Sprite textbox2 = textboxs.Dequeue();    //go down list and put into a sprite/string
                //avatarSprite.enabled = true;
                textboxSprite.enabled = true;   //show image
                //avatarSprite.gameObject.SetActive(true);
                //textboxSprite.gameObject.SetActive(true);
                //nameText.text = name2;
                
                //avatarSprite.GetComponent<Image>().sprite = avatar2;
                //textboxSprite.GetComponent<Image>().sprite = textbox2;   //input sprite/string onto placeholders in canvas
                //Debug.Log(name2);
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

