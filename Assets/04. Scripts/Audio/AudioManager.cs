using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioClip gameBGM;
    [SerializeField] private AudioClip endBGM;
    AudioSource audioSource;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPlay()
    {
        PlayBGM(gameBGM);
    }

    public void OnStop()
    {
        PlayBGM(endBGM);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (backgroundMusic.isPlaying)
            backgroundMusic.Stop();

        backgroundMusic.clip = clip;
        backgroundMusic.Play();
    }

    public void PlaySFX(SFXType type)
    {
        AudioClip clip = GetAudioClip(type);
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetAudioClip(SFXType type) => clips[(int)type];
}