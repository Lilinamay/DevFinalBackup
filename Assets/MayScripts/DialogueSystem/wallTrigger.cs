using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTrigger : MonoBehaviour
{
    bool wallDestroy = false;
    Dialogue dialogue;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;
    //bool convFin = false;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<Dialogue>();

    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue.diaDone)
        {
            if (!wallDestroy)
            {
                wall1.SetActive(false);
                wall2.SetActive(false);
                wall3.SetActive(false);
                wall4.SetActive(false);
                //Destroy(wall1);
                //Destroy(wall2);
                //Destroy(wall3);
                //Destroy(wall4);
                wallDestroy = true;
            }
        }
    }
}
