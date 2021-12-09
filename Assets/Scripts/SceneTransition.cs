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

    // Start is called before the first frame update
    void Start()
    {
        mybody = player.GetComponent<Rigidbody2D>();
        Globals.switchToBound = originalBound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            BlackAnimator.SetTrigger("isBlackOut");
            StartCoroutine(transport());


        }

        IEnumerator transport()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            player.transform.position = TP.position;
            mybody.velocity = Vector3.zero;

            Globals.switchToBound = WorldBound;
        }
    }

    public static class Globals
    {
        public static GameObject switchToBound;
    }
}
