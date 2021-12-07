using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerEnergy : MonoBehaviour
{
    public float energy = 1;
    public float enToCharge = 0.25f;
    public float enToLife = 0.25f;
    public float timer = 0;

    public bool disableEner = false;

    public Image energyUI;
    public GameObject bullet;
    public float shootSpeed;
    float shotCD = 0;
    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        energyUI.fillAmount = energy;
        if (energy >= enToLife && !disableEner)     //have energy to consume
        {
            energyToLife();
        }
        if (shotCD > 0)
        {
            shotCD -= Time.deltaTime;
        }
    }

    void energyToLife()
    {
        if (Input.GetKey(KeyCode.A))
        {
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (timer < 1 && shotCD <=0)
            {
                Debug.Log("charged Shot");
                energy -= enToCharge;
                playerAnimator.SetTrigger("rangeAttack");
                GameObject newBall = Instantiate(bullet, transform.position, transform.rotation); //default to player's position/rotation
                newBall.transform.SetParent(gameObject.transform);
                newBall.GetComponent<bulletBehavior>().OriginPos = gameObject.transform.position;
                float dir = 0f;
                if (GetComponent<PlayerMove>().faceR)
                {
                    dir = 1f;
                }
                else
                {
                    newBall.GetComponent<SpriteRenderer>().flipX = true;    //flip ball 
                    dir = -1f;      //opposite direction
                }
                newBall.transform.localPosition = new Vector3(dir * 1f, -0.1f); ///local position relative to player
                newBall.GetComponent<Rigidbody2D>().velocity = new Vector3(gameObject.GetComponent<Rigidbody2D>().velocity.x + dir * shootSpeed, 0f);  //ball move
                shotCD = 1;
                //hasShoot = true;
               //resetA = true;

            }
            if (timer > 1 && GetComponent<playerHealth>().playerHealthstat< GetComponent<playerHealth>().myHealth)
            {
                Debug.Log("get Health");
                energy -= enToLife;
                GetComponent<playerHealth>().playerHealthstat++;
            }
            timer = 0;
        }

    }
}
