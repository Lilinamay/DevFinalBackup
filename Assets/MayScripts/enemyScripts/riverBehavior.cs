using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riverBehavior : MonoBehaviour
{
    public Transform sendBack;
    int playerHealth;
    [SerializeField] GameObject player;
    public GameObject freezeScreen;
    SpriteRenderer playerRenderer;
    bool respawnBack;
    Rigidbody2D mybody;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<playerHealth>().playerHealthstat;
        playerRenderer = player.GetComponent<SpriteRenderer>();
        respawnBack = player.GetComponent<playerHealth>().respawnBack;
        mybody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("myhealth"+player.GetComponent<playerHealth>().playerHealthstat);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            //playerHealth--;
            
            
            playerRenderer.color = Color.red;
            mybody.velocity = Vector3.zero;
            StartCoroutine(changeColor());
            freezeScreen.GetComponent<freezeScreen>().stop();

        }
    }

    IEnumerator changeColor()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        playerRenderer.color = Color.white;
        if (playerHealth >1 )
        {
            player.transform.position = sendBack.position;
        }
        player.GetComponent<playerHealth>().playerHealthstat--;
        mybody.velocity = Vector3.zero;

    }
}
