using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidPlugin : MonoBehaviour
{
   
   private AndroidJavaObject UnityActivity;
   private AndroidJavaObject UnityInstance;
void Start()
    {
        AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        UnityActivity = ajc.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass ajc2 = new AndroidJavaClass("com.nsu.forunity.Myplugin");
        UnityInstance = ajc2.CallStatic<AndroidJavaObject>("instance");

        UnityInstance.Call("setContext", UnityActivity);
    }

    public void ToastButton()
    {
        ShowToast("유니티에서도 호출이됩니다", true);
    }

    public void ToastButton_AR()
    {
        ShowToast("넓고 평평한 바닥을 탐색해주세요.", true);
    }

     public void Toast_string(string alert)
    {
        ShowToast(alert, true);
    }

    public void ShowToast(string msg, bool isLong)
    {
        UnityActivity.Call("runOnUiThread", new AndroidJavaRunnable(()=>
        {
            if (isLong == false)
            {
                UnityInstance.Call("ShowToast", msg , 0);
            }            
            else
            {
                UnityInstance.Call("ShowToast", msg , 1);
            }
        }
        ));
    }
}
