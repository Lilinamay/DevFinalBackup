using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBMove : MonoBehaviour
{
    public float enemySpeed;
    public float enemyMaxDistance;
    private float enemyCurrentDistance;
    private float enemyCurrentX;
    private float enemyX;
    Rigidbody2D enemyBody;
    SpriteRenderer myRenderer;
    public Animator EnemyBAnimator;

    public float RayDis = 10f;
    public bool alert = false;
    public float dashMaxDis = 6;
    float dashleftbound;
    float dashrightbound;
    public float orgPosx;
    //public float Posx;
    public float dashCD = 0;
    public float chargeCD = 0;
    ///[SerializeField] private Sprite[] IdleSprites;

    //[SerializeField] private float animationSpeed = 0.3f;
    //private float timer;
    //private int currentSpriteIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyBody = gameObject.GetComponent<Rigidbody2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();

        enemyX = gameObject.transform.position.x;

        //orgPosx = transform.position.x;
        dashleftbound = enemyX - (dashMaxDis / 2);
        dashrightbound = enemyX + (dashMaxDis / 2);
    }

    // Update is called once per frame
    void Update()
    {

        enemyCurrentX = gameObject.transform.position.x;
        enemyCurrentDistance = Mathf.Abs(enemyCurrentX - enemyX);

        //Debug.Log(enemyCurrentDistance);
        if ((enemyCurrentDistance > enemyMaxDistance)
            &&
            (Mathf.Sign(enemyCurrentX - enemyX) == Mathf.Sign(enemySpeed))) //speed and direction consistant
        {
            enemySpeed = -1 * enemySpeed;
            //Debug.Log("change dir");
        }

        //MovingAnimation(IdleSprites);
        if (enemySpeed < 0)
        {
            myRenderer.flipX = false;
        }
        if (enemySpeed >= 0)
        {
            myRenderer.flipX = true;
        }

       

        //Posx = transform.position.x;
        if (alert)
        {
            if (enemyCurrentX >= dashleftbound && enemyCurrentX <= dashrightbound)
            {
                if (dashCD <= 0)
                {
                    dashCD = 5;
                    //chargeCD = 1;
                    enemyBody.velocity = new Vector2(0, 0);
                    Debug.Log("CHARGE");
                    EnemyBAnimator.SetBool("canCharge", true);
                }
            }
            else
            {
                //walk
                enemyBody.velocity = new Vector2(enemySpeed, 0);
            }
        }
        else
        {
            //walk
            enemyBody.velocity = new Vector2(enemySpeed, 0);
        }

        if (dashCD <= 4 && dashCD >= 2)
        {
            //DASH
            enemyBody.velocity = new Vector2(enemySpeed * 2, 0);
            EnemyBAnimator.SetBool("canCharge", false);
            EnemyBAnimator.SetBool("canAttack", true);
        }
        else if (dashCD < 2 && dashCD >= 1)
        {
            enemyBody.velocity = new Vector2(0, 0);
            //stop dash
        }
        else if (dashCD < 1)
        {
            //enemyBody.velocity = new Vector3(enemySpeed, enemyBody.velocity.y);
            enemyBody.velocity = new Vector2(enemySpeed, 0);
            EnemyBAnimator.SetBool("canAttack", false);
        }


        if (dashCD >= 0)
        {
            dashCD -= Time.deltaTime;
        }
        if (chargeCD >= 0)
        {
           chargeCD -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!myRenderer.flipX)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, RayDis, 7);
            if (hit.collider)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "Player")
                {
                    //attack ready
                    alert = true;
                }
                else
                {
                    alert = false;
                }
            }
            else
            {
                alert = false;
            }
        }
        if (myRenderer.flipX)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, RayDis, 7);
            if (hit.collider)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "Player")
                {
                    //attack ready
                    alert = true;
                }
                else
                {
                    alert = false;
                }
            }
            else
            {
                alert = false;
            }
        }
        //Debug.DrawRay(hit);

    }



}
