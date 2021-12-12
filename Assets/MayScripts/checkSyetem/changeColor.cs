using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class changeColor : MonoBehaviour
{

    public int textColor = 0;
    public TMP_Text op1;
    public GameObject op2;
    public TMP_Text op3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(textColor == 0)
        {
            
            op1.color = new Color32(255, 255, 255,255);
            op2.GetComponent<TMP_Text>().color = new Color(255, 255, 255);
            op3.color = new Color32(255, 255, 255,255);
        }

        if(textColor == 1)
        {
            Debug.Log("changeColor");
            op1.color = new Color32(52, 228, 241,255);
            op2.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
            op3.color = new Color32(255, 255, 255,255);
        }

        if (textColor == 2)
        {
            
            op1.color = new Color32(255, 255, 255,255);
            op2.GetComponent<TMP_Text>().color = new Color32(52, 228, 241, 255);
            op3.color = new Color32(255, 255, 255,255);
        }

        if (textColor == 3)
        {
            op1.color = new Color32(255, 255, 255,255);
            op3.color = new Color32(52, 228, 241,255);
            op2.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
        }

    }
}
