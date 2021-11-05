using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySE : MonoBehaviour
{ 
    AudioSource audioSouce;
    void Start()
    {
        audioSouce = GetComponent<AudioSource>();
    }

    public void AudioPlay(AudioClip clip)
    {
        audioSouce.PlayOneShot(clip);
        audioSouce.volume = VolumeData.seVol * VolumeData.masterVol;
    }
}
