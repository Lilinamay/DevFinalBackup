using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehavior : MonoBehaviour
{
    public Vector3 OriginPos;
    Vector3 myPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myPos = gameObject.transform.position;
        if(Vector3.Distance(OriginPos, myPos) > 11)
        {
            Destroy(gameObject);
        }
    }


}
