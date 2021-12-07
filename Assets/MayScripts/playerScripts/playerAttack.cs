using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public bool upgrade = false;
    public GameObject meleeBox;
    //GameObject newMelee;
    Rigidbody2D myBody;
    public float timer = 0;
    public float holdTime;
    bool countTime = false;
    float distanceX;
    float distanceY;
    bool startA;
    //float chargeTimer = 0;
    SpriteRenderer meleeRenderer;
    playerEnergy playeren;

    public Animator meleeAnim;
    public Animator playerAnim;
    public AudioSource playerAudio;

    public float rotation;
    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        meleeBox.SetActive(false);
        meleeRenderer = meleeBox.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceX = gameObject.transform.position.x - meleeBox.transform.position.x;
        distanceY = gameObject.transform.position.y - meleeBox.transform.position.y;

            if (Input.GetKeyDown(KeyCode.X))
            {
                if(!meleeBox.activeSelf && timer == 0)
                {
                    Debug.Log("attackbox");
                    meleeBox.SetActive(true);
                    meleeAnim.SetTrigger("meleeAttack");
                    playerAnim.SetTrigger("meleeAttack");
                playerAudio.Play();
                    countTime = true;
                    
                }
            }
            if(timer >= holdTime && meleeBox.activeSelf)
            {
                meleeBox.SetActive(false);
                timer = 0;
                countTime = false;
            }



        meleeBoxBehav();

        if (countTime)
        {
            timer += Time.deltaTime;
        }
    }

    void meleeBoxBehav()
    {
        if (meleeBox.activeSelf)
        {
            if (!startA)
            {
                if (GetComponent<PlayerMove>().faceR)
                {
                    meleeRenderer.flipX = false;
                    if (PlayerMove.Globals.playerY == -1)
                    {
                        meleeBox.transform.localPosition = new Vector3(2, -1, 0);
                        meleeBox.transform.rotation = Quaternion.Euler(0, 0, -rotation);
                        Debug.Log("rightattackDown");
                        startA = true;
                    }
                    if (PlayerMove.Globals.playerY == 0)
                    {
                        meleeBox.transform.localPosition = new Vector3(2, 0, 0);
                        meleeBox.transform.rotation = Quaternion.Euler(0, 0, 0);
                        Debug.Log("rightattack");
                        startA = true;
                    }
                    if (PlayerMove.Globals.playerY == 1)
                    {
                        meleeBox.transform.localPosition = new Vector3(2, 1, 0);
                        meleeBox.transform.rotation = Quaternion.Euler(0, 0, rotation);
                        Debug.Log("rightattackup");
                        startA = true;
                    }
                }
                else if (GetComponent<PlayerMove>().faceL)
                {
                    meleeRenderer.flipX = true;
                    if (PlayerMove.Globals.playerY == -1)
                    {
                        meleeBox.transform.localPosition = new Vector3(-2, -1, 0);
                        meleeBox.transform.rotation = Quaternion.Euler(0, 0, rotation);
                        Debug.Log("LEFTattackDown");
                        startA = true;
                    }
                    if (PlayerMove.Globals.playerY == 0)
                    {
                        meleeBox.transform.localPosition = new Vector3(-2, 0, 0);
                        meleeBox.transform.rotation = Quaternion.Euler(0, 0, 0);
                        Debug.Log("LEFTattack");
                        startA = true;
                    }
                    if (PlayerMove.Globals.playerY == 1)
                    {
                        meleeBox.transform.localPosition = new Vector3(-2, 1, 0);
                        meleeBox.transform.rotation = Quaternion.Euler(0, 0, -rotation);
                        Debug.Log("LEFTattackup");
                        startA = true;
                    }
                }
            }
            
        }
        if (!meleeBox.activeSelf)
        {
            meleeBox.transform.position = gameObject.transform.position;
            meleeBox.transform.rotation = Quaternion.Euler(0, 0, 0);
            startA = false;
            //Debug.Log("noattack");
        }
    }
}
