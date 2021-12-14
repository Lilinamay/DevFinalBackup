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
            Invoke("listenNPC", 0.3f);
            listenH = true;
        }

        if (listened)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Audiomanager.Instance.PlaySound(Audiomanager.Instance.dialogSound, Audiomanager.Instance.dialogVolume);
                convTriggered = true;
                Debug.Log("convTriggered");
                player.GetComponent<PlayerMove>().disableMove = true;
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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
            //newListener.transform.SetParent(gameObject.transform);
            //newListener.transform.localPosition = new Vector3( 0f, 5f); ///local position relative to player
            newListener.transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y + 4);
            listened = true;
        }
    }
}
