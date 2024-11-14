using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundSource;

    public static SfxController instance;
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
            return;
        }
    }
    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }
    public void PlaySound(AudioClip soundClip)
    {
        soundSource.PlayOneShot(soundClip);
    }

    public void StopSound()
    {
        soundSource.Stop();
    }
}

