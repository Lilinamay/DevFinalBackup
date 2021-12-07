using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour
{
    public float duration = 1.0f;
    public float magnitude = 1.0f;
    public float smoothR = 0.5f;

    [SerializeField] SpriteRenderer playerRenderer;
    public void Startshake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsed = 0.0f;

        Vector3 oriPos = transform.localPosition;
        Debug.Log("shake!");
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition =
                Vector3.Lerp(                                           //smooth transition
                    transform.localPosition,
                    new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z),
                    smoothR
                    );
            //

            elapsed += Time.deltaTime;              //run in everyframe
            //Debug.Log(elapsed);
            yield return null;

        }
        transform.localPosition = oriPos;
        //playerRenderer.color = Color.white;


    }
}
