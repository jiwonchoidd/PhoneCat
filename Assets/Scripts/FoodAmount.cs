using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodAmount : MonoBehaviour
{
    public Text food7;
    public Text food8;
    public Text food9;

    void Update()
    {
        foodamounttext();
    }
        void foodamounttext()
    {
        int amount1 = PlayerPrefs.GetInt("food7");
        int amount2 = PlayerPrefs.GetInt("food8");
        int amount3 = PlayerPrefs.GetInt("food9");
        food7.text=""+amount1;
        food8.text=""+amount2;
        food9.text=""+amount3;
    }

    
}
