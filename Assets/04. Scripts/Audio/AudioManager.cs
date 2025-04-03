using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static float ttime = 0;
    
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioClip gameBGM;
    [SerializeField] private AudioClip endBGM;
    
    [SerializeField] private int sfxPoolSize = 5;
    private List<AudioSource> sfxPool;

    private void Update()
    {
        ttime += Time.deltaTime;
    }
    
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
        
        AudioMixerGroup[] groups = mixer.FindMatchingGroups("Master");
        
        sfxPool = new List<AudioSource>();
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = groups[0];
            sfxPool.Add(source);
        }
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

    public void PlaySFX(SFXType type, float? volume = 1f, float? startTime = null, double? playDuration = null )
    {
        AudioClip clip = GetAudioClip(type);

        
        AudioSource audioSource = GetAvailableSFXSource();
        if (volume.HasValue)
        {
            audioSource.volume = volume.Value;
        }
        
        if (startTime.HasValue || playDuration.HasValue)
        {
            audioSource.clip = clip;
        
        
            if (startTime.HasValue)
            {
                audioSource.time = startTime.Value;
            }
        
            audioSource.Play();
        
            if (playDuration.HasValue)
            {
                audioSource.SetScheduledEndTime(AudioSettings.dspTime + playDuration.Value);
            }
        }
        else
        {
            if (volume.HasValue)
            {
                audioSource.PlayOneShot(clip, volume.Value);
            }
            else
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
    
    private AudioSource GetAvailableSFXSource()
    {
        foreach (AudioSource source in sfxPool)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        sfxPool.Add(newSource);
        return newSource;
    }

    private AudioClip GetAudioClip(SFXType type) => clips[(int)type];
}