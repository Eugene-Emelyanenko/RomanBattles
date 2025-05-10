using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GetVolumeSettings : MonoBehaviour
{
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetInt("soundOn", 1) == 0 ? 0 : 1;
        Time.timeScale = 1;
    }
}
