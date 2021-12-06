using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezeScreen : MonoBehaviour
{
    public float duration = 0.5f;

    public void stop()
    {
        {
            Time.timeScale = 0.0f;              //stop time
            Debug.Log("stop screen");
            
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        Debug.Log("stopped.....");
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        //yield return new WaitUntil 
        //FindObjectOfType<startshake>().
        FindObjectOfType<camShake>().Startshake();
    }
}
