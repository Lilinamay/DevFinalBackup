using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public float enemySpeed;
    public float enemyMaxDistance;
    private float enemyCurrentDistance;
    private float enemyCurrentX;
    private float enemyX;
    Rigidbody2D enemyBody;
    SpriteRenderer myRenderer;

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
            myRenderer.flipX = true;
        }
        if (enemySpeed >= 0)
        {
            myRenderer.flipX = false;
        }

        enemyBody.velocity = new Vector3(enemySpeed, enemyBody.velocity.y);

    }

    /*void MovingAnimation(Sprite[] currentSprite)                                    //animations
    {
        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            timer = 0;
            currentSpriteIndex++;
            currentSpriteIndex %= currentSprite.Length;
        }
        myRenderer.sprite = currentSprite[currentSpriteIndex];
    }*/
}
