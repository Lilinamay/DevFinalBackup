using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public bool upgrade = false;
    public GameObject meleeBox;
    GameObject newMelee;
    Rigidbody2D myBody;
    public float timer = 0;
    bool countTime = false;
    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        meleeBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!upgrade)
        {
            if (Input.GetKey(KeyCode.X))
            {
                if(!meleeBox.activeSelf)
                {
                    Debug.Log("attackbox");
                    meleeBox.SetActive(true);
                    countTime = true;
                    
                }
            }
            if(timer >= 1 && meleeBox.activeSelf)
            {
                meleeBox.SetActive(false);
                timer = 0;
                countTime = false;
            }
        }

        if (countTime)
        {
            timer += Time.deltaTime;
        }
    }
}
