using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riverBehavior : MonoBehaviour
{
    public Transform sendBack;
    int playerHealth;
    [SerializeField] GameObject player;
    public GameObject freezeScreen;
    SpriteRenderer playerRenderer;
    bool respawnBack;
    Rigidbody2D mybody;
    playerHealth playerhealth;
    PlayerMove pMove;
    playerAttack pAttack;

    public Animator blackAnimator;
    public Animator PlayerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerhealth = player.GetComponent<playerHealth>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
        respawnBack = player.GetComponent<playerHealth>().respawnBack;
        mybody = player.GetComponent<Rigidbody2D>();
        pMove = player.GetComponent<PlayerMove>();
        pAttack = player.GetComponent<playerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("myhealth"+player.GetComponent<playerHealth>().playerHealthstat);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            //playerHealth--;
            //StartCoroutine(disableWalk());
            PlayerMove.Globals.stopWalkSound = true;

            Audiomanager.Instance.PlaySound(Audiomanager.Instance.spikeSound, Audiomanager.Instance.spikeVolume);
            playerRenderer.color = Color.red;
            mybody.velocity = Vector3.zero;
            StartCoroutine(changeColor());
            freezeScreen.GetComponent<freezeScreen>().stop();

        }
        
    }

    //IEnumerator disableWalk()
    //{
    //    yield return new WaitForSecondsRealtime(0f);
    //    pMove.disableMove = true;
    //    pAttack.canAttack = false;
    //    StartCoroutine(enableWalk());
    //}

    //IEnumerator enableWalk()
    //{
    //    yield return new WaitForSecondsRealtime(2.0f);
    //    if (playerhealth.playerHealthstat >= 1)
    //    {
    //        pMove.disableMove = false;
    //        pAttack.canAttack = true;
    //    }
    //}

    IEnumerator changeColor()
    {
        if (playerhealth.playerHealthstat > 1)
        {
            blackAnimator.SetTrigger("isBlackOut");
        }
        if (playerhealth.playerHealthstat <= 1)
        {
            pMove.disableMove = true;
            pAttack.canAttack = false;
        }
        yield return new WaitForSecondsRealtime(1f);
        playerRenderer.color = Color.white;
        if (playerhealth.playerHealthstat > 1 )
        {
            player.transform.position = sendBack.position;
        }
        player.GetComponent<playerHealth>().playerHealthstat--;
        mybody.velocity = Vector3.zero;
        //if (playerhealth.playerHealthstat <= 0)
        //{
        //    pMove.disableMove = true;
        //    pAttack.canAttack = false;
        //    PlayerAnimator.SetBool("isSitting", true);
        //    PlayerAnimator.SetBool("isStanding", false);
        //    PlayerAnimator.SetBool("isWalking", false);
        //    PlayerAnimator.SetBool("isJumpDown", false);
        //    PlayerAnimator.SetBool("isJumpUp", false);
        //}


    }
}
