using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackout : MonoBehaviour
{
    [SerializeField] Image black;
    
    [SerializeField] float fadeSpeed;
    [SerializeField] private bool textComplete = false;
    bool fadeComplete = true;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

        black.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (textComplete == false && black.enabled == true)
        {
            fadeInText(black, fadeSpeed);
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
        if (fadeComplete == false && black.color.a < 1.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a + (Time.deltaTime * fadeSpeed));
            //Debug.Log(fadingText.color.a);
        }
        if (black.color.a >= 1.0f)
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
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a - (Time.deltaTime * fadeSpeed));
        }
        //if (fadingText != null)
        {
            if (black.color.a >= 1.0f && fadeComplete == true)
            {
                timer += Time.deltaTime;
            }
            else if (black.color.a < 0 && fadeComplete == true)
            {
                //text.text = "";
                //fadingText.text = "";
                //fadingText = null;
                timer = 0;
                textComplete = false;
                black.enabled = false;
                //gameObject.SetActive(false);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //audioManager.Instance.PlaySound(audioManager.Instance.tutorialSound, audioManager.Instance.tutorialVolume);
            black.enabled = true;
            //text.text = inform;
            //FindObjectOfType<checkManager>().itemList.Add(gameObject);
        }
    }
}
