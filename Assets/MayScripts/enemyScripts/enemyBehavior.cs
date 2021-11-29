using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public int enemyHealth;
    public bool added;
    public int myHealth;
    public float attackedTimer = 0;
    public float rangedTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        myHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthManage();
        if (attackedTimer > 0)
        {
            attackedTimer -= Time.deltaTime;
        }
        if (rangedTimer > 0)
        {
            rangedTimer -= Time.deltaTime;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "meleeBox")
        {
            if(attackedTimer <= 0)
            {
                enemyHealth--;
                attackedTimer = 0.3F;
            }
        }

        if(collision.gameObject.tag == "bullet")
        {
            if(rangedTimer <= 0)
            {
                Debug.Log("range attacked");
                enemyHealth--;
                rangedTimer = 0.5f;
            }
        }
    }
}
