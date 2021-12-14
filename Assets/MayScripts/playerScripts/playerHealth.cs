using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public int playerHealthstat;
    public int myHealth;
    public bool respawn = false;
    public bool respawned = false;
    public bool respawnBack = false;
    public float invinsibleTimer = 0;
    public float invinsibleT = 0;

    public bool dead = false;


    public Image health1;
    public Image health2;
    public Image health3;
    public Image health4;
    public float lostA = 0.3f;

    public GameObject freezeScreen;

    SpriteRenderer myRenderer;
    Rigidbody2D mybody;


    // Start is called before the first frame update
    void Start()
    {
        myHealth = playerHealthstat;
        myRenderer = GetComponent<SpriteRenderer>();
        mybody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        healthUI();
        Debug.Log("timer" + invinsibleTimer);
        if (playerHealthstat <= 0)              //death
        {
            respawn = true;
            //respawned = true;
            respawnBack = true;
            mybody.velocity = Vector3.zero;

        }


        if(invinsibleTimer > 0)
        {
            invinsibleTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            if (invinsibleTimer <= 0)
            {
                playerHealthstat--;                                             //-health, cameraShake maybe?
                invinsibleTimer = invinsibleT;
                myRenderer.color = Color.red;
                StartCoroutine(changeColor());
                freezeScreen.GetComponent<freezeScreen>().stop();               //if -- health, freeze, then screen shake
                if (GetComponent<PlayerMove>().faceR)
                {
                    gameObject.transform.position = new Vector3(transform.position.x - 1, transform.position.y);
                    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x+1, collision.gameObject.transform.position.y);
                }
                else
                {
                    gameObject.transform.position = new Vector3(transform.position.x + 1, transform.position.y);
                    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x - 1, collision.gameObject.transform.position.y);
                    //gameObject.transform.localPosition = new Vector3(1, 0, 0);
                    //collision.gameObject.transform.localPosition = new Vector3(-1, 0, 0);
                }
                //Debug.Log("timer" + invinsibleTimer);
            }
        }

        if (collision.tag == "arrow")
        {
            if (invinsibleTimer <= 0)
            {
                playerHealthstat--;                                             //-health, cameraShake maybe?
                invinsibleTimer = invinsibleT;
                myRenderer.color = Color.red;
                StartCoroutine(changeColor());
                freezeScreen.GetComponent<freezeScreen>().stop();               //if -- health, freeze, then screen shake
                //if (GetComponent<PlayerMove>().faceR)
                //{
                //    gameObject.transform.position = new Vector3(transform.position.x - 1, transform.position.y);
                //    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + 1, collision.gameObject.transform.position.y);
                //}
                //else
                //{
                //    gameObject.transform.position = new Vector3(transform.position.x + 1, transform.position.y);
                //    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x - 1, collision.gameObject.transform.position.y);
                //    //gameObject.transform.localPosition = new Vector3(1, 0, 0);
                //    //collision.gameObject.transform.localPosition = new Vector3(-1, 0, 0);
                //}
                //Debug.Log("timer" + invinsibleTimer);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            //invinsibleTimer = 0;
        }
    }

    void healthUI()
    {
        
        if (playerHealthstat == 0 || dead)
        {
            health1.color = new Color(health1.color.r, health1.color.g, health1.color.b, lostA);
            health2.color = new Color(health2.color.r, health2.color.g, health2.color.b, lostA);
            health3.color = new Color(health3.color.r, health3.color.g, health3.color.b, lostA);
            health4.color = new Color(health4.color.r, health4.color.g, health4.color.b, lostA);
        }
        if (playerHealthstat == 1)
        {
            health1.color = new Color(health1.color.r, health1.color.g, health1.color.b, 1);
            health2.color = new Color(health2.color.r, health2.color.g, health2.color.b, lostA);
            health3.color = new Color(health3.color.r, health3.color.g, health3.color.b, lostA);
            health4.color = new Color(health4.color.r, health4.color.g, health4.color.b, lostA);
        }
        if (playerHealthstat == 2)
        {
            health1.color = new Color(health1.color.r, health1.color.g, health1.color.b, 1);
            health2.color = new Color(health2.color.r, health2.color.g, health2.color.b, 1);
            health3.color = new Color(health3.color.r, health3.color.g, health3.color.b, lostA);
            health4.color = new Color(health4.color.r, health4.color.g, health4.color.b, lostA);
        }
        if (playerHealthstat == 3)
        {
            health1.color = new Color(health1.color.r, health1.color.g, health1.color.b, 1);
            health2.color = new Color(health2.color.r, health2.color.g, health2.color.b, 1);
            health3.color = new Color(health3.color.r, health3.color.g, health3.color.b, 1);
            health4.color = new Color(health4.color.r, health4.color.g, health4.color.b, lostA);
        }
        if (playerHealthstat == 4 && !dead)
        {
            health1.color = new Color(health1.color.r, health1.color.g, health1.color.b, 1);
            health2.color = new Color(health2.color.r, health2.color.g, health2.color.b, 1);
            health3.color = new Color(health3.color.r, health3.color.g, health3.color.b, 1);
            health4.color = new Color(health4.color.r, health4.color.g, health4.color.b, 1);
        }
    }

    IEnumerator changeColor()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        myRenderer.color = Color.white;

    }

}
