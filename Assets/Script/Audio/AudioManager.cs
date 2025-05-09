using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public float volume = 1;

    public AudioClip ClickButtonClip;
    public AudioClip BuyAudioClip;
    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClickButtonAudio()
    {
        PlaySound(ClickButtonClip);
    }
    public void PlayBuyAudio()
    {
        PlaySound(BuyAudioClip);
    }
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource availableSource = GetAvailableAudioSource();
            if (availableSource == null)
            {
                availableSource = gameObject.AddComponent<AudioSource>();
                availableSource.volume = volume;
                audioSources.Add(availableSource);
            }
            availableSource.PlayOneShot(clip);
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }
    static public AudioManager Instance()
    {
        return instance;
    }
}