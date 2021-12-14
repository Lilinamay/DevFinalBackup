using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float length;
    float startPosX;
    float height;
    float startPosY;

    public GameObject mainCam;

    public float parallaxAmt;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosY = transform.position.y;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = mainCam.transform.position.x * (1 - parallaxAmt);

        float distX = (mainCam.transform.position.x * parallaxAmt);

        float tempY = mainCam.transform.position.y * (1 - parallaxAmt);

        float distY = (mainCam.transform.position.y * parallaxAmt);

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        if (tempX > startPosX + length)
        {
            startPosX += length;
        }
        else if (tempX < startPosX - length)
        {
            startPosX -= length;
        }

        if (tempY > startPosY + height)
        {
            startPosY += height;
        }
        else if (tempY < startPosY - height)
        {
            startPosY -= height;
        }
    }
}
