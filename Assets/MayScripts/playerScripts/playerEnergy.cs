using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerEnergy : MonoBehaviour
{
    public float energy = 1;
    public float enToCharge = 0.25f;
    public float enToLife = 0.25f;
    public float timer = 0;

    public bool disableEner = false;

    public Image energyUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        energyUI.fillAmount = energy;
        if (energy >= enToLife && !disableEner)     //have energy to consume
        {
            energyToLife();
        }
    }

    void energyToLife()
    {
        if (Input.GetKey(KeyCode.A))
        {
            timer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (timer < 1)
            {
                Debug.Log("charged Shot");
                energy -= enToCharge;
            }
            if (timer > 1 && GetComponent<playerHealth>().playerHealthstat< GetComponent<playerHealth>().myHealth)
            {
                Debug.Log("get Health");
                energy -= enToLife;
                GetComponent<playerHealth>().playerHealthstat++;
            }
            timer = 0;
        }

    }
}
