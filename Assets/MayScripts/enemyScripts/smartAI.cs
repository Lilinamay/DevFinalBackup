using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class smartAI : MonoBehaviour
{
    private AIDestinationSetter aiDest;
    [SerializeField] GameObject[] points;
    //[SerializeField] GameObject bound;
    [SerializeField] GameObject player;
    public int randomInt;
    public float Timer;
    [SerializeField] GameObject bound;
    // Start is called before the first frame update
    void Start()
    {
       aiDest= GetComponent<AIDestinationSetter>();
        Timer = 0;
        aiDest.target = points[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        //aiDest.enabled = false;
        //GetComponent<Seeker>.()
        //gameObject.GetComponent<Seeker>();
        if (bound.GetComponent<bound>().playerEnt)
        {
            //aiDest.enabled = true;
            aiDest.target = player.transform;
            Debug.Log("");
        }
        else
        {
            //aiDest.enabled = false;
            randomPos();


        }
    }

    void randomPos()
    {
        
        Timer += Time.deltaTime;
        if (Timer >= 3)
        {
            randomInt = Random.Range(0, 3);
            aiDest.target = points[randomInt].transform;
            Timer = 0;
        }

    }


}
