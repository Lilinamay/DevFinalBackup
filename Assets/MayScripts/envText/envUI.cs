using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class envUI : MonoBehaviour
{
    [SerializeField] Image topUI;
    [SerializeField] Image botUI;
    [SerializeField] float fadeSpeed;
    [SerializeField] private bool textComplete = false;
    bool fadeComplete = true;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

        topUI.enabled = false;
        botUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (textComplete == false && topUI.enabled == true)
        {
            fadeInText(topUI, fadeSpeed);
            fadeInText(botUI, fadeSpeed);
            textComplete = true;
        }
        //if (fadingText != null)
        {
            fadeCheck();
        }
        fadeOutCheck();
        //Debug.Log(timer);
    }

    void fadeInText(Image image, float fadeSpeed)
    {
        Debug.Log("put in text fade");
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        //fadingText = text;
        fadeComplete = false;
    }

    void fadeCheck()
    {
        if (fadeComplete == false && topUI.color.a < 1.0f)
        {
            topUI.color = new Color(topUI.color.r, topUI.color.g, topUI.color.b, topUI.color.a + (Time.deltaTime * fadeSpeed));
            botUI.color = new Color(botUI.color.r, botUI.color.g, botUI.color.b, botUI.color.a + (Time.deltaTime * fadeSpeed));
            //Debug.Log(fadingText.color.a);
        }
        if (topUI.color.a >= 1.0f)
        {
            fadeComplete = true;
            //fadingText = null;
        }
    }

    void fadeOutCheck()
    {
        if (timer >= 2f)
        {
            Debug.Log("fade text");
            topUI.color = new Color(topUI.color.r, topUI.color.g, topUI.color.b, topUI.color.a - (Time.deltaTime * fadeSpeed));
            botUI.color = new Color(botUI.color.r, botUI.color.g, botUI.color.b, botUI.color.a - (Time.deltaTime * fadeSpeed));
        }
        //if (fadingText != null)
        {
            if (topUI.color.a >= 1.0f && fadeComplete == true)
            {
                timer += Time.deltaTime;
            }
            else if (topUI.color.a < 0 && fadeComplete == true)
            {
                //text.text = "";
                //fadingText.text = "";
                //fadingText = null;
                timer = 0;
                textComplete = false;
                topUI.enabled = false;
                botUI.enabled = false;
                //gameObject.SetActive(false);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //audioManager.Instance.PlaySound(audioManager.Instance.tutorialSound, audioManager.Instance.tutorialVolume);
            topUI.enabled = true;
            botUI.enabled = true;
            //text.text = inform;
            //FindObjectOfType<checkManager>().itemList.Add(gameObject);
        }
    }
}
