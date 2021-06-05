using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_START : MonoBehaviour
{
    private static BGM_START instance = null;
    public static BGM_START Instance
    {
        get { return instance; }
    }


    void Start()
    {
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
}
