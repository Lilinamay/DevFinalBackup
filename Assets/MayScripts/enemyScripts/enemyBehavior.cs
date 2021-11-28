using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public int enemyHealth;
    public bool added;
    public int myHealth;


    // Start is called before the first frame update
    void Start()
    {
        myHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthManage();
    }

    void HealthManage()
    {
        if(enemyHealth < myHealth)
        {
            if (added == false)
            {
                FindObjectOfType<checkManager>().itemList.Add(gameObject);
                //gameObject.GetComponent<checkManager>().itemList.Add(gameObject);
                added = true;
            }
        }
        if (enemyHealth <= 0)
        {
            Debug.Log("i am dead");
            gameObject.SetActive(false);
        }
    }
}
