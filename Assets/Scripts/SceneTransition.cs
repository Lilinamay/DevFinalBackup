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

    // Start is called before the first frame update
    void Start()
    {
        mybody = player.GetComponent<Rigidbody2D>();
        Globals.switchToBound = originalBound;
        canTrans = true;
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


        }

        IEnumerator transport()
        {
            yield return new WaitForSecondsRealtime(0.8f);
            player.transform.position = TP.position;
            mybody.velocity = Vector3.zero;

            Globals.switchToBound = WorldBound;
            canTrans = true;
        }
    }

    public static class Globals
    {
        public static GameObject switchToBound;
    }
}
