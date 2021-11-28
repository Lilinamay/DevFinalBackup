using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    bool listen = false;
    bool listenH = false;
    public bool listened = false;
    public GameObject listenObject;
    public bool convTriggered = false;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!listenH && listen)
        {
            Invoke("listenNPC", 1);
            listenH = true;
        }

        if (listened)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                convTriggered = true;
                Debug.Log("convTriggered");
                player.GetComponent<PlayerMove>().disableMove = true;
                player.GetComponent<PlayerMove>().convMove = true;
                player.GetComponent<PlayerMove>().npcPosX  = transform.position.x;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            listen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            listen = false;
            listenH = false;
            if (!convTriggered)
            {
                listened = false;
            }
        }
    }

    private void listenNPC()
    {
        if (listen)
        {
            Debug.Log("listen");
            GameObject newListener = Instantiate(listenObject, transform.position, transform.rotation);
            newListener.transform.SetParent(gameObject.transform);
            newListener.transform.localPosition = new Vector3( 0f, 0.9f); ///local position relative to player
            listened = true;
        }
    }
}
