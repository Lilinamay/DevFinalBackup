using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeChair : MonoBehaviour
{
    public bool comeHome = false;

    public int option = 0;
    GameObject newOptions;
    public GameObject optionObject;
    GameObject newOptionsUI;
    public GameObject optionObjectUI;
    public GameObject fluteManager;

    public Animator PlayerAnimator;

    [SerializeField] GameObject player;
    //[SerializeField] GameObject chair;
    Rigidbody2D playerBody;
    checkManager checkmanager;
    HomeTrans hometrans;
    [SerializeField] GameObject homeMang;
    public Animator BlackAnimator;

    bool Come = false;
    bool check = false;
    //GameObject collObject;
    GameObject newChecker;
    bool triggerH = false;
    public bool checkered = false;
    public GameObject checkObject;

    public bool saveRecord = false;
    //bool optionshow = false;
    public bool saved = false;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = player.GetComponent<Rigidbody2D>();
        hometrans = homeMang.GetComponent<HomeTrans>();
        checkmanager = player.GetComponent<checkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (comeHome)
        {
            createOptions(0.3f);   //0.5
            comeHome = false;
            Come = true;
        }

        options();

        if (check)
        {
            if (!triggerH)
            {
                Invoke("sit", 1);
                triggerH = true;
            }

        }

        checkerKey();
    }

    private void createOptions(float upDis) { 

            PlayerAnimator.SetBool("isSitting", true);
            PlayerAnimator.SetBool("isStanding", false);
            PlayerAnimator.SetBool("isWalking", false);
            player.GetComponent<PlayerMove>().disableMove = true;
            player.GetComponent<playerEnergy>().disableEner = true;
            //player sit
            Debug.Log("new options");
            option = 1;
            newOptions = Instantiate(optionObject, gameObject.transform.position, gameObject.transform.rotation);
            newOptions.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 6f); ///local position relative to checkpoint
            newOptionsUI = Instantiate(optionObjectUI, gameObject.transform.position, gameObject.transform.rotation);
            newOptionsUI.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 7.1f); ///local position relative to checkpoint
            PlayerMove.Globals.ApplyV = false;
            player.transform.position = new Vector3(transform.position.x, transform.position.y + upDis);
            //mybody.constraints = RigidbodyConstraints2D.FreezePositionY;

            playerBody.gravityScale = 0.0f;

    }

    private void options()
    {
        if (option == 0)
        {
            if (newOptions != null)
            {
                Destroy(newOptions);
                Destroy(newOptionsUI);
            }
        }
        if (option != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (option == 1)
                {
                    BlackAnimator.SetTrigger("isBlackOut");
                    Debug.Log("go under, change scene");
                    fluteManager.SetActive(false);
                    if (!checkmanager.first)
                    {
                        option = 0;
                        StartCoroutine(waitForTrans());
                        Come = false;
                    }


                }
                if (option == 2)
                {
                    Debug.Log("play flute");
                    fluteManager.SetActive(true);
                    PlayerAnimator.SetBool("isFlute", true);
                    PlayerAnimator.SetBool("isSitting", false);
                }
                if (option == 3)
                {
                    Debug.Log("leave");
                    player.GetComponent<PlayerMove>().disableMove = false;
                    player.GetComponent<playerEnergy>().disableEner = false;
                    //duringRes = false;
                    option = 0;

                    fluteManager.SetActive(false);
                    PlayerMove.Globals.ApplyV = true;
                    playerBody.gravityScale = 4.5f;
                    //transform.position = new Vector3(transform.position.x, transform.position.y - 1);
                    PlayerAnimator.SetBool("isFlute", false);
                    PlayerAnimator.SetBool("isSitting", false);
                    PlayerAnimator.SetBool("isStanding", true);
                    Come = false;

                }
            }

            if (option < 3 && Input.GetKeyDown(KeyCode.DownArrow))
            {
                option++;
                newOptionsUI.transform.position = new Vector3(newOptionsUI.transform.position.x, newOptionsUI.transform.position.y - 1.05f);
            }
            if (option > 1 && Input.GetKeyDown(KeyCode.UpArrow))
            {
                option--;
                newOptionsUI.transform.position = new Vector3(newOptionsUI.transform.position.x, newOptionsUI.transform.position.y + 1.05f);
            }
        }
    }

    IEnumerator waitForTrans()
    {
        yield return new WaitForSecondsRealtime(1);
        hometrans.goUnder();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !Come)
        {
            Debug.Log("checkpoint");
            check = true;
            //collObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            check = false;
            triggerH = false;
            checkered = false;
            saveRecord = false;
            saved = false;
            //if (!duringRes)
            //{
                option = 0;
            //}
            //GetComponent<playerHealth>().respawnBack = false;
            if (newChecker != null)
            {
                Destroy(newChecker);
            }

        }

    }

    private void sit()
    {
        if (check && !checkered && !Come)
        {
            Debug.Log("checked");
            //Debug.Log(collObject.transform.position);
            newChecker = Instantiate(checkObject, transform.position, transform.rotation);
            //newChecker.transform.SetParent(gameObject.transform);
            newChecker.transform.localPosition = new Vector3(transform.position.x, transform.position.y + 5f); ///local position relative to checkpoint
            checkered = true;
        }
    }

    private void checkerKey()
    {
        if (checkered && option == 0)
        {
            if (Input.GetKey(KeyCode.Space) && !saved)
            {
                saved = true;
                Destroy(newChecker);
                createOptions(0.5f);
            }
        }
    }
}
