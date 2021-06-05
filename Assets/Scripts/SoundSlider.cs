using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{

    private float backVol = 1f;
    public AudioSource audio;
    public Slider backVolume;

    void Start()
    {
        audio = GameObject.Find("BGM").GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("backvol"))
        {
            backVol = 1f;

            audio.volume = backVol;
            backVolume.value = backVol;
            PlayerPrefs.SetFloat("backvol", backVol);
            PlayerPrefs.Save();
        }
        else
        {
            backVol = PlayerPrefs.GetFloat("backvol");
            audio.volume = backVol;
            backVolume.value = backVol;
        }
    }

    private void Update()
    {
        //  audio.volume = backVol;
        //  backVolume.value = backVol;
        PlayerPrefs.SetFloat("volume", backVol);

    }

    public void VolumeUpdater(float volume)
    {
        backVol = volume;
    }


    public void VolumeController()
    {

        audio.volume = backVolume.value;
        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
        PlayerPrefs.Save();
    }

}
