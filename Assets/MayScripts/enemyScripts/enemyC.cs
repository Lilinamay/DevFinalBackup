using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyC : MonoBehaviour
{
    public float enemySpeed;
    public float enemyMaxDistance;
    private float enemyCurrentDistance;
    private float enemyCurrentX;
    private float enemyX;
    Rigidbody2D enemyBody;
    SpriteRenderer myRenderer;

    public Animator EnemyCAnimator;

    public float RayDis = 10f;
    public bool alert = false;
    public float dashMaxDis = 6;
    float dashleftbound;
    float dashrightbound;
    public float orgPosx;
    //public float Posx;
    public float dashCD = 0;
    public float chargeCD = 0;

    public GameObject arrow;

    bool hasShoot = false;
    public float shootSpeed = 3;

    public LayerMask layerMask;

    public SpriteRenderer pRenderer;

    enemyBehavior behavior;
    ///[SerializeField] private Sprite[] IdleSprites;

    //[SerializeField] private float animationSpeed = 0.3f;
    //private float timer;
    //private int currentSpriteIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyBody = gameObject.GetComponent<Rigidbody2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        behavior = gameObject.GetComponent<enemyBehavior>();
        enemyX = gameObject.transform.position.x;

        //orgPosx = transform.position.x;
        dashleftbound = enemyX - (dashMaxDis / 2);
        dashrightbound = enemyX + (dashMaxDis / 2);
    }

    // Update is called once per frame
    void Update()
    {
        turnAround();
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
                    hasShoot = false;
                    dashCD = 5;
                    //chargeCD = 1;
                    enemyBody.velocity = new Vector2(0, 0);
                    Debug.Log("CHARGE");
                    EnemyCAnimator.SetBool("charge", true);
                    EnemyCAnimator.SetBool("Walk", false);
                    EnemyCAnimator.SetBool("done", false);
                    //EnemyBAnimator.SetBool("canCharge", true);
                }
            }
            else
            {
                //walk
                enemyBody.velocity = new Vector2(enemySpeed, 0);
                EnemyCAnimator.SetBool("charge", false);
                EnemyCAnimator.SetBool("Walk", true);
                EnemyCAnimator.SetBool("done", false);
            }
        }
        else
        {
            //walk
            enemyBody.velocity = new Vector2(enemySpeed, 0);
            EnemyCAnimator.SetBool("charge", false);
            EnemyCAnimator.SetBool("Walk", true);
            EnemyCAnimator.SetBool("done", false);
        }

        if (dashCD <= 4 && dashCD >= 3)
        {
            //DASH
            //enemyBody.velocity = new Vector2(enemySpeed * 2, 0);
            //EnemyBAnimator.SetBool("canCharge", false);
            //EnemyBAnimator.SetBool("canAttack", true);
            if (!hasShoot)
            {
                GameObject newBall = Instantiate(arrow, transform.position, transform.rotation); //default to player's position/rotation
                newBall.transform.SetParent(gameObject.transform);
                newBall.GetComponent<bulletBehavior>().OriginPos = gameObject.transform.position;
                float dir = 0f;
                if (myRenderer.flipX)
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
                hasShoot = true;
            }
            enemyBody.velocity = new Vector2(0, 0);
            EnemyCAnimator.SetBool("charge", false);
            EnemyCAnimator.SetBool("Walk", false);
            EnemyCAnimator.SetBool("done", true);

        }
        //else if (dashCD < 2 && dashCD >= 1)
        //{
            
        //    //stop dash
        //}
        else if (dashCD < 3)
        {
            //enemyBody.velocity = new Vector3(enemySpeed, enemyBody.velocity.y);
            enemyBody.velocity = new Vector2(enemySpeed, 0);
            //EnemyBAnimator.SetBool("canAttack", false);
            EnemyCAnimator.SetBool("charge", false);
            EnemyCAnimator.SetBool("Walk", true);
            EnemyCAnimator.SetBool("done", false);
            hasShoot = false;
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

    void turnAround()
    {
        if (behavior.getHit)
        {
            if ((!myRenderer.flipX && pRenderer.flipX) || (myRenderer.flipX && !pRenderer.flipX))
            {
                //gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                enemySpeed = -1 * enemySpeed;
                if (!myRenderer.flipX)
                {
                    myRenderer.flipX = true;
                }
                else if (myRenderer.flipX)
                {
                    myRenderer.flipX = false;
                }
                behavior.getHit = false;
            }
            else
            {
                behavior.getHit = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (!myRenderer.flipX)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, RayDis, layerMask);
            if (hit.collider)
            {
                Debug.Log("enemyC" +hit.collider.name);
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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, RayDis, layerMask);
            if (hit.collider)
            {
                Debug.Log("enemyC" + hit.collider.name);
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
