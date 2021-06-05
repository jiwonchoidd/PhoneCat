using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class soundsave : MonoBehaviour
{
    public Slider backVolume; // 슬라이더를 저장해야함


    public AudioSource audio;
    public AudioClip mainBGM;
    public AudioClip shopBGM; // 상점 비지엠


    private float backVol = 1f;

    /*
    private static soundsave instance = null;
    public static soundsave Instance
    {
        get { return instance; }
    }
    */

    private void Start()
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


    //씬이 옮겨지면 backVolume이 해당 씬에 있는 슬라이더로 바뀌게 되야한다. 밸류만 옮기게 된다면 조절이 안될거야
    //애초에 슬라이더를 find로 찾아서 넣는걸로 해보자.

    //다른 씬에는 사운드매니저가 없어서 못찾는건가?
    //판넬이 꺼져있으면 찾지못하는듯하다. find 못쓸듯
    public void Loadslider()
    {
        backVolume = GameObject.Find("SoundSlider").GetComponent<Slider>(); // 수정 야매로 해보자

    }


    private void Awake() // 이게 문젠가
    {
        /*
        if (instance != null)
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
        */



    }
}