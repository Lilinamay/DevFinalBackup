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
    SpriteRenderer myRenderer;
    public GameObject player;
    playerEnergy pEnergy;
    public bool getHit;

    // Start is called before the first frame update
    void Start()
    {
        myHealth = enemyHealth;
        myRenderer = GetComponent<SpriteRenderer>();
        pEnergy = player.GetComponent<playerEnergy>();
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
                getHit = true;
                enemyHealth--;
                attackedTimer = 0.3F;
                if (pEnergy.energy < 1)
                {
                    pEnergy.energy += 0.1f;
                }
                if (player.GetComponent<PlayerMove>().faceR)
                {
                    player.transform.position = new Vector3(player.transform.position.x - 0.1f, player.transform.position.y);
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.7f, gameObject.transform.position.y);
                }
                else
                {
                    player.transform.position = new Vector3(player.transform.position.x + 0.1f, player.transform.position.y);
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.7f, gameObject.transform.position.y);
                    //gameObject.transform.localPosition = new Vector3(1, 0, 0);
                    //collision.gameObject.transform.localPosition = new Vector3(-1, 0, 0);
                }
                myRenderer.color = Color.red;
                StartCoroutine(changeColor());
            }
        }

        if(collision.gameObject.tag == "bullet")
        {
            if(rangedTimer <= 0)
            {
                getHit = true;
                Debug.Log("range attacked");
                enemyHealth--;
                myRenderer.color = Color.red;
                StartCoroutine(changeColor());
                rangedTimer = 0.5f;
            }
        }
    }

    IEnumerator changeColor()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        myRenderer.color = Color.white;

    }
}
