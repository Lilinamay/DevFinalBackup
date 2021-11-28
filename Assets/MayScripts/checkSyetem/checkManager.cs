using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkManager : MonoBehaviour
{
    public float SaveX;
    public float SaveY;
    //int SaveSparkle;
    //int SaveBullet;
    bool check = false;
    //GameObject[] itemList;
    //GameObject items;
    public List<GameObject> itemList;
    GameObject collObject;
    GameObject newChecker;
    bool triggerH = false;
    public bool checkered = false;
    public GameObject checkObject;

    public bool saveRecord = false;
    //bool optionshow = false;
    public bool saved = false;

    bool first = true;

    public int option = 0;
    GameObject newOptions;
    public GameObject optionObject;
    GameObject newOptionsUI;
    public GameObject optionObjectUI;
    public GameObject fluteManager;
    // Start is called before the first frame update
    void Start()
    {
        //SaveBullet = FindObjectOfType<playerShoot>().bulletCount;
        //SaveSparkle = 0;
        SaveX = transform.position.x;
        SaveY = transform.position.y;

        itemList = new List<GameObject>();

        fluteManager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            if (!triggerH)
            {
                Invoke("sit", 1);
                triggerH = true;
            }

        }

        options();
        checkerKey();
        respawnOptions();



        if (saved)
        {
            if (!saveRecord)
            { 
            SaveX = transform.position.x;
            SaveY = transform.position.y;
            //SaveSparkle = FindObjectOfType<playerInteract>().starCount;
            //SaveBullet = FindObjectOfType<playerShoot>().bulletCount;
            Debug.Log("progress saved");
            //check = false;
            itemList.Clear();
            saveRecord = true;
            //saved = false;
            }
        }


        if (GetComponent<playerHealth>().respawn == true)
        {
            transform.position = new Vector3(SaveX, SaveY);
            Debug.Log(new Vector3(SaveX, SaveY));                                                               //add anything that need to be reset after respawn
            GetComponent<playerHealth>().playerHealthstat = GetComponent<playerHealth>().myHealth;
            GetComponent<playerHealth>().invinsibleTimer = GetComponent<playerHealth>().invinsibleT;
            //FindObjectOfType<playerInteract>().starCount = SaveSparkle;
            //FindObjectOfType<playerShoot>().bulletCount = SaveBullet;
            foreach (GameObject item in itemList)
            {
                item.SetActive(true);
                if (item.tag == "enemy")
                {
                    item.GetComponent<enemyBehavior>().added = false;
                    item.GetComponent<enemyBehavior>().enemyHealth = item.GetComponent<enemyBehavior>().myHealth;
                    /*if (item.name == "movingEnemy")
                    {
                        item.GetComponent<enemyBehavior>().enemyHealth = 1;
                    }*/
                }
            }
            GetComponent<playerHealth>().respawn = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "checkpoint" && !GetComponent<playerHealth>().respawnBack)
        {
            Debug.Log("checkpoint");
            check = true;
            collObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "checkpoint")
        {
            check = false;
            triggerH = false;
            checkered = false;
            saveRecord = false;
            saved = false;
            option = 0;
            GetComponent<playerHealth>().respawnBack = false;
            if (newChecker != null)
            {
                Destroy(newChecker);
            }
        }
        if(collision.gameObject.name == "firstCheck")
        {
            check = false;
            triggerH = false;
            checkered = false;
            saveRecord = false;
        }
    }

    private void sit()
    {
        if (check &&!checkered)
        {
            Debug.Log("checked");
            //Debug.Log(collObject.transform.position);
            newChecker = Instantiate(checkObject, collObject.transform.position, collObject.transform.rotation);
            //newChecker.transform.SetParent(gameObject.transform);
            newChecker.transform.localPosition = new Vector3(collObject.transform.position.x, collObject.transform.position.y+5f); ///local position relative to checkpoint
            checkered = true;
        }
    }

    private void checkerKey()
    {
        if (checkered && option ==0)
        {
            if (Input.GetKey(KeyCode.Space) && !saved)
            {
                first = false;
                saved = true;
                Destroy(newChecker);
                createOptions();
            }
        }
    }

    private void respawnOptions()
    {
        if (GetComponent<playerHealth>().respawned)
        {
            createOptions();
            GetComponent<playerHealth>().respawned = false;
            
        }
    }
    
    private void createOptions()
    {
        if (!first)
        {
            GetComponent<PlayerMove>().disableMove = true;
            GetComponent<playerEnergy>().disableEner = true;
            //player sit
            Debug.Log("new options");
            option = 1;
            newOptions = Instantiate(optionObject, collObject.transform.position, collObject.transform.rotation);
            newOptions.transform.localPosition = new Vector3(collObject.transform.position.x, collObject.transform.position.y + 6f); ///local position relative to checkpoint
            newOptionsUI = Instantiate(optionObjectUI, collObject.transform.position, collObject.transform.rotation);
            newOptionsUI.transform.localPosition = new Vector3(collObject.transform.position.x, collObject.transform.position.y + 7.72f); ///local position relative to checkpoint
            //create new object
        }

    }

    private void options()
    {
        if(option == 0)
        {
            if (newOptions != null)
            {
                Destroy(newOptions);
                Destroy(newOptionsUI);
            }
        }
        if(option != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(option == 1)
                {
                    Debug.Log("go home, change scene");
                    fluteManager.SetActive(false);
                }
                if (option == 2)
                {
                    Debug.Log("play flute");
                    fluteManager.SetActive(true);
                }
                if (option == 3)
                {
                    Debug.Log("leave");
                    GetComponent<PlayerMove>().disableMove = false;
                    GetComponent<playerEnergy>().disableEner = false;
                    option = 0;
                    fluteManager.SetActive(false);
                }
            }

            if(option<3 && Input.GetKeyDown(KeyCode.DownArrow))
            {
                option++;
                newOptionsUI.transform.position = new Vector3(newOptionsUI.transform.position.x, newOptionsUI.transform.position.y - 1.65f);
            }
            if(option >1 && Input.GetKeyDown(KeyCode.UpArrow))
            {
                option--;
                newOptionsUI.transform.position  = new Vector3(newOptionsUI.transform.position.x, newOptionsUI.transform.position.y+ 1.65f);
            }
        }
    }



}
