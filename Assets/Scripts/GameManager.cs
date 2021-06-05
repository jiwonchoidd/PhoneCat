using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public GameObject optionMenuSet;
    public Text talkText;
    public GameObject mainUISet;
    public GameObject socialMenuSet;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
        if(optionMenuSet.activeSelf == true){
        if (Input.GetButtonDown("Cancel")){
          optionMenuSet.SetActive(false);
        }
        }
        //
    }

    
    public void ChangeGameScene_bluetooth()
    {
        SceneManager.LoadScene("Bluetooth");
    }
    public void ChangeGameScene_MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
        public void ChangeGameScene_ARScene()
    {
        SceneManager.LoadScene("ARScene");
    }
    

    public void GameExit()
    {
        Application.Quit();
  
    }




}
