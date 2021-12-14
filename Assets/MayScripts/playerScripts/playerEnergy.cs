using UnityEngine;
using UnityEngine.UI;

public class playerEnergy : MonoBehaviour
{
    public float energy = 1;
    public float enToCharge = 0.25f;
    public float enToLife = 0.25f;
    public float timer = 0;
    bool hasCharged = false;
    bool toHealth = false;

    public bool disableEner = false;

    public Image energyUI;
    public GameObject bullet;
    public float shootSpeed;
    float shotCD = 0;
    public Animator playerAnimator;
    public Animator chargeAnimator;

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
            if (timer < 0.18f && shotCD <=0)
            {
                Debug.Log("charged Shot");
                energy -= enToCharge;
                playerAnimator.SetTrigger("rangeAttack");
                Audiomanager.Instance.PlaySound(Audiomanager.Instance.chargeAttackSound, Audiomanager.Instance.chargeAttackVolume);
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
            hasCharged = false;
            toHealth = false;
            timer = 0;
        }
        if (timer > 0.18f && !hasCharged && GetComponent<playerHealth>().playerHealthstat < GetComponent<playerHealth>().myHealth)
        {

            chargeAnimator.SetTrigger("isCharging");
            hasCharged = true;
        }

        if (timer > 1 && GetComponent<playerHealth>().playerHealthstat < GetComponent<playerHealth>().myHealth && !toHealth)
        {
            toHealth = true;
            Audiomanager.Instance.PlaySound(Audiomanager.Instance.gainHealthSound, Audiomanager.Instance.gainHealthVolume);
            Debug.Log("get Health");
            energy -= enToLife;
            GetComponent<playerHealth>().playerHealthstat++;
        }

    }



}
