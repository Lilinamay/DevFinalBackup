using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{

    public Transform TP;
    [SerializeField] GameObject player;
    Rigidbody2D mybody;
    public GameObject originalBound;
    public GameObject WorldBound;
    public Animator BlackAnimator;
    bool canTrans;
    bool goUp;
    float upVelocity;
    public Animator playerAnimator;
    PlayerMove pMove;
    checkManager pCheck;

    // Start is called before the first frame update
    void Start()
    {
        mybody = player.GetComponent<Rigidbody2D>();
        Globals.switchToBound = originalBound;
        canTrans = true;
        goUp = false;
        pMove = player.GetComponent<PlayerMove>();
        pCheck = player.GetComponent<checkManager>();
        upVelocity = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp)
        {
            mybody.velocity = new Vector2(mybody.velocity.x, upVelocity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && canTrans)
        {
            BlackAnimator.SetTrigger("isBlackOut");
            canTrans = false;
            if (mybody.velocity.y > 0)
            {
                goUp = true;
                
            }
            
            StartCoroutine(transport());
            StartCoroutine(disableWalk());


        }

        IEnumerator transport()
        {
            
            
            yield return new WaitForSecondsRealtime(1f);
            pCheck.respawnEnemy();
            goUp = false;
            playerAnimator.SetBool("isJumpUp", false);
            playerAnimator.SetBool("isJumpDown", false);
            playerAnimator.SetBool("isStanding", true);
            playerAnimator.SetBool("isWalking", false);
            mybody.velocity = Vector3.zero;
            player.transform.position = TP.position;
            
        

        Globals.switchToBound = WorldBound;
            
            canTrans = true;
        }
    }

    IEnumerator disableWalk()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        pMove.disableMove = true;
        StartCoroutine(enableWalk());
    }

    IEnumerator enableWalk()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        pMove.disableMove = false;
    }

    public static class Globals
    {
        public static GameObject switchToBound;
    }
}
