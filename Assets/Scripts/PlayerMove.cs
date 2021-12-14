using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float gravityMultiplier;
    public float dashSpeed;

    public bool onFloor;
    public bool onPlatform;
    public bool jumpKeyReleased;
    bool canDash;
   
    int dashCd;

    
    bool isDashing;
    bool hasJumpedOnce;
    bool checkOnFloor;


    public Animator BlackAnimator;

    public float npcPosX;
    float myPosX;

    public bool disableMove = false;
    public bool convMove = false;

    Rigidbody2D myBody;

    SpriteRenderer myRenderer;

    public Animator PlayerAnimator;

    public AudioSource PlayerAudioSource;


    //public float test = 3;
    public float rayDis = 1;
    public Transform rayCastOrigin;
   //float jumpTimer = 0;
    //public float theTimer;
    public bool canJump = true;
    float onfloorY;
    float jumpY;

    public bool faceR;
    public bool faceL;
    public bool canLand;

    public LayerMask layerMask;

    public GameObject dashParticle;
    public SpriteRenderer dashRenderer;


    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        jumpKeyReleased = true;
        BlackAnimator.SetTrigger("isBlackOut");
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!myRenderer.flipX)
        {
            faceR = true;
            faceL = false;
        }
        if (myRenderer.flipX)
        {
            faceR = false;
            faceL = true;
        }

        if(myBody.velocity.y == 0)
        {
            Globals.playerY = 0;
        }
        if (myBody.velocity.y < 0)
        {
            Globals.playerY = -1;
        }
        if (myBody.velocity.y > 0)
        {
            Globals.playerY = 1;
        }

        if (onFloor && !onPlatform)
        {
            onfloorY = transform.position.y;
        }
        if (!onFloor)
        {
            PlayerAudioSource.Stop();
            jumpY = transform.position.y;
            Globals.jumpDistance = jumpY - onfloorY;
        }
        if (myBody.velocity.y < 0 && !onFloor && !isDashing)
        {
            PlayerAnimator.SetBool("isJumpUp", false);
            PlayerAnimator.SetBool("isJumpDown", true);
            PlayerAnimator.SetBool("isStanding", false);
            //PlayerAnimator.SetBool("isWalking", false);
        }else if (myBody.velocity.y > 0 && !onFloor && !isDashing)
        {
            PlayerAnimator.SetBool("isJumpUp", true);
            PlayerAnimator.SetBool("isJumpDown", false);
            PlayerAnimator.SetBool("isStanding", false);
        }

            if (onFloor && myBody.velocity.y > 0.1)
        {
            onFloor = false;
        }
        if (!disableMove)               //player can only control when notdisableMove
        {
            CheckKeys();
            JumpPhysics();
        }
        if (convMove)
        {
            HandleConvMove();
        }
        if (dashCd > 0)
        {
            dashCd--;
        }
        if (!onFloor)
        {
            speed = 10f;
        }
        else
        {
            
            PlayerAnimator.SetBool("isJumpUp", false);
            PlayerAnimator.SetBool("isJumpDown", false);
        }

        if (checkOnFloor)
        {
            ConstantCheckOnFloor();
        }

        /*if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;            //reset jumpTimer
        }
        Debug.Log("jumpTimer:" + jumpTimer);*/

        if (isDashing)
        {
            PlayerAudioSource.Stop();
        }

        if (Globals.stopWalkSound)
        {
            PlayerAudioSource.Stop();
            Globals.stopWalkSound = false;
        }

    }

    void CheckKeys()
    {


        if (Input.GetKey(KeyCode.RightArrow) && !isDashing)     //left right movement
        {
            if (onFloor)
            {
                if (!PlayerAudioSource.isPlaying)
                {

                    PlayerAudioSource.Play();
                }
            }
            myRenderer.flipX = false;
            PlayerAnimator.SetBool("isWalking", true);
            PlayerAnimator.SetBool("isStanding", false);

            HandleLRMovement(speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !isDashing)
        {
            if (onFloor)
            {
                if (!PlayerAudioSource.isPlaying)
                {

                    PlayerAudioSource.Play();
                }
            }
            myRenderer.flipX = true;
            PlayerAnimator.SetBool("isWalking", true);
            PlayerAnimator.SetBool("isStanding", false);

            HandleLRMovement(-speed);

        }
        else
        {
            PlayerAnimator.SetBool("isWalking", false);
            PlayerAnimator.SetBool("isStanding", true);
        }

        

        if (Input.GetKeyUp(KeyCode.Z))   //fall when jump key released
        {
            jumpKeyReleased = true;
            if (myBody.velocity.y > 0)
            {
                myBody.velocity = new Vector3(myBody.velocity.x, 0);
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && !isDashing)
        {
            if (onFloor)
            {
                myBody.velocity = new Vector3(0, myBody.velocity.y);
                PlayerAudioSource.Stop();
            }
            else
            {
                checkOnFloor = true;
            }
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && !isDashing)
        {
            if (onFloor)
            {
                myBody.velocity = new Vector3(0, myBody.velocity.y);
                PlayerAudioSource.Stop();
            }
            else
            {
                checkOnFloor = true;
            }
        }

            if (Input.GetKey(KeyCode.Z) && onFloor && jumpKeyReleased && !isDashing)   //jump conditions
        {
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);

            jumpKeyReleased = false;
            hasJumpedOnce = true;
            //Debug.Log("jump time:" + Time.deltaTime);
            //Debug.Log("firstJump");
            PlayerAnimator.SetBool("isJumpUp", true);
            PlayerAnimator.SetBool("isJumpDown", false);
            PlayerAnimator.SetBool("isStanding", false);
            //PlayerAnimator.SetBool("isWalking", false);

        }

        if (Input.GetKey(KeyCode.Z) && hasJumpedOnce && jumpKeyReleased && !isDashing)   //double jump conditions
        {   if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                myBody.velocity = new Vector3(0, jumpHeight);
            }
            else
            {
                myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);
            }
            

            jumpKeyReleased = false;
            hasJumpedOnce = false;
            //Debug.Log("secondJump");
            PlayerAnimator.SetBool("isJumpUp", true);
            PlayerAnimator.SetBool("isJumpDown", false);
            PlayerAnimator.SetBool("isStanding", false);
            //PlayerAnimator.SetBool("isWalking", false);
        }


        if (onFloor)    //dash conditions when on floor
        {
            if (Input.GetKeyDown(KeyCode.C) && dashCd == 0)
            {
                if (!myRenderer.flipX)
                {
                    StartCoroutine(Dash(1));
                }
                else
                {
                    StartCoroutine(Dash(-1));
                }
                dashCd = 15;
            }
        }
        else     //dash conditions when in the air
        {
            if (Input.GetKeyDown(KeyCode.C) && canDash)
            {
                if (!myRenderer.flipX)
                {
                    StartCoroutine(Dash(1));

                }
                else
                {
                    StartCoroutine(Dash(-1));
                }
                canDash = false;
            }
        }

    }

    void JumpPhysics()    //gravity multiplier
    {
        if (myBody.velocity.y < 0 && Globals.ApplyV)
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1f) * Time.deltaTime;
            PlayerAnimator.SetBool("isStanding", false);
            PlayerAnimator.SetBool("isWalking", false);
            PlayerAnimator.SetBool("isJumpDown", true);
        }
    }

    void HandleLRMovement(float dir)     //left right movement
    {
        myBody.velocity = new Vector3(dir, myBody.velocity.y);
        
    }


    IEnumerator Dash(float dir)    //dash codes
    {
        isDashing = true;
        Audiomanager.Instance.PlaySound(Audiomanager.Instance.dashSound, Audiomanager.Instance.dashVolume);
        dashParticle.SetActive(true);
        if (dir < 0)
        {
            dashRenderer.flipX = true;
            
        }
        else
        {
            dashRenderer.flipX = false;
            
        }

        myBody.velocity = new Vector2(dashSpeed * dir, 0f);
        
        float gravity = myBody.gravityScale;
        myBody.gravityScale = 0;
        yield return new WaitForSeconds(0.16f);
        dashParticle.SetActive(false);
        myBody.velocity = new Vector2(0f, 0f);
        isDashing = false;
        myBody.gravityScale = gravity;
    }


    void HandleConvMove()               //move player to the right during conversation
    {
        myPosX = transform.position.x;
        if ((myPosX- npcPosX) < 2.3f)
        {
            HandleLRMovement(speed);
            myRenderer.flipX = false;
        }if((myPosX - npcPosX) > 2.3f)
        {
            myRenderer.flipX = true;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    void ConstantCheckOnFloor()
    {
        if (onFloor)
        {
            myBody.velocity = new Vector3(0, myBody.velocity.y);
            checkOnFloor = false;
        }
    }


    private void FixedUpdate()
    {
        Debug.Log(onFloor);

        RaycastHit2D hit = Physics2D.Raycast(rayCastOrigin.position, Vector2.down, rayDis,layerMask);
        if (hit.collider)
        {
            Debug.Log("playerRAYCAST:" +hit.collider.name);
            if (hit.collider.tag == "floor")
            {
                if (myBody.velocity.y < 0 && canLand && !onFloor)
                {
                    canLand = false;
                    Audiomanager.Instance.PlaySound(Audiomanager.Instance.lightLand, Audiomanager.Instance.lightLandVolume);
                    StartCoroutine(LandCd());
                   
                }
                Debug.Log("player: floor below, can jump");
                onFloor = true;

                canDash = true;
                onPlatform = false;
                Globals.CamOnfloor = true;
                Globals.CamOnplatform = false;
                Globals.CamInair = false;
                Globals.CamFloorY = transform.position.y;
            }
            else if(hit.collider.tag == "platform")
            {
                if (myBody.velocity.y < 0 && canLand)
                {
                    canLand = false;
                    Audiomanager.Instance.PlaySound(Audiomanager.Instance.lightLand, Audiomanager.Instance.lightLandVolume);
                    StartCoroutine(LandCd());

                }
                onFloor = true;
                canDash = true;
                onPlatform = true;
                /*Globals.CamOnfloor = false;
                Globals.CamPlatformY = transform.position.y;
                Globals.CamOnplatform = true;
                Globals.CamInair = false;*/
            } 

            else
            {
                //Debug.Log("can't jump");
                onPlatform = false;
                Globals.CamInair = true;
                Globals.CamOnfloor = false;
                Globals.CamOnplatform = false;
                onFloor = false;
                
                //canDash = true;
            }
        }
        else
        {
            onPlatform = false;
            Globals.CamInair = true;
            Globals.CamOnfloor = false;
            Globals.CamOnplatform = false;
            //Debug.Log("can't jump, no collider detected");
            onFloor = false;
            //canDash = true;
        }

        IEnumerator LandCd()
        {
            yield return new WaitForSecondsRealtime(0.2f);
            canLand = true;

        }

    }

    public static class Globals
    {
        public static bool CamOnfloor = false;
        public static bool CamOnplatform = false;
        public static float CamPlatformY;
        public static bool CamInair = false;
        public static float jumpDistance;
        public static float CamFloorY;
        public static int playerY;
        public static bool ApplyV = true;
        public static bool stopWalkSound = false;

    }

}