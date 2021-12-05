using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riverBehavior : MonoBehaviour
{
    public Transform sendBack;
    int playerHealth;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<playerHealth>().playerHealthstat;
        
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
            player.GetComponent<playerHealth>().playerHealthstat--;
            collision.gameObject.transform.position = sendBack.position;
            
        }
    }
}
