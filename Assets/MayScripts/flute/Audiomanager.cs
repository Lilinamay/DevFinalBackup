using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager Instance;
    public GameObject SoundPrefab;

    public AudioClip jumpSound;
    [Range(0f, 1f)] public float jumpVolume = 1.0f;
    [Space(10)]
    public AudioClip shootSound;
    [Range(0f, 1f)] public float shootVolume = 1.0f;
    public AudioClip starSound;
    [Range(0f, 1f)] public float starVolume = 1.0f;
    public AudioClip bulletSound;
    [Range(0f, 1f)] public float bulletVolume = 1.0f;
    public AudioClip megaSound;
    [Range(0f, 1f)] public float megaVolume = 1.0f;
    public AudioClip hitSound;
    [Range(0f, 1f)] public float hitVolume = 1.0f;
    public AudioClip CheckSound;
    [Range(0f, 1f)] public float CheckVolume = 1.0f;
    public AudioClip tutorialSound;
    [Range(0f, 1f)] public float tutorialVolume = 1.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(AudioClip clipToPlay, float volume = 0.5f)
    {
        if (clipToPlay == null)
        {
            Debug.Log("AUDIO CLIP NOT ASSIGNED ON AUDIO DIRECTOR!");
            return;
        }

        GameObject newSound = Instantiate(SoundPrefab, Vector3.zero, Quaternion.identity);
        AudioSource newSoundSource = newSound.GetComponent<AudioSource>();
        newSoundSource.clip = clipToPlay;
        newSoundSource.volume = volume;
        //Debug.Log("volume" + volume);
        newSoundSource.Play();
        Destroy(newSound, clipToPlay.length);
    }
}

//Audiomanager.Instance.PlaySound(audioManager.Instance.CheckSound, audioManager.Instance.CheckVolume);