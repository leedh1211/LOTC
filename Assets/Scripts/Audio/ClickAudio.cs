using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ClickAudio : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioMixerGroup MixerGroup;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = MixerGroup;
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    public void PlayClickSound()

    {
        if (clickSound != null)
        { 
            audioSource.PlayOneShot(clickSound);
        }
    }
}

