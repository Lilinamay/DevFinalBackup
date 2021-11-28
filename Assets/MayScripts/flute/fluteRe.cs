using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fluteRe : MonoBehaviour
{
    public AudioSource audioSource;

    public float maxVolume;
    public float attackTime;
    public float releaseTime;

    public KeyCode keyToPlay = KeyCode.S;


    public enum ASRState { inactive = 0, attack, sustain, release }
    public ASRState asrState;

    // Start is called before the first frame update
    void Start()
    {
        asrState = ASRState.inactive;
        audioSource.volume = 0f;
    }

    // Update is called once per frame
    void Update()
    {


        //if we press down "A" for the length of attack time, we reach max volume
        if (Input.GetKey(keyToPlay))
        {
            switch (asrState)
            {
                case ASRState.inactive:
                    asrState = ASRState.attack;
                    break;
                case ASRState.attack:
                    if (audioSource.volume < maxVolume)
                    {
                        audioSource.volume += (Time.deltaTime / attackTime) * maxVolume;
                    }

                    else if (audioSource.volume >= maxVolume)
                    {
                        audioSource.volume = maxVolume;
                        asrState = ASRState.sustain;
                    }
                    break;
                case ASRState.sustain:
                    break;
                case ASRState.release:
                    asrState = ASRState.attack;
                    break;
            }
        }

        else
        {
            switch (asrState)
            {
                case ASRState.inactive:
                    break;
                case ASRState.attack:
                    asrState = ASRState.release;
                    break;
                case ASRState.sustain:
                    asrState = ASRState.release;
                    break;
                case ASRState.release:

                    if (audioSource.volume > 0f)
                    {
                        audioSource.volume -= (Time.deltaTime / releaseTime) * maxVolume;
                    }
                    else
                    {
                        audioSource.volume = 0f;
                        asrState = ASRState.inactive;
                    }
                    break;
            }
        }

    }
}
