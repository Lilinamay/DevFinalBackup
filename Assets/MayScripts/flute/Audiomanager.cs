using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager Instance;
    public GameObject SoundPrefab;

    public AudioClip lightLand;
    [Range(0f, 1f)] public float lightLandVolume = 1.0f;
    public AudioClip HeavyLand;
    [Range(0f, 1f)] public float HeavyLandVolume = 1.0f;
    [Space(10)]

    public AudioClip playerAttackSound;
    [Range(0f, 1f)] public float PlayerAttackVolume = 1.0f;
    public AudioClip chargeAttackSound;
    [Range(0f, 1f)] public float chargeAttackVolume = 1.0f;
    public AudioClip hitEnemySound;
    [Range(0f, 1f)] public float hitEnemyVolume = 1.0f;
    public AudioClip gainHealthSound;
    [Range(0f, 1f)] public float gainHealthVolume = 1.0f;
    public AudioClip playerTakeDamageSound;
    [Range(0f, 1f)] public float playerTakeDamageVolume = 1.0f;
    public AudioClip optionSound;
    [Range(0f, 1f)] public float optionVolume = 1.0f;
    public AudioClip dashSound;
    [Range(0f, 1f)] public float dashVolume = 1.0f;
    public AudioClip spikeSound;
    [Range(0f, 1f)] public float spikeVolume = 1.0f;
    public AudioClip footstepSound;
    [Range(0f, 1f)] public float footstepVolume = 1.0f;
    public AudioClip dialogSound;
    [Range(0f, 1f)] public float dialogVolume = 1.0f;
    [Space(10)]

    public AudioClip enemyDashSound;
    [Range(0f, 1f)] public float enemyDashVolume = 1.0f;
    public AudioClip enemyChargeAttackSound;
    [Range(0f, 1f)] public float enemyChargeAttackVolume = 1.0f;


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

//Audiomanager.Instance.PlaySound(Audiomanager.Instance.CheckSound, Audiomanager.Instance.CheckVolume);