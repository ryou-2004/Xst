using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip clip;
    [Header("1～0で")]
    public float volume;
    private void Start()
    {
        SetAudio(GetComponent<AudioSource>(), clip, volume);
    }
    public void SetAudio(AudioSource au,AudioClip clip,float volume)
    {
        au.clip = clip;
        au.volume = volume;
        au.Play();
    }
}
