using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    private int soundOn;
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    private Image soundImage;
    private bool sound;
    void Start()
    {
        soundImage = GetComponent<Image>();
        soundOn = PlayerPrefs.GetInt("soundOn", 1);
        if (soundOn == 1)
            sound = true;
        else
            sound = false;
        soundImage.sprite = soundOn == 1 ? on : off;
    }

    public void Sound()
    {
        sound = !sound;
        soundOn = sound ? 1 : 0;
        PlayerPrefs.SetInt("soundOn", soundOn);
        soundImage.sprite = soundOn == 1 ? on : off;
        AudioListener.volume = soundOn;
    }
}
