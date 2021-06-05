using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BGMchange : MonoBehaviour
{
    public Slider backVolume;
    public AudioSource audio;
    public AudioClip mainBGM;
    public AudioClip shopBGM; // ªÛ¡° ∫Ò¡ˆø•


    private float backVol = 1f;


    private static BGMchange instance = null;
    public static BGMchange Instance
    {
        get { return instance; }
    }


    private void Start()
    {
        if (!PlayerPrefs.HasKey("backvol"))
        {
            backVol = 1f;
            PlayerPrefs.SetFloat("backvol", backVol);
            PlayerPrefs.Save();
            audio.volume = backVol;
            backVolume.value = backVol;
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
        audio.volume = backVol;
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

    public void BackMain()
    {
        audio.clip = mainBGM;
        audio.Play();
        SceneManager.LoadScene("MainScene");
    }

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }




}