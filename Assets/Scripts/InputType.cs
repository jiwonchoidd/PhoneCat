using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InputType : MonoBehaviour
{
    public GameObject toast;
    string name;
    public InputField inputText;


    public void name_typing()
     {   
         if(inputText.text=="")
         {
            toast.GetComponent<AndroidPlugin>().Toast_string("이름이 입력되지 않았습니다.");
            return;
         }
          else if(inputText.text=="최지원")
         {
            toast.GetComponent<AndroidPlugin>().Toast_string("네? 전 고양이가 아닙니다.");
            return;
         }
        else
        {
        name=inputText.text;
        PlayerPrefs.SetString("name", name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainScene");
        }
    }
}
