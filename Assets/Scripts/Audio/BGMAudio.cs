using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMAudio : MonoBehaviour
{
    public AudioClip bgmClip;
    public AudioMixerGroup bgmMixerGroup;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.outputAudioMixerGroup = bgmMixerGroup;
        audioSource.Play();
    }

}
