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
    PlayerMove pMove;

    // Start is called before the first frame update
    void Start()
    {
        mybody = player.GetComponent<Rigidbody2D>();
        Globals.switchToBound = originalBound;
        canTrans = true;
        pMove = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && canTrans)
        {
            BlackAnimator.SetTrigger("isBlackOut");
            canTrans = false;
            StartCoroutine(transport());
            StartCoroutine(disableWalk());


        }

        IEnumerator transport()
        {
            mybody.velocity = Vector3.zero;
            
            yield return new WaitForSecondsRealtime(0.8f);
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
        yield return new WaitForSecondsRealtime(1.2f);
        pMove.disableMove = false;
    }

    public static class Globals
    {
        public static GameObject switchToBound;
    }
}
