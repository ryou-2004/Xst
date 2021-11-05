using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    AudioSource audioSouce;
    void Start()
    {
        audioSouce = GetComponent<AudioSource>();
        if (audioSouce)
        {
            Debug.Log("GetSouce");
        }
    }

    public void AudioPlay(AudioClip clip)
    {
        audioSouce.PlayOneShot(clip);
    }
    
}
