using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class haegum : MonoBehaviour
{
    public Text level;
    public GameObject panel1;
    public GameObject panel2;

    private void Update()
    {
        if (level.text == "5") // 레벨 5에서만 해금되네? / >=사용안됨 (string 변수라 안되는듯) / 아싸리 5~30까지를 전부 넣으면 어떨까.... 비효율적이지만 해보는걸로
        {
            panel1.SetActive(false);
        }
        else if (level.text == "6") 
        {
            panel1.SetActive(false);
        }
        else if (level.text == "7")
        {
            panel1.SetActive(false);
        }
        else if (level.text == "8")
        {
            panel1.SetActive(false);
        }
        else if (level.text == "9")
        {
            panel1.SetActive(false);
        }
        else if (level.text == "10")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "11")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "12")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "13")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "14")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "15")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "16")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "17")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "18")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "19")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "20")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "21")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "22")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "23")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "24")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }

        else if (level.text == "25")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }

        else if (level.text == "26")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }

        else if (level.text == "27")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "28")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "29")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }
        else if (level.text == "30")
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        }

        // 여기까지 쥐돌이 해금




        /*

        else if (level.text == "10") 
        {
            
        }
        else if (level.text == "11")
        {
            panel2.SetActive(false);
        }
        else if (level.text == "12") 
        {
            panel2.SetActive(false);
        }
        else if (level.text == "13")
        {
            panel2.SetActive(false);
        }
        else if (level.text == "14") 
        {
            panel2.SetActive(false);
        }
        else if (level.text == "15")
        {
            panel2.SetActive(false);
        }
        else if (level.text == "16") 
        {
            panel2.SetActive(false);
        }
        else if (level.text == "17")
        {
            panel2.SetActive(false);
        }
        else if (level.text == "18") 
        {
            panel2.SetActive(false);
        }
        else if (level.text == "19")
        {
            panel2.SetActive(false);
        }
        else if (level.text == "20") 
        {
            panel2.SetActive(false);
        }
        else if (level.text == "21")
        {
            panel2.SetActive(false);
        }
        else if (level.text == "22") 
        {
            panel2.SetActive(false);
        }
        else if (level.text == "23")
        {
            panel2.SetActive(false);
        }
        else if (level.text == "24") 
        {
            panel2.SetActive(false);
        }
        else if (level.text == "25")
        {
            panel2.SetActive(false);
        }
        else if (level.text == "26") 
        {
            panel2.SetActive(false);
        }
        else if (level.text == "27")
        {
            panel2.SetActive(false);
        }
        else if (level.text == "28") 
        {
            panel2.SetActive(false);
        }
        else if (level.text == "29")
        {
            panel2.SetActive(false);
        }
        else if (level.text == "30") 
        {
            panel2.SetActive(false);
        }
        */
        //여기까지 낚싯대 해금
    }
}
