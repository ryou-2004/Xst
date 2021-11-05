using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeData
{
    static VolumeData m_instance = new VolumeData();
    static public VolumeData Instance => m_instance;
    private VolumeData() { }
    static public float bgmVol = 1f;
    static public float seVol = 1f;
    static public float masterVol = 1f;
}
