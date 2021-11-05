using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public enum VolumeType { MASTER, BGM, SE }

    [SerializeField]
    VolumeType volumeType = 0;
    AudioSource audioSource;

    Slider slider;

    
    void Start()
    {
        slider = GetComponent<Slider>();
        switch (volumeType)
        {
            case VolumeType.MASTER:
                slider.value = VolumeData.masterVol;
                break;
            case VolumeType.BGM:
                slider.value = VolumeData.bgmVol;
                break;
            case VolumeType.SE:
                slider.value = VolumeData.seVol;
                break;
        }
    }
    public void OnValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                VolumeData.masterVol = slider.value;
                break;
            case VolumeType.BGM:
                VolumeData.bgmVol = slider.value;
                break;
            case VolumeType.SE:
                VolumeData.seVol = slider.value;
                break;
        }
    }
}