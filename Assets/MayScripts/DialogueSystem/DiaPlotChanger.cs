using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaPlotChanger : MonoBehaviour
{
    bool changePloted = false;
    Dialogue dialogue;
    public int newPlot;
    public GameObject plotmanager;
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
            if (!changePloted)
            {
                if(newPlot> plotmanager.GetComponent<plotNumber>().myPlot)
                {
                    plotmanager.GetComponent<plotNumber>().myPlot = newPlot;
                }

            }
        }
    }
}
