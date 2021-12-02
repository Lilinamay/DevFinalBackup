using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plotNumber : MonoBehaviour
{
    public int myPlot ;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         plotNum ==0, story start
         plotNum ==1, comeback
         */
        Globals.plotNum = myPlot;
    }

    public static class Globals
    {
        public static int plotNum;
        public static int maxPlotNum = 3;
    }
}
