using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform followTransform;
    public BoxCollider2D worldBounds;

    float xMin, xMax, yMin, yMax;
    float camY, camX;

    float camSize;
    float camRatio;
    public float ratio;

    Camera mainCam;

    Vector3 smoothPos;
    public float smoothRate;
    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        

        mainCam = gameObject.GetComponent<Camera>();

        camSize = mainCam.orthographicSize;
        
        

    }

    // Update is called once per frame
    void Update()
    {
        worldBounds = SceneTransition.Globals.switchToBound.GetComponent<BoxCollider2D>();
        ratio = (worldBounds.bounds.max.x + 10f) / 18;
        xMin = worldBounds.bounds.min.x;
        Debug.Log("boundmax" + worldBounds.bounds.max.x);
        xMax = worldBounds.bounds.max.x;
        yMin = worldBounds.bounds.min.y;
        yMax = worldBounds.bounds.max.y;
        camRatio = (xMax + camSize) / ratio;

    }

    void FixedUpdate()
    {
        if (PlayerMove.Globals.CamOnfloor)
        {
            camY = Mathf.Clamp(PlayerMove.Globals.CamFloorY + 4f, yMin + camSize, yMax - camSize);
        }
        /*else if (PlayerMove.Globals.CamOnplatform)
        {
            camY = Mathf.Clamp(PlayerMove.Globals.CamPlatformY, yMin + camSize, yMax - camSize);
        }*/
        else if (PlayerMove.Globals.CamInair)
        {
            if (PlayerMove.Globals.jumpDistance > 4f)
            {
                camY = Mathf.Clamp(PlayerMove.Globals.CamFloorY + PlayerMove.Globals.jumpDistance, yMin + camSize, yMax - camSize);
            }
            else if (PlayerMove.Globals.jumpDistance < -1f)
            {
                camY = Mathf.Clamp(Player.transform.position.y, yMin + camSize, yMax - camSize);
            }
            else
            {
                camY = Mathf.Clamp(PlayerMove.Globals.CamFloorY + 4f, yMin + camSize, yMax - camSize);
                //Debug.Log("small jump");
            }
        }

        
        camX = Mathf.Clamp(followTransform.position.x + 0, xMin + camRatio, xMax - camRatio);

        smoothPos = Vector3.Lerp(gameObject.transform.position, new Vector3(camX, camY, gameObject.transform.position.z), smoothRate);
        gameObject.transform.position = smoothPos;
    }
}
