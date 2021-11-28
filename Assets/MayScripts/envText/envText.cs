using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class envText : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [TextArea(3, 10)]
    [SerializeField] string inform;
    TMP_Text fadingText;
    [SerializeField] float fadeSpeed;
    [SerializeField] private bool textComplete = false;
    bool fadeComplete = true;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (textComplete == false && text.enabled == true && text.text == inform)
        {
            fadeInText(text, fadeSpeed);
            textComplete = true;
        }
        if (fadingText != null)
        {
            fadeCheck();
        }
        fadeOutCheck();
        //Debug.Log(timer);
    }

    void fadeInText(TMP_Text text, float fadeSpeed)
    {
        Debug.Log("put in text fade");
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        fadingText = text;
        fadeComplete = false;
    }

    void fadeCheck()
    {
        if (fadeComplete == false && fadingText.color.a < 1.0f)
        {
            fadingText.color = new Color(fadingText.color.r, fadingText.color.g, fadingText.color.b, fadingText.color.a + (Time.deltaTime * fadeSpeed));
            //Debug.Log(fadingText.color.a);
        }
        if (fadingText.color.a >= 1.0f)
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
            fadingText.color = new Color(fadingText.color.r, fadingText.color.g, fadingText.color.b, fadingText.color.a - (Time.deltaTime * fadeSpeed));
        }
        if (fadingText != null)
        {
            if (fadingText.color.a >= 1.0f && fadeComplete == true)
            {
                timer += Time.deltaTime;
            }
            else if (fadingText.color.a < 0 && fadeComplete == true)
            {
                text.text = "";
                //fadingText.text = "";
                fadingText = null;
                timer = 0;
                textComplete = false;
                //gameObject.SetActive(false);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //audioManager.Instance.PlaySound(audioManager.Instance.tutorialSound, audioManager.Instance.tutorialVolume);
            text.enabled = true;
            text.text = inform;
            //FindObjectOfType<checkManager>().itemList.Add(gameObject);
        }
    }
}
